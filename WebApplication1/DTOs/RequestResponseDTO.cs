using Azure.Core;

namespace WebApplication1.DTOs
{
    public class RequestResponseDTO: RequestDTO
    {
        public ClientDTO Client { get; set; }
        public StatusDTO Status { get; set; }
        public RequestTypeDTO RequestType { get; set; }

       
    }
}
