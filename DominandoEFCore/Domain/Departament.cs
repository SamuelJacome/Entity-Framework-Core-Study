using System.Collections.Generic;

namespace DominandoEFCore.Domain
{
    public class Departament
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<Employee> Employees { get; set; }
    }
}