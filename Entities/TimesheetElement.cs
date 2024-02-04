namespace Entities
{
    public class TimesheetElement
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? Employee { get; set; }
        public int? ReasonId { get; set; }
        public string? Reason { get; set; }
        public DateTime StartDate { get; set; }
        public int? Duration { get; set; }
        public bool? Discounted { get; set; }
        public string? Desc { get; set; }
        public TimesheetElement(int? id, int? employeeId, string? employee, int? reasonId, string? reason, DateTime startdate, int? duration, bool? discounted, string? desc)
        {
            Id = id;
            EmployeeId = employeeId;
            Employee = employee;
            ReasonId = reasonId;
            Reason = reason;
            StartDate = startdate;
            Duration = duration;
            Discounted = discounted;
            Desc = desc;
        }
    }
}
