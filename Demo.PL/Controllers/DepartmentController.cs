using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        //private IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork,IMapper mapper) {
        
       // this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> All(string SearchValue)
        {
            
            var res = await unitOfWork.DepartmentRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                res = await unitOfWork.DepartmentRepository.GetDepartmentByNameAsync(SearchValue);
            }
            var mappedDepartment = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(res);
            return View(mappedDepartment);
        }

        public IActionResult Add()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentViewModel department) {
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                await unitOfWork.DepartmentRepository.AddAsync(mappedDepartment);
                int result = await unitOfWork.Complete();
                if (result > 0)
                {
                    TempData["DepartmentAdd"] = "Department was Added Successfully!";
                }
                return RedirectToAction("All");
            }
            return View(department);
        

        }

        public async Task<IActionResult> Details(int? id,string ViewName = "Details")
        {
            if(id is not null)
            {
                var result = await unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
                var mappedDepartment = mapper.Map<Department, DepartmentViewModel>(result);
                if (result is not null)
                {
                    return View(ViewName,mappedDepartment);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> Edit(int ? id)
        {
            //if (id is not null)
            //{
            //    var result = this.departmentRepository.GetDepartmentById(id.Value);
            //    if (result is not null)
            //    {
            //        return View(result);
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}
            //return BadRequest();
            return await Details(id, "Edit");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel d, [FromRoute] int id)
        {
            
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentViewModel, Department>(d);
                try
                {
                    
                   unitOfWork.DepartmentRepository.Update(mappedDepartment);
                    int result = await unitOfWork.Complete();
                    if (result > 0)
                    {
                        TempData["DepartmentEdit"] = "Department was Updated Successfully!";
                    }
                    return RedirectToAction("All");
                }
                catch (Exception ex) {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(d);
        }
        public async Task<IActionResult> Delete(int? id) {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel d, [FromRoute] int id)
        {
            if (id != d.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentViewModel, Department>(d);
                try
                {
                   unitOfWork.DepartmentRepository.Delete(mappedDepartment);
                    int result = await unitOfWork.Complete();
                    if (result > 0)
                    {
                        TempData["DepartmentDelete"] = "Department was Deleted Successfully!";
                    }
                    return RedirectToAction("All");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(d);
        }






    }
}
