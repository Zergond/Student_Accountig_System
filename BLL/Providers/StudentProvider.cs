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

        public async Task<object> GetStudentsAsync()
        {
           var qstudent= await Task.Run(() => _studentRepository.GetAll<Student>());
            return qstudent.Select(s => new
            { Id = s.Id, Name = s.Name, LastName = s.LastName, Age = s.Age, RegisteredDate = s.RegisteredDate, StudyDate = s.StudyDate }).ToList();           
        }
        public async Task<object> GetStudentsAsyncByFilter(string Name, string LastName, string Age, string StudyDate, string RegisteredDate)
        {
            Student s = new Student();
            s.Name = Name;
            s.LastName = LastName;
            if(Age!="")
            s.Age=Int32.Parse(Age);
            if(StudyDate!="")
            s.StudyDate = DateTime.Parse(StudyDate);
            if(RegisteredDate!="")
            s.RegisteredDate = DateTime.Parse(RegisteredDate);

            var qstudent = await Task.Run(() => _studentRepository.GetAll<Student>());
            var sel = qstudent.Select(a => new { Id = a.Id, Name = a.Name, LastName = a.LastName, Age = a.Age, RegisteredDate = a.RegisteredDate, StudyDate = a.StudyDate });
            return sel.Where(a => a.Name.Contains(s.Name) && a.LastName.Contains(s.LastName) && a.Age.ToString().Contains(Age) && a.RegisteredDate.ToString().Contains(RegisteredDate));

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
