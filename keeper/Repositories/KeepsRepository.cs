namespace keeper.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, creatorId)
            VALUES
            (@name, @description, @img, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }

        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            keep.*,
            COUNT(vk.id) AS kept,
            creator.*
            FROM keeps keep
            LEFT JOIN vaultKeeps vk ON vk.keepId = keep.id
            JOIN accounts creator ON keep.creatorId = creator.id
            GROUP BY keep.id
            ";
            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) => 
            {
                keep.Creator = creator;
                return keep;
            }).ToList();
            return keeps;
        }
    }
}