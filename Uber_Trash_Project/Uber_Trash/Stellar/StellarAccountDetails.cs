using System.ComponentModel.DataAnnotations;

namespace Uber_Trash.Stellar
{
    public class StellarAccountDetails
    {
        [Required]
        public string PublicKey { get; set; } = "GA4JMY6O3AWDUWGD4XH74ZH4BJVO3V7FYKAAUIEWCLH5GNUGIVSSS7RG";

        [Required]
        public string? SecretKey { get; set; } = "SDJ3GZ2LBX7WB2E2CB3ZY7GMJ2TQACMKZJDTL4TEASPABXE53TJTB6I3";
        public string? Code { get; set; }
    }
}
