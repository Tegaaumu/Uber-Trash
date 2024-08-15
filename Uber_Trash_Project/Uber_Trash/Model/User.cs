namespace Uber_Trash.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public string? Phone_Number { get; set; }
        public string? Address { get; set; }
        public string Email_Code { get; set; }
        public bool EmailConfirmation { get; set; } = false;
        //newly added.
        public string PublicKey { get; set; }
        public string AgentORUser { get; set; }

    }
}
