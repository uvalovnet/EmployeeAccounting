using DB.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;

namespace DB.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task CreateEmployeeAsync(Employee item)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "INSERT INTO dbo.employees " +
                        "(last_name," +
                        "first_name)" +
                        " VALUES " +
                        $"('{item.Last_name}', " +
                        $"'{item.First_name}');";
                    var cmd = new SqlCommand(query, cn);
                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result == 0) { throw new Exception("Failed to create record"); }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = $"DELETE FROM dbo.employees WHERE id={id}";
                    var cmd = new SqlCommand(query, cn);
                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result == 0) { throw new Exception("Id is not exist"); }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task DeleteEmployeeWithHisTimesheetAsync(int id)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = $"DELETE FROM dbo.timesheet WHERE employee={id}";
                    var cmd = new SqlCommand(query, cn);
                    await cmd.ExecuteNonQueryAsync();
                    query = $"DELETE FROM dbo.employees WHERE id={id}";
                    cmd = new SqlCommand(query, cn);
                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result == 0) { throw new Exception("Id is not exist"); }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> response = new List<Employee>();
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "SELECT *," +
                        " (SELECT COUNT(*) FROM dbo.Timesheet WHERE employee = emp.id)" +
                        " FROM dbo.employees AS emp";
                    var cmd = new SqlCommand(query, cn);
                    var result = await cmd.ExecuteReaderAsync();
                    while (result.Read())
                    {
                        response.Add(new Employee((int)result.GetValue(0),
                            (string)result.GetValue(1),
                            (string)result.GetValue(2),
                            (int)result.GetValue(3)));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return response;
        }

        public async Task<Employee> GetDetailsEmployeeAsync(int id)
        {
            Employee response = null;
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = $"SELECT * FROM dbo.employees WHERE id = {id}";
                    var cmd = new SqlCommand(query, cn);
                    var result = await cmd.ExecuteReaderAsync();
                    while (result.Read())
                    {
                        response = new Employee((int)result.GetValue(0),
                            (string)result.GetValue(1),
                            (string)result.GetValue(2),
                            null);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return response;
        }

        public async Task UpdateEmployeeAsync(Employee item)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "UPDATE dbo.employees" +
                        " SET " +
                        $"last_name = '{item.Last_name}'," +
                        $"first_name = '{item.First_name}'" +
                        " WHERE " +
                        $"id = {item.Id}";
                    var cmd = new SqlCommand(query, cn);
                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result == 0) { throw new Exception("Id is not exist"); }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
