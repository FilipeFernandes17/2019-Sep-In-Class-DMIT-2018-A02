<Query Kind="Program">
  <Connection>
    <ID>bce7373a-cb15-4435-9906-9e5a8aa77ecd</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
    //act like im the presentation layer calling this...
    var output = ListShippers();
    output.Dump();
    
    
}

// Define other methods and classes here
    public List<ShipperSelection> ListShippers()
    {
        //throw new NotImplementedException();
        //TODO: Queries for all the Shippers.
        var result = from shipper in Shippers
                     select new ShipperSelection
                     {
                        ShipperId = shipper.ShipperID,
                        Shipper = shipper.CompanyName
                     };
        return result.ToList();
            
    }
        
public class ShipperSelection
{
    public int ShipperId { get; set; }
    public string Shipper { get; set; }
}