using System.Collections.Generic;
using MyApp.Core.Contracts.Model;

namespace MyApp.Core.Contracts.Queries
{
    public class FooQuery : Query<FooResult>
    {
        public string Bar { get; set; }
    }

    public class FooResult : QueryResult
    {
        public FooResult(IEnumerable<FooBar> foobar)
        {
            FooBar = foobar;
        }

        public IEnumerable<FooBar> FooBar { get; set; }
    }
}
