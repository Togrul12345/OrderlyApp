using MediatR;
using Orderly.Application.Repositories;
using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Products.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return Result<bool>.Failure("Yenilənməli olan məhsul tapılmadı.");
            }

  
            product.RestaurantId = request.RestaurantId;
            product.CategoryId = request.CategoryId;
            product.Name = request.Name;
            product.Price = request.Price;
            product.IsAvailable = request.IsAvailable;

     

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
