using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDTO> villaList = new List<VillaDTO>
    {
        new VillaDTO{Id=1, Name = "PoolView", Occupancy = 1, Sqft = 5}, 
        new VillaDTO{Id=2, Name = "BeachView", Occupancy = 4, Sqft = 80},
        new VillaDTO{Id=3, Name = "Blala", Occupancy = 5, Sqft = 50},
        new VillaDTO{Id=4, Name = "Shit", Occupancy = 0, Sqft = 1}
        
    };
}