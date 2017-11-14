using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities.Models;
using DAL.Interfaces;

namespace BLL.Providers
{
    public class StudentProvider : IStudentProvider
    {
        private readonly IStudentRepository _studentRepository;
        public StudentProvider(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Status> CreateAsync(Student student)
        {
            await Task.Run(()=> this._studentRepository.Add(student));
            return Status.Succees;

        }

        public async Task<Status> DeleteAsync(Student entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IQueryable<Student>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public  Task<Student> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
