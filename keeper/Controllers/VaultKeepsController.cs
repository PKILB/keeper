namespace keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth;
        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vaultKeepData.CreatorId = userInfo.Id;
                VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo);
                vaultKeep.Creator = userInfo;
                return Ok(vaultKeep);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteVaultKeep(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultKeepsService.deleteVaultKeep(id, userInfo.Id);
                return Ok(message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}