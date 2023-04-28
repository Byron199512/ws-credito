using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/seed")]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SeedController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<string>> ExecuteSeed()
        {
            StreamReader r = new StreamReader("./Seed/seed.json");
            string jsonString = r.ReadToEnd();

            SeedDTO seed = JsonConvert.DeserializeObject<SeedDTO>(jsonString);

            List<Status> status = mapper.Map<List<Status>>(seed.Status);
            List<RequestType> requestTypes = mapper.Map<List<RequestType>>(seed.RequestTypes);
            List<Rol> roles = mapper.Map<List<Rol>>(seed.Roles);

            context.AddRange(status);
            context.AddRange(requestTypes);
            context.AddRange(roles);

            await context.SaveChangesAsync();

            return "Executed Seed Data";
        }
    }
}
