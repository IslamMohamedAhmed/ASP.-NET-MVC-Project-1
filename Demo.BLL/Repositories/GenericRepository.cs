using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
       private readonly DbContactor contactor;
        public GenericRepository(DbContactor dbContactor) { 
        contactor = dbContactor;
        }
        public /*int*/ async Task AddAsync(T Item)
        {
            await contactor.Set<T>().AddAsync(Item);
            //return contactor.SaveChanges();
        }

        public /*int*/ void Delete(T Item)
        {
            contactor.Set<T>().Remove(Item);
            //return contactor.SaveChanges();
        }

        public async Task <IEnumerable<T>> GetAllAsync()

        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await contactor.employees.Include(e=>e.Department).ToListAsync();
            }
            return await contactor.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await contactor.Set<T>().FindAsync(id);
        }

    

        public /*int*/ void Update(T Item)
        {
            contactor.Set<T>().Update(Item);
            //return contactor.SaveChanges();
        }
    }
}
