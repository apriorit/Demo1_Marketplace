using Autofac;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Marketplace.Data.Models;
using Marketplace.Integrations.ElasticSearch.Models;
using Marketplace.Integrations.ElasticSearch.Services;
using System;

namespace Marketplace.Services.Elastic
{
    public class ElasticSearchModule : Module
    {
        private readonly ElasticSettings elkSettings;

        public ElasticSearchModule(ElasticSettings elkSettings)
        {
            this.elkSettings = elkSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterClient(builder);
            RegisterDomainService(builder);
        }

        private void RegisterDomainService(ContainerBuilder builder)
        {
            builder.RegisterType<ElasticService>()
                .As<IFilterableDomainService<int, ElasticEntity>>()
                .InstancePerLifetimeScope();
        }

        private void RegisterClient(ContainerBuilder builder)
        {
            var settings = new ElasticsearchClientSettings(new Uri(elkSettings.Url))
                .CertificateFingerprint(elkSettings.Fingerprint)
                .DefaultIndex(elkSettings.DefaultIndex)
                .Authentication(new BasicAuthentication(elkSettings.User, elkSettings.Password));
            var client = new ElasticsearchClient(settings);

            builder.RegisterInstance(client).As<ElasticsearchClient>();
        }
    }
}
