namespace Products.Common.Extensions
{
    // Клас розширень для класу String
    public static class StringExtensions
    {
        // Метод розширення для форматування ціни
        public static string FormatPrice(this string priceString)
        {
            if (decimal.TryParse(priceString, out decimal price))
            {
                return price.ToString("C");
            }
            return priceString;
        }
    }
}