namespace UberTrashInterface.Entities
{
    public class AgentTransaction
    {
        public string SenderPublicKey { get; set; }
        public string SenderSecretKey { get; set; }
        public string? RecieverPublicKey { get; set; } 
        public string amount { get; set; }
    }
}
