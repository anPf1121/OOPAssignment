namespace Model
{
    [Serializable]
    public class Student : Person
    {
        public string? Class { set; get; }
        public string? StudentStatus { set; get; }
        public string? BirthDay { set; get; }
        public string? Address { set; get; }
        public string? Note { set; get; }
        public Student(
        int id, string? firstName, string? middleName,
        string? lastName, string? email, string? phone,
        string? cls, string? studentStatus,
        string? birthday, string? address, string? note)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Class = cls;
            this.StudentStatus = studentStatus;
            this.BirthDay = birthday;
            this.Address = address;
            this.Note = note;
        }
    }
}