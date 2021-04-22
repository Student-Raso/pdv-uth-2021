using System;
using System.Collections.Generic;

namespace LibBD
{
    public abstract class BD

    {
        //props
        public static string ERROR;
        //db props
        public string SERVER { get; set; }
        public string DBNAME { get; set; }
        public string US { get; set; }
        public string PWD { get; set; }
        public string PORT { get; set; }

        //metodos abstractos
        /// <summary>
        /// Create a new ROW in the DATABASE selected adn configure by the constructor 
        /// </summary>
        /// <param name="table"></param> The table that will create a new row</param>
        /// <param name="data"></param> The data that will be used to create the row
        /// <returns></returns>
        public abstract bool create(string table, List<DataCollection> data);
        
        public abstract bool update(string table, List<DataCollection> data, int id);
        public abstract bool delete(string table, int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public abstract List<List<object>> read(List<string> fields, string table, List<SearchCollection> search);
        
        
        // public abstract List<object> read(List<string> fields, string table, List<SearchCollection> search);  
        public abstract List<List<object>> index(List<string> fields, string table, List<SearchCollection> search);
        /// <summary>
        /// open the connection to the server stablished by the connectionString
        /// </summary>
        /// <returns></returns>
        public abstract bool Connect();
        public abstract bool Disconnect();
    } 
}
    