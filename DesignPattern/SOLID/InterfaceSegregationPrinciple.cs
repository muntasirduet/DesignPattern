using System;

namespace DesignPattern.SOLID
{
    // Order Model
    public class Order
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public bool IsProcessed { get; set; }
    }

    // Interfaces - small, focused, ISP-compliant
    public interface IOrderProcessor
    {
        void Process(Order order);
    }

    public interface ICancelable
    {
        void Cancel(Order order);
    }

    public interface IRefundable
    {
        void Refund(Order order);
    }

    public interface ISender
    {
        void Send(Order order, string message);
    }

    // Concrete implementations
    public class OrderProcessor : IOrderProcessor
    {
        public void Process(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            order.IsProcessed = true;
            Console.WriteLine($"Order {order.Id} has been processed.");
        }
    }

    public class NotificationSender : ISender
    {
        public void Send(Order order, string message)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (message == null) throw new ArgumentNullException(nameof(message));

            Console.WriteLine($"📧 [Order {order.Id}] {message}");
        }
    }

    public class OrderCancelProcessor : ICancelable
    {
        public void Cancel(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            Console.WriteLine($"Order {order.Id} has been cancelled.");
        }
    }

    public class OrderRefundProcessor : IRefundable
    {
        public void Refund(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            Console.WriteLine($"Order {order.Id} has been refunded.");
        }
    }

    // PaymentService with improved Random usage and no side effects
    public class PaymentService
    {
        private readonly Random _random = new Random();

        // Returns true if payment successful, false otherwise
        public bool ProcessPayment()
        {
            return _random.Next(2) == 0;
        }
    }

    // High-level module depends only on abstractions - DIP applied
    public class OrderService
    {
        private readonly IOrderProcessor _orderProcessor;
        private readonly ICancelable _cancelProcessor;
        private readonly IRefundable _refundProcessor;
        private readonly ISender _notificationSender;

        public OrderService(IOrderProcessor orderProcessor,
                            ICancelable cancelProcessor,
                            IRefundable refundProcessor,
                            ISender notificationSender)
        {
            _orderProcessor = orderProcessor ?? throw new ArgumentNullException(nameof(orderProcessor));
            _cancelProcessor = cancelProcessor ?? throw new ArgumentNullException(nameof(cancelProcessor));
            _refundProcessor = refundProcessor ?? throw new ArgumentNullException(nameof(refundProcessor));
            _notificationSender = notificationSender ?? throw new ArgumentNullException(nameof(notificationSender));
        }

        // Process order based on payment status
        public void ProcessOrder(Order order, bool isPaymentSuccessful)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            if (isPaymentSuccessful)
            {
                _orderProcessor.Process(order);
                _notificationSender.Send(order, "Your order has been processed successfully.");
            }
            else
            {
                _cancelProcessor.Cancel(order);
                _notificationSender.Send(order, "Your payment failed. Order has been cancelled.");
            }
        }

        // Refund order
        public void RefundOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            _refundProcessor.Refund(order);
            _notificationSender.Send(order, "Your order has been refunded.");
        }
    }

    public class InterfaceSegregationPrinciple
    {
        public void Run()
        {
            Console.WriteLine("=== Order System ===\n");

            var order = new Order { Id = 500 };

            // Low-level modules (details)
            IOrderProcessor orderProcessor = new OrderProcessor();
            IRefundable refundProcessor = new OrderRefundProcessor();
            ICancelable cancelProcessor = new OrderCancelProcessor();
            ISender notificationSender = new NotificationSender();
            var paymentService = new PaymentService();

            // High-level module
            var orderService = new OrderService(orderProcessor, cancelProcessor, refundProcessor, notificationSender);

            // Process payment (returns true/false)
            bool paymentSuccess = paymentService.ProcessPayment();
            order.IsPaid = paymentSuccess;

            orderService.ProcessOrder(order, paymentSuccess);

            // Ask user if they want refund (only if processed)
            if (order.IsProcessed)
            {
                Console.WriteLine("\nDo you want to refund this order? (y/n)");
                var refundInput = Console.ReadLine();

                if (refundInput?.Trim().ToLower() == "y")
                {
                    orderService.RefundOrder(order);
                }
            }

            Console.WriteLine("\n=== Process Completed ===");
            Console.ReadLine();
        }
    }
}
