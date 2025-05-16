using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Delivery
{

    public DateTime date { get; set; }

    public Customer customer { get; set; }

    public Driver driver { get; set; }
    
    public List<Product> products { get; set; }



}