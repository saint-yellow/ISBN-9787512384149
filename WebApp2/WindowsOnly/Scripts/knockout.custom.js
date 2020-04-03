ko.extenders.subTotalPrice = function (target, multiplier) {
    target.subTotalPrice = ko.observable();

    function calculateTotalPrice(newValue) {
        target.subTotalPrice((newValue * multiplier).toFixed(2));
    };

    calculateTotalPrice(target());

    target.subscribe(calculateTotalPrice);

    return target;
};

ko.observableArray.fn.totalPrice = function () {
    return ko.pureComputed(function () {
        var runningTotalPrice = 0;

        for (var i = 0; i < this().length; i++) {
            runningTotalPrice += parseFloat(this()[i].quantity.subTotalPrice());
        }

        return runningTotalPrice.toFixed(2);
    }, this);
};

ko.bindingHandlers.isDirty = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var originalValue = ko.unwrap(valueAccessor());
        var interceptor = ko.pureComputed(function () {
            return (bindingContext.$data.showButton !== undefined && bindingContext.$data.showButton === true) || originalValue !== valueAccessor()();
        });

        ko.applyBindingsToNode(element, {
            visible: interceptor
        });
    }
};

ko.bindingHandlers.appendToHref = {
    init: function (element, valueAccessor) {
        var currentHref = $(element).attr('href');
        $(element).attr('href', currentHref + '/' + valueAccessor());
    }
};
