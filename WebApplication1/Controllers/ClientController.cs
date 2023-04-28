using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebApplication1.Controllers.Entities;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ClientController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> CreateClient([FromBody] ClientDocumentDTO clientDTO)
        {

            try
            {
                if (!String.IsNullOrEmpty(context.Documents.FirstOrDefault(document => document.Id == clientDTO.Document.Id)?.Id))
                {
                    var clientExist = await context.Clients.Include(client => client.Document).FirstOrDefaultAsync(client => client.Document.Id == clientDTO.Document.Id);
                    if (clientExist == null) return NotFound("El usuario no se encuentra registrado");
                    return mapper.Map<ClientDTO>(clientExist);
                }
                 

                var document = mapper.Map<DocumentClient>(clientDTO.Document);
               

                clientDTO.Client.FullName = String.Concat(clientDTO.Client.FirstName, " ", clientDTO.Client.LastName);
                clientDTO.Client.DocumentId = clientDTO.Document.Id;

                var client = mapper.Map<Client>(clientDTO.Client);

                context.Add(document);
                context.Add(client);

                await context.SaveChangesAsync();

                return Ok(mapper.Map<ClientDTO>(client));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }

        [HttpGet("{documentId}")]
        public async Task<ActionResult<ClientDTO>> GetClientByDocument([FromRoute] string documentId)
        {
            var client = await context.Clients.Include(client=> client.Document).FirstOrDefaultAsync(client => client.Document.Id == documentId);
            if (client == null) return NotFound("El usuario no se encuentra registrado");
            return mapper.Map<ClientDTO>(client);

        }


        


    }
}
