using System.Security.Cryptography;
using System.Text;

namespace FuerzaBrutaDefinitivo;


public class Program
{
    
    private static ManualResetEvent _doneEvent = new ManualResetEvent(false); // El problema que me daba era que el programa terminaba antes que los hilos.
    private static string _foundPassword = "Not found.";
    
    public static void Main()
    {
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        List<string> lines = File.ReadLines($"{projectDirectory}/10000-passwords.txt").ToList();
        List<string> listOfPasswords = lines.ToList();
        
        // Elegimos la password aleatoriamente:
        string chosenPassword = listOfPasswords[RandomNumberGenerator.GetInt32(0, listOfPasswords.Count)];
        Console.WriteLine($"Password seleccionada: {chosenPassword}.");
        
        
        // Hasheamos la password:
        string hashedPassword = HashPassword256(chosenPassword);
        Console.WriteLine($"Password hasheada: {hashedPassword}.");


        // Usamos una pool de hilos:
        ThreadPool.QueueUserWorkItem(_ =>
        {

            _foundPassword = DeHashPassword256();

            _doneEvent.Set();
        });
        
        _doneEvent.WaitOne();
        Console.WriteLine($"Found: {_foundPassword}.");
        
        
        
        String DeHashPassword256()
        {
            foreach (var password in listOfPasswords)
            {
                string hashedPassword2 = HashPassword256(password);
                if (hashedPassword2 == hashedPassword)
                {
                    return $"Password: {password}";

                }
            }
            return "";
        }

        // Funcioncita pa hashear la password:
        string HashPassword256(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
            
        }
    }


   
}