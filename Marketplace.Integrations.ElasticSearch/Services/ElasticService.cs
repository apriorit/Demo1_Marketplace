using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Marketplace.Data.Models;

namespace Marketplace.Integrations.ElasticSearch.Services
{
    public class ElasticService : IFilterableDomainService<int, ElasticEntity>
    {
        readonly ElasticsearchClient _client;

        public ElasticService(ElasticsearchClient client)
        {
            _client = client;
        }
        /// <summary>
        /// Executes query:
        /// POST rdbms_sync_idx/_search
        ///    "query": {
        ///        "bool": {
        ///            "filter": [
        ///                { "term": { "isactive": true }},
        ///                { "query_string": { "query": "string1" }}
        ///            ]
        ///        }
        ///    }
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<ElasticEntity>> Filter(string keyword)
        {
            SearchResponse<ElasticEntity> searchResponse = new();
            try
            {
                searchResponse = await _client.SearchAsync<ElasticEntity>(
                    s => s.Query(
                        b => b.Bool(m => m.Filter(
                            t => t.Term(new TermQuery { Field = new Field("isactive"), Value = true }),
                            q => q.QueryString(
                                d => d.Query(keyword)
                    )))).Size(5000));
                return searchResponse.Documents;
            }
            catch (Exception ex)
            {
                throw new ElasticException(searchResponse?.DebugInformation ?? ex.Message);
            }
        }
    }
}
