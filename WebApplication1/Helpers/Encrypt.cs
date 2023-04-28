using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Helpers
{
    public class EncryptHelper
    {
        public string EncryptPassword(string password)
        {

            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            Byte[] hashedBytes = SHA256.HashData(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        
    }
}
