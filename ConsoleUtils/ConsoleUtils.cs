using System;
using Model;

namespace Utils
{
    public class Cnsole
    {
        public static int Menu(string title, string[] menuItem)
        {
            int i = 0, choice = 0;

            if (title != "")
                Title(title);
            else
                DoubleLine();

            for (; i < menuItem.Count(); i++)
                Console.WriteLine($" {i + 1}. {menuItem[i]} ");

            DoubleLine();
            Console.Write(" Your choice: ");
            InputInt(out choice);
            if (choice <= 0 || choice > menuItem.Count())
                choice = -1;

            Console.Clear();
            return choice;
        }
        public static void InputInt(out int var)
        {
            int.TryParse(Console.ReadLine(), out var);
        }
        public static string GetValueField(string field)
        {
            Console.Write(" " + field + ": ");
            string? value = Console.ReadLine();
            if (value != null)
                return value;
            else
                return "";
        }
        public static string? YesOrNoQuestion()
        {
            Line();
            Console.Write(" Do You Want To Continue? (Y/N): ");
            string? answerOfUser = Console.ReadLine();
            Console.Clear();
            return answerOfUser;
        }
        public static void Line()
        {
            Console.WriteLine("----------------------------------");
        }
        public static void DoubleLine()
        {
            Console.WriteLine("==================================");
        }
        public static void PressEnterToContinue()
        {
            Console.Write(" Press Enter To Continue..");
            Console.ReadKey();
        }
        public static void Title(string title)
        {
            DoubleLine();
            Console.WriteLine(" " + title);
            Line();
        }
        public static void Notification(string message)
        {
            DoubleLine();
            Console.WriteLine(" " + message);
            PressEnterToContinue();
            if (message == "Invalid Choice!")
                Console.Clear();
        }
        public static Class GetClass()
        {
            Class cls = new Class("", "", "", "", "", "");
            cls.ClassName = Utils.Cnsole.GetValueField("Class Name").ToUpper();

            if (!GetStudyDay(cls))
                GetStudyDay(cls);

            if (!GetStudyTime(cls))
                GetStudyTime(cls);

            if (!GetClassStatus(cls))
                GetClassStatus(cls);

            Utils.Cnsole.DoubleLine();
            cls.ClassRoom = Utils.Cnsole.GetValueField("Class Room");

            return cls;
        }
        public static bool GetStudyDay(Class cls)
        {
            string[] studyDays = new[] { "2-4-6", "3-5-7" };
            switch (Utils.Cnsole.Menu("CHOOSE STUDY DAYS", studyDays))
            {
                case 1:
                    cls.StudyDay = "2-4-6";
                    break;
                case 2:
                    cls.StudyDay = "3-5-7";
                    break;
                default:
                    Utils.Cnsole.Notification("Invalid Choice!");
                    return false;
            }
            Utils.Cnsole.Notification("Update Class Complete!");
            return true;
        }
        public static bool GetStudyTime(Class cls)
        {
            string[] studyTime = new[] { "8:30 - 11:30", "14:00 - 17:00", "18:00 - 21:00" };
            switch (Utils.Cnsole.Menu("CHOOSE STUDY TIME", studyTime))
            {
                case 1:
                    cls.StudyTime = "8:30 - 11:30";
                    break;
                case 2:
                    cls.StudyTime = "14:00 - 17:00";
                    break;
                case 3:
                    cls.StudyTime = "18:00 - 21:00";
                    break;
                default:
                    Utils.Cnsole.Notification("Invalid Choice!");
                    return false;
            }
            Utils.Cnsole.Notification("Update Class Complete!");
            return true;
        }
        public static bool GetClassStatus(Class cls)
        {
            string[] classStatus = new[] { "Studying", "Pause", "Finish", "Closed" };
            switch (Utils.Cnsole.Menu("CHOOSE CLASS STATUS", classStatus))
            {
                case 1:
                    cls.ClassStatus = "Studying";
                    // Write();
                    break;
                case 2:
                    cls.ClassStatus = "Pause";
                    // Write();
                    break;
                case 3:
                    cls.ClassStatus = "Finish";
                    // Write();
                    break;
                case 4:
                    cls.ClassStatus = "Closed";
                    // Write();
                    break;
                default:
                    Utils.Cnsole.Notification("Invalid Choice!");
                    return false;
            }
            Utils.Cnsole.Notification("Update Class Status Complete!");
            return true;
        }
        public static bool GetStudentStatus(Student student)
        {
            string[] menu = new[] { "Studying", "Reserve", "Suspended", "Dropout", "Graduated" };
            Utils.Cnsole.DoubleLine();
            int choose = Utils.Cnsole.Menu("STATUS SELECT", menu);
            switch (choose)
            {
                case 1:
                    student.StudentStatus = "Studying";
                    return true;
                case 2:
                    student.StudentStatus = "Reserve";
                    return true;
                case 3:
                    student.StudentStatus = "Suspended";
                    return true;
                case 4:
                    student.StudentStatus = "Dropout";
                    return true;
                case 5:
                    student.StudentStatus = "Graduated";
                    return true;
                default:
                    return false;
            }
        }
        public static Faculty GetFaculty()
        {
            Random RandomID = new Random();
            Faculty faculty = new Faculty(0, "", "", "", "", "");
            faculty.Id = RandomID.Next(1000, 9999);
            faculty.FirstName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("First Name"));
            faculty.MiddleName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("Middle Name"));
            faculty.LastName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("Last Name"));
            string? email = Utils.Cnsole.GetValueField("Email");
            while (!Standardize.Input.EmailStandardize(email))
            {
                Console.WriteLine(" Wrong email format, please re-enter your email!");
                Utils.Cnsole.PressEnterToContinue();
                email = Utils.Cnsole.GetValueField("Email");
            }
            faculty.Email = email;
            string? phone = Utils.Cnsole.GetValueField("Phone");
            while (!Standardize.Input.PhoneNumberStandardize(ref phone))
            {
                Console.WriteLine(" Wrong email format, please re-enter your email!");
                phone = Utils.Cnsole.GetValueField("Phone");
            }
            faculty.Phone = phone;
            return faculty;
        }
        public static int GetFieldToUpdate(string[] fields, string title)
        {
            int choose = Utils.Cnsole.Menu($"UPDATE {title.ToUpper()} INFO", fields);

            if (choose > 0 && choose <= fields.Count())
                return choose;
            else return 0;
        }
        public static int GetFieldToUpdateClass() {
            string[] classField = new[] { "Class Name", "Study Day", "Study Time", "Class Room", "Status", "Faculty" };
            int choose = Utils.Cnsole.GetFieldToUpdate(classField, "class");
            
            if(choose == 0)
                return -1;

            return choose;
        }
        public static int GetIdClassOfStudent(Management.ClassesManager classesManager)
        {
            List<string> classesName = classesManager.GetListClassName();
            int id = Utils.Cnsole.Menu("CHOOSE A CLASS FOR THIS STUDENT", classesName.ToArray());
            if (id <= 0 || id > classesName.Count())
                return -1;

            return id;
        }
        public static int GetStudentFieldToUpdate() {
            string[] menu = new[] { "First Name", "Middle Name", "Last Name", "Email", "Phone", "Birthday", "Address", "Note" };
            Utils.Cnsole.Title("UPDATE STUDENT INFO");
            int choose = Utils.Cnsole.Menu("", menu);
            Utils.Cnsole.Title("UPDATE STUDENT INFO");
            return choose;
        }
        public static string? GetStudentNameToSearch() {
            Utils.Cnsole.Title("SEARCH STUDENTS");
            Console.Write(" Enter Search Name: ");
            string? searchName = Standardize.Input.NameStandardize(Console.ReadLine());
            return searchName;
        }
        public static Student GetStudent()
        {
            Random RandomID = new Random();
            Student student = new Student(0, "", "", "", "", "", "", "", "", "", "");
            student.Id = RandomID.Next(1000, 9999);
            student.FirstName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("First Name"));
            student.MiddleName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("Middle Name"));
            student.LastName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("Last Name"));
            string? email = Utils.Cnsole.GetValueField("Email");
            while (!Standardize.Input.EmailStandardize(email))
            {
                Console.WriteLine(" Wrong email format, please re-enter your email!");
                email = Utils.Cnsole.GetValueField("Email");
            }
            student.Email = email;
            string? phone = Utils.Cnsole.GetValueField("Phone");
            while (!Standardize.Input.PhoneNumberStandardize(ref phone))
            {
                Console.WriteLine(" Wrong phone format, phone number requires 10 numbers!");
                phone = Utils.Cnsole.GetValueField("Phone");
            }
            student.Phone = phone;
            string? birthDay = Utils.Cnsole.GetValueField("Birthday");
            while (!Standardize.Input.BirthDayStandardize(ref birthDay))
            {
                Console.WriteLine(" Wrong birthday format (mm/dd/yyyy)!");
                birthDay = Utils.Cnsole.GetValueField("Birthday");
            }
            student.BirthDay = birthDay;
            student.Address = Utils.Cnsole.GetValueField("Address");
            student.Note = Utils.Cnsole.GetValueField("Note");
            student.StudentStatus = "Reserve";
            Console.WriteLine(" Status: " + student.StudentStatus);
            return student;
        }
    }
}