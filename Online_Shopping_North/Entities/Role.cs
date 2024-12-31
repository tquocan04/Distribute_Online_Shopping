namespace Online_Shopping_North.Entities
{
    public class Role
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
