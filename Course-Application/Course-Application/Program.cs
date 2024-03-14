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
            case (int)OperationType.StudentCreate:
                studentController.Create();
                break;
            case (int)OperationType.StudentGetAll:
                studentController.GetAll();
                break;

            case (int)OperationType.StudentGetByAge:
                studentController.GetByAge();
                break;
            case (int)OperationType.StudentDelete:
                studentController.Delete();
                break;
            case (int)OperationType.GetByGroupId:
                studentController.GetByGroupId();
                break;

            //case (int)OperationType.GetAllLibraries:
            //    libraryController.GetAll();
            //    break;
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
    ConsoleColor.Cyan.WriteConsole("Choose one operation :  1-Group create, 2-Student create, 3-Get all students, 4-StudentGetByAge,5-StudentDelete,6-GetByGroupId");
}