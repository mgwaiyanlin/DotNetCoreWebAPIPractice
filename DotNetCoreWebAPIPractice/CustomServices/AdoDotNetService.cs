using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace DotNetCoreWebAPIPractice.CustomServices
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            if (parameters is not null && parameters.Length > 0)
            {
                //foreach (var parameter in parameters)
                //{
                //    sqlCommand.Parameters.AddWithValue(parameter.Name, parameter.Value);
                //}

                // another way to add value
                sqlCommand.Parameters.AddRange(parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            sqlConnection.Close();

            string json = JsonConvert.SerializeObject(dataTable); // convert C# to json
            List<T> values = JsonConvert.DeserializeObject<List<T>>(json)!; // convert json to C#

            return values;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            if (parameters is not null && parameters.Length > 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            sqlConnection.Close();

            string json = JsonConvert.SerializeObject(dataTable); // convert C# to json
            List<T> values = JsonConvert.DeserializeObject<List<T>>(json)!; // convert json to C#

            return values[0];
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            if (parameters is not null && parameters.Length > 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray());
            }

            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }

        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}


