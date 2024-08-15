using StellarDotnetSdk.Accounts;

namespace Uber_Trash.Stellar
{
    public static class StellarAccount
    {
        public static KeyPair? SenderKeyPair { get; set; }

        public static void SetSenderSecretKey(string secretKey)
        {
            // Validate the secret key format
            if (string.IsNullOrEmpty(secretKey) || !secretKey.StartsWith("S") || secretKey.Length != 56)
            {
                throw new ArgumentException("Invalid secret key format!");
            }

            SenderKeyPair = KeyPair.FromSecretSeed(secretKey);
        }
    }
}
