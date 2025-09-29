using Taller_HU3.Data;
using Taller_HU3.Services;

namespace Taller_HU3;

public class Program
{
    public static async Task Main(string[] args)
    {
        var context = new AppDbContext();
        var clientServices = new clientService(context);

        try
        {
            await clientServices.ShowMenuAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
