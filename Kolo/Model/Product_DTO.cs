using System.ComponentModel.DataAnnotations;

namespace Kolo.Model;

public class Product
{
    public string name { get; set; }
    public double Price { get; set; }

    public int amount{ get; set; }


}