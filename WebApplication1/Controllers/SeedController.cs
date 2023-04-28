using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Controllers.Entities;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Helpers;

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

            int? rolId = context.Roles.FirstOrDefault(rol => rol.Name == "asesor")?.Id;
            if (rolId == null) return BadRequest("No existen roles para asignar");
            seed.idRol = rolId ?? default(int);

            await register(seed.Adviser, seed.idRol);

            return "Executed Seed Data";
        }


        public async Task register(List<UserDTO> userDTO, int rol)
        {
            try
            {
                foreach (var item in userDTO)
                {
                    item.UserName = item.Email.Split("@")[0];
                    item.RolId = rol;
                    item.Password = new EncryptHelper().EncryptPassword(item.Password);
                }
                List<User> adviser = mapper.Map<List<User>>(userDTO);
                context.AddRange(adviser);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

        }
    }

    }
