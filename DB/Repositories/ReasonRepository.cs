using DB.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;

namespace DB.Repositories
{
    public class ReasonRepository : IReasonRepository
    {
        private string _connectionString;

        public ReasonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Reason>> GetAllReasonsAsync()
        {
            List<Reason> response = new List<Reason>();
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "SELECT * FROM dbo.reasons";
                    var cmd = new SqlCommand(query, cn);
                    var result = await cmd.ExecuteReaderAsync();
                    while (result.Read())
                    {
                        response.Add(new Reason((int)result.GetValue(0),
                            (string)result.GetValue(1)));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return response;
        }
    }
}
