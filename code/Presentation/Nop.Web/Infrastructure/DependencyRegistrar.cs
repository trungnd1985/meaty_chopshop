﻿using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Framework.Factories;
using Nop.Web.Infrastructure.Installation;

namespace Nop.Web.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="appSettings">App settings</param>
        public virtual void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            //installation localization service
            services.AddScoped<IInstallationLocalizationService, InstallationLocalizationService>();

            //common factories
            services.AddScoped<IAclSupportedModelFactory, AclSupportedModelFactory>();
            services.AddScoped<IDiscountSupportedModelFactory, DiscountSupportedModelFactory>();
            services.AddScoped<ILocalizedModelFactory, LocalizedModelFactory>();
            services.AddScoped<IStoreMappingSupportedModelFactory, StoreMappingSupportedModelFactory>();

            //admin factories
            services.AddScoped<IBaseAdminModelFactory, BaseAdminModelFactory>();
            services.AddScoped<IActivityLogModelFactory, ActivityLogModelFactory>();
            services.AddScoped<IAddressModelFactory, AddressModelFactory>();
            services.AddScoped<IAddressAttributeModelFactory, AddressAttributeModelFactory>();
            services.AddScoped<IAffiliateModelFactory, AffiliateModelFactory>();
            services.AddScoped<IBlogModelFactory, BlogModelFactory>();
            services.AddScoped<ICampaignModelFactory, CampaignModelFactory>();
            services.AddScoped<ICategoryModelFactory, CategoryModelFactory>();
            services.AddScoped<ICheckoutAttributeModelFactory, CheckoutAttributeModelFactory>();
            services.AddScoped<ICommonModelFactory, CommonModelFactory>();
            services.AddScoped<ICountryModelFactory, CountryModelFactory>();
            services.AddScoped<ICurrencyModelFactory, CurrencyModelFactory>();
            services.AddScoped<ICustomerAttributeModelFactory, CustomerAttributeModelFactory>();
            services.AddScoped<ICustomerModelFactory, CustomerModelFactory>();
            services.AddScoped<ICustomerRoleModelFactory, CustomerRoleModelFactory>();
            services.AddScoped<IDiscountModelFactory, DiscountModelFactory>();
            services.AddScoped<IEmailAccountModelFactory, EmailAccountModelFactory>();
            services.AddScoped<IExternalAuthenticationMethodModelFactory, ExternalAuthenticationMethodModelFactory>();
            services.AddScoped<IForumModelFactory, ForumModelFactory>();
            services.AddScoped<IGiftCardModelFactory, GiftCardModelFactory>();
            services.AddScoped<IHomeModelFactory, HomeModelFactory>();
            services.AddScoped<ILanguageModelFactory, LanguageModelFactory>();
            services.AddScoped<ILogModelFactory, LogModelFactory>();
            services.AddScoped<IManufacturerModelFactory, ManufacturerModelFactory>();
            services.AddScoped<IMeasureModelFactory, MeasureModelFactory>();
            services.AddScoped<IMessageTemplateModelFactory, MessageTemplateModelFactory>();
            services.AddScoped<IMultiFactorAuthenticationMethodModelFactory, MultiFactorAuthenticationMethodModelFactory>();
            services.AddScoped<INewsletterSubscriptionModelFactory, NewsletterSubscriptionModelFactory>();
            services.AddScoped<INewsModelFactory, NewsModelFactory>();
            services.AddScoped<IOrderModelFactory, OrderModelFactory>();
            services.AddScoped<IPaymentModelFactory, PaymentModelFactory>();
            services.AddScoped<IPluginModelFactory, PluginModelFactory>();
            services.AddScoped<IPollModelFactory, PollModelFactory>();
            services.AddScoped<IProductModelFactory, ProductModelFactory>();
            services.AddScoped<IProductAttributeModelFactory, ProductAttributeModelFactory>();
            services.AddScoped<IProductReviewModelFactory, ProductReviewModelFactory>();
            services.AddScoped<IReportModelFactory, ReportModelFactory>();
            services.AddScoped<IQueuedEmailModelFactory, QueuedEmailModelFactory>();
            services.AddScoped<IRecurringPaymentModelFactory, RecurringPaymentModelFactory>();
            services.AddScoped<IReturnRequestModelFactory, ReturnRequestModelFactory>();
            services.AddScoped<IReviewTypeModelFactory, ReviewTypeModelFactory>();
            services.AddScoped<IScheduleTaskModelFactory, ScheduleTaskModelFactory>();
            services.AddScoped<ISecurityModelFactory, SecurityModelFactory>();
            services.AddScoped<ISettingModelFactory, SettingModelFactory>();
            services.AddScoped<IShippingModelFactory, ShippingModelFactory>();
            services.AddScoped<IShoppingCartModelFactory, ShoppingCartModelFactory>();
            services.AddScoped<ISpecificationAttributeModelFactory, SpecificationAttributeModelFactory>();
            services.AddScoped<IStoreModelFactory, StoreModelFactory>();
            services.AddScoped<ITaxModelFactory, TaxModelFactory>();
            services.AddScoped<ITemplateModelFactory, TemplateModelFactory>();
            services.AddScoped<ITopicModelFactory, TopicModelFactory>();
            services.AddScoped<IVendorAttributeModelFactory, VendorAttributeModelFactory>();
            services.AddScoped<IVendorModelFactory, VendorModelFactory>();
            services.AddScoped<IWidgetModelFactory, WidgetModelFactory>();
            services.AddScoped<INewsCategoryModelFactory, NewsCategoryModelFactory>();


            //factories
            services.AddScoped<Factories.IAddressModelFactory, Factories.AddressModelFactory>();
            services.AddScoped<Factories.IBlogModelFactory, Factories.BlogModelFactory>();
            services.AddScoped<Factories.ICatalogModelFactory, Factories.CatalogModelFactory>();
            services.AddScoped<Factories.ICheckoutModelFactory, Factories.CheckoutModelFactory>();
            services.AddScoped<Factories.ICommonModelFactory, Factories.CommonModelFactory>();
            services.AddScoped<Factories.ICountryModelFactory, Factories.CountryModelFactory>();
            services.AddScoped<Factories.ICustomerModelFactory, Factories.CustomerModelFactory>();
            services.AddScoped<Factories.IForumModelFactory, Factories.ForumModelFactory>();
            services.AddScoped<Factories.IExternalAuthenticationModelFactory, Factories.ExternalAuthenticationModelFactory>();
            services.AddScoped<Factories.INewsModelFactory, Factories.NewsModelFactory>();
            services.AddScoped<Factories.INewsletterModelFactory, Factories.NewsletterModelFactory>();
            services.AddScoped<Factories.IOrderModelFactory, Factories.OrderModelFactory>();
            services.AddScoped<Factories.IPollModelFactory, Factories.PollModelFactory>();
            services.AddScoped<Factories.IPrivateMessagesModelFactory, Factories.PrivateMessagesModelFactory>();
            services.AddScoped<Factories.IProductModelFactory, Factories.ProductModelFactory>();
            services.AddScoped<Factories.IProfileModelFactory, Factories.ProfileModelFactory>();
            services.AddScoped<Factories.IReturnRequestModelFactory, Factories.ReturnRequestModelFactory>();
            services.AddScoped<Factories.IShoppingCartModelFactory, Factories.ShoppingCartModelFactory>();
            services.AddScoped<Factories.ITopicModelFactory, Factories.TopicModelFactory>();
            services.AddScoped<Factories.IVendorModelFactory, Factories.VendorModelFactory>();
            services.AddScoped<Factories.IWidgetModelFactory, Factories.WidgetModelFactory>();
            services.AddScoped<Factories.IMeatyCatalogModelFactory, Factories.MeatyCatalogModelFactory>();
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 2;
    }
}
