using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShoppingTesting.Application.ShoppingCarts.Dtos;
using MediatR;

namespace ShoppingTesting.Application.ShoppingCarts.Queries
{
    /// <summary>
    /// Tekil sepet dönecek olan Query object'i. Aldığı parametre IValidatableObject ile valide edilecektir.
    /// </summary>
    public class GetShoppingCartQuery : IRequest<ShoppingCartDto>, IValidatableObject
    {
        public Guid ShoppingCartId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ShoppingCartId == Guid.Empty)
            {
                yield return new ValidationResult($"Cannot be default!",
                    new[] {nameof(ShoppingCartId)});
            }
        }
    }
}