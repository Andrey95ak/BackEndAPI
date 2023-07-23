namespace BackEndAPI.DTO
{
    public class EmployeeDTO
    {
        public int IdEmpoyee { get; set; }
        public string? FullName { get; set; }
        public int? IdDeparment { get; set; }
        public string? DepartmentName { get; set; }
        public string? Salary { get; set; }
        public string? HireDate { get; set; }
    }
}
