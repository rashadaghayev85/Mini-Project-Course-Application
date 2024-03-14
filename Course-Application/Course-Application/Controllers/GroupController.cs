using Domain.Models;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Course_Application.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        public GroupController()
        {
            _groupService = new GroupService();
        }
        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Add Group name:");
        Name: string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Name;
            }

            ConsoleColor.Cyan.WriteConsole("Add Teacher name :");
        Teachername: string teachername = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(teachername))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Teachername;
            }


            ConsoleColor.Cyan.WriteConsole("Add room name:");
            string roomname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(roomname))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Teachername;
            }
            
            try
            {
                _groupService.Create(new Group {Name = name, Teacher = teachername, Room = roomname });


                ConsoleColor.Green.WriteConsole("Data successfully added");

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto Name;
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
                    _groupService.Delete(id);
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
        public void GetAll()
        {
            var result = _groupService.GetAll();
            foreach (var student in result)

            {
                Console.WriteLine("Name: " + student.Name + " Surname: " + student.Surname + " Age: " + student.Age + " Id: " + student.Id + " Group Name:" + student.Group);
            }
        }
        public void GetAllByRoom()
        {

            var response = _groupService.GetAllByRoom();
        }
        public void GetAllByTeacher()
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
                    var response = _groupService.GetById(id);
                    Console.WriteLine("Name:" + response.Name + " Surname:" + response.Surname + " Age" + response.Age);
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
        public void GetByName()
        {

        }
    }
}

