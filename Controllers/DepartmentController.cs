using AutoMapper;
using BackEndAPI.DTO;
using BackEndAPI.Models;
using BackEndAPI.Services.Contractors;
using BackEndAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            this.departmentService = departmentService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            ResponseApi<List<DepartmentDTO>> response = new ResponseApi<List<DepartmentDTO>>();

            try
            {
                List<Department> departmentList = await departmentService.GetList();
                if (departmentList.Count > 0)
                {
                    List<DepartmentDTO> dtoList = mapper.Map<List<DepartmentDTO>>(departmentList);
                    response = new ResponseApi<List<DepartmentDTO>>() { Status = true, Message = "OK", Value = dtoList };
                } else
                {
                    response = new ResponseApi<List<DepartmentDTO>>() { Status = false, Message = "" };
                }

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                response = new ResponseApi<List<DepartmentDTO>>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
