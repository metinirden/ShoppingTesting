using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingTesting.Application.ShoppingCarts.Dtos;
using ShoppingTesting.Application.ShoppingCarts.Queries;
using ShoppingTesting.Domain;
using MediatR;

namespace ShoppingTesting.Application.ShoppingCarts.Handlers
{
    /// <summary>
    /// GetProductQuery'sini handle eden class. Bütün işi organiasyonu sağlayıp sonuç dönmektir. (Repository abstraction'ı ile data'yı almak, map'leyip geri dönmek.)
    /// </summary>
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IRepository<Product, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.GetAll().ToList();
            var productsDtos = _mapper.Map<List<Product>, List<ProductDto>>(products);
            return Task.FromResult(productsDtos);
        }
    }
}