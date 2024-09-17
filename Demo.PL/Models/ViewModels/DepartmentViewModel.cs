using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.PL.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Department Code is Required!")]
        public int Code { get; set; }
        public DateTime? DateOfCreation { get; set; }

  
    }
}
