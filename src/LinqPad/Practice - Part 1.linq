<Query Kind="Expression">
  <Connection>
    <ID>ae9a8623-ece9-458e-a491-37bb62b62c31</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// Practice questions - do each one in a separate LinqPad query.


//A) List all the customer company names for those with more than 5 orders.
from company in Customers
where company.Orders.Count() > 5
select company.CompanyName

//B) Get a list of all the region names
from place in Regions
select place.RegionDescription

//C) Get a list of all the territory names
from place in Territories
select place.TerritoryDescription

//D) List all the regions and the number of territories in each region
from place in Regions
select new 
{
	Region = place.RegionDescription,
	TerritoryCount = place.Territories.Count()
}


//E) List all the region and territory names in a "flat" list
from place in Territories 
//where place.Region.RegionDescription.Contains("stern")
select new
{
	Territory = place.TerritoryDescription, 
	Region = place.Region.RegionDescription
}


//F) List all the region and territory names as an "object graph"
   //- use a nested query
from place in Regions
select new 
{
	Region = place.RegionDescription,
	Territories = from area in place.Territories
				  select area.TerritoryDescription
}
   
   
   
//) List all the product names that contain the word "chef" in the name.



//H) List all the discontinued products, specifying the product name and unit price.



//I) List the company names of all Suppliers in North America (Canada, USA, Mexico)

