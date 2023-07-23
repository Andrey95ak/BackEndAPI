using BackEndAPI.Models;
using BackEndAPI.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DBEmployeeContext dBEmployeeContext;
        public DepartmentService(DBEmployeeContext dBEmployeeContext)
        {
            this.dBEmployeeContext = dBEmployeeContext;
        }

        public async Task<List<Department>> GetList()
        {
            try
            {
                List<Department> departmentList = new List<Department>();
                departmentList = await dBEmployeeContext.Departments.ToListAsync();

                return departmentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
