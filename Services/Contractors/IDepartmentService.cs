using BackEndAPI.Models;

namespace BackEndAPI.Services.Contractors
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetList();
    }
}
