using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data; 

namespace DoNowAPI.Utility
{
    

    public  class DbPool
    {
        private  IDbConnection[] Connections;
        private  int POOL_SIZE = 100;
        private  int MAX_IDLE_TIME = 10;

        private  int[] Locks;
        private  DateTime[] Dates;

        public IDbConnection GetConnection(out int identifier)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                if (System.Threading.Interlocked.CompareExchange(ref Locks[i], 1, 0) == 0)
                {
                    if (Dates[i] != DateTime.MinValue && (DateTime.Now - Dates[i]).TotalMinutes > MAX_IDLE_TIME)
                    {
                        Connections[i].Dispose();
                        Connections[i] = null;
                    }

                    if (Connections[i] == null)
                    {
                        IDbConnection conn = CreateConnection();
                        Connections[i] = conn;
                        conn.Open();
                    }

                    Dates[i] = DateTime.Now;
                    identifier = i;
                    return Connections[i];
                }
            }

            throw new Exception("No free connections");
        }
        private IDbConnection CreateConnection()
        {
            string ConnectionString = ConfigurationManager.AppSettings["DoNowConnectionString"].ToString();
            return new MySqlConnection(ConnectionString);
        }

        public void FreeConnection(int identifier)
        {
            if (identifier < 0 || identifier >= POOL_SIZE)
                return;

            System.Threading.Interlocked.Exchange(ref Locks[identifier], 0);
        }
    }
}
