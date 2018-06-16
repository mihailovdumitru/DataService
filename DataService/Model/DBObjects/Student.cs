namespace Model.DBObjects
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ClassID { get; set; }
        public int UserID { get; set; }
    }
}