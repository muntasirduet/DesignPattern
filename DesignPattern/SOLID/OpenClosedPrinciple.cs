using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.SOLID
{
    public interface  IDiscountStrategy
    {
        double ApplyDiscount(double total);
    }
    public class RegularDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double total) => total * 0.95;
    }
    public class PremiumDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double total) => total * 0.90;
    }

    // The Open/Closed Principle states that software entities (classes, modules, functions, etc.) should be open for extension but closed for modification.
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly double _percentage;
        public PercentageDiscountStrategy(double percentage)
        {
            _percentage = percentage;
        }
        public double ApplyDiscount(double total)
        {
            return total - (total * _percentage / 100);
        }
    }

    public class DiscountService
    {
        private readonly IDiscountStrategy _discountStrategy;
        public DiscountService(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }
        public double CalculateTotal(double total)
        {
            return _discountStrategy.ApplyDiscount(total);
        }
    }
    
    public class OpenClosedPrinciple
    {
        DiscountService discountService = new DiscountService(new RegularDiscount());
        public void Run()
        {
            double total = 100.0;
            double discountedTotal = discountService.CalculateTotal(total);
            Console.WriteLine($"Total after discount: {discountedTotal:C}");
            // Change the discount strategy without modifying the DiscountService class
            discountService = new DiscountService(new PremiumDiscount());
            discountedTotal = discountService.CalculateTotal(total);
            Console.WriteLine($"Total after premium discount: {discountedTotal:C}");
            // Using a percentage-based discount strategy
            discountService = new DiscountService(new PercentageDiscountStrategy(20));
            discountedTotal = discountService.CalculateTotal(total);
            Console.WriteLine($"Total after 20% discount: {discountedTotal:C}");
        }

    }
}
