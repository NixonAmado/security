namespace Api.Helpers
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Manager,
            Employee
        }

        public const Roles rol_default = Roles.Employee;
    }
}
