using AutoMapper;
using Confluent.Kafka;
using IOrderManagementSystem.Domain.Dtos;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Dtos.OrderDtos;
using IOrderManagementSystem.Repositories.Abstractions;
using IOrderManagementSystem.Services.Abstractions;
using System.Text.Json;

namespace IOrderManagementSystem.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly string _topic = "order-created";
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IProducer<Null, string> _kafkaProducer;

        public OrderService(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IMapper mapper,
            IProducer<Null, string> kafkaProducer)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _kafkaProducer = kafkaProducer;
        }
        public async Task<long> CreateOrder(CreateOrderDto createOrderDto)
        {

            // Validate the input DTO
            ValidateOrderCreation(createOrderDto);

            // Fetch products based on the product IDs in the DTO
            var productIds = createOrderDto.Products.Select(x => x.ProductId).ToList();
            var products = await _productRepository.GetProductsListAsync(productIds);

            // Ensure products are found
            if (products is null || !products.Any())
            {
                throw new ArgumentException("No products found for the given order.");
            }

            // Create the order and initialize variables
            var order = Order.CreateOrder(createOrderDto);
            var productOrders = createOrderDto.Products
                .Select(product => CreateProductOrder(order, product, products))
                .ToList();

            // Assign product orders to the order
            order.OrderProducts = productOrders;
            order.AddOrderTotalPrice(productOrders.Sum(po => po.Product.Price * po.Quantity));
            // Save the order and return its ID
            try
            {
                var orderId = await _orderRepository.AddOrderAsync(order);
                await _kafkaProducer.ProduceAsync(_topic, new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(_mapper.Map<OrderNotificationMessageDto>(order))
                });
                return orderId;
            }
            catch (Exception)
            {
                return default;
            }

        }

        // Helper method to create a ProductOrder
        private ProductOrder CreateProductOrder(Order order, CreateProductOrderDto productDto, List<Product> products)
        {
            var product = products.FirstOrDefault(p => p.Id == productDto.ProductId)
                          ?? throw new ArgumentException($"Product with ID {productDto.ProductId} not found.");

            return ProductOrder.Create(new ProductOrderDto
            {
                Order = order,
                Product = product,
                Quantity = productDto.ProductQuantity
            });
        }

        private void ValidateOrderCreation(CreateOrderDto order)
        {
            if(order is null)
            {
                throw new ArgumentNullException("invalid order");
            }
            if(string.IsNullOrEmpty(order.ShippingAddress))
            {
                throw new ArgumentNullException("invalid shipping address");
            }
            if (order.ArrivalDate < DateTime.Now)
            {
                throw new ArgumentNullException("invalid arrival date");
            }
            if (string.IsNullOrEmpty(order.CustomerPhoneNumber) || order.CustomerPhoneNumber.Length < 11)
            {
                throw new ArgumentNullException("invalid phone number");
            }
            if(!order.Products.Any() || order.Products.Any(x=>x.ProductId < 0 || x.ProductQuantity < 0))
            {
                throw new ArgumentException("invalid order products");
            }
        }
    }
}
