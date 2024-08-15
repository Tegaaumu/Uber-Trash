using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UberTrashInterface.Entities
{
    public class User
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        [PhoneNumber(ErrorMessage = "Phone number must contain only digits.")]
        public string Phone_Number { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? PublicKey { get; set; }
        public string? AgentORUser { get; set; }

          
        public string PhoneNumber
        {
            get => Phone_Number;
            set
            {
                // Check if the input contains only digits and is of valid length
                if (!Regex.IsMatch(value, @"^\d+$"))
                {
                    throw new ArgumentException("Phone number must contain only digits.");
                }

                Phone_Number = value;
            }
        }


    }
}
