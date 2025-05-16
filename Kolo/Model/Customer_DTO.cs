using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime dateOfBirth{ get; set; }


}