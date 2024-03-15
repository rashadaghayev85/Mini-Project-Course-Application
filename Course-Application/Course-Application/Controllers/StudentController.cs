using Domain.Models;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_Application.Controllers
{
    internal class StudentController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        public StudentController()
        {
            _studentService=new StudentService();
            _groupService=new GroupService();
        }
        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Add student name:");
        Name: string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Name;
            }

            ConsoleColor.Cyan.WriteConsole("Add student surname :");
        Teachername: string surname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(surname))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Teachername;
            }
            ConsoleColor.Cyan.WriteConsole("Add student group name :");
        Groupname: string groupname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupname))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Groupname;
            }
            var dedected=_groupService.GetByName(groupname);
            if (dedected == null)
            {
                ConsoleColor.Red.WriteConsole("create student cannot,because group not exist");
                return;
            }
               Group group=_groupService.GetByName(groupname);
            ConsoleColor.Cyan.WriteConsole("Add student age:");
            Age: string ageStr = Console.ReadLine();
            int age;
            bool isCorrectAgeFormat = int.TryParse(ageStr, out age);
            if (isCorrectAgeFormat)
            {
                try
                {

                    _studentService.Create(new Student { Name = name, Surname = surname, Age = age, Group =  group });

                    ConsoleColor.Green.WriteConsole("Data successfully added");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Name;
                }

            }
            else
            {
                ConsoleColor.Red.WriteConsole("Age format is wrong, please add operation again");
                goto Age;
            }
        }
        public void GetAll()
        {
            var result=_studentService.GetAll();
            foreach (var student in result)

            {
                Console.WriteLine("Name: " + student.Name + " Surname: " + student.Surname + " Age: " + student.Age + " Id: " + student.Id + " Group Name:" + student.Group.Name);
            }
        }
        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Add student id:");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    _studentService.Delete(id);
                    ConsoleColor.Green.WriteConsole("Data successfully deleted");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong, please add again");
                goto Id;
            }
        }
         public void Update()
        {

        }
        public void GetById()
        {
            ConsoleColor.Cyan.WriteConsole("Add student id:");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var response = _studentService.GetById(id);
                    Console.WriteLine("Name:"+response.Name+" Surname:"+response.Surname+" Age:"+response.Age);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong, please add again");
                goto Id;
            }
        }
        public void GetByAge()
        {
            ConsoleColor.Yellow.WriteConsole("search enter student age:");
        Age: string ageStr =Console.ReadLine();
            int age;
            bool isCorrectAgeFormat = int.TryParse(ageStr, out age);
            if(isCorrectAgeFormat)
            {
                var response = _studentService.GetByAge(age);
                foreach (var student in response)
                {
                    Console.WriteLine("Name:"+ student.Name + " Surname:" + student.Surname + " Age:" + student.Age + " Id:" + student.Id);
                }
                
            }
            else
            {
                goto Age;
            }
            
        }
        public void GetByGroupId()
        {
            ConsoleColor.Cyan.WriteConsole("Add group id:");
        Id: string groupId = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(groupId, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var response = _studentService.GetByGroupId(id);
                    Console.WriteLine(response);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong, please add again");
                goto Id;
            }

            
        }

        public void GetByNameOrSurname()
        {
            ConsoleColor.Cyan.WriteConsole("Add search name or surname:");
            searchText: string searchText = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto searchText;
            }
            else
            {
                var response=_studentService.GetByNameOrSurname(searchText);
                foreach (var student in response)

                {
                    Console.WriteLine("Name: " + student.Name + " Surname: " + student.Surname + " Age: " + student.Age + " Id: " + student.Id+" Group Name:"+student.Group.Name);
                }
            }

        }

    }
}
