using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Customer
{
   required public string FirstName { get; set; }
   required public string LastName { get; set; }

   required public DateTime dateOfBirth{ get; set; }


}