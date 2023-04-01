namespace keeper.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;
        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo)
        {
            VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
            if (vaultKeep.CreatorId != userInfo.Id) throw new Exception("You cannot do that");
            return vaultKeep;
        }

        internal string deleteVaultKeep(int id, string userId)
        {
            VaultKeep vaultKeep = _repo.GetOne(id);
            if (vaultKeep == null) throw new Exception($"there is no vaultkeep at that id; {id}");
            if (vaultKeep.CreatorId != userId) throw new UnauthorizedAccessException("that is not for you to delete");
            _repo.deleteVaultKeep(id);
            return $"You have successfully deleted the vaultkeep!";
        }

        internal List<KeptKeep> GetKeepsByVault(int vaultId, string userId)
        {
            Vault vault = _vaultsService.GetOneVault(vaultId, userId);
            if (vault.CreatorId != userId && vault.isPrivate == true) throw new Exception($"You do not have access to this vault.");
            List<KeptKeep> keptKeeps = _repo.GetKeepsByVault(vaultId);
            return keptKeeps;
        }
    }
}