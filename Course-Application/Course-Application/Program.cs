using Course_Application.Controllers;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;

GroupController groupController = new GroupController();
StudentController studentController = new StudentController();
while (true)
{
    GetMenues();
Operation: string operationStr = Console.ReadLine();

    int operation;

    bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);
    if (isCorrectOperationFormat)
    {
        switch (operation)
        {
            case (int)OperationType.GroupCreate:
                groupController.Create();
                break;
            case (int)OperationType.GroupDelete:
                groupController.Delete();
                break;
            case (int)OperationType.GroupUpdate:
                groupController.GetAll();
                break;

            case (int)OperationType.GroupGetAll:
                groupController.GetAll();
                break;
            case (int)OperationType.GroupGetAllByRoom:
                groupController.GetAllByRoom();
                break;
            case (int)OperationType.GroupGetAllByTeacher:
                groupController.GetAllByTeacher();
                break;

            case (int)OperationType.GroupGetById:
                groupController.GetById();
                break;
            case (int)OperationType.GroupGetByName:
                groupController.GetByName();
                break;
            case (int)OperationType.StudentCreate:
                studentController.Create();
                break;
            case (int)OperationType.StudentDelete:
                studentController.Delete();
                break;
            case (int)OperationType.StudentUpdate:
                studentController.Update();
                break;
            case (int)OperationType.StudentGetAll:
                studentController.GetAll();
                break;
            case (int)OperationType.StudentGetByAge:
                studentController.GetByAge();
                break;
            case (int)OperationType.StudentGetById:
                studentController.GetById();
                break;
            case (int)OperationType.StudentGetByGroupId:
                studentController.GetByGroupId();
                break;
            case (int)OperationType.StudentGetByNameorSurname:
                studentController.GetByNameOrSurname();
                break;
            default:
                ConsoleColor.Red.WriteConsole("Operation is wrong, please choose again");
                goto Operation;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
        goto Operation;
    }
}
static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose one operation :\n  " +
        "Group opinion:\n"+
        "1-Group create,2-Group Delete,3-Group Uptade,4-Group Get All,5-Group Get All By Room,6-Group Get All By Teacher,7-Group Get By Id,8-Group Get By Name\n" +
        "Student opinion:\n" +
        "9-Student create,10-Student delete,11-Student update 12-Get all students, 13-StudentGetByAge,14-Student Get By Id,15-Student Get By GroupId,16-Student Get By Name or Surname");
}