using MediatR;
using Orderly.Application.Repositories;
using Orderly.Domain.Common;
using Orderly.Domain.Entities;
using Orderly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestourantRepository _restourantRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IRestourantRepository restourantRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _restourantRepository = restourantRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderResponseDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restourantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                return Result<OrderResponseDto>.Failure("Seçilmiş restoran tapılmadı.");
            }

            var order = new Domain.Entities.Order
            {
                RestaurantId = request.RestaurantId,
                OrderStatusId = (int)OrderStatusEnum.Pending,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemDto in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);

                if (product == null)
                {
                    return Result<OrderResponseDto>.Failure($"ID-si {itemDto.ProductId} olan məhsul tapılmadı.");
                }

                if (product.RestaurantId != request.RestaurantId)
                {
                    return Result<OrderResponseDto>.Failure($"'{product.Name}' məhsulu bu restorana aid deyil.");
                }

                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPriceSnapshot = product.Price
                };

                order.OrderItems.Add(orderItem);
            }

            order.CalculateTotalPrice();

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var responseDto = new OrderResponseDto
            {
                Id = order.Id,
                RestaurantId = order.RestaurantId,
                OrderStatusId = order.OrderStatusId,
                TotalPrice = order.TotalPrice
            };

            return Result<OrderResponseDto>.Success(responseDto);
        }
    }
}