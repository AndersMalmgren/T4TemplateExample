using MyApp.Core.Contracts.Commands;
using MyApp.Core.Contracts.Queries;

namespace MyApp.Core.CommandQuery
{
    public interface IClient
    {
        TResult SendQuery<TQuery, TResult>(TQuery query) where TQuery : Query<TResult> where TResult : QueryResult;
        QueryResult SendQuery(Query query);
        void SendCommand<TCommand>(TCommand command) where TCommand : Command;
    }
}