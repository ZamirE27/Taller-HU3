using Taller_HU3.Data;
using Taller_HU3.Services;

namespace Taller_HU3.Menus;

public class IMenu
{
    public readonly AppDbContext _context;

    public IMenu(AppDbContext context)
    {
        _context = context;
    }
    public void MainMenu()
    {
        while (true)
        {
            var context = new AppDbContext();
            var clientServices = new clientService(context);
            var petServices = new petServices(context);
            Console.Write("==============================\n");
            Console.Write("==         Main Menu        ==\n");
            Console.Write("==============================\n");
            
            Console.WriteLine("1): Clients Management");
            Console.WriteLine("2): Pets Management");
            Console.WriteLine("3): Vets Management");
            Console.WriteLine("4): Medical Appoiment Management");
            Console.WriteLine("5): Exit");
            string option = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrEmpty(option))
            {
                switch (option)
                {
                    case "1":
                        clientServices.ShowMenuAsync();
                        break;
                    case "2":
                        petServices.ShowMenuAsync();
                        break;
                }
            }
            
        }
    }
}