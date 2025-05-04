namespace GaryKAuthProject.Models
{
    public class Role
    {
        public string Name {  get; set; }   

        public static Role UserRole = new Role() { Name = "User"};
        public static Role AdministratorRole = new Role() { Name = "Administrator" };

    }
}
