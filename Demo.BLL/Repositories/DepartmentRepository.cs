using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly DbContactor dbContactor;
        public DepartmentRepository(DbContactor dbContactor): base (dbContactor) {
        this.dbContactor = dbContactor;
        }
        public async Task<IEnumerable<Department>> GetDepartmentByNameAsync(string name)
        {
            return await Task.Run(() => dbContactor.departments.Where(w => w.Name.ToLower().Contains(name.ToLower())));
        }

        //    public DepartmentRepository(DbContactor dbContactor) {
        //    this.dbContactor = dbContactor;
        //    }

        //    public int AddDepartment(Department department)
        //    {
        //        this.dbContactor.departments.Add(department);
        //        return this.dbContactor.SaveChanges();
        //    }

        //    public int DeleteDepartment(Department d)
        //    {
        //        this.dbContactor.departments.Remove(d);
        //        return this.dbContactor.SaveChanges();
        //    }

        //    public IEnumerable<Department> GetAllDepartments()
        //    {
        //        return this.dbContactor.departments.ToList();
        //    }

        //    public Department GetDepartmentById(int Id)
        //    {
        //        return this.dbContactor.departments.Find(Id);
        //    }

        //    public int UpdateDepartment(Department d)
        //    {
        //        this.dbContactor.departments.Update(d);
        //        return this.dbContactor.SaveChanges();
        //    }
    }
}
