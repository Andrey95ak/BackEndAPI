using System;
using System.Collections.Generic;

namespace BackEndAPI.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdDepartment { get; set; }
        public string? FullName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
