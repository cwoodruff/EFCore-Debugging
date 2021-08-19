using System;
using System.Linq;
using Chinook;
using Microsoft.EntityFrameworkCore;

namespace efcoredebugging
{
    public static class Program
    {
        static void Main()
        {
            using var db = new ChinookContext();

            var artists = db.Artists
                .Include(e => e.Albums)
                .AsSplitQuery()
                .TagWith("Description: Query for Albums including Artists")
                .TagWith("Query located: efcoredebugging.Main() method")
                .TagWith(@"Parameters: None")
                .ToList();
            
            foreach (var artist in artists)
            {
                Console.WriteLine($"Found Artist: {artist.Name}");
            }
            
            Console.WriteLine("--------------------------------------------------------");
            
            var invoices = db.Invoices
                .Include(e => e.InvoiceLines)
                .AsSingleQuery()
                .TagWith("Description: Query for Invoices including InvoiceLines")
                .TagWith("Query located: efcoredebugging.Main() method")
                .TagWith(@"Parameters: None")
                .ToList();
            
            foreach (var invoice in invoices)
            {
                Console.WriteLine($"Found Invoice: {invoice.Id}");
            }
            
            Console.WriteLine("--------------------------------------------------------");
            
            var city = "London";

            var customers = db.Customers
                .FromSqlInterpolated($@"SELECT * FROM Customer WHERE City = {city}")
                .TagWith("Description: Query for Customers for selected City")
                .TagWith("Query located: efcoredebugging.Main() method")
                .TagWith(@"Parameters: City")
                .ToList();
            
            foreach (var customer in customers)
            {
                Console.WriteLine($"Found Customer: {customer.FirstName} {customer.LastName}");
            }
        }
    }
}