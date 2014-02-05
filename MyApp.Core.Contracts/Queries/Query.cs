namespace MyApp.Core.Contracts.Queries
{
    public abstract class Query<TResult> : Query where TResult : QueryResult
    {
    }

    public abstract class Query
    {
        
    }

    public abstract class QueryResult
    {
        
    }
}
