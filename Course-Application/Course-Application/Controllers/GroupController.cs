﻿using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_Application.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        public GroupController()
        {
            _groupService = new GroupService();
            _studentService = new StudentService(); 
        }
        public void Create()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue creating a group ?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr=Console.ReadLine();
            if (chooseStr=="2")
            {
                return;
            }
            ConsoleColor.Cyan.WriteConsole("Add Group name:");
        Name: string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto Name;
            }

            ConsoleColor.Cyan.WriteConsole("Add Teacher name :");
        Teachername: string teacherName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto Teachername;
            }
            foreach (var item in teacherName)
            {
                if (!char.IsLetter(item))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                    goto Teachername;
                }
            }


            ConsoleColor.Cyan.WriteConsole("Add room name:");
        RoomName: string roomName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto RoomName;
            }
            foreach (var item in roomName)
            {
                if (char.IsPunctuation(item))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                    goto RoomName;
                }
            }

            try
            {
                _groupService.Create(new Group { Name = name, Teacher = teacherName, Room = roomName });


                ConsoleColor.Green.WriteConsole(ResponseMessages.SuccesOperation);

            }
             catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto Name;
            }
            
        }
        public void Delete()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue deleting a group ?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            ConsoleColor.Cyan.WriteConsole("Add group id:");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    _groupService.Delete(id);
                    _studentService.DeleteAll(id);                 
                    ConsoleColor.Green.WriteConsole("Group and its students successfully deleted");
                    return;
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
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue update a group ?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
        Id: ConsoleColor.Yellow.WriteConsole("Select the Id you want to update:");
            string idStr=Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var data = _groupService.GetById(id);
                    if (data is null)
                    {
                        throw new NotFoundException(ResponseMessages.DataNotFound);
                        //ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    }
                    Console.WriteLine(" enter new name ");
                    string newName = Console.ReadLine();
                    if (newName != string.Empty)
                    {
                        data.Name = newName;
                    }
                    Console.WriteLine("enter new teacher");
                    string newTeacher = Console.ReadLine();
                    if (newTeacher != string.Empty)
                    {
                        data.Teacher = newTeacher;
                    }
                    Console.WriteLine(" enter new romm ");
                    string newRoom = Console.ReadLine();
                    if (newRoom != string.Empty)
                    {
                        data.Room = newRoom;
                    }


                    _groupService.Update(data);
                    ConsoleColor.Green.WriteConsole("Data update succes");
                }
                catch (Exception ex)
                { 
                    ConsoleColor.Red.WriteConsole(ex.Message);  
                    ConsoleColor.Blue.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
                    string choose = Console.ReadLine();
                    if (choose == "2")
                    {
                        return;
                    }
                    goto Id;
                }
                
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }
        }
        public void GetAll()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to see all groups?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            var result = _groupService.GetAll();
            if (result.Count == 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                return;
            }
            foreach (var group in result)

            {
                ConsoleColor.DarkGray.WriteConsole("Group Name: " + group.Name + " Teacher Name: " + group.Teacher + " Room Name: " + group.Room + " Id: " + group.Id);
            }
        }
        public void GetAllByRoom()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            ConsoleColor.Cyan.WriteConsole("Add Room name:");
        RoomName: string roomName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto RoomName;
            }

            var response = _groupService.GetAllByRoom(roomName);
            if(response.Count==0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
            }

            foreach (var item in response)
            {
                ConsoleColor.DarkGray.WriteConsole("Group Name: " + item.Name + " Teacher Name: " + item.Teacher + " Room Name: " + item.Room + " Id: " + item.Id);
            }
        }
        public void GetAllByTeacher()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            ConsoleColor.Cyan.WriteConsole("Add Teacher name:");
        TeacherName: string teacherName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto TeacherName;
            }

            try
            {
                var response = _groupService.GetAllByTeacher(teacherName);
                if (response.Count==0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                }
                foreach(var item in response)
                {
                    ConsoleColor.DarkGray.WriteConsole("Group Name: " + item.Name + " Teacher Name: " + item.Teacher + " Room Name: " + item.Room + " Id: " + item.Id);

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
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            int count = 0;
            ConsoleColor.Cyan.WriteConsole("Add group id:");
        Id: string idStr = Console.ReadLine();
            int id;
            
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
               // try
                //{
                    var response = _groupService.GetById(id);
                    if (response is null)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                        count++;
                        if (count > 3)
                        {
                            ConsoleColor.DarkGreen.WriteConsole(ResponseMessages.ProcessFinish);
                            return;
                    }
                    else
                    {
                        goto Id;
                    }
                    }
                        
                        
                   if (response is not null)
                    {
                    ConsoleColor.DarkGray.WriteConsole("Group Name: " + response.Name + " Teacher Name: " + response.Teacher + " Room Name: " + response.Room + " Id: " + response.Id);

                    }




               // }
                //catch (Exception ex)
                //{
                    
                //    ConsoleColor.Red.WriteConsole(ex.Message);
                   
                //    goto Id;
                //}
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong, please add again");
                goto Id;
            }
        }
        public void GetByName()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            int count = 0;
            ConsoleColor.Cyan.WriteConsole("Add Group name:");
        GroupName: string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto GroupName;
            }

            //try
            //{
                var response = _groupService.GetByName(groupName);
                if (response is null)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                count++;
                if (count > 3)
                {
                    ConsoleColor.DarkGreen.WriteConsole(ResponseMessages.ProcessFinish);
                    return;
                }
                goto GroupName;
                }
            else
            {
                ConsoleColor.DarkGray.WriteConsole("Group Name: " + response.Name + " Teacher Name: " + response.Teacher + " Room Name: " + response.Room + " Id: " + response.Id);

            }




            //}
            //catch (Exception ex)
            //{
            //    ConsoleColor.Red.WriteConsole(ex.Message);
            //    goto GroupName;
            //}

        }
    }
}

