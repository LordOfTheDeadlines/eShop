namespace Auth.API.Data.Entities
{
    public class AppRole
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public string Name { get; set; }

        public AppRole() { }

        public AppRole(string name)
        {
            Name = name;
        }
    }
}
