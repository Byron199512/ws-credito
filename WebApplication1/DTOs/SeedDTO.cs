namespace WebApplication1.DTOs
{
    public class SeedDTO
    {
        public List<StatusDTO> Status { get; set; }
        public List<RequestTypeDTO> RequestTypes { get; set; }

        public List<RolDTO> Roles { get; set; }

        public List<UserDTO> Adviser { get; set; }

        public int idRol { get; set; }

    }
}
