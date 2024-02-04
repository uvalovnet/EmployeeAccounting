using DB.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;

namespace DB.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {

        private string _connectionString;

        public TimesheetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateRowAsync(TimesheetElement item)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "INSERT INTO dbo.timesheet " +
                        "(employee, " +
                        "reason, " +
                        "start_date, " +
                        "duration, " +
                        "discounted, " +
                        "description)" +
                        " VALUES " +
                        $"('{item.EmployeeId}', " +
                        $"'{item.ReasonId}', " +
                        $"'{item.StartDate.Date}', " +
                        $"'{item.Duration}', " +
                        $"'{item.Discounted}', " +
                        $"'{item.Desc}');";
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

        public async Task DeleteRowAsync(int id)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = $"DELETE FROM dbo.timesheet WHERE id={id}";
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

        public async Task<List<TimesheetElement>> GetAllRowsAsync()
        {
            List<TimesheetElement> response = new List<TimesheetElement>();
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "SELECT dbo.timesheet.id, dbo.employees.last_name, dbo.reasons.reason, dbo.timesheet.start_date, dbo.timesheet.duration, dbo.timesheet.discounted" +
                        " FROM dbo.timesheet " +
                        "LEFT JOIN dbo.employees ON dbo.timesheet.employee = dbo.employees.id " +
                        "LEFT JOIN dbo.reasons ON dbo.timesheet.reason = dbo.reasons.id";
                    var cmd = new SqlCommand(query, cn);
                    var result = await cmd.ExecuteReaderAsync();
                    while (result.Read())
                    {
                        response.Add(new TimesheetElement(
                            (int)result.GetValue(0),
                            null,
                            (string)result.GetValue(1),
                            null,
                            (string)result.GetValue(2),
                            (DateTime)result.GetValue(3),
                            (int)result.GetValue(4),
                            (bool)result.GetValue(5),
                            null));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return response;
        }

        public async Task<TimesheetElement> GetDetailsRowAsync(int id)
        {
            TimesheetElement response = null;
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "SELECT dbo.timesheet.id, dbo.timesheet.employee, dbo.timesheet.reason," +
                        " dbo.timesheet.start_date, dbo.timesheet.duration, dbo.timesheet.discounted," +
                        " dbo.timesheet.description" +
                        " FROM dbo.timesheet " +
                        $"WHERE dbo.timesheet.id = {id}";
                    var cmd = new SqlCommand(query, cn);
                    var result = await cmd.ExecuteReaderAsync();
                    while (result.Read())
                    {
                        response = new TimesheetElement(
                            (int)result.GetValue(0),
                            (int)result.GetValue(1),
                            null,
                            (int)result.GetValue(2),
                            null,
                            (DateTime)result.GetValue(3),
                            (int)result.GetValue(4),
                            (bool)result.GetValue(5),
                            (string)result.GetValue(6));
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return response;
        }

        public async Task UpdateRowAsync(TimesheetElement item)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cn.OpenAsync();
                    string query = "UPDATE dbo.timesheet" +
                        " SET " +
                        $"employee = '{item.EmployeeId}'," +
                        $"reason = '{item.ReasonId}'," +
                        $"start_date = '{item.StartDate.Date}'," +
                        $"duration = '{item.Duration}'," +
                        $"discounted = '{item.Discounted}'," +
                        $"description = '{item.Desc}'" +
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
