using Stripe;

namespace StripDemo.Extentions
{
    public static class MyConfigration
    {
        public static IServiceCollection StripConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<PaymentIntentService>()
                .AddScoped<PaymentMethodService>();
        }
    }
}
