namespace Model
{
    [Serializable]
    public class Person
    {
        public int Id { set; get; }
        public string? FirstName { set; get; }
        public string? MiddleName { set; get; }
        public string? LastName { set; get; }
        public string? Email { set; get; }
        public string? Phone { set; get; }
        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}".Trim(); }
        }
    }
}