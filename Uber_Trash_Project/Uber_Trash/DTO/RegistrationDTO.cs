namespace Uber_Trash.DTO
{
    public class RegistrationDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_Number { get; set; }
        public string? Address { get; set; }
        public string PublicKey { get; set; }
        public string AgentORUser { get; set; }
    }
}
