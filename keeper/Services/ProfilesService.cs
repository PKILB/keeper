namespace keeper.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;
        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetProfile(string id)
        {
            Profile profile = _repo.GetById(id);
            return profile;
        }
    }
}