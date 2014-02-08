using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using MyApp.Core.Contracts.Commands;
using MyApp.Core.Contracts.Model;
using MyApp.Core.Contracts.Queries;
using MyApp.Core.Extensions;

namespace MyApp.Core.CommandQuery
{
    public class Client : IClient
    {
        private static Dictionary<Type, QueryInfo> queryHandlers;
        private static Dictionary<Type, Delegate> messageBusStub = new Dictionary<Type, Delegate>() { {typeof(FooQuery), new Func<FooQuery, FooResult>(FooStub)}};
        
        static Client()
        {
            BuildCache();
        }

        public QueryResult SendQuery(Query query)
        {
            return
                queryHandlers[query.GetType()]
                .Handler
                .Invoke(this, new[] {query}) as QueryResult;
        }

        public TResult SendQuery<TQuery, TResult>(TQuery query) where TQuery : Query<TResult> where TResult : QueryResult
        {
            //Generic static typed entry point here, either call a message bus or a handler for TQuery, code below is just a stub

            return (messageBusStub[typeof (TQuery)] as Func<TQuery, TResult>)(query);
        }
        
        public void SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            //Stub
        }
        
        private static void BuildCache()
        {
            var senderMethodName = ReflectionExtension.GetMethodName<Client>(x => x.SendQuery(null));
            var senderMethod = typeof (Client)
                .GetMethods()
                .Single(m => m.Name == senderMethodName && m.IsGenericMethodDefinition);


            queryHandlers = ReflectionExtension.ListDescendants<Query>()
                .Select(t => new QueryInfo(senderMethod, t))
                .ToDictionary(qi => qi.Type, qi => qi);
        }
        
        private class QueryInfo
        {
            private readonly static Type queryResultLookup = typeof (QueryResult);

            public QueryInfo(MethodInfo nonGenericHandler, Type queryType)
            {
                Handler = nonGenericHandler.MakeGenericMethod(queryType, queryType.BaseType.GetGenericArguments().Single(queryResultLookup.IsAssignableFrom));
                Type = queryType;
            }

            public Type Type { get; set; }
            public MethodInfo Handler { get; private set; }
        }

        private static FooResult FooStub(FooQuery query)
        {
            var rand = new Random();
            var result = Enumerable.Range(0, 50)
                .Select(i => string.Format("{0}:{1}",query.Bar, rand.Next()))
                .Select(fooBar => new FooBar(fooBar));

            return new FooResult(result);
        }

    }
}
