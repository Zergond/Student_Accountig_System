using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Models;
using DAL.Interfaces;

namespace DAL.Entities.Repositories
{
    public class StudentRepository : SqlRepository, IStudentRepository
    {
        public StudentRepository(IAppDBContext context)
            : base(context)
        {

        }
        public async Task<Student> Add(Student student)
        {
            this.Insert(student);
            this.SaveChanges();
            return student;
        }

    }
}
