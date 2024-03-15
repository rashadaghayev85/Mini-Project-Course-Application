using Domain.Models;
using Repository.Data;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
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
        private readonly IStudentRepository _StudentRepo;
      
        private int count = 1;
        public StudentService()
        {
            _StudentRepo = new StudentRepository();
            
        }

        public void Create(Student data)
        {
            if (data is null) throw new ArgumentNullException();
            data.Id = count;
            _StudentRepo.Create(data);
            count++;
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            Student student = _StudentRepo.GetById((int)id);

            if (student is null) throw new NotFoundException(ResponseMessages.DataNotFound);

            _StudentRepo.Delete(student);
        }
        public void DeleteAll(int ? id)
        {
            if (id is null) throw new ArgumentNullException();

            List<Student> students =_StudentRepo.GetAllWithExpression(s => s.Group.Id == id);

            if (students is null) throw new NotFoundException(ResponseMessages.DataNotFound);
            
             _StudentRepo.DeleteAll((int)id);
            
            
        }
        public void Update(Student data)
        {
            if (data is null) throw new ArgumentNullException();
            _StudentRepo.Update(data);
        }

        public List<Student> GetAll()
        {
            return _StudentRepo.GetAll();
        }

        public List<Student> GetByAge(int age)
        {
            return _StudentRepo.GetByAge( age);
        }

        public List<Student> GetByGroupId(int id)
        {
            return _StudentRepo.GetByGroupId(id);
        }

        public Student GetById(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            Student student = _StudentRepo.GetById((int)id);

            if (student is null) throw new NotFoundException(ResponseMessages.DataNotFound);

            return student;
        }

        public List<Student> GetByNameOrSurname(string searchText)
        {
            return _StudentRepo.GetByNameOrSurname(searchText);
        }

      
    }
}
