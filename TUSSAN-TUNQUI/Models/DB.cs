using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace TUSSAN_TUNQUI.Models {
    public class DB {

        OleDbConnection con;
        private void openConexion() {
            con = new OleDbConnection();
            con.ConnectionString = "Provider = SQLNCLI11; Data Source = tussanbd.database.windows.net; Persist Security Info = True; User ID = administrador; password = Almacen$; Initial Catalog = tussanbd";
            con.Open();
        }

        public void transaccion(string sql) {
            OleDbCommand cmd = new OleDbCommand();
            openConexion();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            con.Close();
        }

    }
}