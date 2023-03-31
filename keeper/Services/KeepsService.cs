namespace keeper.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _repo.CreateKeep(keepData);
            return keep;
        }

        internal List<Keep> GetAllKeeps(string userId)
        {
            List<Keep> allKeeps = _repo.GetAllKeeps();
            List<Keep> filteredKeeps = allKeeps.FindAll(k => k.CreatorId == userId);
            return filteredKeeps;
        }

        internal Keep GetOneKeep(int id, string userId)
        {
            Keep keep = _repo.GetOneKeep(id);
            if (keep == null) throw new Exception($"There is no keep at that id; {id}");
            return keep;
        }

        internal Keep UpdateKeep(Keep updateData)
        {
            Keep orginal = this.GetOneKeep(updateData.Id, updateData.CreatorId);
            orginal.Name = updateData.Name == null ? orginal.Name : updateData.Name;
            orginal.Description = updateData.Description != null ? updateData.Description : orginal.Description;
            orginal.Img = updateData.Img != null ? updateData.Img : orginal.Img;

            _repo.UpdateKeep(orginal);
            return orginal;
        }
    }
}