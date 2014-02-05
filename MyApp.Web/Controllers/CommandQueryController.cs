using System.Web.Http;
using MyApp.Core.CommandQuery;
using MyApp.Core.Contracts.Commands;
using MyApp.Core.Contracts.Queries;
using Newtonsoft.Json.Linq;

namespace MyApp.Web.Controllers
{
    public class CommandQueryController : ApiController
    {
        private readonly IClient client;

        public CommandQueryController(IClient client)
        {
            this.client = client;
        }

        public QueryResult Get([FromUri]Contract contract)
        {
            var query = CreateDto<Query>(contract);
            return client.SendQuery(query);
        }

        public void Post([FromBody]Contract contract)
        {
            var command = CreateDto<Command>(contract);
            client.SendCommand(command as dynamic);
        }

        private TDto CreateDto<TDto>(Contract contract) where TDto : class
        {
            var type = typeof(Query).Assembly.GetType(contract.Type);
            var jObject = JObject.Parse(contract.Data);
            var dto = jObject.ToObject(type);

            return dto as TDto;
        }

        public class Contract
        {
            public string Type { get; set; }
            public string Data { get; set; }
        }
    }
}