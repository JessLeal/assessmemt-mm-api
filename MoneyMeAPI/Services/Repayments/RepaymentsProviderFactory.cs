using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoneyMeAPI.Interfaces;
using static MoneyMeAPI.Constants.ProductTypeEnum;

namespace MoneyMeAPI.Services.Repayments
{
    public class RepaymentsProviderFactory : IRepaymentsProviderFactory
    {
        private readonly Dictionary<ProductType, IRepaymentsCalculatorProvider> _productTypeMapper;
        private readonly IPMTCalculator _pmtCalculator;

        public RepaymentsProviderFactory(IPMTCalculator pmtCalculator)
        {
            
            _pmtCalculator = pmtCalculator;

            //Create a dictionary based on the product type
            _productTypeMapper = new Dictionary<ProductType, IRepaymentsCalculatorProvider>();
            _productTypeMapper.Add(ProductType.ProductA, new ProductARepaymentProvider(_pmtCalculator));
            _productTypeMapper.Add(ProductType.ProductB, new ProductBRepaymentProvider(_pmtCalculator));
            _productTypeMapper.Add(ProductType.ProductC, new ProductCRepaymentProvider(_pmtCalculator));
        }

        public IRepaymentsCalculatorProvider GetProviderBasedOnType(ProductType productType)
        {
            //Get the Repayment calculator provider based on type
            return _productTypeMapper[productType];
        }
    }
}
