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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            ResponseApi<List<EmployeeDTO>> response = new ResponseApi<List<EmployeeDTO>>(); ;

            try
            {
                List<Employee> employeeList = await employeeService.GetList();
                if (employeeList.Count > 0)
                {
                    List<EmployeeDTO> dtoList = mapper.Map<List<EmployeeDTO>>(employeeList);
                    response = new ResponseApi<List<EmployeeDTO>>() { Status = true, Message = "OK", Value = dtoList };
                } else
                {
                    response = new ResponseApi<List<EmployeeDTO>>() { Status = false, Message = "No data" };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<List<EmployeeDTO>>() { Status = false, Message = ex.Message };

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO request)
        {
            ResponseApi<EmployeeDTO> response = new ResponseApi<EmployeeDTO>();

            Employee model = mapper.Map<Employee>(request);
            Employee createdEmployee = await employeeService.Add(model);

            try
            {
                if (createdEmployee.IdEmpoyee != 0)
                {
                    response = new ResponseApi<EmployeeDTO>()
                    {
                        Status = true,
                        Message = "OK",
                        Value = mapper.Map<EmployeeDTO>(createdEmployee)
                    };
                } else
                {
                    response = new ResponseApi<EmployeeDTO>()
                    {
                        Status = false,
                        Message = "Couldn't create employee"
                    };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<EmployeeDTO>()
                {
                    Status = false,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDTO request)
        {
            ResponseApi<EmployeeDTO> response = new ResponseApi<EmployeeDTO>();

            try
            {
                Employee model = mapper.Map<Employee>(request);
                Employee updatedEmployee = await employeeService.Update(model);
                
                    response = new ResponseApi<EmployeeDTO>
                    {
                        Status = true,
                        Message = "OK",
                        Value = mapper.Map<EmployeeDTO>(updatedEmployee)
                    };

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch(Exception ex)
            {
                response = new ResponseApi<EmployeeDTO>
                {
                    Status = false,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();

            try
            {
                Employee foundEmployee = await employeeService.GetById(id);
                bool deletedEmployee = await employeeService.Delete(foundEmployee);

                if (deletedEmployee)
                {
                    response = new ResponseApi<bool>()
                    {
                        Status = true,
                        Message = "OK"
                    };
                } else
                {
                    response = new ResponseApi<bool>()
                    {
                        Status = false,
                        Message = "Employee not found"
                    };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch(Exception ex)
            {
                response = new ResponseApi<bool>()
                {
                    Status = false,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
