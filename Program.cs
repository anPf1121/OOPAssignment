using Model;
using Management;
public class Program
{
    public static void Main()
    {
        string[] mainMenuItem = new string[] { "FACULTY LIST MANAGEMENT", "STUDENT LIST MANAGEMENT", "CLASSES MANAGEMENT", "EXIT APPLICATION" };
        FacultiesManager facultiesManager = new FacultiesManager();
        StudentsManager studentsManager = new StudentsManager();
        ClassesManager classesManager = new ClassesManager();
        int choice = 0;
        do
        {
            choice = Utils.Cnsole.Menu("SCHOOL MANAGEMENT SYSTEM", mainMenuItem);
            switch (choice)
            {
                case 1:
                    FacultiesManagementMenu(facultiesManager);
                    break;
                case 2:
                    StudentsManagementMenu(classesManager, studentsManager);
                    break;
                case 3:
                    ClassesManagementMenu(classesManager, facultiesManager, studentsManager);
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        } while (choice != 4);
    }
    private static void FacultiesManagementMenu(FacultiesManager facultiesManager)
    {
        int choice, id = 0;
        string[] facultiesManagerMenuItem = new string[] { "ADD FACULTY", "SEARCH FACULTIES", "SHOW ALL FACULTIES", "BACK TO MAIN MENU" };
        do
        {
            choice = Utils.Cnsole.Menu("FACULTIES MANAGEMENT", facultiesManagerMenuItem);
            switch (choice)
            {
                case 1:
                    facultiesManager.AddFaculty();
                    break;
                case 2:
                    ViewFacultyDetailsHandle(facultiesManager.SearchFaculties(), ref id, facultiesManager);
                    break;
                case 3:
                    ViewFacultyDetailsHandle(facultiesManager.ShowAllFaculty(), ref id, facultiesManager);
                    break;
                case 4:
                    break;
                default:
                    Utils.Cnsole.Notification("Invalid Choice!");
                    break;
            }
        } while (choice != 4);
    }
    private static void StudentsManagementMenu(ClassesManager classesManager, StudentsManager studentsManager)
    {
        int choice, id = 0;
        string? studentSearchName = "";
        string[] studentsManagerMenuItem = new string[] { "ADD STUDENT", "SEARCH STUDENTS", "SHOW ALL STUDENTS", "BACK TO MAIN MENU" };
        do
        {
            choice = Utils.Cnsole.Menu("STUDENT MANAGEMENT", studentsManagerMenuItem);
            switch (choice)
            {
                case 1:
                    studentsManager.AddStudent();
                    break;
                case 2:
                    studentSearchName = Utils.Cnsole.GetStudentNameToSearch();
                    ViewStudentDetailsHandle(classesManager, studentsManager.SearchStudents(studentSearchName) == 1, ref id, studentsManager);
                    break;
                case 3:
                    ViewStudentDetailsHandle(classesManager, studentsManager.ShowAllStudent(), ref id, studentsManager);
                    break;
                case 4:
                    break;
                default:
                    Utils.Cnsole.Notification("Invalid Choice!");
                    break;
            }
        } while (choice != 4);
    }
    private static void ClassesManagementMenu(ClassesManager classesManager, FacultiesManager facultiesManager, StudentsManager studentsManager)
    {
        int choice;
        string? searchName = "";
        string[] classesManagerMenuItem = new string[] { "ADD CLASS", "STUDYING CLASSES", "COMPLETED CLASSES", "CLOSED CLASSES", "SHOW ALL CLASSES", "BACK TO MAIN MENU" };
        do
        {
            choice = Utils.Cnsole.Menu("STUDENT MANAGEMENT", classesManagerMenuItem);
            switch (choice)
            {
                case 1:
                    classesManager.AddClass();
                    break;
                case 2:
                    ViewClassDetailsHandle(classesManager.FilterClassesByStatus("STUDYING"), ref searchName, classesManager, facultiesManager, studentsManager);
                    break;
                case 3:
                    ViewClassDetailsHandle(classesManager.FilterClassesByStatus("COMPLETED"), ref searchName, classesManager, facultiesManager, studentsManager);
                    break;
                case 4:
                    ViewClassDetailsHandle(classesManager.FilterClassesByStatus("CLOSED"), ref searchName, classesManager, facultiesManager, studentsManager);
                    break;
                case 5:
                    ViewClassDetailsHandle(classesManager.ShowClasses(), ref searchName, classesManager, facultiesManager, studentsManager);
                    break;
                case 6:
                    return;
                default:
                    Utils.Cnsole.Notification("Invalid Choice!");
                    break;
            }
        } while (choice != 6);
    }
    private static void ViewFacultyDetailsHandle(bool condition, ref int id, FacultiesManager facultiesManager)
    {
        if (condition)
        {
            int temp = facultiesManager.ShowFacultyDetails(ref id);
            if (temp == 1)
            {
                string[] facultyDetailsMenuItem = new string[] { "BACK TO PREVIOUS MENU" };
                int choice = Utils.Cnsole.Menu("", facultyDetailsMenuItem);
                switch (choice)
                {
                    case 1:
                        return;
                    default:
                        Utils.Cnsole.Notification("Invalid Choice!");
                        break;
                }
            }
            else if (temp == 0) return;
            else Utils.Cnsole.Notification("Invalid Choice!");

        }
    }
    private static void ViewStudentDetailsHandle(ClassesManager classesManager, bool condition, ref int id, StudentsManager studentsManager)
    {
        int updateStudent = 0, idClassOfStudent = 0, idFieldToUpdate = 0;
        if (condition)
        {
            int temp = studentsManager.ShowStudentDetails(ref id);
            if (temp == 1)
            {
                string[] studentDetailsMenuItem = new string[] { "UPDATE STUDENT", "CHANGE STATUS", "CHANGE CLASS", "BACK TO PREVIOUS MENU" };
                int choice = Utils.Cnsole.Menu("", studentDetailsMenuItem);
                switch (choice)
                {
                    case 1:
                        idFieldToUpdate = Utils.Cnsole.GetStudentFieldToUpdate();
                        updateStudent = studentsManager.UpdateStudentInfo(id, idFieldToUpdate);
                        if (updateStudent == 1)
                        {
                            Utils.Cnsole.Notification("Update Completed!");
                            Utils.Cnsole.PressEnterToContinue();
                        }
                        else if (updateStudent == 0) Utils.Cnsole.Notification("Invalid Choice!");
                        else if (updateStudent == -1) Utils.Cnsole.Notification("Don't Have Any Student In List!");
                        break;
                    case 2:
                        if (studentsManager.ChangeStudentStatus(id))
                            Utils.Cnsole.Notification("Change Student Status Completed!");
                        break;
                    case 3:
                        idClassOfStudent = Utils.Cnsole.GetIdClassOfStudent(classesManager);
                        if (classesManager.ChangeClassOfStudent(id, idClassOfStudent, studentsManager) == 1)
                            Utils.Cnsole.Notification("Change Class Of Student Completed!");
                        break;
                    case 4:
                        return;
                    default:
                        Utils.Cnsole.Notification("Invalid Choice!");
                        break;
                }
            }
            else if (temp == 0) return;
            else Utils.Cnsole.Notification("Invalid Choice!");
        }
    }
    private static void ViewClassDetailsHandle(int condition, ref string? searchName, ClassesManager classesManager, FacultiesManager facultiesManager, StudentsManager studentsManager)
    {
        int updateStatus = 0, changeStatus = 0, showStatus = 0, fieldToUpdate = 0;
        if (condition == 1)
        {
            int temp = classesManager.ViewDetailsClass(ref searchName);
            if (temp == 1)
            {
                string[] classDetailsMenuItem = new string[] { "UPDATE CLASS", "CHANGE STATUS", "SHOW STUDENT LIST", "BACK TO PREVIOUS MENU" };
                int choice = Utils.Cnsole.Menu("", classDetailsMenuItem);
                switch (choice)
                {
                    case 1:
                        fieldToUpdate = Utils.Cnsole.GetFieldToUpdateClass();
                        updateStatus = classesManager.UpdateClassInfo(searchName, fieldToUpdate, facultiesManager);

                        if (updateStatus == 0) Utils.Cnsole.Notification("Invalid Choice!");

                        return;
                    case 2:
                        changeStatus = classesManager.ChangeStatus(searchName);

                        if (changeStatus == 0) Utils.Cnsole.Notification("Not Found!");
                        else if (changeStatus == -1) Utils.Cnsole.Notification("Don't Have Any Class In Class List!");
                        else Utils.Cnsole.Notification("Change Status Complete!");

                        return;
                    case 3:
                        showStatus = classesManager.ShowStudents(searchName, studentsManager);
                        if (showStatus == 0) Utils.Cnsole.Notification("Not Found!");
                        else if (showStatus == -1) Utils.Cnsole.Notification("Class Don't Have Any Student!");
                        else if (showStatus == 1)
                        {
                            Console.WriteLine("-------------------------------------------------------------------------------");
                            Utils.Cnsole.PressEnterToContinue();
                        }
                        return;
                    case 4:
                        return;
                    default:
                        Utils.Cnsole.Notification("Invalid Choice!");
                        return;
                }
            }
            else if (temp == 0) return;
            else Utils.Cnsole.Notification("Invalid Choice!");
        }
    }
}