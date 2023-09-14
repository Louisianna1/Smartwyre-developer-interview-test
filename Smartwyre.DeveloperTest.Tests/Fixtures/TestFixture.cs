using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Tests.DummyServices;
using Smartwyre.DeveloperTest.Logic;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Tests.Fixtures
{
    public class TestFixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            services.AddTransient<IRebateService, RebateService>();
            services.AddTransient<IRebateDataStore, DummyRebateDataStore>();
            services.AddTransient<IProductDataStore, DummyProductDataStore>();

            /* Add concrete RebateCalculator for each IncentiveType */
            services.AddTransient<RebateCalculator, AmountPerUomCalculator>();
            services.AddTransient<RebateCalculator, FixedCashAmountCalculator>();
            services.AddTransient<RebateCalculator, FixedRateRebateCalculator>();

            services.AddSingleton<RebateCalculatorFactory>();

        }


        protected override ValueTask DisposeAsyncCore()
            => new();


        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = true };
        }
    }
}
