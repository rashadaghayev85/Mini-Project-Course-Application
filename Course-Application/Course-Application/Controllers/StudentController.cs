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
        private readonly IHelperService _helperService;
        public StudentController()
        {
            _studentService = new StudentService();
            _groupService = new GroupService();
            _helperService = new HelperService();
        }
        public void Create()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            var result = _groupService.GetAll();
            if (result.Count == 0)
            {
                ConsoleColor.Red.WriteConsole("Sorry, there is no active group at the moment");
            }
            else
            {
                ConsoleColor.Cyan.WriteConsole("Add student name:");
            Name: string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Name;
                }
                foreach (var item in name)
                {
                    if (!char.IsLetter(item))
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                        goto Name;
                    }
                }

                ConsoleColor.Cyan.WriteConsole("Add student surname :");
            Teachername: string surname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surname))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Teachername;
                }
                foreach (var item in surname)
                {
                    if (!char.IsLetter(item))
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                        goto Name;
                    }
                }
                var response = _groupService.GetAll();
                if (response.Count == 0)
                {
                    return;
                }
                ConsoleColor.Yellow.WriteConsole("Possible selection options");
                foreach (var item in result)
                {
                    ConsoleColor.DarkGreen.WriteConsole("Group Name: " + item.Name + " Id: " + item.Id);
                }

                ConsoleColor.Cyan.WriteConsole("Add student group name :");
            Groupname: string groupname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(groupname))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Groupname;
                }
                var dedected = _groupService.GetByName(groupname);
                if (dedected == null)
                {
                    ConsoleColor.Red.WriteConsole("create student cannot,because group not exist");
                    return;
                }
                ConsoleColor.Cyan.WriteConsole("Add student age:");
            Age: string ageStr = Console.ReadLine();
                int age;
                bool isCorrectAgeFormat = int.TryParse(ageStr, out age);
                if (isCorrectAgeFormat)
                {
                    if (15 > age)
                    {
                        ConsoleColor.Red.WriteConsole("Student must be older");
                        return;
                    }
                    try
                    {

                        _studentService.Create(new Student { Name = name, Surname = surname, Age = age, Group = dedected });

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
        }
        public void GetAll()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            var result = _studentService.GetAll();
            if (result.Count == 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                return;
            }
            foreach (var student in result)
            {
                Console.WriteLine("Name: " + student.Name + " Surname: " + student.Surname + " Age: " + student.Age + " Id: " + student.Id + " Group Name:" + student.Group.Name);
            }
        }


        public void Delete()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            int count = 0;
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

                    if (_helperService.CheckTryCount(ref count))
                    {
                        ConsoleColor.Green.WriteConsole(ResponseMessages.ProcessFinish);

                        return;
                    }

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
            
            try
            {

                ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue update a group ?\n1-Yes(press any button)   2-No,Back Menu");
                string chooseStr = Console.ReadLine();
                if (chooseStr == "2")
                {
                    return;
                }
            Id: ConsoleColor.Yellow.WriteConsole("Select the id you want to update");
                string idStr = Console.ReadLine();
                int id;
                bool update = true;
                bool isCorrectIdFormat = int.TryParse(idStr, out id);
                if (isCorrectIdFormat)
                {

                    var data = _studentService.GetById(id);

                    if (data is null)
                    {
                        throw new NotFoundException(ResponseMessages.DataNotFound);

                    }
                    else
                    {


                    NewName: Console.WriteLine(" enter new name ");
                        string newName = Console.ReadLine();
                        foreach (var item in newName)
                        {
                            if (!char.IsLetter(item))
                            {
                                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                                goto NewName;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            if (data.Name.ToLower().Trim() != newName.ToLower().Trim())
                            {
                                data.Name = newName;
                                update = false;
                            }

                        }
                    NewSurname: Console.WriteLine(" enter new surname ");
                        string newSurname = Console.ReadLine();
                        foreach (var item in newSurname)
                        {
                            if (!char.IsLetter(item))
                            {
                                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                                goto NewSurname;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(newSurname))
                        {
                            if (data.Surname.ToLower().Trim() != newSurname.ToLower().Trim())
                            {
                                data.Surname = newSurname;
                                update = false;
                            }

                        }
                        Console.WriteLine("enter new age");
                        string ageStr = Console.ReadLine();
                        int age;
                        bool isCorrectAgeFormat = int.TryParse(ageStr, out age);
                        if (isCorrectAgeFormat && isCorrectAgeFormat != null)
                        {
                            if (15 > age)
                            {
                                ConsoleColor.Red.WriteConsole("Student must be older");
                                return;
                            }
                            if (data.Age != age)
                            {
                                data.Age = age;
                                update = false;
                            }

                        }
                        Console.WriteLine(" enter new group ");
                        string newGroup = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newGroup))
                        {
                            if (data.Group.Name.ToLower().Trim() != newGroup.ToLower().Trim())
                            {

                                var result = _groupService.GetByName(newGroup);

                                data.Group = result;
                                update = false;
                            }

                        }
                        if (update)
                        {
                            ConsoleColor.DarkYellow.WriteConsole("there was no change");
                        }
                        else
                        {
                            _studentService.Update(data);
                            ConsoleColor.Green.WriteConsole("Data update succes");
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                ConsoleColor.Red.WriteConsole(ex.Message);
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
            ConsoleColor.Cyan.WriteConsole("Add student id:");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var response = _studentService.GetById(id);

                    Console.WriteLine("Name:" + response.Name + " Surname:" + response.Surname + " Age:" + response.Age);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    if (_helperService.CheckTryCount(ref count))
                    {
                        ConsoleColor.Green.WriteConsole("Process finished");
                        return;
                    }
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
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            int count = 0;
        Age: ConsoleColor.Yellow.WriteConsole("search enter student age:");
            string ageStr = Console.ReadLine();
            int age;
            bool isCorrectAgeFormat = int.TryParse(ageStr, out age);
            if (isCorrectAgeFormat)
            {
                var response = _studentService.GetByAge(age);
                if (response.Count == 0)
                {
                    if (_helperService.CheckTryCount(ref count))
                    {
                        ConsoleColor.Green.WriteConsole(ResponseMessages.ProcessFinish);

                        return;
                    }
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    goto Age;

                }
                foreach (var student in response)
                {
                    Console.WriteLine("Name:" + student.Name + " Surname:" + student.Surname + " Age:" + student.Age + " Id:" + student.Id);
                }

            }
            else
            {

                ConsoleColor.Red.WriteConsole("age format is wrong, please add again");

                goto Age;
            }

        }
        public void GetByGroupId()
        {
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            ConsoleColor.DarkYellow.WriteConsole("Enter the group ID you are looking for");
        Id: string groupId = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(groupId, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var response = _studentService.GetByGroupId(id);
                    if (response.Count == 0)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);

                    }
                    else
                    {
                        foreach (var student in response)
                        {
                            ConsoleColor.DarkGreen.WriteConsole("Name: " + student.Name + " Surname: " + student.Surname + " Age: " + student.Age + " Group: " + student.Group.Name + " Id: " + student.Id);
                        }
                    }


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
            ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
            string chooseStr = Console.ReadLine();
            if (chooseStr == "2")
            {
                return;
            }
            int count = 0;
        SearchText: ConsoleColor.Cyan.WriteConsole("Add search name or surname:");
            string searchText = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto SearchText;
            }
            else
            {
                var response = _studentService.GetByNameOrSurname(searchText);
                if (response.Count == 0)
                {
                    if (_helperService.CheckTryCount(ref count))
                    {
                        ConsoleColor.Green.WriteConsole(ResponseMessages.ProcessFinish);

                        return;
                    }
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    goto SearchText;
                }
                foreach (var student in response)

                {
                    Console.WriteLine("Name: " + student.Name + " Surname: " + student.Surname + " Age: " + student.Age + " Id: " + student.Id + " Group Name:" + student.Group.Name);
                }
            }

        }


    }
}
