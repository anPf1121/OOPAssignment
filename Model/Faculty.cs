namespace Model
{
    [Serializable]
    public class Faculty : Person
    {
        public Faculty(int id, string? firstName, string? middleName, string? lastName, string? email, string? phone)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
        }
    }
}