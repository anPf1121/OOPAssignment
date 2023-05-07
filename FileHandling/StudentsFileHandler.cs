using Model;
using Interface;

namespace FileHandling
{
    public class StudentsFileHandler : IFile<Student>
    {
        public int WriteToFile(List<Student> students)
        {
            FileStream fs = new FileStream("studentList.dat", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            foreach (Student student in students)
            {
                sw.Write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}\n",
                student.Id, student.FirstName, student.MiddleName,
                student.LastName, student.Email, student.Phone, student.Class,
                student.StudentStatus, student.BirthDay, student.Address, student.Note);
                sw.Flush();
            }
            fs.Close();
            return 0;
        }
        public void ReadFromFile(List<Student> students)
        {
            FileStream fs = new FileStream("studentList.dat", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            fs.Position = 0;
            List<string> lines = File.ReadAllLines("studentList.dat").ToList();
            foreach (string line in lines)
            {
                string[] item = line.Split(",");
                int id = int.Parse(item[0]);
                Student s = new Student(
                    id, item[1], item[2],
                    item[3], item[4], item[5],
                    item[6], item[7], item[8],
                    item[9], item[10]);
                students.Add(s);
            }
            fs.Close();
        }
    }
}