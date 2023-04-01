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

        internal Keep GetOneKeep(int id)
        {
            string sql = @"
            SELECT
            keep.*,
            COUNT(vk.id) AS kept,
            creator.*
            FROM keeps keep
            LEFT JOIN vaultKeeps vk ON vk.keepId = keep.id
            JOIN accounts creator ON keep.creatorId = creator.id
            WHERE keep.id = @id
            GROUP BY keep.id
            ";
            Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) => 
            {
                keep.Creator = creator;
                return keep;
            }, new { id }).FirstOrDefault();
            return keep;
        }

        internal int UpdateKeep(Keep updateData)
        {
            string sql = @"
            UPDATE keeps
            SET

            name = @name,
            description = @description,
            img = @img,
            views = @views

            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, updateData);
            return rows;
        }

        internal bool deleteKeep(int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id;
            ";
            int rows = _db.Execute(sql, new { id });
            return rows == 1;
        }

        internal List<Keep> GetProfileKeeps(string profileId)
        {
            string sql = @"
            SELECT
            keep.*,
            creator.*
            FROM keeps keep
            JOIN accounts creator ON keep.creatorId = creator.id
            WHERE keep.creatorId = @profileId
            ";
            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) => 
            {
                keep.Creator = creator;
                return keep;
            }, new { profileId }).ToList();
            return keeps;
        }
    }
}