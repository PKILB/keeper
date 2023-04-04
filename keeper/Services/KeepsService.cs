namespace keeper.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        private readonly ProfilesService _profilesService;
        public KeepsService(KeepsRepository repo, ProfilesService profilesService)
        {
            _repo = repo;
            _profilesService = profilesService;
            // _vaultsService = vaultsService;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _repo.CreateKeep(keepData);
            return keep;
        }


        internal List<Keep> GetAllKeeps(string userId)
        {
            List<Keep> keeps = _repo.GetAllKeeps();
            // List<Keep> filteredKeeps = allKeeps.FindAll(k => k.CreatorId == userId);
            return keeps;
        }

        internal Keep GetOneKeep(int id, string userId)
        {
            Keep keep = _repo.GetOneKeep(id);
            if (keep == null) throw new Exception($"There is no keep at that id; {id}");
            {
                if (keep.CreatorId != userId)
            // _repo.UpdateKeep(keep);
            keep.Views++;
            }
            return keep;
        }

        internal Keep UpdateKeep(Keep updateData, Account userInfo)
        {
            Keep orginal = this.GetOneKeep(updateData.Id, updateData.CreatorId);
            if(orginal.CreatorId != userInfo.Id) throw new Exception("That is not your keep!");
            orginal.Name = updateData.Name == null ? orginal.Name : updateData.Name;
            orginal.Description = updateData.Description != null ? updateData.Description : orginal.Description;
            orginal.Img = updateData.Img != null ? updateData.Img : orginal.Img;

            _repo.UpdateKeep(orginal);
            return orginal;
        }

        internal string DeleteKeep(int id, Account userInfo)
        {
            Keep keep = this.GetOneKeep(id, userInfo.Id);
            if (keep.CreatorId != userInfo.Id) throw new Exception("You don't own this keep!!");
            bool result = _repo.deleteKeep(id);
            return $"You have successfully deleted the {keep.Name} keep!";
        }

        internal List<Keep> GetProfileKeeps(string profileId)
        {   Profile profile = _profilesService.GetProfile(profileId);
            List<Keep> keeps = _repo.GetProfileKeeps(profileId);
            return keeps;
        }
    }
}