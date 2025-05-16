using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Product
{
    required public string name { get; set; }

    public double Price { get; set; }

    required public int amount{ get; set; }


}