using System.ComponentModel.DataAnnotations;

namespace Uber_Trash.Stellar
{
    public class StellarSendFunds
    {
        [Required]
        public string SenderPublicKey { get; set; } = "GA4JMY6O3AWDUWGD4XH74ZH4BJVO3V7FYKAAUIEWCLH5GNUGIVSSS7RG";

        [Required]
        public string SenderSecretKey { get; set; } = "SDJ3GZ2LBX7WB2E2CB3ZY7GMJ2TQACMKZJDTL4TEASPABXE53TJTB6I3";

        [Required]
        public string? RecieverPublicKey { get; set; } = "GBN3PW7JVSQMGMOSAD5Y2V5GPJR5ASMCBBCJHYD2O32A3MLAXLQ67UHF";
        public string amount { get; set; } = "100";

        //public void InitializeReceiverPublicKey()
        //{
        //    RecieverPublicKey = KeyPair.FromAccountId("GBN3PW7JVSQMGMOSAD5Y2V5GPJR5ASMCBBCJHYD2O32A3MLAXLQ67UHF");
        //}
    }
}
