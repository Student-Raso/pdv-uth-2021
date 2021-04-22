using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

//COMENTARIO PARA MODIFICAR REPOSITORIO

namespace LibBD
{
    public class SqlServer : BD
    {
        //atribs de SQLServer
        SqlConnection con;
        SqlCommand com;
        //SqlDataReader dr;

        //ConnectionString;

        string connectionString;

        //Constructor
        public SqlServer(string server, string db, string us, string pwd, string port = "1433")
        {
            //initiallize atributes
            this.SERVER = server;
            this.DBNAME = db;
            this.US = us;
            this.PWD = pwd;
            //concatenates the ConnectionString
            this.connectionString = $"Data Source={this.SERVER},{this.PORT};Initial Catalog={this.DBNAME};User ID={this.US};Password={this.PWD};";
            //instanciate the connection
            this.con = new SqlConnection(this.connectionString);
        }
        //BND ACTIONS
        public override bool Connect()
        {
            bool res = true;
            try
            {
                if (con.State != ConnectionState.Closed)
                    con.Open();
                else if (con.State == ConnectionState.Broken)
                {
                    con.Close(); con.Open();
                }
                //statnlish correct execution
                res = true;
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "Error at opening SqlServer Connection. " + sqlex.Message;
            }
            //return the method result
            return res;
        }
        public override bool Disconnect()
        {
            bool res = true;
            try
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();

                //statnlish correct execution
                res = true;
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "Error at closing SqlServer Connection. " + sqlex.Message;
            }
            //return the method result
            return res;
        }
        public override bool create(string table, List<DataCollection> data)
        {
            //result variable
            bool res = false;
            //bloque try catch
            try
            {
                this.Connect();
                //parse the column names using dataCollection
                //new string for the columns
                string columns = "";
                foreach (DataCollection column in data)
                    columns += $"{column.Name} ,";
                columns = columns.Remove(columns.Length - 1);
                string values = " ";
                foreach (DataCollection value in data)
                    values += $"{value.Value} ,";
                values = values.Remove(columns.Length - 1);
                //parse the columns values for the dataCollection
                string query = $"INSERT INTO TABLA {table} ({columns}) VALUES ({values})";

                com = new SqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "Error: malfunction query at insert database action. ";
                //cambiar la var de res

            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL ERROR at INSERT sentence. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = "SQL ERROR at INSERT sentence. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "SQL ERROR at INSERT sentence. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }

            return res;
        }
        public override bool update(string table, List<DataCollection> data, int id)
        {
            //result variable
            bool res = false;
            //bloque try catch
            try
            {
                this.Connect();
                //parse the column names using dataCollection
                //new string for the columns
                string columnsValues = "";
                foreach (DataCollection column in data)
                    columnsValues += column.Name + "=" + column.Value + " ,";
                columnsValues = columnsValues.Remove(columnsValues.Length - 1);
                //parse the columns values for the dataCollection
                string query = $"UPDATE {table} SET {columnsValues} WHERE id = {id}";

                com = new SqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "Error: malfunction query at insert database action. ";
                //cambiar la var de res
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + sqlex.Message;
            }
            catch (InvalidOperationException ioex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            return res;
        }

        public override bool delete(string table, int id)
        {
            //result variable
            bool res = false;
            //bloque try catch
            try
            {
                this.Connect();
                string query = $"DELETE FROM {table} WHERE id = {id}";
                com = new SqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "Error: malfunction query at insert database action. ";
                //cambiar la var de res
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + sqlex.Message;
            }
            catch (InvalidOperationException ioex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            return res;
        }
        public override List<List<object>> index(List<string> fields, string table, List<SearchCollection> search)
        {
            //return dynamic 
            List<List<object>> res = new List<List<object>>();
            //try catch
            try
            {
                //connect
                this.Connect();
                //Create the select query
                string query = $"SELECT * FROM {table} WHERE 1 ";
                //instanciate the SQL command
                com = new SqlCommand(query, con);
                //execute READER the query
                SqlDataReader dr = com.ExecuteReader();
                //parse the dataReader
                //return
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        List<object> row = new List<object>();
                        //read every column fo EVERY EACH of the ROWS.
                        for (int i = 0; i < dr.FieldCount; i++)
                            row.Add(dr.GetValue(i));
                        //we add this list to the res Collection
                        res.Add(row);
                    }
                }
                else
                {
                    BD.ERROR = "EMPTY TABLE, NO ROWS RESULTED";
                }

            }
            catch (SqlException sqlex)
            {
                BD.ERROR = $"SQL ERROR in reading the table -{table}-. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = $"SQL ERROR in reading the table -{table}-. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = $"SQL ERROR in reading the table -{table}-. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            return res;
        }





        public override List<List<object>> read(List<string> fields, string table, List<SearchCollection> search)
        {
            //return dynamic 
            List<List<object>> res = new List<List<object>>();
            //try catch
            try
            {
                //connect
                this.Connect();
                //procesar los campos a seleccionar
                string listFields = "";
                foreach (var field in fields)
                    listFields += $"{field} ,";
                //limpiamos la ultima coma que sobra
                listFields = listFields.Remove(listFields.Length - 1);
                string whereString = "";
                foreach (var searchCriteria in search)
                {
                    whereString += $" {searchCriteria.Name} {searchCriteria.resolveOperator()} {searchCriteria.varcharValue()} {searchCriteria.resolveLogicContinue()} ";
                }
                //Create the select query
                string query = $"SELECT {listFields} FROM {table} WHERE {whereString} ";
                //instanciate the SQL command
                com = new SqlCommand(query, con);
                //execute READER the query
                SqlDataReader dr = com.ExecuteReader();
                //parse the dataReader
                //return
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        List<object> row = new List<object>();
                        //read every column fo EVERY EACH of the ROWS.
                        for (int i = 0; i < dr.FieldCount; i++)
                            row.Add(dr.GetValue(i));
                        //we add this list to the res Collection
                        res.Add(row);
                    }
                }
                else
                {
                    BD.ERROR = "EMPTY TABLE, NO ROWS RESULTED";
                }

            }
            catch (SqlException sqlex)
            {
                BD.ERROR = $"SQL ERROR in reading the table -{table}-. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = $"SQL ERROR in reading the table -{table}-. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = $"SQL ERROR in reading the table -{table}-. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            return res;
        }

        //public override List<object> read(List<string> fields, string table, List<SearchCollection> search)
        //{
        //   throw new NotImplementedException();
        //}

        public override List<object> index(List<string> fields, string table, List<SearchCollection> search)
        {
            throw new NotImplementedException();
        }


    }

}
