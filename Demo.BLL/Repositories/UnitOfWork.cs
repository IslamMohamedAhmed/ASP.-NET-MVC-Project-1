using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContactor dbContactor;

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public UnitOfWork(DbContactor dbContactor) {
        DepartmentRepository = new DepartmentRepository(dbContactor);
        EmployeeRepository = new EmployeeRepository(dbContactor);
            this.dbContactor = dbContactor;
        }

        public async Task<int> Complete()
        {
            return await dbContactor.SaveChangesAsync();
        }
    }
}
