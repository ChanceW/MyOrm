using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace MyOrm.Repos
{
    public class MySqlRepo
    {
        private string _connectionString;
        public MySqlRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Collection<T> Query<T>(string query, Dictionary<string,string> mapping) 
        {
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader rdr = cmd.ExecuteReader();
            return ConvertResults<T>(rdr, mapping);
        }

        public Collection<T> Proc<T>(string proc, Dictionary<string, string> mapping, Dictionary<string, object> queryParams = null)
        {
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            MySqlCommand cmd = new MySqlCommand(proc, conn);
            cmd.CommandText = proc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (queryParams != null)
            {
                foreach(KeyValuePair<string, object> param in queryParams)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }
            conn.Open();
            MySqlDataReader rdr = cmd.ExecuteReader();
            return ConvertResults<T>(rdr, mapping);
        }

        public Collection<T> ConvertResults<T>(MySqlDataReader reader, Dictionary<string, string> mapping) {
            var result = new Collection<T>();
            while (reader.Read())
            {
                var model = (T)Activator.CreateInstance(typeof(T));
                foreach (KeyValuePair<string, string> entry in mapping)
                {
                    Type examType = typeof(T);
                    PropertyInfo piShared = examType.GetProperty(entry.Value);
                    piShared.SetValue(model, reader[entry.Key]);
                }
                result.Add(model);
            }
            return result;
        }
    }
}
