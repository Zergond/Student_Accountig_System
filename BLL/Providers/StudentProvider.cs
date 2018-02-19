using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Identity;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities.Models;
using DAL.Interfaces;

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

        public async Task<List<StudentTableViewModel>> GetStudentsAsync()
        {
           var qstudent= await Task.Run(() => _studentRepository.GetAll<Student>().ToList());
            List<StudentTableViewModel> tableView = new List<StudentTableViewModel>();
            tableView = qstudent.Select(s => new StudentTableViewModel { Id = s.Id, Name = s.Name, LastName = s.LastName, Age = s.Age.ToString(), RegisteredDate = s.RegisteredDate.ToString(), StudyDate = s.StudyDate.ToString()}).ToList();
            
            foreach(Student s in qstudent)
            {
               foreach(StudentTableViewModel sv in tableView )
                {
                    sv.RegisteredDate = s.RegisteredDate.ToString(@"MM\/dd\/yyyy");
                    sv.StudyDate = s.StudyDate.ToString(@"MM\/dd\/yyyy");
                }
            }
            return tableView;
        }
        public async Task<List<StudentTableViewModel>> GetStudentsAsyncByFilter(string Name, string LastName, string Age, string StudyDate, string RegisteredDate)
        {
            var listStudent = await Task.Run(() => _studentRepository.GetAll<Student>().ToList());
            List<StudentTableViewModel> tableView = new List<StudentTableViewModel>();
            tableView = listStudent.Select(s => new StudentTableViewModel { Id = s.Id, Name = s.Name, LastName = s.LastName, Age = s.Age.ToString(), RegisteredDate = s.RegisteredDate.ToString(), StudyDate = s.StudyDate.ToString() }).ToList();

            return tableView.Where(a => a.Name.Contains(Name) && a.LastName.Contains(LastName) && a.Age.Contains(Age) && a.RegisteredDate.Contains(RegisteredDate)).ToList();

        }

        public Task<Student> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Status> EditStudentAsync(StudentTableViewModel student, string id)
        {
            throw new System.NotImplementedException();

        }
    }
}
