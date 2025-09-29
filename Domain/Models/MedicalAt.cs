using System.ComponentModel.DataAnnotations;

namespace Taller_HU3.Models;

public class MedicalAt
{
    [Key]
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    public int  PetId { get; set; }
    public int VetId { get; set; }
    public DateTime Date { get; set; }
    
    public Client Client { get; set; }
    public Pet Pet { get; set; }
    public Vet Vet { get; set; }
}

