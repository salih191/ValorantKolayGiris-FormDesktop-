using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using ValorantKolayGiris_FormDesktop_.Entity;

namespace ValorantKolayGiris_FormDesktop_.Classes
{
    public class DB
    {
        Baglanti baglan = new Baglanti();
        SQLiteCommand cmd = new SQLiteCommand();
        private SQLiteDataReader dr;
        private enum Islem
        {
            Update,
            Add,
            Delete
        }

        public void Add(IEntity entity)
        {
            main(entity,Islem.Add);
        }
        public void Update(IEntity entity)
        {
            main(entity, Islem.Update);
        }
        public void Delete(IEntity entity)
        {
            main(entity, Islem.Delete);
        }
        private void main(IEntity entity, Islem islem)
        {

            cmd.Connection = baglan.Baglan();//sqlcommand connection 
            string Id = entity.GetType().GetProperties().SingleOrDefault(p => p.Name == "id" || p.Name == "Id" || p.Name == entity.GetType().Name + "Id").Name;//nesnenin id sinin sınıftaki ismi sınıf adı+id ya da id nin karşılığı
            string propertys = "\"" + string.Join("\",\"", entity.GetType().GetProperties().Where(p => p.Name != Id).Select(p => p.Name)) + "\"";//propertyler
            string cmdParameters = "@" + string.Join(",@", entity.GetType().GetProperties().Where(p => p.Name != Id).Select(p => p.Name));//parametreler
            string commandText = "";
            switch (islem)
            {
                case Islem.Update:
                    string updateCommend = "";
                    for (int j = 0; j < propertys.Split(',').Length; j++)
                    {
                        if (j != 0)
                        {
                            updateCommend += ",";
                        }
                        updateCommend += propertys.Split(',')[j] + "=" + cmdParameters.Split(',')[j];
                    }
                    commandText = $"update {entity.GetType().Name} set {updateCommend} where {Id}={entity.GetType().GetProperty(Id).GetValue(entity)}";
                    break;
                case Islem.Add:
                    commandText = $@"insert into {entity.GetType().Name}({propertys}) values({cmdParameters})";
                    break;
                case Islem.Delete:
                    commandText =
                        $"delete from {entity.GetType().Name} where {Id}={entity.GetType().GetProperty(Id).GetValue(entity)}";
                    break;
            }
            cmd.CommandText = commandText;
            foreach (var paramtre in cmdParameters.Split(','))
            {
                var property = paramtre.Remove(0, 1);
                var value = entity.GetType().GetProperty(property).GetValue(entity);
                cmd.Parameters.AddWithValue(paramtre, value);
            }
            cmd.ExecuteNonQuery();
            baglan.BaglantiKapat();
        }

        public List<T> set<T>() where T : class,IEntity, new()
        {
            List<string> propertys = typeof(T).GetProperties().Select(p => p.Name).ToList();
            var result = new List<T>();
            cmd.Connection = baglan.Baglan();
            cmd.CommandText = $@"select * from {typeof(T).Name}";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var deger = new T();
                foreach (var property in propertys)
                {
                    if (dr[property] != DBNull.Value)
                    {
                        deger.GetType().GetProperty(property).SetValue(deger, Convert.ChangeType(dr[property], typeof(T).GetProperty(property).PropertyType));
                    }
                }
                result.Add(deger);
            }
            dr.Close();
            return result;
        }
    }
}