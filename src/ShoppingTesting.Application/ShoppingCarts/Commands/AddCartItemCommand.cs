using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ShoppingTesting.Application.ShoppingCarts.Commands
{
    public class AddCartItemCommand : IRequest, IValidatableObject
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        [DefaultValue(1)] public int Quantity { get; set; } = 1;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ShoppingCartId == Guid.Empty)
            {
                yield return new ValidationResult("Cannot be default!",
                    new[] {nameof(ShoppingCartId)});
            }

            if (ProductId == Guid.Empty)
            {
                yield return new ValidationResult("Cannot be default!",
                    new[] {nameof(ProductId)});
            }

            if (Quantity <= 0)
            {
                yield return new ValidationResult("Quantity should be greater then Zero!",
                    new[] {nameof(Quantity)});
            }
        }
    }
}