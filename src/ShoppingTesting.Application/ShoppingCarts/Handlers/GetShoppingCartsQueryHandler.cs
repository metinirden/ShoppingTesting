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
    /// GetShoppingCartsQuery'sini handle eden class. Bütün işi organiasyonu sağlayıp sonuç dönmektir. (Repository abstraction'ı ile data'yı almak, map'leyip geri dönmek.)
    /// </summary>
    public class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, List<ShoppingCartDto>>
    {
        private readonly IRepository<ShoppingCart, Guid> _repository;
        private readonly IMapper _mapper;

        public GetShoppingCartsQueryHandler(IRepository<ShoppingCart, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<ShoppingCartDto>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var includes = request.IncludeCartItems ? "CartItems.Product" : string.Empty;
            var shoppingCarts = _repository.GetAll(includes).ToList();
            var shoppingCartDtos = _mapper.Map<List<ShoppingCart>, List<ShoppingCartDto>>(shoppingCarts);
            return Task.FromResult(shoppingCartDtos);
        }
    }
}