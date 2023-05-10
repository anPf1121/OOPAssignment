
using Model;

namespace Management
{
    public class ClassesManager
    {
        private List<Class> Classes { get; set; } = default!;
        public ClassesManager()
        {
            FileHandling.ClassesFileHandler file = new FileHandling.ClassesFileHandler();
            Classes = new List<Class>();
            file.ReadFromFile(Classes);
        }
        public int AddClass()
        {
            FileHandling.ClassesFileHandler file = new FileHandling.ClassesFileHandler();
            string? answerOfUser;
            Utils.Cnsole.Title("ADD CLASS");
            Classes.Add(Utils.Cnsole.GetClass());
            file.WriteToFile(Classes);
            Utils.Cnsole.Notification("Add Class Completed!");
            answerOfUser = Utils.Cnsole.YesOrNoQuestion();
            if (answerOfUser == "Y" || answerOfUser == "y")
                AddClass();
            else if (answerOfUser == "N" || answerOfUser == "n")
                return 0;
            else
            {
                Utils.Cnsole.Notification("Invalid Choice!");
                return -1;
            }
            return 1;
        }
        public int UpdateClassInfo(string? className, int fieldIdToUpdate)
        {
            FileHandling.ClassesFileHandler file = new FileHandling.ClassesFileHandler();
            FacultiesManager facultiesManager = new FacultiesManager();
            int addFacultyStatus = 0;
            foreach (Class cls in Classes)
            {
                if (cls.ClassName == className)
                {
                    switch (fieldIdToUpdate)
                    {
                        case 1:
                            cls.ClassName = Utils.Cnsole.GetValueField("Class Name").ToUpper();
                            Utils.Cnsole.Notification("Update Class Completed!");
                            file.WriteToFile(Classes);
                            return 1;
                        case 2:
                            if (!Utils.Cnsole.GetStudyDay(cls)) Utils.Cnsole.GetStudyDay(cls);
                            file.WriteToFile(Classes);
                            return 1;
                        case 3:
                            if (!Utils.Cnsole.GetStudyTime(cls)) Utils.Cnsole.GetStudyTime(cls);
                            file.WriteToFile(Classes);
                            return 1;
                        case 4:
                            cls.ClassRoom = Utils.Cnsole.GetValueField("Class Room");
                            Utils.Cnsole.Notification("Update Class Completed!");
                            file.WriteToFile(Classes);
                            return 1;
                        case 5:
                            if (!Utils.Cnsole.GetClassStatus(cls))
                                Utils.Cnsole.GetClassStatus(cls);
                            file.WriteToFile(Classes);
                            return 1;
                        case 6:
                            addFacultyStatus = cls.ChangeFaculty();
                            if (!(addFacultyStatus == 1))
                                cls.ChangeFaculty();

                            else if (addFacultyStatus == -1) Utils.Cnsole.Notification("Invalid Choice!");

                            else Utils.Cnsole.Notification("Not Found!");

                            file.WriteToFile(Classes);
                            return 1;
                        default:
                            return 0;
                    }
                }
            }
            return 0;
        }
        private int ShowAllOrDetails(string className)
        {
            if (className == "null")
            {
                foreach (Class cls in Classes)
                {
                    Console.WriteLine("| {0,5} | {1,9} | {2,13} | {3, 10} | {4,8} |", cls.ClassName, cls.StudyDay, cls.StudyTime, cls.ClassRoom, cls.ClassStatus);
                }
                return 1;
            }
            else
            {
                foreach (Class cls in Classes)
                {
                    if (cls.ClassName == className)
                    {
                        string? facultyName = cls.FacultyName;
                        Console.WriteLine($" Class Name: {cls.ClassName}");
                        Console.WriteLine($" Study Day: {cls.StudyDay}");
                        Console.WriteLine($" Study Time: {cls.StudyTime}");
                        Console.WriteLine($" Class Room: {cls.ClassRoom}");
                        Console.WriteLine($" Status: {cls.ClassStatus}");
                        Console.WriteLine($" Faculty: {cls.FacultyName}");
                        return 1;
                    }
                }
            }
            return 0;
        }
        public int ShowClasses()
        {
            string className = "null";
            Utils.Cnsole.Title("CLASSES LIST");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("| {0,5} | {1,9} | {2,13} | {3, 10} | {4,8} |", "Class", "Study Day", "Study Time", "Class Room", "Status");
            if (ShowAllOrDetails(className) == 0)
            {
                Utils.Cnsole.Notification("Not Found!");
                return 0;
            }
            Console.WriteLine("-------------------------------------------------------------");
            return 1;
        }
        public int ViewDetailsClass(ref string? className)
        {
            className = Utils.Cnsole.GetValueField("Input Class Name To View Details And 0 Or Character To Back Main Menu").ToUpper();

            if (className == null)
                return -1;
            else if (className == "0")
                return 0;

            Utils.Cnsole.Title("CLASS DETAILS");

            if (ShowAllOrDetails(className) == 0)
                return -1;

            return 1;
        }

        public int FilterClassesByStatus(string status)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("                      {0} CLASS", status.ToUpper());
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("| {0,5} | {1,9} | {2,13} | {3, 10} | {4,8} |", "Class", "Study Day", "Study Time", "Class Room", "Status");
            switch (status.ToUpper())
            {
                case "STUDYING":
                    foreach (Class cls in Classes)
                    {
                        if (cls.ClassStatus == "Studying")
                            Console.WriteLine("| {0,5} | {1,9} | {2,13} | {3, 10} | {4,8} |", cls.ClassName, cls.StudyDay, cls.StudyTime, cls.ClassRoom, cls.ClassStatus);
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                    return 1;
                case "COMPLETED":
                    foreach (Class cls in Classes)
                    {
                        if (cls.ClassStatus == "Finish")
                            Console.WriteLine("| {0,5} | {1,9} | {2,13} | {3, 10} | {4,8} |", cls.ClassName, cls.StudyDay, cls.StudyTime, cls.ClassRoom, cls.ClassStatus);
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                    return 1;
                case "CLOSED":
                    foreach (Class cls in Classes)
                    {
                        if (cls.ClassStatus == "Closed")
                            Console.WriteLine("| {0,5} | {1,9} | {2,13} | {3, 10} | {4,8} |", cls.ClassName, cls.StudyDay, cls.StudyTime, cls.ClassRoom, cls.ClassStatus);
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                    return 1;
                default:
                    return 0;
            }
        }
        public int ChangeStatus(string? className)
        {
            foreach (Class cls in Classes)
            {
                if (className == null)
                    return 0;

                if (cls.ClassName == className)
                {
                    if (!Utils.Cnsole.GetClassStatus(cls))
                    {
                        Console.WriteLine("Invalid Choice!");
                        Utils.Cnsole.PressEnterToContinue();
                        Utils.Cnsole.GetClassStatus(cls);
                    }
                    else
                        return 1;
                }
            }
            return -1;
        }
        public int ShowStudents(string? className)
        {
            StudentsManager studentsManager = new StudentsManager();
            if (Classes != null)
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("                   LIST STUDENTS IN CLASS {0}", className);
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("| {0,6} | {1,20} | {2,5} | {3, 12} | {4,20} |", "ID", "Full Name", "Class", "Phone", "Email");
                Console.WriteLine("-------------------------------------------------------------------------------");
                foreach (Class cls in Classes)
                {
                    if (cls.ClassName == className)
                    {
                        ResetAfterUpdateStudentInfo(cls);
                        ResetStudentsForClass(cls);
                        if (cls.StudentList() != null)
                        {
                            foreach (Student student in cls.StudentList())
                            {
                                Console.WriteLine("| {0,6} | {1,20} | {2,5} | {3, 12} | {4,20} |", student.Id, student.FullName, student.Class, student.Phone, student.Email);
                            }
                        }
                        else return -1;
                    }
                }
            }
            else return 0;

            return 1;
        }
        public int ResetAfterUpdateStudentInfo(Class cls)
        {
            StudentsManager studentsManager = new StudentsManager();
            for (int i = 0; i < cls.StudentList().Count(); i++)
            {
                for (int j = 0; j < studentsManager.StudentList().Count(); j++)
                {
                    if (cls.StudentList()[i].Id == studentsManager.StudentList()[j].Id)
                    {
                        cls.StudentList()[i] = studentsManager.StudentList()[j];
                    }
                }
            }
            return 0;
        }
        public int ResetStudentsForClass(Class cls)
        {
            StudentsManager studentsManager = new StudentsManager();
            foreach (Student student in studentsManager.StudentList())
            {
                if (cls.ClassName == student.Class)
                {
                    if (cls.AddStudent(student.Id) == 0)
                        return 0;
                    else if (cls.AddStudent(student.Id) == 1)
                        return 1;
                    else if (cls.AddStudent(student.Id) == -1)
                        return -1;
                }
            }
            return -2;
        }
        public List<string> GetListClassName()
        {
            List<string> classesName = new List<string>();
            foreach (Class cls in Classes)
                classesName.Add(cls.ClassName);
            return classesName;
        }
        public int ChangeClassOfStudent(int studentId, int idClassOfStudent)
        {
            StudentsManager studentsManager = new StudentsManager();
            List<string> classesName = GetListClassName();
            FileHandling.ClassesFileHandler file = new FileHandling.ClassesFileHandler();
            foreach (Class cls in Classes)
            {
                if (!(cls.ClassName == classesName[idClassOfStudent - 1]))
                {
                    cls.RemoveStudent(studentId);
                }
                else
                {
                    foreach (Student student in cls.StudentList())
                    {
                        if (student.Class == classesName[idClassOfStudent - 1])
                            return -2;
                    }
                    cls.AddStudent(studentId);
                    studentsManager.ChangeStudentClass(studentId, classesName[idClassOfStudent - 1]);
                    return 1;
                }
            }
            file.WriteToFile(Classes);
            return 0;
        }
    }
}