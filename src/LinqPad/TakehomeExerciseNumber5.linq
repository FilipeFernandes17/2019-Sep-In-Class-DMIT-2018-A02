<Query Kind="Statements">
  <Connection>
    <ID>6ae542d8-1202-43e0-9f44-e577444a578b</ID>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

//5.Select all orders a picker has done on a particular week (Sunday through Saturday). Group and sorted by picker. Sort the orders by picked date. Hint: you will need to use the join operator.
//Explore to see what the last pickedDate is, and what day of week it is

DateTime start = new DateTime(2018,1,7)// Last date of a picked order, is sunday
				.AddDays(-14);		   // Go two weeks earlier
DateTime end = start.AddDays(7);
var diff = end - start;
diff.Dump("Time between two dates");
var result = from sale in Orders
			//we dont have a "between" operator in C#
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