namespace keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _repo.CreateVault(vaultData);
            return vault;
        }

        internal Vault GetOneVault(int id, string userId)
        {
            Vault vault = _repo.GetOneVault(id);
            if (vault == null) throw new Exception($"no vault at that id; {id}");
            if (vault.CreatorId != userId && vault.isPrivate == true) throw new Exception("That is my vault, you are not allowed to see it.");
            return vault;
        }
    }
}