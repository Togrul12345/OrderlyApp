using MediatR;
using Orderly.Application.Repositories;
using Orderly.Domain.Common;
using Orderly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Products.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                RestaurantId = request.RestaurantId,
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                IsAvailable = true
            };

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

         

     
            var productResult = new ProductResult
            {
                Id = product.Id,
                RestaurantId = product.RestaurantId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                IsAvailable = product.IsAvailable
            };

      
            return Result<ProductResult>.Success(productResult, "Məhsul uğurla əlavə edildi.");
        }
    }
}