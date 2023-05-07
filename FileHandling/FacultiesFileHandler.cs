using Model;
using Interface;

namespace FileHandling
{
    public class FacultiesFileHandler : IFile<Faculty>
    {
        public int WriteToFile(List<Faculty> faculties)
        {
            FileStream fs = new FileStream("facultyList.dat", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            foreach (Faculty faculty in faculties)
            {
                sw.Write("{0},{1},{2},{3},{4},{5}\n", faculty.Id, faculty.FirstName, faculty.MiddleName, faculty.LastName, faculty.Email, faculty.Phone);
                sw.Flush();
            }
            fs.Close();
            return 0;
        }
        public void ReadFromFile(List<Faculty> faculties)
        {
           FileStream fs = new FileStream("facultyList.dat", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            fs.Position = 0;
            List<string> lines = File.ReadAllLines("facultyList.dat").ToList();
            foreach (string line in lines)
            {
                string[] item = line.Split(",");
                int id = int.Parse(item[0]);
                Faculty f = new Faculty(id, item[1], item[2], item[3], item[4], item[5]);
                faculties.Add(f);
            }
            fs.Close();
        }
    }
}