using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class Db
    {

        public static SqlConnection GetConnection()
        {
            var cn = new SqlConnection(
                @"Server=(localdb)\MSSQLLocalDB;Database=TP_Formes;Trusted_Connection=True;"
            );
            cn.Open();
            return cn;
        }

    }
}
