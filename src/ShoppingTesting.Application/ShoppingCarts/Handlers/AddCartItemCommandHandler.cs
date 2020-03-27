using System;
using System.Threading;
using System.Threading.Tasks;
using ShoppingTesting.Application.ShoppingCarts.Commands;
using ShoppingTesting.Domain;
using MediatR;

namespace ShoppingTesting.Application.ShoppingCarts.Handlers
{
    /// <summary>
    /// AddCartItemCommand'ini handle eden class. Bütün işi organiasyonu sağlamaktır. (Repository abstraction'ı ile data'yı almak, domain object'ine gerekli parameterleri geçmek.)
    /// </summary>
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly IRepository<ShoppingCart, Guid> _scRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IProductStockProvider _productStockProvider;

        public AddCartItemCommandHandler(IRepository<ShoppingCart, Guid> scRepository,
            IRepository<Product, Guid> productRepository, IProductStockProvider productStockProvider)
        {
            _scRepository = scRepository;
            _productRepository = productRepository;
            _productStockProvider = productStockProvider;
        }

        public async Task<Unit> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _scRepository.GetById(request.ShoppingCartId, "CartItems.Product");
            var product = await _productRepository.GetById(request.ProductId);
            var currentStock = await _productStockProvider.GetStockForProduct(request.ProductId);
            
            shoppingCart.AddItem(new CartItem(Guid.NewGuid(), product, (uint) request.Quantity), currentStock);
            
            await _scRepository.Update(shoppingCart);
            return Unit.Value;
        }
    }
}