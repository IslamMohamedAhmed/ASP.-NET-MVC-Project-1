using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Name length is 50")]
        [MinLength(5, ErrorMessage = "Min Name length is 5")]
        public string Name { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }

        [Range(22, 35, ErrorMessage = "Age should be in range of 22 -35")]
        public int? Age { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? DateOfCreation { get; set; } = DateTime.Now;

        
        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }



    }
}
