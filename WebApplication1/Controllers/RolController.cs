using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/rol")]
    public class RolController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RolController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<RolDTO>> createRole([FromBody] RolDTO rolDTO)
        {
            try
            {
                var rol = mapper.Map<Rol>(rolDTO);
                context.Add(rol);
                await context.SaveChangesAsync();
                return mapper.Map<RolDTO>(rol);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

    }
}
