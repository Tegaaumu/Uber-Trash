using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UberTrashInterface.Entities
{
    public class UserSellers
    {
        [Required]
        public string? Address { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        [PhoneNumber(ErrorMessage = "Phone number must contain only digits.")]
        public string Phone_Number { get; set; }
        [Range(100, 1000,ErrorMessage ="We only accept number of cans with in 100 - 1000")]
        public int NumberOfItemsToBeOrdered { get; set; }
        public string? PublicKey { get; set; }
        public string? AgentSecertKey { get; set; }
        public string? Agent { get; set; } = null;
        public bool? Received { get; set; } = false;
    }
}
