using System.Security.Cryptography;
using System.Text;

namespace APIPokemon
{
    public class CriptoPassword
    {
        private string Criptography(MD5 md5hash, string password)
        {
            byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public string GetHashPassword(string password)
        {
            using (MD5 md5hash = MD5.Create())
            {
                return Criptography(md5hash, password);
            }
        }

        //private bool VerifyHashPassword(MD5 md5hash, string password, string hash)
        //{
        //    string hashOfInput = Criptography(md5hash, password);
        //    StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        //    return comparer.Compare(hashOfInput, hash) == 0;
        //}
    }
}
