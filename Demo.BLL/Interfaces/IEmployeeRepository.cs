using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();

        //Employee GetById(int id);

        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);

        Task<IEnumerable<Employee>> GetEmployeeByAddressAsync(string address);
        Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string name);

    }
}
