using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CargaClic.API.Controllers.Despacho
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {

     
        private readonly IConfiguration _config;
     
        public OrdenController(IConfiguration config
         ) {
            _config = config;
            
        }
    

    }
}