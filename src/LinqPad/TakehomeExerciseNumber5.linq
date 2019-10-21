<Query Kind="Statements">
  <Connection>
    <ID>6ae542d8-1202-43e0-9f44-e577444a578b</ID>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

DateTime start = new DateTime(2017,12,17).AddDays(-14);
DateTime end = start.AddDays(7);
var result = from sale in Orders
			 where sale.OrderDate >= start && sale.OrderDate < end
			 orderby sale.PickedDate
			 group sale by sale.PickerID into pickedOrders
			 select new 
			 {
			 	Picker = //pickedOrders.Key, 
						Pickers.Single(x => x.PickerID == pickedOrders.Key),
				Orders = from picked in pickedOrders
						 select new 
						 {
						 	Order = picked.OrderID,
							Items = from item in picked.OrderLists
									select new 
									{
										item = item.Product.Description,
										Ordered = item.QtyOrdered, 
										Picked = item.QtyPicked
									}
						 }
			 };
result.Dump("result");
