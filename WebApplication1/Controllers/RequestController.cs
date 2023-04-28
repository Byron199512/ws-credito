using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Authentication;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly CurrentUser currentUser;

        public RequestController(ApplicationDbContext context, IMapper mapper, CurrentUser currentUser)
        {
            this.context = context;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }

        [HttpPost("type")]
        //[Authorize(Roles = (RolEnum.admin))]
        public async Task<ActionResult<RequestTypeDTO>> createRequestType([FromBody] RequestTypeDTO typeDTO)
        {
            try
            {
                var requestType = mapper.Map<RequestType>(typeDTO);
                context.Add(requestType);
                await context.SaveChangesAsync();
                return mapper.Map<RequestTypeDTO>(requestType);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpPost("status")]
        [Authorize(Roles = (RolEnum.admin))]
        public async Task<ActionResult<StatusDTO>> createStatus([FromBody] StatusDTO statusDTO)
        {
            try
            {
                var status = mapper.Map<Status>(statusDTO);
                context.Add(status);
                await context.SaveChangesAsync();
                return mapper.Map<StatusDTO>(status);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RequestDTO>> createRequest([FromBody] RequestDTO requestDTO)
        {
            try
            {

                //asign advisor
                var clientExist = await context.Clients.Include(client => client.Document).FirstOrDefaultAsync(client => client.Document.Id == requestDTO.documentId);
                requestDTO.ClientId = clientExist.Id;
                //obtener los asesores que no estan asignados a ninguna solicitud
                var adviserIds = context.Requests.Select(s => s.AdvisorId).Distinct().ToArray();
                var advisers = context.Users.Where(user => !adviserIds.Contains(user.Id) && user.Rol.Name == "asesor").FirstOrDefault();

                if (advisers == null)
                {
                    //obtener los asesores que tienen menos solicitudes
                    var minAdvisor = context.Requests.GroupBy(g => g.Advisor.Id).Select(s => new
                    {
                        count = s.Count(),
                        Id = s.Min(x => x.Advisor.Id)
                    }).OrderBy(c => c.count).FirstOrDefault();

                    requestDTO.AdvisorId = minAdvisor.Id;
                }
                else
                {
                    requestDTO.AdvisorId = advisers.Id;
                }

                requestDTO.StatusId = context.Status.Where(status => status.Name == "new").Select(s => s.Id).FirstOrDefault();

                var request = mapper.Map<Request>(requestDTO);
                context.Add(request);
                await context.SaveChangesAsync();



                return mapper.Map<RequestDTO>(request);


            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = RolEnum.asesor)]
        public async Task<ActionResult<List<RequestResponseDTO>>> GetRequesByAdvisor()
        {
            string idAdvisor = currentUser.GetIdUser();
       

            var request = await context.Requests.Include(request => request.RequestType)
                .Include(request => request.Status)
                .Include(request => request.Client)
                .Where(request => request.Advisor.Id == Int32.Parse(idAdvisor)).ToListAsync();

            return mapper.Map<List<RequestResponseDTO>>(request);


        }

        [HttpGet("type")]
        public async Task<ActionResult<List<RequestTypeDTO>>> GetRequestTypes()
        {
            var request = await context.RequestTypes.ToListAsync();
            return mapper.Map<List<RequestTypeDTO>>(request);

        }

        [HttpGet("type/{id:int}")]
        public async Task<ActionResult<RequestTypeDTO>> GetRequestTypeById(int id)
        {
            var request = await context.RequestTypes.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<RequestTypeDTO>(request);

        }

        [HttpGet("status")]
        public async Task<ActionResult<List<StatusDTO>>> GetRequestStatus()
        {
            var request = await context.Status.ToListAsync();
            return mapper.Map<List<StatusDTO>>(request);

        }

        [HttpPut("{idRequest:int}")]
        [Authorize(Roles = RolEnum.asesor)]
        public async Task<ActionResult<RequestDTO>> UpdateRequestStatus([FromRoute] int idRequest, [FromQuery] int idStatus)
        {
            try
            {
                string idAdvisor = currentUser.GetIdUser();
                var request = context.Requests.FirstOrDefault(request => request.Advisor.Id == Int32.Parse(idAdvisor) && request.Id == idRequest);

                if (request == null) return BadRequest("La solicitud no existe para el usuario logeado");
                request.LastDate = DateTime.Now;
                request.StatusId = idStatus;

                context.Update(request);
                await context.SaveChangesAsync();

                return mapper.Map<RequestDTO>(request);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }





    }
}
