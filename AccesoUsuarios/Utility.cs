using System.Text;
using System.Security.Cryptography;

namespace AccesoUsuarios
{
    public static class Utility
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Puedes agregar más métodos útiles aquí
    }
}

