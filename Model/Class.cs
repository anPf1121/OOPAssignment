using Management;

namespace Model
{
    [Serializable]
    public class Class
    {
        public List<Student> Students { get; set; } = default!;
        public Class(
            string? classRoom, string className,
            string? studyDay, string studyTime,
            string? classStatus, string? faculty
            )
        {
            Students = new List<Student>();
            this.ClassRoom = classRoom;
            this.ClassName = className;
            this.StudyDay = studyDay;
            this.StudyTime = studyTime;
            this.ClassStatus = classStatus;
            this.Faculty = faculty;
        }
        public string? ClassRoom { get; set; }
        public string ClassName { get; set; } = default!;
        public string? StudyDay { get; set; }
        public string? StudyTime { get; set; }
        public string? ClassStatus { get; set; }
        public string? Faculty { get; set; }
        public int ChangeFaculty(FacultiesManager facultiesManager)
        {
            if (facultiesManager.FacultyList() != null)
            {
                List<string> facultiesName = new List<string>();
                foreach (Faculty faculty in facultiesManager.FacultyList())
                    facultiesName.Add(faculty.FullName);

                int choose = Utils.Cnsole.Menu("CHOOSE FACULTY FOR THIS CLASS", facultiesName.ToArray());

                if (!(choose > 0 && choose <= facultiesName.Count()))
                    return -1;

                Faculty = facultiesName[choose - 1];
                return 1;
            }
            else return 0;
        }
        public int AddStudent(int id)
        {
            Management.StudentsManager sm = new StudentsManager();

            foreach (Student student in Students)
            {
                if (student.Id == id)
                    return 0;
            }
            foreach (Student student in sm.StudentList())
            {
                if (student.Id == id)
                {
                    Students.Add(student);
                    return 1;
                }
            }
            return -1;
        }
        public int RemoveStudent(int id)
        {
            bool check = false;
            if (Students == null) return 0;
            else
            {
                foreach (Student item in Students)
                {
                    if (item.Id == id)
                        check = true;
                }
                if (!check) return -1;

                Students.RemoveAll(student => student.Id == id);
                return 1;
            }
        }
    }
}