using Course_Application.Controllers;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;

GroupController groupController = new GroupController();
StudentController studentController = new StudentController();
ConsoleColor.DarkYellow.WriteConsole("You Are Welcome Our Page");


ConsoleColor.DarkMagenta.WriteConsole("Do you want to continue our page ?\n1-Yes(press any button)   2-No,Back Menu");
string chooseStr = Console.ReadLine();
if (chooseStr == "2")
{
    return;
}
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
                groupController.Update();
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
            case (int)OperationType.GroupsGetByName:
                groupController.GroupsByName();
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
//void ShowText()
//{
//    ConsoleColor.DarkYellow.WriteConsole("You Are Welcome Our Page");
//}
static void GetMenues()
{
    

    ConsoleColor.Cyan.WriteConsole("Choose one operation :\n" +
                                     "    ----------------------------" + "-----------------------------\n" +
                                    "    | Group opinion:            |" + "Student opinion:            |\n" +
                                    "    |---------------------------|" + "--------------------------- |\n" +
                                    "    | 1-Group Create            |" + "9-Student Create            |\n" +
                                    "    | 2-Group Delete            |" + "10-Student Delete           |\n" +
                                    "    | 3-Group Uptade            |" + "11-Student Update           |\n" +
                                    "    | 4-Group Get All           |" + "12-Get all students         |\n" +
                                    "    | 5-Group Get All By Room   |" + "13-Student Get By Age       |\n" +
                                    "    | 6-Group Get All By Teacher|" + "14-Student Get By Id        |\n" +
                                    "    | 7-Group Get By Id         |" + "15-Student Get By Group Id  |\n" +
                                    "    | 8-Groups Get By Name      |" + "16-StudentGetByNameOrSurname|\n" +
                                    "    ----------------------------------------------------------\n");


}
//1 - Group c1 - Group create,2-Group Delete,3-Group Uptade