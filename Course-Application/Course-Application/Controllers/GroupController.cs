using Domain.Models;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Xml.Linq;

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
        RoomName: string roomname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(roomname))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto RoomName;
            }

            try
            {
                _groupService.Create(new Group { Name = name, Teacher = teachername, Room = roomname });


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
            foreach (var group in result)

            {
                Console.WriteLine("Group Name: " + group.Name + " Teacher Name: " + group.Teacher + " Room Name: " + group.Room + " Id: " + group.Id);
            }
        }
        public void GetAllByRoom()
        {
            ConsoleColor.Cyan.WriteConsole("Add Room name:");
        RoomName: string roomName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto RoomName;
            }

            var response = _groupService.GetAllByRoom(roomName);

            foreach (var item in response)
            {
                Console.WriteLine("Group Name: " + item.Name + " Teacher Name: " + item.Teacher + " Room Name: " + item.Room + " Id: " + item.Id);
            }
        }
        public void GetAllByTeacher()
        {
            ConsoleColor.Cyan.WriteConsole("Add Group name:");
        TeacherName: string teacherName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto TeacherName;
            }

            try
            {
                var response = _groupService.GetAllByTeacher(teacherName);
                foreach(var item in response)
                {
                    Console.WriteLine("Group Name: " + item.Name + " Teacher Name: " + item.Teacher + " Room Name: " + item.Room + " Id: " + item.Id);

                }
                


            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherName;
            }
           
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

                    Console.WriteLine("Group Name: " + response.Name + " Teacher Name: " + response.Teacher + " Room Name: " + response.Room + " Id: " + response.Id);



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
            ConsoleColor.Cyan.WriteConsole("Add Group name:");
        GroupName: string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto GroupName;
            }

            try
            {
                var response = _groupService.GetByName(groupName);

                Console.WriteLine("Group Name: " + response.Name + " Teacher Name: " + response.Teacher + " Room Name: " + response.Room + " Id: " + response.Id);



            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto GroupName;
            }
            
        }
    }
}

