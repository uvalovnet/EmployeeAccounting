namespace Entities
{
    public class Reason
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Reason(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
