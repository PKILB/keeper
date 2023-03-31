namespace keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _repo.CreateVault(vaultData);
            return vault;
        }

        internal Vault GetOneVault(int id, string userId)
        {
            Vault vault = _repo.GetOneVault(id);
            if (vault == null) throw new Exception($"no vault at that id; {id}");
            if (vault.CreatorId != userId && vault.isPrivate == true) throw new Exception("That is my vault, you are not allowed to see it.");
            return vault;
        }


        internal Vault UpdateVault(int id, Vault updateData, Account userInfo)
        {
            Vault original = this.GetOneVault(id, userInfo.Id);
            if (original.CreatorId != userInfo.Id) throw new Exception("that is not your vault");

            original.Name = updateData.Name != null ? updateData.Name : original.Name;
            original.Description = updateData.Description != null ? updateData.Description : original.Description;
            original.Img = updateData.Img != null ? updateData.Img : original.Img;
            original.isPrivate = updateData.isPrivate != null ? updateData.isPrivate : original.isPrivate;
            int rowsAffected = _repo.UpdateVault(original);
            if (rowsAffected == 0) throw new Exception($"Could not modify {updateData.Name} @ id {updateData.Id} for some reason.");
            if (rowsAffected > 1) throw new Exception($"Something went wrong, you made at least {rowsAffected} of vaults into {updateData.Name}, check out the db.");

            return original;
        }

        internal string RemoveVault(int id, Account userInfo)
        {
            Vault vault = this.GetOneVault(id, userInfo.Id);
            bool result = _repo.removeVault(id);
            if (vault.CreatorId != userInfo.Id) throw new Exception("You don't own this vault!!");
            return $"You have successfully deleted the {vault.Name} vault!";
        }
    }
}