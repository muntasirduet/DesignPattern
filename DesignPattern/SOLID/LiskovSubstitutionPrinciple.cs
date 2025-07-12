using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.SOLID
{
    public abstract class PaymentMethod
    {
        public abstract void Pay();
    }

    public class CreditCardPayment : PaymentMethod
    {
        public override void Pay()
        {
            Console.WriteLine("Payment made using Credit Card.");
        }
    }

    public class PayPalPayment : PaymentMethod
    {
        public override void Pay()
        {
            Console.WriteLine("Payment made using PayPal.");
        }
    }

    public class PaymentProcessor
    {
        public void ProcessPayment(PaymentMethod paymentMethod)
        {
            paymentMethod.Pay();
        }
    }
    public class LiskovSubstitutionPrinciple
    {
        public void Run()
        {
            PaymentProcessor paymentProcessor = new PaymentProcessor();
            // Using CreditCardPayment
            PaymentMethod creditCardPayment = new CreditCardPayment();
            paymentProcessor.ProcessPayment(creditCardPayment);
            // Using PayPalPayment
            PaymentMethod payPalPayment = new PayPalPayment();
            paymentProcessor.ProcessPayment(payPalPayment);
        }

    }
}
