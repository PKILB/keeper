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
    }
}