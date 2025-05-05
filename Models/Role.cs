namespace GaryKAuthProject.Models
{
    public class Role
    {
        public string Name {  get; set; }   

        public static Role UserRole = new Role() { Name = USER_ROLE };
        public static Role AdministratorRole = new Role() { Name = ADMINISTRATOR_ROLE };

        public const string USER_ROLE = "User";
        public const string ADMINISTRATOR_ROLE  = "Administrator";

    }
}
