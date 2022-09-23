using System.Text.Json.Serialization;

namespace RpmFsServices.Models;

public class FuelPrice
{
   public string PriceDate { get; set; }
   
   public decimal Price { get; set; }

   public DateTime Date { get; set; }
}