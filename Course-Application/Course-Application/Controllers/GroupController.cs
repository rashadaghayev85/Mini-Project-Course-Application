using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        private readonly IHelperService _helperService;
        public GroupController()
        {
            _groupService = new GroupService();
            _studentService = new StudentService();
            _helperService = new HelperService();
        }
        public void Create()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue creating a group ?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
        Name: ConsoleColor.Cyan.WriteConsole("Add Group name:");
            string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto Name;
            }
            var response = _groupService.GetByName(groupName);
            if (response != null)
            {
                ConsoleColor.Red.WriteConsole("a group with this name exists");
                ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue creating a group ?\n1-Yes(press any button)   2-No,Back Menu");
                string chooseeStr = Console.ReadLine();
                if (chooseeStr == "2")
                {
                    return;
                }
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
                _groupService.Create(new Group { Name = groupName.Trim().ToLower(), Teacher = teacherName.Trim(), Room = roomName.Trim() });


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
            int count = 0;
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
                    if (_helperService.CheckTryCount(ref count))
                    {
                        ConsoleColor.DarkGreen.WriteConsole(ResponseMessages.ProcessFinish);
                        return;
                    }
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
            bool update = true;
        Id: ConsoleColor.Yellow.WriteConsole("Select the Id you want to update:");
            string idStr = Console.ReadLine();
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
                    Console.WriteLine(" enter new  group name ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        var response = _groupService.SearchGroupsByName(newName);
                        if (response.Count == 0)
                        {
                            if (data.Name.ToLower().Trim() != newName.ToLower().Trim())
                            {
                                data.Name = newName;
                                update = false;
                            }
                        }

                    }
                    Console.WriteLine("enter new teacher");
                    string newTeacher = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTeacher))
                    {
                        if (data.Teacher.ToLower().Trim() != newTeacher.ToLower().Trim())
                        {
                            data.Teacher = newTeacher;
                            update = false;
                        }



                    }
                    Console.WriteLine(" enter new room ");
                    string newRoom = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newRoom))
                    {

                        if (data.Room.ToLower().Trim() != newRoom.ToLower().Trim())
                        {
                            data.Room = newRoom;
                            update = false;
                        }



                    }

                    if (update)
                    {
                        ConsoleColor.DarkYellow.WriteConsole("there was no change");
                    }
                    else
                    {
                        _groupService.Update(data);
                        ConsoleColor.Green.WriteConsole("Data update succes");
                    }

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
            if (response.Count == 0)
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
                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                }
                foreach (var item in response)
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
            ConsoleColor.Cyan.WriteConsole("Add group Id:");
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
        public void GroupsByName()
        {

            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            int count = 0;
        GroupName: ConsoleColor.Cyan.WriteConsole("Add Group name:");
            string groupName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto GroupName;
            }

            //try
            //{
            var response = _groupService.SearchGroupsByName(groupName);
            if (response is null)
            {

                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);

                goto GroupName;
            }
            else
            {
                foreach (var item in response)
                {
                    ConsoleColor.DarkGray.WriteConsole("Group Name: " + item.Name + " Teacher Name: " + item.Teacher + " Room Name: " + item.Room + " Id: " + item.Id);

                }

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

