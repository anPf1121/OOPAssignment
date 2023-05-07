using System.Text.RegularExpressions;
using Model;

namespace Management
{
    public class FacultiesManager
    {
        private List<Faculty> Faculties { get; set; } = default!;
        public FacultiesManager()
        {
            FileHandling.FacultiesFileHandler file = new FileHandling.FacultiesFileHandler();
            Faculties = new List<Faculty>();
            file.ReadFromFile(Faculties);
        }
        public List<Faculty> FacultyList()
        {
            return Faculties;
        }
        public void AddFaculty()
        {
            FileHandling.FacultiesFileHandler file = new FileHandling.FacultiesFileHandler();
            Utils.Cnsole.Title("ADD FACULTY");
            Faculties.Add(Utils.Cnsole.GetFaculty());
            file.WriteToFile(Faculties);
            Utils.Cnsole.Notification("Add Faculty Completed!");
            string? answerOfUser = Utils.Cnsole.YesOrNoQuestion();

            if (answerOfUser == "Y" || answerOfUser == "y")
                AddFaculty();

            else if (answerOfUser == "N" || answerOfUser == "n")
                return;

            else
                Utils.Cnsole.Notification("Invalid Choice!");
        }
        public bool SearchFaculties()
        {
            int count = 0;
            Utils.Cnsole.Title("SEARCH FACULTIES");
            Console.Write(" Enter Search Name: ");
            string? searchName = Standardize.Input.NameStandardize(Console.ReadLine());
            Utils.Cnsole.Line();
            Console.WriteLine("| {0,5} | {1,22} |", "ID", "Full Name");
            Console.WriteLine("----------------------------------");
            foreach (Faculty faculty in Faculties)
            {
                if (faculty.FirstName == searchName || faculty.LastName == searchName || faculty.MiddleName == searchName)
                {
                    string? facultyName = faculty.FullName;
                    Console.WriteLine("| {0,5} | {1,22} |", faculty.Id, Standardize.Input.NameStandardize(facultyName));
                    count++;
                }
            }
            Utils.Cnsole.Line();
            if (count == 0)
                return false;
            return true;
        }
        private bool ShowAllOrDetails(int id)
        {
            if (id <= 0)
            {
                foreach (Faculty faculty in Faculties)
                {
                    string? facultyName = faculty.FullName;
                    Console.WriteLine("| {0,5} | {1,22} |", faculty.Id, Standardize.Input.NameStandardize(facultyName));
                }
                return true;
            }
            else if (id > 0)
            {
                Utils.Cnsole.Title("FACULTY DETAILS");
                foreach (Faculty faculty in Faculties)
                {
                    if (faculty.Id == id)
                    {
                        string? facultyFirstName = faculty.FirstName;
                        string? facultyMiddleName = faculty.MiddleName;
                        string? facultyLastName = faculty.LastName;
                        Console.WriteLine($" First Name: {Standardize.Input.NameStandardize(facultyFirstName)}");
                        Console.WriteLine($" Middle Name: {Standardize.Input.NameStandardize(facultyMiddleName)}");
                        Console.WriteLine($" LastName: {Standardize.Input.NameStandardize(facultyLastName)}");
                        Console.WriteLine($" Email: {faculty.Email}");
                        Console.WriteLine($" Phone: {faculty.Phone}");
                        return true;
                    }
                }
                int a = 120, count = 0;
                for (int i = 0; i <= a; i++)
                {
                    count++;
                }
                Console.WriteLine($"{a}: {count}!");
            }
            return false;
        }
        public bool ShowAllFaculty()
        {
            Utils.Cnsole.Title("FACULTY LIST");

            Console.WriteLine("| {0,5} | {1,22} |", "ID", "Full Name");
            Console.WriteLine("----------------------------------");
            if (!ShowAllOrDetails(-1))
            {
                Utils.Cnsole.Notification("Not Found!");
                return false;
            }
            Utils.Cnsole.Line();
            return true;
        }
        public int ShowFacultyDetails(ref int id)
        {
            Console.Write(" Input No To View Details And 0 Or Character To Back Menu: ");
            Utils.Cnsole.InputInt(out id);
            Console.Clear();

            if (id == 0)
                return 0;

            if (!ShowAllOrDetails(id))
                return -1;

            return 1;
        }
    }
}