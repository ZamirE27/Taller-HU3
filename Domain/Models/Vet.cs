using System.ComponentModel.DataAnnotations;

namespace Taller_HU3.Models;

public class Vet : Person
{
    [Key]
    public int Id { get; set; }
    public string Speciality { get; set; }
    
    public ICollection<MedicalAt>  MedicalAts { get; set; }
    
    public Vet(string name, string lastName, string phone, string speciality) : base(name, lastName, phone)
    {
        Speciality = speciality;
    }
}