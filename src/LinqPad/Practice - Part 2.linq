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

// A) Group employees by region and show the results in this format:
/* ----------------------------------------------
 * | REGION        | EMPLOYEES                  |
 * ----------------------------------------------
 * | Eastern       | ------------------------   |
 * |               | | Nancy Devalio        |   |
 * |               | | Fred Flintstone      |   |
 * |               | | Bill Murray          |   |
 * |               | ------------------------   |
 * |---------------|----------------------------|
 * | ...           |                            |
 * 
 */
 
 
 from place in Regions
select new
{
    Region = place.RegionDescription,
	Employees = (from area in place.Territories
	            from managedTerritory in area.EmployeeTerritories
				select managedTerritory.Employee.FirstName + " " + managedTerritory.Employee.LastName).Distinct()
}

// B) List all the Customers sorted by Company Name. Include the Customer's company name, contact name, and other contact information in the result.
from vendor in Customers
orderby vendor.CompanyName
select new 
{
	CompanyName = vendor.ContactName,
	Contact = new 
	{
		Title = vendor.ContactTitle,
		Email = vendor.ContactEmail,
		Phone = vendor.Phone,
		Fax = vendor.Fax
	}
}



// C) List all the employees and sort the result in ascending order by last name, then first name. Show the employee's first and last name separately, along with the number of customer orders they have worked on.
from person in Employees
orderby person.LastName, person.FirstName
select new
{
	person.FirstName,
	person.LastName,
	OrderCount = person.SalesRepOrders.Count()
}


// D) List all the employees and sort the result in ascending order by last name, then first name. Show the employee's first and last name separately, along with the number of customer orders they have worked on.
from person in Employees
orderby person.LastName, person.FirstName
select new
{
	person.FirstName,
	person.LastName,
	OrderCount = person.SalesRepOrders.Count()
}



// E) Group all customers by city. Output the city name, aalong with the company name, contact name and title, and the phone number.
from buyer in Customers
group buyer by buyer.Address.City into cityVendors
select new 
{
	City = cityVendors.Key,
	Company = from company in cityVendors
			  select new 
			  {
			  	company.CompanyName,
				company.ContactName,
				company.ContactTitle,
				company.Phone
			  }
}

// F) List all the Suppliers, by Country
from vendor in Suppliers
group vendor by vendor.Address.Country