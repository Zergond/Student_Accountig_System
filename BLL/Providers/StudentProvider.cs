using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Identity;
using BLL.Interfaces;
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

        public async Task<Status> DeleteAsync(Student entity)
        {
            throw new System.NotImplementedException();
        }

        public  async Task<List<Student>> GetStudentsAsync()
        {
           return await Task.Run(() => _studentRepository.GetAll<Student>().ToList());
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
