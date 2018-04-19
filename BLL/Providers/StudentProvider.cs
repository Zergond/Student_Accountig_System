using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BLL.Identity;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities.Identity;
using DAL.Entities.Models;
using DAL.Entities.Repositories;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using static BLL.Identity.Service;

namespace BLL.Providers
{
    public class StudentProvider : IStudentProvider
    {
        private readonly IStudentRepository _studentRepository;
        private readonly Service.ApplicationUserManager _userManager;
        private readonly Service.ApplicationRoleManager _roleManager;

        public StudentProvider(IStudentRepository studentRepository,Service.ApplicationUserManager userManager,Service.ApplicationRoleManager roleManager)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Status> CreateAsync(Student student)
        {
            var res=await Task.Run(()=> this._studentRepository.Add(student));
            if(res!=null)
            return Status.Succees;
            return Status.Failure;

        }

        public async Task<Status> DeleteStudentAsync(Student student)
        {
            throw new System.NotImplementedException();
        }


        public async Task<StudentTableViewModel> GetCurrentStudentAsync()
        {
            var student = await _studentRepository.GetStudentByIdAsync(HttpContext.Current.User.Identity.GetUserId());

            return new StudentTableViewModel { Name = student.Name, LastName = student.LastName, Age = student.Age.ToString(), StudyDate = student.StudyDate.ToString(@"MM\/dd\/yyyy") };
        }

        public async Task<List<StudentTableViewModel>> GetStudentsAsyncByFilter(string pageIndex, string pageSize)
        {
            List<Student> listStudent;
            int pageindex =Int32.Parse(pageIndex);
            int pagesize = Int32.Parse(pageSize);
            if (pageIndex=="1")
                listStudent = await Task.Run(() => _studentRepository.GetAll<Student>().Take(pagesize).ToList());
            else
                listStudent = await Task.Run(() => _studentRepository.GetAll<Student>().OrderBy(s=> s.Id).Skip(pagesize*(pageindex-1)).Take(1).ToList());
       
            List<StudentTableViewModel> tableView = new List<StudentTableViewModel>();
            tableView = listStudent.Select(s => new StudentTableViewModel { Id = s.Id, Name = s.Name, LastName = s.LastName, Age = s.Age.ToString(), RegisteredDate = s.RegisteredDate.ToString(), StudyDate = s.StudyDate.ToString() }).ToList();

            tableView = ChangeDateFormat(tableView, listStudent);
            return tableView;
        }
        public int GetStudentsCount()
        {
            return _studentRepository.Count();
        }
        public Task<Student> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Status> EditStudentAsync(StudentTableViewModel student, string Id)
        {
            throw new System.NotImplementedException();

        }

        private List<StudentTableViewModel> ChangeDateFormat(List<StudentTableViewModel> tableView, List<Student> listStudent)
        {
            foreach (Student s in listStudent)
            {
                foreach (StudentTableViewModel sv in tableView)
                {
                    sv.RegisteredDate = s.RegisteredDate.ToString(@"MM\/dd\/yyyy");
                    sv.StudyDate = s.StudyDate.ToString(@"MM\/dd\/yyyy");
                }
            }
            return tableView;
        }
    }
}
