namespace DesignPattern.SOLID
{

    //A class should have only one reason to change.
    // This means that a class should only
    // have one responsibility or job. If a class has multiple responsibilities, it becomes harder to maintain and test.

    public class InvoiceCalculator { 
        public decimal CalculateTotal(List<decimal> itemPrices)
        {
            return itemPrices.Sum();
        }
    }
    // The InvoiceCalculator class is responsible for calculating the total of a list of item prices.
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
    // The InvoicePrinter class is responsible for printing the invoice details to the console.
    public class EmailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Simulate sending an email
            Console.WriteLine($"Sending email to {recipient} with subject '{subject}'");
            Console.WriteLine($"Body: {body}");
        }
    }
    // The EmailSender class is responsible for sending emails. It has a single method to send an email with a recipient, subject, and body.
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
    // The InvoiceManager class is responsible for managing the invoice processing. It uses the InvoiceCalculator to calculate the total and the InvoicePrinter to
    // print the invoice details. This class has a single responsibility of managing the invoice workflow.
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
