namespace Entities
{
    public class Employee
    {
        public int? Id { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public int? RowsQty { get; set; }
        public Employee(int? id, string last_name, string first_name, int? rowsQty)
        {
            Id = id;
            Last_name = last_name;
            First_name = first_name;
            RowsQty = rowsQty;
        }
    }
}
