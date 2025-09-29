using System.ComponentModel.DataAnnotations;

namespace Taller_HU3.Models;

public class Client : Person
{
    [Key]
    public int Id { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    
    public ICollection<MedicalAt> MedicalAts { get; set; }
    public ICollection<Pet> Pets { get; set; }
    public Client(string name, string lastName, string phone, string email, string address) : base(name, lastName, phone)
    {
        Address = address;
        Email = email;
    }
}
