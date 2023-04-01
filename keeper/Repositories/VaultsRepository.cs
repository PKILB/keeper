namespace keeper.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, img)
            VALUES
            (@creatorId, @name, @description, @img);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal Vault GetOneVault(int id)
        {
            string sql = @"
            SELECT
            vault.*,
            acct.*
            FROM vaults vault
            JOIN accounts acct ON vault.creatorId = acct.id
            WHERE vault.id = @id;
            ";
            Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, prof) => 
            {
                vault.Creator = prof;
                return vault;
            }, new { id }).FirstOrDefault();
            return vault;
        }


        internal int UpdateVault(Vault update)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @name,
            description = @description,
            img = @img,
            isPrivate = @isPrivate

            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, update);
            return rows;
        }

        internal bool removeVault(int id)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @id;
            ";
            int rows = _db.Execute(sql, new { id });
            return rows == 1;
        }

        internal List<Vault> GetProfileVaults(string profileId)
        {
            string sql = @"
            SELECT
            vault.*,
            creator.*
            FROM vaults vault
            JOIN accounts creator ON vault.creatorId = creator.id
            WHERE vault.creatorId = @profileId
            ";
            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, creator) => 
            {
                vault.Creator = creator;
                return vault;
            }, new { profileId }).ToList();
            return vaults;
        }

        internal List<Vault> GetMyVaults(string accountId)
        {
            string sql = @"
            SELECT
            vault.*,
            creator.*
            FROM vaults vault
            JOIN accounts creator ON vault.creatorId = creator.id
            WHERE vault.creatorId = @accountId
            ";
            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => 
            {
                vault.Creator = profile;
                return vault;
            }, new { accountId }).ToList();
            return vaults;
        }
    }
    
}