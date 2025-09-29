using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Taller_HU3.Data;
using Taller_HU3.Domain.Interfaces;
using Taller_HU3.Models;

namespace Taller_HU3.Services;

public class clientService : IRepository<Client>
{
    public readonly AppDbContext _context;

    public clientService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Client> InsertAsync(Client client)
    {
        try
        {
            
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting client: {ex.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        bool showed = false;
        await _context.Clients.ToListAsync();
        foreach (var c in _context.Clients)
        {
            if (!showed)
            {
                Console.WriteLine("Clients found.");
                Console.WriteLine(
                    "{0,-15} {1,-15} {2,-25} {3,-15} {4,-20}",
                    "Name", "Last name", "Email", "Phone",  "Address"
                );
                showed = true;
            }
            Console.WriteLine(
                "{0,-15} {1,-15} {2,-25} {3,-15} {4,-20}",
                c.Name, c.LastName, c.Email, c.Phone, c.Address
            );
        }
        return _context.Clients.ToList();
         
         
    }

    public async Task<Client> GetOneAsync(Client client)
    {
        
        if (client == null)
        {
            return null;
        }
        Console.WriteLine(
            "{0,-15} {1,-15} {2,-25} {3,-15} {4,-20}",
            "Name", "Last name", "Email", "Phone",  "Address");
        Console.WriteLine(
            "{0,-15} {1,-15} {2,-25} {3,-15} {4,-20}",
            client.Name, client.LastName, client.Email, client.Phone, client.Address);
        return client;
    }

    public async Task<Client> UpdateAsync(Client clientToUpdate)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == clientToUpdate.Name && c.LastName == clientToUpdate.LastName);
        if (client == null)
        {
            return null;
        }
         client.Name = clientToUpdate.Name;
         client.LastName = clientToUpdate.LastName;
         client.Email = clientToUpdate.Email;
         client.Phone = clientToUpdate.Phone;
         client.Address = clientToUpdate.Address;
        
         await _context.SaveChangesAsync();
         return client;
    }

    public async Task<Client> DeleteAsync(Client  client)
    {
        
        if (client == null)
        {
            return null;
        }
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return client;
    }

    private async Task RegisterClientAsync()
    {
       try
       {
             while (true)
             {
                 string name, last_name, email, phone_number, address, response;
                 while (true)
                 {
                     Console.Write("Enter the client Name: ");
                     name = Console.ReadLine()?.Trim().ToLower();
                     if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                     {
                         break;
                     }

                     Console.WriteLine("Invalid client name, please try again");
                 }

                 while (true)
                 {
                     Console.Write("Enter the Client last name: ");
                     last_name = Console.ReadLine()?.Trim().ToLower();
                     if (last_name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(last_name))
                     {
                         break;
                     }

                     Console.WriteLine("Invalid client last name, please try again");
                 }

                 while (true)
                 {

                     Console.Write("Enter the Client's email: ");
                     email = Console.ReadLine()?.Trim().ToLower();
                     try
                     {
                         var mail = new MailAddress(email);
                         break;

                     }
                     catch
                     {
                         Console.WriteLine("Invalid email address, please try again");
                     }
                 }

                 while (true)
                 {
                     Console.Write("Enter the Client's Phone number: ");
                     phone_number = Console.ReadLine()?.Trim().ToLower();
                     if (phone_number.All(char.IsDigit) && !String.IsNullOrWhiteSpace(phone_number))
                     {
                         break;
                     }

                     Console.WriteLine("Invalid client phone number, please try again");
                 }

                 while (true)
                 {
                     Console.Write("Enter the Client's address: ");
                     address = Console.ReadLine()?.Trim().ToLower();
                     if (address.All(char.IsLetter) && !String.IsNullOrWhiteSpace(address))
                     {
                         break;
                     }

                     Console.WriteLine("Invalid client last name, please try again");
                 }
                 Console.WriteLine("Hasta aqui todo bien");
                 var client = new Client(name, last_name, phone_number, email, address);
                 await InsertAsync(client);
                 if (client != null)
                 {
                     Console.WriteLine("User has been successfully created");
                 }
                 else
                 {
                     Console.WriteLine("User has not been created");
                 }

                 while (true)
                 {
                     Console.WriteLine("Do you want to add another one? (y/n)");
                     response = Console.ReadLine().Trim().ToLower();

                     if (response == "y" || response == "yes" && response.Any(char.IsLetter) &&
                         !String.IsNullOrWhiteSpace(response))
                     {
                         break;
                     } else if (response == "n" || response == "no" && response.Any(char.IsLetter) &&
                               !string.IsNullOrWhiteSpace(response))
                     {
                         break;
                     }
                     else
                     {
                         Console.WriteLine("Incorrect answer, try again");
                     }
                 }

                 if (response == "y" || response == "yes")
                 {
                     continue;
                 }
                 else
                 {
                     break;
                 }
             }
       }catch (Exception ex)
       {
             Console.WriteLine(ex);
       }
    }

    private async Task getClientAsync()
    {
        try
        {
            while (true)
            {
                string name, lastName, response;
                while (true)
                {
                    Console.Write("Enter the client name: ");
                    name = Console.ReadLine()?.Trim().ToLower();
                    if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    Console.WriteLine("Invalid client name, please try again");
                }

                while (true)
                {
                    Console.Write("Enter the Client last name: ");
                    lastName = Console.ReadLine()?.Trim().ToLower();

                    if (lastName.All(char.IsLetter) && !String.IsNullOrWhiteSpace(lastName))
                    {
                        break;
                    }

                    Console.WriteLine("Invalid client last name, please try again");
                }
                
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == name && c.LastName == lastName);
                if (client != null)
                {
                    
                    await GetOneAsync(client);
                }
                else
                {
                    Console.WriteLine("Client not found");
                }
                
                while (true)
                {
                    Console.WriteLine("Do you want to find another one? (y/n)");
                    response = Console.ReadLine().Trim().ToLower();

                    if (response == "y" || response == "yes" && response.Any(char.IsLetter) &&
                        !String.IsNullOrWhiteSpace(response))
                    {
                        break;
                    } else if (response == "n" || response == "no" && response.Any(char.IsLetter) &&
                               !string.IsNullOrWhiteSpace(response))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect answer, try again");
                    }
                }

                if (response == "y" || response == "yes")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    private async Task UpdateMenuAsync()
    {
        while (true)
        {
            string name, last_name ,response;
            while (true)
            {
                Console.Write("Enter the client's name you want to edit: ");
                 name = Console.ReadLine()?.Trim().ToLower();
                if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Console.WriteLine("Invalid client name, please try again");
            }

            while (true)
            {
                Console.Write("Enter the Client last name you want to edit: ");
                 last_name = Console.ReadLine()?.Trim().ToLower();
                if (last_name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(last_name))
                {
                    break;
                }
                Console.WriteLine("Invalid client last name, please try again");
            }

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == name && c.LastName == last_name);
            await GetOneAsync(client);
            if (client == null)
            {
                Console.WriteLine("Client not found");
                return;
            }
            
            Console.WriteLine("What do you want to edit?");
            Console.WriteLine("1): Name");
            Console.WriteLine("2): Last name");
            Console.WriteLine("3): Email");
            Console.WriteLine("4): Phone Number");
            Console.WriteLine("5): Address");
            Console.WriteLine("6): Exit");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    while (true)
                    {
                        Console.Write("Enter the client name: ");
                        client.Name = Console.ReadLine()?.Trim().ToLower();
                        if (client.Name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(client.Name))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid client name, please try again");
                    }
                    break;
                case "2":
                    while (true)
                    {
                        Console.Write("Enter the Client last name: ");
                        client.LastName = Console.ReadLine()?.Trim().ToLower();
                        if (client.LastName.All(char.IsLetter) && !String.IsNullOrWhiteSpace(client.LastName))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid client last name, please try again");
                    }
                    break;
                case "3":
                    while (true)
                    {
                        Console.Write("Enter the Client Email: ");
                        client.Email = Console.ReadLine()?.Trim().ToLower();
                        if (client.Email.All(char.IsLetter) && !String.IsNullOrWhiteSpace(client.Email))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid client email, please try again");
                    }
                    break;
                case "4":
                    while (true)
                    {
                        Console.Write("Enter the Client Phone Number: ");
                        client.Phone =  Console.ReadLine()?.Trim().ToLower();
                        if (client.Phone.All(char.IsDigit) && !String.IsNullOrWhiteSpace(client.Phone))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid client phone number, please try again");
                    }
                    break;
                case "5":
                    while (true)
                    {
                        Console.Write("Enter the Client Address: ");
                        client.Address = Console.ReadLine()?.Trim().ToLower();
                        if (client.Address.All(char.IsLetter) && !String.IsNullOrWhiteSpace(client.Address))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid client last name, please try again");
                    }
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    continue;
            }
            await UpdateAsync(client);
            Console.WriteLine("Client has been updated");
            
            while (true)
            {
                Console.WriteLine("Do you want to edit another one? (y/n)");
                response = Console.ReadLine().Trim().ToLower();

                if (response == "y" || response == "yes" && response.Any(char.IsLetter) &&
                    !String.IsNullOrWhiteSpace(response))
                {
                    break;
                } else if (response == "n" || response == "no" && response.Any(char.IsLetter) &&
                           !string.IsNullOrWhiteSpace(response))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect answer, try again");
                }
            }

            if (response == "y" || response == "yes")
            {
                continue;
            }
            else
            {
                break;
            }
                
        }
    }

    private async Task DeleteCLAsync()
    {
        while (true)
        {
            string name, last_name, response;
            while (true)
            {
                Console.Write("Enter the client name you want to delete: ");
                name = Console.ReadLine()?.Trim().ToLower();
                if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Console.WriteLine("Invalid client name, please try again");
            }

            while (true)
            {
                Console.Write("Enter the Client last name you want to delete: ");
                last_name = Console.ReadLine()?.Trim().ToLower();
                if (last_name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(last_name))
                {
                    break;
                }
                Console.WriteLine("Invalid client last name, please try again");
            }
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == name && c.LastName == last_name);
            await DeleteAsync(client);
            if (client != null)
            {
                Console.WriteLine("User has been successfully deleted");
                while (true)
                {
                    Console.WriteLine("Do you want to delete another one? (y/n)");
                    response = Console.ReadLine().Trim().ToLower();

                    if (response == "y" || response == "yes" && response.Any(char.IsLetter) &&
                        !String.IsNullOrWhiteSpace(response))
                    {
                        break;
                    } else if (response == "n" || response == "no" && response.Any(char.IsLetter) &&
                               !string.IsNullOrWhiteSpace(response))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect answer, try again");
                    }
                }

                if (response == "y" || response == "yes")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("User not found");
                Console.WriteLine("Please try again");
                continue;
            }

            
        }
    }
    public async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("================================ \n"); 
            Console.WriteLine("==    Client's Management     == \n"); 
            Console.WriteLine("================================ \n");
         
            Console.WriteLine("1): Register a client");
            Console.WriteLine("2): Find a client");
            Console.WriteLine("3): Show all the clients");
            Console.WriteLine("4): Update a client");
            Console.WriteLine("5): Delete a client");
            Console.WriteLine("6): Exit");
         
            String option = Console.ReadLine().Trim().ToLower();

            switch (option)
            {
                case "1":
                    await RegisterClientAsync();   
                    break;
                case "2":
                    await getClientAsync();
                    break;
                case "3":
                    await GetAllAsync();
                    break;
                case "4":
                    await UpdateMenuAsync();
                    break;
                case "5":
                    await DeleteCLAsync();
                    break;
                case "6":
                    return;
            }
        }
    }
     
    
}

