namespace ShoppingTesting.Infrastructure.Seed
{
    public static class SeedHelper
    {
        /// <summary>
        /// EFCore için seedExtension, uygulama ayağa kalkarken kullanılıyor. 
        /// </summary>
        public static void SeedHostDb(this ShoppingTestingContext context)
        {
            new DefaultProductsBuilder(context).Create();
            new DefaultShoppingCartsBuilder(context).Create();
        }
    }
}