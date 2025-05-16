using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Delivery
{

   required public DateTime date { get; set; }

   required public Customer customer { get; set; }

   required public Driver driver { get; set; }
    
   required public List<Product> products { get; set; }



}