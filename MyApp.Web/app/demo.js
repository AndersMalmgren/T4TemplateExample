(function(MyApp) {
    MyApp.DemoViewModel = function() {
        this.bar = ko.observable();
        this.fooBars = ko.observableArray();

        this.canGetFooBars = ko.computed(this.getCanGetFooBars, this);
    };

    MyApp.DemoViewModel.prototype = {
        getCanGetFooBars: function() {
            return this.bar() != null && this.bar().length > 0;
        },
        getFooBars: function() {
            MyApp.cqrs.sendQuery(new MyApp.Core.Contracts.Queries.FooQuery(this.bar()), function(queryResult) {
                this.fooBars(queryResult.fooBar);
            }.bind(this));
        }
    };

    ko.applyBindings(new MyApp.DemoViewModel);
})(window.MyApp = window.MyApp || {})