<Query Kind="Expression">
  <Connection>
    <ID>bce7373a-cb15-4435-9906-9e5a8aa77ecd</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//show only the orders for april, 2018
from sale in Orders
where sale.OrderDate.Value.Month == 4
    && sale.OrderDate.Value.Year == 2018
select new 
	{
		Compamy = sale.Customer.CompanyName,
        DateOrdered = sale.OrderDate,
        TimetToDelivery = sale.RequiredDate - sale.OrderDate
	}