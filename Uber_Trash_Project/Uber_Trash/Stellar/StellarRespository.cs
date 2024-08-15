using Microsoft.Extensions.Logging;
using StellarDotnetSdk.Responses.Operations;
using StellarDotnetSdk.Responses;
using StellarDotnetSdk;
using System.Reflection;
using StellarDotnetSdk.Accounts;
using Microsoft.AspNetCore.Hosting.Server;
using StellarDotnetSdk.Requests;
using StellarDotnetSdk.Assets;
using StellarDotnetSdk.Operations;
using StellarDotnetSdk.Transactions;
using StellarDotnetSdk.Memos;
using Microsoft.AspNetCore.Mvc;

namespace Uber_Trash.Stellar
{
    public static class StellarRespository
    {

        public static IAccountId? RecieverPublicKeyx { get; set; }
        public static IAccountId? SenderSecretKeyx { get; set; }

        public static void LoadServer(StellarAccountDetails stellarAccountDetails)
        {
            using (var servertest = new Server("https://horizon-testnet.stellar.org"))
            {
                Console.WriteLine("-- All Streaming Attu. Transaction for this Ledger --");
                //var abc = servertest.Transactions
                //. ForAccount ("GDLRM2QFV7G00AZDELHFCC5RBRMX4NAZNBSTAK6NG04CFODY5PSCYWT" )
                //. Stream (sender, response) →> { ShowPaymentResponse(servertest, sender, response); }) Connect();

                servertest.Operations
                    .Stream((sender, response) => { ShowPaymentResponse(servertest, sender, response, stellarAccountDetails.PublicKey); }).Connect();
            }

        }

        public static IAccountId InitializeReceiverPublicKey(string key)
        {
            RecieverPublicKeyx = KeyPair.FromAccountId(key);
            return RecieverPublicKeyx;
        }
        public static IAccountId InitializeReceiverPublicKey2(string key)
        {
            SenderSecretKeyx = KeyPair.FromAccountId(key);
            return SenderSecretKeyx;
        }
        public static async Task<string> SendFunds(StellarSendFunds stellarSendFunds)
        {

            var server = new Server("https://horizon-testnet.stellar.org");
            using (var servertest = new Server("https://horizon-testnet.stellar.org"))
            {
                Console.WriteLine("-- sending funds --");

                //trow error message if it do not imput secret key
                StellarAccount.SetSenderSecretKey(stellarSendFunds.SenderSecretKey);
                var sourceKeyPair = StellarAccount.SenderKeyPair;
                var sourceKeyPairx = KeyPair.FromSecretSeed(stellarSendFunds.SenderSecretKey);


                IAccountId destinationPublicKey = InitializeReceiverPublicKey(stellarSendFunds.RecieverPublicKey!);
                // Check if the destination account exists
                try
                {
                    var destinationPublicKeyx = stellarSendFunds.RecieverPublicKey;
                    var accountResponse = await servertest.Accounts.Account(destinationPublicKeyx);
                }
                catch (HttpResponseException e)
                {
                    if (e.StatusCode == 404)
                    {
                        Console.WriteLine("The destination account does not exist!");
                        return "The destination account does not exist!";
                    }
                    Console.WriteLine($"Error message {e.Message} status {e.StatusCode} {e.Data.Values}");
                }
                // Load the source account
                var sourceAccount = await servertest.Accounts.Account(sourceKeyPair.AccountId);

                // Build the transaction
                var transaction = new TransactionBuilder(sourceAccount)
                    .AddOperation(new PaymentOperation(destinationPublicKey, new AssetTypeNative(), stellarSendFunds.amount))
                    .AddMemo(Memo.Text("Test Transaction"))
                    .SetFee(100)
                    .Build();

                // Sign the transaction

                try
                {
                    //SenderSecretKeyx = sourceKeyPair;
                    //var key = SenderSecretKeyx.ToString();
                    //var key1 = sourceKeyPair.SecretSeed.ToString();
                    //IAccountId keyPair = KeyPair.FromSecretSeed(key1);
                    transaction.Sign(sourceKeyPair, Network.Test());
                    //transaction.Sign(keyPair);
                }
                catch (Exception e)
                {
                    return $"Error message {e.Message} status {e.StackTrace} {e.Data.Values}";
                }
                // Submit the transaction
                try
                {
                    var response = await servertest.SubmitTransaction(transaction);
                    Console.WriteLine($"Success! Transaction hash: {response.IsSuccess} ledger {response.Ledger}");
                    return "Successfull";
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong! " + e.Message);
                }
                return null;
            }

        }
        public static async Task ShowPaymentResponse(Server server, object sender, OperationResponse lr, string publicKey)
        {

            var test = lr.CreatedAt;
            var op = server.Operations.ForAccount(publicKey);
            var operations = await op.Execute().ConfigureAwait(true);
            foreach (var dr in operations.Records){
                if (dr.Type == "payment"){
                    var abc = dr.CreatedAt;
                    Console.WriteLine($"Time created {test} abc: {abc}");
                }
            }
        }
    }
}
