namespace TheTop.Application.Services.DTOs
{
    public class EmployeeSalaryDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal ShouldSalary { get; set; }
        public decimal Salary { get; set; }
        public decimal HourSalary { get; set; }
        public int ShouldWorkingHours { get; set; }
        public int WorkingHours { get; set; }
    }
}