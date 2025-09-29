using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Taller_HU3.Data;
using Taller_HU3.Domain.Interfaces;
using Taller_HU3.Models;

namespace Taller_HU3.Services;

public class petServices : IRepository<Pet> 
{
    public readonly AppDbContext _context;

    public petServices(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Pet> InsertAsync(Pet pet)
    {
        try
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return pet;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting pet: {ex.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        bool showed = false;
        await _context.Pets.ToListAsync();
        foreach (var p in _context.Pets)
        {
            if (!showed)
            {
                Console.WriteLine("Clients found.");
                Console.WriteLine(
                    "{0,-15} {1,-15} {2,-25}",
                    "Name", "Specie", "Owner"
                );
                showed = true;
            }
            Console.WriteLine(
                "{0,-15} {1,-15} {2,-25}",
                p.Name, p.Specie, p.ClientId
            );
        }
        return _context.Pets.ToList();
         
         
    }

    public async Task<Pet> GetOneAsync(Pet pet)
    {
        
        if (pet == null)
        {
            return null;
        }

        Console.WriteLine(
            "{0,-15} {1,-15} {2,-25}",
            "Name", "Specie", "Owner");
        Console.WriteLine(
            "{0,-15} {1,-15} {2,-25}",
            pet.Name, pet.Specie, pet.ClientId);
        return pet;
    }

    public async Task<Pet> UpdateAsync(Pet petToUpdate)
    {
        var pet = await _context.Pets.FirstOrDefaultAsync(p => p.ClientId == petToUpdate.ClientId);
        if (pet == null)
        {
            return null;
        }
         pet.Name = petToUpdate.Name;
         pet.Specie = petToUpdate.Specie;
         pet.ClientId = petToUpdate.ClientId;
         
         
         await _context.SaveChangesAsync();
         return pet;
    }

    public async Task<Pet> DeleteAsync(Pet  pet)
    {
        
        if (pet == null)
        {
            return null;
        }
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
        return pet;
    }
    
    private async Task RegisterPetAsync()
    {
       try
       {
             while (true)
             {
                 string name, specie, response;
                 int clientId;
                 while (true)
                 {
                     Console.Write("Enter the Pet's Name: ");
                     name = Console.ReadLine()?.Trim().ToLower();
                     if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                     {
                         break;
                     }

                     Console.WriteLine("Invalid pet name, please try again");
                 }

                 while (true)
                 {
                     Console.Write("Enter the Pet's specie: ");
                     specie = Console.ReadLine()?.Trim().ToLower();
                     if (specie.All(char.IsLetter) && !String.IsNullOrWhiteSpace(specie))
                     {
                         break;
                     }

                     Console.WriteLine("Invalid Pet specie, please try again");
                 }
                 
                 while (true)
                 {
                     Console.Write("Enter the Pet's owner name: ");
                     clientId = int.Parse(Console.ReadLine()?.Trim().ToLower());
                     if (clientId > 0 && clientId != null)
                     {
                         break;
                     }

                     Console.WriteLine("Invalid Pet's owner name, please try again");
                 }
                 var pet = new Pet(name, specie, clientId);
                 await InsertAsync(pet);
                 if (pet != null)
                 {
                     Console.WriteLine("Pet has been successfully created");
                 }
                 else
                 {
                     Console.WriteLine("Pet has not been created");
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

    private async Task getPetsAsync()
    {
        try
        {
            while (true)
            {
                string name, response;
                while (true)
                {
                    Console.Write("Enter the Pet's name: ");
                    name = Console.ReadLine()?.Trim().ToLower();
                    if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    Console.WriteLine("Invalid pet name, please try again");
                }
                
                
                var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Name == name);
                if (pet != null)
                {
                    await GetOneAsync(pet);
                }
                else
                {
                    Console.WriteLine("Pet not found");
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

    private async Task UpdatePetAsync()
    {
        while (true)
        {
            string name,option, response;
            int clientId;
            while (true)
            {
                Console.Write("Enter the Pet's name: ");
                name = Console.ReadLine()?.Trim().ToLower();
                if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Console.WriteLine("Invalid pet name, please try again");
            }
            var pt = await _context.Pets.FirstOrDefaultAsync(p => p.Name == name);
            if (pt == null)
            {
                Console.WriteLine("Client not found");
                return;
            }
            await GetOneAsync(pt);
            Console.WriteLine("What do you want to edit?");
            Console.WriteLine("1: Name");
            Console.WriteLine("2: Specie");
            Console.WriteLine("3: exit");
            option = Console.ReadLine()?.Trim().ToLower();
            switch (option)
            {
                case "1":
                    while (true)
                    {
                        Console.Write("Enter the new Pet's name: ");
                        pt.Name = Console.ReadLine()?.Trim().ToLower();
                        if (pt.Name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(pt.Name))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid pet name, please try again");
                    }
                    break;
                case "2":
                    while (true)
                    {
                        Console.Write("Enter the new Pet's specie: ");
                        pt.Specie = Console.ReadLine()?.Trim().ToLower();
                        if (pt.Specie.All(char.IsLetter) && !String.IsNullOrWhiteSpace(pt.Specie))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid pet specie, please try again");
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Incorrect answer, try again");
                    continue;
            }
            await UpdateAsync(pt);
            Console.WriteLine("Pet has been updated");
            
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

    private async Task DeletePtAsync()
    {
        while (true)
        {
            string name;
            Console.WriteLine("Enter the Pet's name: ");
            name = Console.ReadLine().Trim().ToLower();
            if (name.All(char.IsLetter) && !String.IsNullOrWhiteSpace(name))
            {
                break;
            }
        }
    }
    
    public async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("================================ \n"); 
            Console.WriteLine("==    Pet's Management     == \n"); 
            Console.WriteLine("================================ \n");
         
            Console.WriteLine("1): Register a Pet");
            Console.WriteLine("2): Find a Pet");
            Console.WriteLine("3): Show all the pet");
            Console.WriteLine("4): Update a Pet");
            Console.WriteLine("5): Delete a Pet");
            Console.WriteLine("6): Exit");
         
            String option = Console.ReadLine().Trim().ToLower();

            switch (option)
            {
                case "1":
                    await RegisterPetAsync();   
                    break;
                case "2":
                    await getPetsAsync();
                    break;
                case "3":
                    await GetAllAsync();
                    break;
                case "4":
                    await UpdatePetAsync();
                    break;
                // case "5":
                //     await DeleteCLAsync();
                case "6":
                    return;
                default:
                    Console.WriteLine("Incorrect answer, try again");
                    continue;
            }
        }
    }
}

