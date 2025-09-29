namespace Taller_HU3.Models;

public class Person
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }


    public Person(string name, string lastName, string phone)
    {
        Name = name;
        LastName = lastName;
        Phone = phone;
    }
    
}
