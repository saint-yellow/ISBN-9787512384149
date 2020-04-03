function BookDetailsViewModel(model) {
    var self = this;

    self.commodityItem = {
        shoppingCartId: shoppingCartSummaryViewModel.shoppingCart.id,
        quantity: ko.observable(1),
        book: model
    };
};