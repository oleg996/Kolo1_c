using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Delivery_add
{   
    
   required public int deliveryId { get; set; }

   required public int customerId { get; set; }

    [Length(16,16)]
   required public string licenceNumber { get; set; }
  required  public List<Product> products { get; set; }

}
