function ShoppingCartDetailsViewModel(model) {
    var self = this;

    self.sending = ko.observable(false);

    self.shoppingCart = model;

    for (var i = 0; i < self.shoppingCart.commodityItems.length; i++) {
        self.shoppingCart.commodityItems[i].quantity = ko.observable(self.shoppingCart.commodityItems[i].quantity).extend({ subTotalPrice: self.shoppingCart.commodityItems[i].book.salePrice });
    }

    self.shoppingCart.commodityItems = ko.observableArray(self.shoppingCart.commodityItems);

    self.shoppingCart.totalPrice = self.shoppingCart.commodityItems.totalPrice();

    self.commodityItemBeingChanged = null;

    self.deleteCommodityItem = function (commodityItem) {
        self.sending(true);

        self.commodityItemBeingChanged = commodityItem;

        $.ajax({
            url: '/api/shoppingcartitems',
            method: 'delete',
            contentType: 'application/json',
            data: ko.toJSON(commodityItem),
            error: self.errorDelete,
            complete: self.completeDelete
        });
    };

    self.errorDelete = function () {
        $('.body-content').prepend(`
            <div class="alert alert-danger">
                <strong>
                    Error!
                </strong>
                 There was an error updating the item to your cart.
            </div>
        `);
    };

    self.completeDelete = function (data) {
        $('.body-content').prepend(`
            <div class="alert alert-success">
                <strong>
                    Success!
                </strong>
                 The item has been deleted from your cart.
            </div>
        `);

        self.shoppingCart.commodityItems.remove(self.commodityItemBeingChanged);

        shoppingCartSummaryViewModel.deleteCommodityItem(ko.toJS(self.commodityItemBeingChanged));

        self.commodityItemBeingChanged = null;

        self.sending(false);
    };

    self.fadeOut = function (element) {
        $(element).fadeOut(1000, function () {
            $(element).remove();
        });
    };
};