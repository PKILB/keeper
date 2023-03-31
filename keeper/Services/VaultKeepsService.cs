namespace keeper.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;
        // private readonly KeptKeep _keptKeep;
        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
            return vaultKeep;
        }

        internal List<KeptKeep> GetKeepsByVault(int vaultId, string userId)
        {
            Vault vault = _vaultsService.GetOneVault(vaultId, userId);
            List<KeptKeep> keptKeeps = _repo.GetKeepsByVault(vaultId);
            return keptKeeps;
        }
    }
}