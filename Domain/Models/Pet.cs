using System.ComponentModel.DataAnnotations;

namespace Taller_HU3.Models;

public class Pet
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specie { get; set; }
    
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public ICollection<MedicalAt>  MedicalAts { get; set; }

    public Pet(string name, string specie, int clientId)
    {
        Name = name;
        Specie = specie;
        ClientId = clientId;
    }
}