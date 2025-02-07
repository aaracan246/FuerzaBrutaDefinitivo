using System.Security.Cryptography;
using System.Text;

namespace FuerzaBrutaDefinitivo;


public class Program
{
    public static void Main()
    {
        List<string> lines = File.ReadLines("C:\\Users\\UsuarioT\\RiderProjects\\FuerzaBrutaDefinitivo\\FuerzaBrutaDefinitivo\\10000-passwords.txt").ToList();
        List<string> listOfPasswords = lines.ToList();
        
        // Elegimos la password aleatoriamente:
        string chosenPassword = listOfPasswords[RandomNumberGenerator.GetInt32(0, listOfPasswords.Count)];
        Console.WriteLine($"Password seleccionada: {chosenPassword}.");
        
        
        // Hasheamos la password:
        string hashedPassword = HashPassword256(chosenPassword);
        Console.WriteLine($"Password hasheada: {hashedPassword}.");


        foreach (var password in listOfPasswords)
        {
            string hashedPassword2 = HashPassword256(password);
            if (hashedPassword2 == hashedPassword)
            {
                Console.WriteLine($"Password revealed: {password}.");
            }
        }
        // Funcioncita pa hashear la password:
        string HashPassword256(string password)
        {
            using var sha256 = SHA256.Create();
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", string.Empty);
            } 
        }
    }


   
}