using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        //public IDepartmentRepository _DepartmentRepository { get; }

        public EmployeeController(/*IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            //this.employeeRepository = employeeRepository;
            //_DepartmentRepository = departmentRepository;
            this.mapper = mapper;
        }
        public async Task<IActionResult> All(string SearchValue)
        {
            var res = await unitOfWork.EmployeeRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                res = await unitOfWork.EmployeeRepository.GetEmployeeByNameAsync(SearchValue);
            }


            var mappedEmployee = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(res);
            return View(mappedEmployee);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View(new EmployeeViewModel());

        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel employeeView)
        {
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            if (ModelState.IsValid)
            {
                
                if(employeeView.Image is not null)
                {
                    employeeView.ImageName = DocumentSettings.UploadFile(employeeView.Image, "Images");
                }
                var mappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employeeView);
                await unitOfWork.EmployeeRepository.AddAsync(mappedEmployee);
                int result = await unitOfWork.Complete();
                if (result > 0)
                {
                    TempData["EmployeeAdd"] = "Employee was Added Successfully!";
                }
                return RedirectToAction("All");
            }
            return View(employeeView);


        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            if (id is not null)
            {
                var result = await unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
                var mappedEmployee = mapper.Map<Employee, EmployeeViewModel>(result);
                if (result is not null)
                {
                    TempData["ImageName"] = result.ImageName as string;
                    return View(ViewName, mappedEmployee);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> Edit(int? id)
        {

            //if (id is not null)
            //{
            //    var result = this.employeeRepository.GetemployeeById(id.Value);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeViewModel, [FromRoute] int id)
        {
            
            if (ModelState.IsValid)
            {

                try
                {
                    if (employeeViewModel.Image is not null)
                    {
                        var EmployeeImageBeforeUpdate = TempData["ImageName"] as string;


                        DocumentSettings.DeleteFile(EmployeeImageBeforeUpdate, "Images");
                        employeeViewModel.ImageName = DocumentSettings.UploadFile(employeeViewModel.Image, "Images");
                    }
                    
                    var mappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    unitOfWork.EmployeeRepository.Update(mappedEmployee);
                    int result = await unitOfWork.Complete();
                    if (result > 0)
                    {
                        TempData["EmployeeEdit"] = "Employee was Updated Successfully!";
                    }
                    return RedirectToAction("All");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View(employeeViewModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeViewModel, [FromRoute] int id)
        {

            if (id != employeeViewModel.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var mappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                try
                {
                    unitOfWork.EmployeeRepository.Delete(mappedEmployee);
                    int result = await unitOfWork.Complete();
                    if (result > 0 && employeeViewModel.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(employeeViewModel.ImageName, "Images");
                        TempData["EmployeeDelete"] = "Employee was Deleted Successfully!";
                    }
                   
                    return RedirectToAction("All");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeViewModel);
        }


    }
}
