using BackEndAPI.Models;
using BackEndAPI.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DBEmployeeContext dBEmployeeContext;

        public EmployeeService(DBEmployeeContext dBEmployeeContext)
        {
            this.dBEmployeeContext = dBEmployeeContext;
        }

        public async Task<List<Employee>> GetList()
        {
            try
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = await dBEmployeeContext.Employees.Include(dpt => dpt.IdDeparmentNavigation).ToListAsync();

                return employeeList;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Employee> GetById(int idEmployee)
        {
            try
            {
                Employee? employeeFound = new Employee();
                employeeFound = await dBEmployeeContext.Employees.Include(dpt => dpt.IdDeparmentNavigation)
                    .Where(e => e.IdEmpoyee == idEmployee)
                    .FirstOrDefaultAsync();

                return employeeFound;
            }

            catch (Exception ex) { throw ex; }
        }

        public async Task<Employee> Add(Employee model)
        {
            try
            {
                dBEmployeeContext.Add(model);
                await dBEmployeeContext.SaveChangesAsync();
                return model;
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<Employee> Update(Employee model)
        {
            try
            {
                dBEmployeeContext.Update(model);
                await dBEmployeeContext.SaveChangesAsync();
                return model;
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<bool> Delete(Employee model)
        {
            try
            {
                dBEmployeeContext.Remove(model);
                await dBEmployeeContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
