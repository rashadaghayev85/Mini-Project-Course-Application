using Domain.Models;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        public List<Student> GetByAge(int age)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetByGroupId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetByNameOrSurname(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
