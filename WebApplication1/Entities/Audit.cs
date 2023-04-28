using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Entities
{
    public class Audit
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public int CreateId { get; set; }= (Int32)DateTime.Now.ToOADate();
        public DateTime LastDate { get; set; } = DateTime.Now;
    }
}
