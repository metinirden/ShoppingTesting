using System;
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
    /// GetShoppingCartQuery'sini handle eden class. Bütün işi organiasyonu sağlayıp sonuç dönmektir. (Repository abstraction'ı ile data'yı almak, map'leyip geri dönmek.)
    /// </summary>
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartDto>
    {
        private readonly IRepository<ShoppingCart, Guid> _repository;
        private readonly IMapper _mapper;

        public GetShoppingCartQueryHandler(IRepository<ShoppingCart, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDto> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _repository.GetById(request.ShoppingCartId, "CartItems.Product");
            return _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart);
        }
    }
}