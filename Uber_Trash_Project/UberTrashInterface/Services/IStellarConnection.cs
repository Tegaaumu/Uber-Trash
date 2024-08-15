using UberTrashInterface.Entities;

namespace UberTrashInterface.Services
{
    public interface IStellarConnection
    {
        Task<string> CreateWallet(KeyPairModel keyPairModel);
    }
}
