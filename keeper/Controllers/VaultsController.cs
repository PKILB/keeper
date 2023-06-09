namespace keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;
        private readonly VaultKeepsService _vaultKeepsService;
        // private readonly KeptKeep _keptKeep;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth, VaultKeepsService vaultKeepsService)
        {
            _vaultsService = vaultsService;
            _auth = auth;
            _vaultKeepsService = vaultKeepsService;
        }

        [HttpGet("{id}/keeps")]
    
    public async Task<ActionResult<List<KeptKeep>>> GetKeepsByVault(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<KeptKeep> keptKeeps = _vaultKeepsService.GetKeepsByVault(id, userInfo?.Id);
            return Ok(keptKeeps);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]

    async public Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            vault.Creator = userInfo;
            return Ok(vault);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetOneVault(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetOneVault(id, userInfo?.Id);
            return Ok(vault);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    // [Authorize]
    public async Task<ActionResult<Vault>> UpdateVault(int id, [FromBody] Vault updateData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.UpdateVault(id, updateData, userInfo);
            return Ok(vault);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]

    async public Task<ActionResult<string>> RemoveVault(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            String message = _vaultsService.RemoveVault(id, userInfo);
            return Ok(message);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
    }