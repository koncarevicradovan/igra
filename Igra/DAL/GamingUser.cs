namespace Igra.DAL
{
    public class GamingUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFemale { get; set; }
        public int SumOfPoints { get; set; }
    }
}
