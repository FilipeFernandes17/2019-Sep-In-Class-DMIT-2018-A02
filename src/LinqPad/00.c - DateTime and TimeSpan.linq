<Query Kind="Statements">
  <Connection>
    <ID>bce7373a-cb15-4435-9906-9e5a8aa77ecd</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//midnight of today
DateTime whenever;
whenever = DateTime.Today;
whenever.Dump("DateTime.Today");

//time and date of right now
whenever = DateTime.Now;
whenever.Dump("DateTime.Now");

whenever.Month.Dump("Month value");
whenever.Day.Dump("Day value");
whenever.Year.Dump("Year value");

//can add or sub datetime
DateTime tomorrow = DateTime.Today.AddDays(1);
(tomorrow > whenever).Dump("Is tomorrow greater than now?");

//pass in year month and day
var lastDayOfYear = new DateTime(2019, 12, 31);
lastDayOfYear.Dump("The end of this year");

var diff = tomorrow - whenever;
diff.Dump("The difference between tomorrow and now");
diff.GetType().Name.Dump("The data type of diff");