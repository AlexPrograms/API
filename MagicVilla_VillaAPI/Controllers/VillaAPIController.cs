using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
        return new List<VillaDTO>
        {
            new VillaDTO{Id=1, Name = "PoolView"}, 
            new VillaDTO{Id=2, Name = "BeachView"}
        };
    }
}