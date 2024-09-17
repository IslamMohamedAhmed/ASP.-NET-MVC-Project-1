
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetDepartmentByNameAsync(string name);
        //IEnumerable<Department> GetAllDepartments();

        //Department GetDepartmentById(int Id);

        //int UpdateDepartment(Department d);

        //int DeleteDepartment(Department d);

        //int AddDepartment(Department d); 
    }
}
