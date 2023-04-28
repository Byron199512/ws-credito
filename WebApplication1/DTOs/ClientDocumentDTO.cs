using WebApplication1.Entities;

namespace WebApplication1.DTOs
{
    public class ClientDocumentDTO
    {
        public ClientDTO Client { get; set; }
        public DocumentDTO   Document { get; set; }
    }
}
