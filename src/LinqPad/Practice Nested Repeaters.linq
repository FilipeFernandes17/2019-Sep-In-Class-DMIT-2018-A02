<Query Kind="Program">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>


/*Supplier:
	Company Name:
	Contact Name:
	Phone Number:
	Product Summary:
	Product Name:
	Category Name:
	Unit Price:
	Quantity/Unit:
	
	*/

void Main()
{
	var result = from company in Suppliers
	select new 
	{
		Company = company.CompanyName,
		Conatct = company.ContactName,
		Phone = company.Phone,
		Products = from item in company.Products
				   select new
				   {
				   		Name = item.ProductName,
						Category = item.Category.CategoryName,
						Price = item.UnitPrice,
						QtyPerUnit =item.QuantityPerUnit
				   }
	};
	result.Dump();	
}

	
// Define other methods and classes here

public class SupplierProduct 
{
	public string Name {get;set;}
	public string Category {get;set;}
	public string Price {get;set;}
	public string QtyPerUnit {get;set;}
}

public class SupplierSummary 
{
	public string Company {get;set;}
	public string Contact {get;set;}
	public string Phone {get;set;}
	public IEnumerable<SupplierProduct> Products {get;set;}
}
