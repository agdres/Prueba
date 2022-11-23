using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BackEnd.Core
{
    public class Conexiones
    {
        // cadena de conexión base de datos
        private string conexName;
        public Conexiones(string conexionDb)
        {
            this.conexName = conexionDb;
        }

        private SqlConnection cone;

        private SqlConnection sqlConn = new SqlConnection();
        private DataSet ds = new DataSet();
        public string DataBase = "";
        public string Server = "";
        public string User = "";
        public string Pass = "";

        private SqlDataAdapter adapter;
        private SqlTransaction transaccion;


        private void Connect()
        {
           // Construccion cadena de conexion
            this.sqlConn = new SqlConnection();
            var builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = this.conexName;
            this.DataBase = builder.InitialCatalog;
            this.Server = builder.DataSource;
            this.User = builder.UserID;

            this.sqlConn.ConnectionString = builder.ConnectionString;
            this.cone = new SqlConnection(builder.ConnectionString);
            this.ds = new DataSet();

        }

        public DataTable getDataTable()
        {
            return this.ds.Tables[0];
        }


        public async Task<int> ejecutarProcedimiento(string _procedimiento)
        {

            try
            {
                // Verificar conexion, Abrir conexion
                if (this.sqlConn.State != ConnectionState.Open)
                {
                    this.Connect();
                    this.sqlConn.Open();
                }

                this.adapter = new SqlDataAdapter(_procedimiento, this.sqlConn);
                this.adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                this.adapter.SelectCommand.Parameters.Clear();
                this.ds.Clear();
                
                await this.adapter.SelectCommand.ExecuteNonQueryAsync();

                this.adapter.Fill(this.ds);
               
                this.adapter.SelectCommand.Parameters.Clear();
                if (this.transaccion == null)
                {
                    this.adapter.Dispose();
                    this.sqlConn.Close();
                }
                return 0;
            }
            catch (SqlException ex)
            {
                if (this.transaccion != null)
                    await this.transaccion.RollbackAsync();
                return 1;
            }
            finally
            {
                if (this.transaccion == null)
                    sqlConn.Dispose();
            }

        }

        // Convertir DataTable en un List
        public List<T> DataTableToList<T>(string[] DatosDt = null) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                DataTable dt = DatosDt == null ? ds.Tables[0] : ds.Tables[0].DefaultView.ToTable(true, DatosDt);
                foreach (DataRow row in dt.Rows)
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
