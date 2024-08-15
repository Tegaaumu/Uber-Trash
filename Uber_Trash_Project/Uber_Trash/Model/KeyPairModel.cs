using System.ComponentModel.DataAnnotations;

namespace Uber_Trash.Model
{
    public class KeyPairModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Key]
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
        public string Balance { get; set; }
    }
}
