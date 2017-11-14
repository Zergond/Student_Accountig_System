using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Models;
using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Entities.Repositories
{
    public class StudentRepository : SqlRepository, IStudentRepository
    {
        private readonly IAppDBContext _context;
        public StudentRepository(IAppDBContext context)
            : base(context)
        {
            _context = context;
        }
        public Student Add(Student student)
        {
            this.Insert(student);
            this.SaveChanges();
            return student;
        }

    }
}
