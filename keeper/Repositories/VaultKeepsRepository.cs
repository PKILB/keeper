namespace keeper.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (vaultId, keepId, creatorId)
            Values
            (@vaultId, @keepId, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = id;
            return vaultKeepData;
        }

        internal List<KeptKeep> GetKeepsByVault(int vaultId)
        {
            string sql = @"
            SELECT
            keep.*,
            vk.*,
            creator.*
            FROM vaultKeeps vk
            JOIN keeps keep ON vk.keepId = keep.id
            JOIN accounts creator ON keep.creatorId = creator.id
            WHERE vk.vaultId = @vaultId
            ";
            List<KeptKeep> keptKeeps = _db.Query<KeptKeep, VaultKeep, Profile, KeptKeep>(sql, (keptKeep, vaultKeep, profile) =>
            {
                keptKeep.VaultKeepId = vaultKeep.Id;
                keptKeep.Creator = profile;
                return keptKeep;
            }, new { vaultId }).ToList();
            return keptKeeps;
        }
    }
}