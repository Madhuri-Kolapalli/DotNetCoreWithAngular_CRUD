namespace AngularCrud.Server.Data
{
    public class EmployeeRepository
    {
        private readonly AngularCrudServerContext _context;

        public EmployeeRepository(AngularCrudServerContext context)
        {
            _context = context;
        }
    }
}
