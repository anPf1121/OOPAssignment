using Model;
using Interface;

namespace FileHandling
{
    public class ClassesFileHandler : IFile<Class>
    {
        public int WriteToFile(List<Class> classes)
        {
            FileStream fs = new FileStream("classList.dat", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            foreach (Class cls in classes)
            {
                sw.Write("{0},{1},{2},{3},{4},{5}\n", cls.ClassRoom, cls.ClassName, cls.StudyDay,
                cls.StudyTime, cls.ClassStatus, cls.FacultyName);
                sw.Flush();
            }
            fs.Close();
            return 0;
        }
        public void ReadFromFile(List<Class> classes)
        {
            FileStream fs = new FileStream("classList.dat", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            fs.Position = 0;
            List<string> lines = File.ReadAllLines("classList.dat").ToList();
            foreach (string line in lines)
            {
                string[] item = line.Split(",");
                Class c = new Class(item[0], item[1], item[2], item[3], item[4], item[5]);
                classes.Add(c);
            }
            fs.Close();
        }
    }
}