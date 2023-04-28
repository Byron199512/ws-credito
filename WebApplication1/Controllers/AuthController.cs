using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Authentication;
using WebApplication1.Controllers.Entities;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly JsonWebToken jsonWebToken;
        private readonly CurrentUser currentUser;

        public AuthController(ApplicationDbContext context, IMapper mapper, JsonWebToken jsonWebToken, CurrentUser currentUser)
        {
            this.context = context;
            this.mapper = mapper;
            this.jsonWebToken = jsonWebToken;
            this.currentUser = currentUser;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseLoginDTO>> login([FromBody] LoginDTO loginDTO)
        {

            if (loginDTO == null)
                return BadRequest("Invalid Parameters");
            loginDTO.Password = new EncryptHelper().EncryptPassword(loginDTO.Password);
            var user = await context.Users.Include(user => user.Rol).FirstOrDefaultAsync(user => user.UserName == loginDTO.UserName && user.Password == loginDTO.Password);

            if (user == null)
                return NotFound("User or Email incorrect");

            var token = jsonWebToken.CreateToken(user);

            ResponseLoginDTO responseLoginDTO = new ResponseLoginDTO();
            responseLoginDTO.Token = token;
            responseLoginDTO.User = mapper.Map<UserResponseDTO>(user);

            return responseLoginDTO;
        }

        [HttpGet("revalidate")]
        [Authorize]
        public async Task<ActionResult<ResponseLoginDTO>> revalidateToken()
        {
            string idAdvisor = currentUser.GetIdUser();
           
            var user = await context.Users.Include(user => user.Rol).FirstOrDefaultAsync(user => user.Id == Int32.Parse(idAdvisor));

            var token = jsonWebToken.CreateToken(user);

            ResponseLoginDTO responseLoginDTO = new ResponseLoginDTO();
            responseLoginDTO.Token = token;
            responseLoginDTO.User = mapper.Map<UserResponseDTO>(user);

            return responseLoginDTO;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ResponseLoginDTO>> register([FromBody] UserDTO userDTO)
        {
            try
            {
                var userEmail = await context.Users.Include(user => user.Rol).FirstOrDefaultAsync(user => user.Email.ToLower().Contains(userDTO.Email.ToLower()));

                if (userEmail != null)
                    return BadRequest("Ya existe un usuario con ese email!");


                if (userDTO.RolId == 0)
                {
                    int? rolId = context.Roles.FirstOrDefault(rol => rol.Name == "asesor")?.Id;
                    if (rolId == null) return BadRequest("No existen roles para asignar");
                    userDTO.RolId = rolId ?? default(int);
                }


                if (userDTO.UserName == null)
                {
                    userDTO.UserName = userDTO.Email.Split("@")[0];
                }

                userDTO.Password = new EncryptHelper().EncryptPassword(userDTO.Password);

                var user = mapper.Map<User>(userDTO);
                context.Add(user);
                await context.SaveChangesAsync();
                user = await context.Users.Include(user => user.Rol).FirstOrDefaultAsync(u => u.Id == user.Id);

                var token = jsonWebToken.CreateToken(user);

                ResponseLoginDTO responseLoginDTO = new ResponseLoginDTO();
                responseLoginDTO.Token = token;
                responseLoginDTO.User = mapper.Map<UserResponseDTO>(user);

                return responseLoginDTO;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }





    }
}
