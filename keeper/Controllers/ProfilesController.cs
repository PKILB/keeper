namespace keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly Auth0Provider _auth;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        public ProfilesController(Auth0Provider auth, ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
        {
            _auth = auth;
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Profile>> Get(string id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                return Ok(_profilesService.GetProfile(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]

        public async Task<ActionResult<List<Keep>>> GetProfileKeeps(string id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                List<Keep> keeps = _keepsService.GetProfileKeeps(id);
                return Ok(keeps);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        
        public async Task<ActionResult<List<Vault>>> GetProfileVaults(string id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                List<Vault> vaults = _vaultsService.GetProfileVaults(id);
                return Ok(vaults);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}