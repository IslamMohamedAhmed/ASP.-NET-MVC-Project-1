using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DbContactor dbContactor;
        public EmployeeRepository(DbContactor dbContactor): base(dbContactor) { 
        
        this.dbContactor = dbContactor;
        }

     
        public async Task<IEnumerable<Employee>> GetEmployeeByAddressAsync(string address)
        {
            return await Task.Run(() => dbContactor.employees.Where(x => x.Address.ToLower().Contains(address.ToLower())));
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string name)
        {
            return await Task.Run(() => dbContactor.employees.Where(x => x.Name.ToLower().Contains(name.ToLower())));
        }

      


        //public EmployeeRepository(DbContactor dbContactor) {
        //this.dbContactor = dbContactor;
        //}
        //public int Add(Employee employee)
        //{
        //    dbContactor.employees.Add(employee);
        //    return dbContactor.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    dbContactor.employees.Remove(employee);
        //    return dbContactor.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return dbContactor.employees.ToList();
        //}

        //public Employee GetById(int id)
        //{
        //    return dbContactor.employees.Find(id);

        //}

        //public int Update(Employee employee)
        //{
        //    dbContactor.employees.Update(employee);
        //    return dbContactor.SaveChanges();
        //}
    }
}
