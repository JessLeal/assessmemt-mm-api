using MoneyMeAPI.Constants;

namespace MoneyMeAPI.Interfaces
{
    public interface IRepaymentsProviderFactory
    {
        IRepaymentsCalculatorProvider GetProviderBasedOnType(ProductTypeEnum.ProductType productType);
    }
}