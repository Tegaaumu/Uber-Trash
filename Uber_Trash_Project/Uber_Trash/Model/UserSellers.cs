using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Uber_Trash.Model
{
    public class UserSellers
    {
        [Required]
        public string? Address { get; set; }
        [Required]
        public string Phone_Number { get; set; }
        public int NumberOfItemsToBeOrdered { get; set; }
        [Key]
        public string? PublicKey { get; set; }
        public string? Agent { get; set; } = null;
        public bool? Received { get; set; } = false;
    }
}
