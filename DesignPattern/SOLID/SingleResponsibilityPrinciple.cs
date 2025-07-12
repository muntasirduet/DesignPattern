namespace DesignPattern.SOLID
{
    public class InvoiceCalculator { 
        public decimal CalculateTotal(List<decimal> itemPrices)
        {
            return itemPrices.Sum();
        }
    }
    public class InvoicePrinter
    {
        public void PrintInvoice(List<decimal> itemPrices)
        {
            Console.WriteLine("Invoice:");
            foreach (var price in itemPrices)
            {
                Console.WriteLine($"Item Price: {price:C}");
            }
            Console.WriteLine($"Total: {itemPrices.Sum():C}");
        }
    }

    public class EmailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Simulate sending an email
            Console.WriteLine($"Sending email to {recipient} with subject '{subject}'");
            Console.WriteLine($"Body: {body}");
        }
    }
    public class InvoiceManager
    {
        private readonly InvoiceCalculator _calculator;
        private readonly InvoicePrinter _printer;
        public InvoiceManager(InvoiceCalculator calculator, InvoicePrinter printer)
        {
            _calculator = calculator;
            _printer = printer;
        }
        public void ProcessInvoice(List<decimal> itemPrices)
        {
            var total = _calculator.CalculateTotal(itemPrices);
            _printer.PrintInvoice(itemPrices);
            Console.WriteLine($"Final Total: {total:C}");
        }
    }
    public class SingleResponsibilityPrinciple
    {
        InvoiceManager invoiceManager = new InvoiceManager(new InvoiceCalculator(), new InvoicePrinter());
        public void Run()
        {
            List<decimal> itemPrices = new List<decimal> { 19.99m, 5.49m, 3.99m };
            invoiceManager.ProcessInvoice(itemPrices);
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail("Muntasir","Invoice", "Your invoice has been processed successfully.");
        }

    }
}
