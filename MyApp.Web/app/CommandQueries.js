(function(MyApp) {
	function buildContract(contract) {
		return { type: contract.constructor.type, data: ko.toJSON(contract) };
	}
	var url = "api/commandQuery";
	MyApp.cqrs = {
		sendQuery: function(query, callback) {
			$.getJSON(url, buildContract(query), callback);
		},
		sendCommand: function(command) {
			$.post(url, buildContract(command));
		}
	};

	window.MyApp = (window.MyApp || {});
	window.MyApp.Core = (window.MyApp.Core || {});
	window.MyApp.Core.Contracts = (window.MyApp.Core.Contracts || {});
	window.MyApp.Core.Contracts.Queries = (window.MyApp.Core.Contracts.Queries || {});
	
	
    window.MyApp.Core.Contracts.Queries.FooQuery = function(bar) {
        this.bar = bar;
    };
	window.MyApp.Core.Contracts.Queries.FooQuery.type = "MyApp.Core.Contracts.Queries.FooQuery";
})(window.MyApp = window.MyApp || {});
