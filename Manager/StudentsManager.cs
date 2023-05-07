using Model;

namespace Management
{
    public class StudentsManager
    {
        private List<Student> Students { get; set; } = default!;
        public List<Student> StudentList() {
            return Students;
        }
        public StudentsManager()
        {
            FileHandling.StudentsFileHandler file = new FileHandling.StudentsFileHandler();
            Students = new List<Student>();
            file.ReadFromFile(Students);
        }
        public void AddStudent()
        {
            FileHandling.StudentsFileHandler file = new FileHandling.StudentsFileHandler();
            Utils.Cnsole.Title("ADD STUDENT");
            Students.Add(Utils.Cnsole.GetStudent());
            file.WriteToFile(Students);
            Utils.Cnsole.Notification("Add Student Completed!");
            string? answerOfUser = Utils.Cnsole.YesOrNoQuestion();

            if (answerOfUser == "Y" || answerOfUser == "y") AddStudent();

            else if (answerOfUser == "N" || answerOfUser == "n") return;

            else Utils.Cnsole.Notification("Invalid Choice!");
        }
        public int UpdateStudentInfo(int id, int idFieldToUpdate)
        {
            FileHandling.StudentsFileHandler file = new FileHandling.StudentsFileHandler();
            foreach (Student student in Students)
            {
                if (student.Id == id)
                {
                    switch (idFieldToUpdate)
                    {
                        case 1:
                            student.FirstName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("First Name"));
                            file.WriteToFile(Students);
                            return 1;
                        case 2:
                            student.MiddleName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("Middle Name"));
                            file.WriteToFile(Students);
                            return 1;
                        case 3:
                            student.LastName = Standardize.Input.NameStandardize(Utils.Cnsole.GetValueField("Last Name"));
                            file.WriteToFile(Students);
                            return 1;
                        case 4:
                            string? email = Utils.Cnsole.GetValueField("Email");
                            while (!Standardize.Input.EmailStandardize(email))
                            {
                                Console.WriteLine(" Wrong email format, please re-enter your email!");
                                email = Utils.Cnsole.GetValueField("Email");
                            }
                            student.Email = email;
                            file.WriteToFile(Students);
                            return 1;
                        case 5:
                            string? phone = Utils.Cnsole.GetValueField("Phone");
                            while (!Standardize.Input.PhoneNumberStandardize(ref phone))
                            {
                                Console.WriteLine(" Wrong phone format, phone number requires 10 numbers!");
                                phone = Utils.Cnsole.GetValueField("Phone");
                            }
                            student.Phone = phone;
                            file.WriteToFile(Students);
                            return 1;
                        case 6:
                            string? birthDay = Utils.Cnsole.GetValueField("Birthday");
                            while (!Standardize.Input.BirthDayStandardize(ref birthDay))
                            {
                                Console.WriteLine(" Wrong birthday format (mm/dd/yyyy)!");
                                birthDay = Utils.Cnsole.GetValueField("Birthday");
                            }
                            student.BirthDay = birthDay;
                            file.WriteToFile(Students);
                            return 1;
                        case 7:
                            student.Address = Utils.Cnsole.GetValueField("Address");
                            file.WriteToFile(Students);
                            return 1;
                        case 8:
                            student.Note = Utils.Cnsole.GetValueField("Note");
                            file.WriteToFile(Students);
                            return 1;
                        default:
                            return 0;
                    }
                }
            }
            return -1;
        }
        public int SearchStudents(string? studentSearchName)
        {
            int count = 0;
            Utils.Cnsole.Line();
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("| {0,6} | {1,20} | {2,5} | {3, 10} | {4,20} |", "ID", "Full Name", "Class", "Phone", "Email");
            foreach (Student student in Students)
            {
                if (student.FirstName == studentSearchName || student.LastName == studentSearchName || student.MiddleName == studentSearchName)
                {
                    string? studentName = student.FullName;
                    Console.WriteLine("| {0,6} | {1,20} | {2,5} | {3, 10} | {4,20} |", student.Id, student.FullName, student.Class, student.Phone, student.Email);
                    count++;
                }
            }
            Console.WriteLine("-----------------------------------------------------------------------------");
            if (count == 0)
                return 0;
            return 1;
        }
        public bool ChangeStudentStatus(int id)
        {
            foreach (Student student in Students)
            {
                if (student.Id == id)
                    return Utils.Cnsole.GetStudentStatus(student);
            }
            return false;
        }
        private bool ShowAllOrDetails(int id)
        {
            if (id <= 0)
            {
                foreach (Student student in Students)
                {
                    Console.WriteLine("| {0,6} | {1,20} | {2,5} | {3, 12} | {4,20} |", student.Id, student.FullName, student.Class, student.Phone, student.Email);
                }
                return true;
            }
            else if (id > 0)
            {
                foreach (Student student in Students)
                {
                    if (student.Id == id)
                    {
                        Console.WriteLine($" First Name: {student.FirstName}");
                        Console.WriteLine($" Middle Name: {student.MiddleName}");
                        Console.WriteLine($" LastName: {student.LastName}");
                        Console.WriteLine($" Email: {student.Email}");
                        Console.WriteLine($" Phone: {student.Phone}");
                        Console.WriteLine($" BirthDay: {student.BirthDay}");
                        Console.WriteLine($" Address: {student.Address}");
                        Console.WriteLine($" Class: {student.Class}");
                        Console.WriteLine($" Note: {student.Note}");
                        Console.WriteLine($" Status: {student.StudentStatus}");
                        return true;
                    }
                }
            }
            return false;
        }
        public int ChangeStudentClass(int id, string? className)
        {
            FileHandling.StudentsFileHandler file = new FileHandling.StudentsFileHandler();
            foreach (Student student in Students)
            {
                if (student.Id == id)
                {
                    student.Class = className;
                }
            }
            file.WriteToFile(Students);
            return -3;
        }
        public bool ShowAllStudent()
        {
            Utils.Cnsole.Title("LIST STUDENTS");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("| {0,6} | {1,20} | {2,5} | {3, 12} | {4,20} |", "ID", "Full Name", "Class", "Phone", "Email");
            if (!ShowAllOrDetails(-1))
            {
                Utils.Cnsole.Notification("Not Found!");
                return false;
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
            return true;
        }
        public int ShowStudentDetails(ref int id)
        {
            Console.Write(" Input No To View Details And 0 Or Character To Back Main Menu: ");
            Utils.Cnsole.InputInt(out id);
            Console.Clear();

            if (id == 0)
                return 0;

            Utils.Cnsole.Title("STUDENT DETAILS");

            if (!ShowAllOrDetails(id))
                return -1;

            return 1;
        }
    }
}