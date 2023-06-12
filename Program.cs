using System;
using System.Collections.Generic;
using System.Text;
namespace VuelingInvoiceManagerCrud_Test
{
    internal class Program
    {
        private static readonly WCFService.Service1Client WCFServer = new WCFService.Service1Client();

        private static readonly WCFService.Invoice newInvoice = new WCFService.Invoice
        {
            Date = DateTime.Now,
            OrderNumber = "123456",
            ExpiryDate = DateTime.Now.AddMonths(1),
            SubtotalPrice = 1000.00m,
            Discount = 0.05m,
            TaxPercentage = 0.1m,
            TotalPrice = 950.00m, // Assuming total price is after discount and before tax
            EntityFrom = new WCFService.Entity
            {
                Name = "Entity1",
                Address = "123 Street",
                PhoneNumber = "1234567890",
                Email = "entity1@example.com"
            },
            EntityTo = new WCFService.Entity
            {
                Name = "Entity2",
                Address = "456 Street",
                PhoneNumber = "0987654321",
                Email = "entity2@example.com"
            },
            Products = new List<WCFService.Product>
            {
                new WCFService.Product { Description = "Product1" },
                new WCFService.Product { Description = "Product2" }
            }
        };

        static void Main(string[] args)
        {

            int addedInvoiceID = AddInvoiceTest(newInvoice);

            Console.WriteLine("Added invoice:");

            List<WCFService.Invoice>invoices = GetAllInvoicesTest();
            foreach (WCFService.Invoice invoice in invoices)
            {
                Console.WriteLine(ToString(invoice));
            }

            bool result = RemoveInvoiceTest(addedInvoiceID);

            Console.WriteLine("Deleted invoice " + addedInvoiceID + ":");

            invoices = GetAllInvoicesTest();
            foreach (WCFService.Invoice invoice in invoices)
            {
                Console.WriteLine(ToString(invoice));
            }
            Console.ReadLine();
        }

        public static string ToString(WCFService.Invoice invoice)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n\n\nInvoice:");
            sb.AppendLine($" >ID: {invoice.ID}");
            sb.AppendLine($" >Date: {invoice.Date}");
            sb.AppendLine($" >OrderNumber: {invoice.OrderNumber}");
            sb.AppendLine($" >ExpiryDate: {invoice.ExpiryDate}");
            sb.AppendLine($" >SubtotalPrice: {invoice.SubtotalPrice}");
            sb.AppendLine($" >Discount: {invoice.Discount}");
            sb.AppendLine($" >TaxPercentage: {invoice.TaxPercentage}");
            sb.AppendLine($" >TotalPrice: {invoice.TotalPrice}");

            sb.AppendLine("\nEntityFrom:");
            sb.AppendLine(" >" + invoice.EntityFrom.ID.ToString());
            sb.AppendLine(" >" + invoice.EntityFrom.Name.ToString());
            sb.AppendLine(" >" + invoice.EntityFrom.Address.ToString());
            sb.AppendLine(" >" + invoice.EntityFrom.Email.ToString());

            sb.AppendLine("\nEntityTo:");
            sb.AppendLine(" >" + invoice.EntityTo.ID.ToString());
            sb.AppendLine(" >" + invoice.EntityTo.Name.ToString());
            sb.AppendLine(" >" + invoice.EntityTo.Address.ToString());
            sb.AppendLine(" >" + invoice.EntityTo.Email.ToString());

            sb.AppendLine("\nProducts:");
            foreach (var product in invoice.Products)
            {
                sb.AppendLine(" >" + product.ID.ToString());
                sb.AppendLine(" >" + product.Description.ToString());
            }

            return sb.ToString();
        }

        private static List<WCFService.Invoice> GetAllInvoicesTest()
        {
            return WCFServer.GetAllInvoices();
        }

        private static int AddInvoiceTest(WCFService.Invoice newInvoice)
        {
            return WCFServer.AddInvoice(newInvoice);
        }

        private static bool RemoveInvoiceTest(int addedInvoiceID)
        {
            return WCFServer.RemoveInvoice(addedInvoiceID);
        }
    }
}
