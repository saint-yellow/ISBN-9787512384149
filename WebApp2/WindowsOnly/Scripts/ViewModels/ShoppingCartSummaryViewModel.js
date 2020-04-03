function ShoppingCartSummaryViewModel(model) {
    var self = this;

    self.shoppingCart = model;

    for (var i = 0; i < self.shoppingCart.commodityItems.length; i++) {
        var commodityItem = self.shoppingCart.commodityItems[i];
        commodityItem.quantity = ko.observable(commodityItem.quantity).extend({ subTotalPrice: commodityItem.book.salePrice });
    }

    self.shoppingCart.commodityItems = ko.observableArray(self.shoppingCart.commodityItems);

    self.shoppingCart.totalPrice = self.shoppingCart.commodityItems.totalPrice();

    self.updateCommodityItem = function (commodityItem) {
        var isNewItem = true;

        for (var i = 0; i < self.shoppingCart.commodityItems().length; i++) {
            if (self.shoppingCart.commodityItems()[i].id === commodityItem.id) {
                self.shoppingCart.commodityItems()[i].quantity(commodityItem.quantity);
                isNewItem = false;
                break;
            }
        }

        if (isNewItem) {
            commodityItem.quantity = ko.observable(commodityItem.quantity).extend({ subTotalPrice: commodityItem.book.salePrice });
            self.shoppingCart.commodityItems.push(commodityItem);
        }
    };

    self.deleteCommodityItem = function (commodityItem) {
        for (var i = 0; i < self.shoppingCart.commodityItems().length; i++) {
            if (self.shoppingCart.commodityItems()[i].id === commodityItem.id) {
                self.shoppingCart.commodityItems.remove(self.shoppingCart.commodityItems()[i]);
                break;
            }
        }
    };

    self.showShoppingCart = function () {
        $("#shopping-cart").popover('toggle');
    };

    self.fadeIn = function (element) {
        setTimeout(function () {
            $('#shopping-cart').popover('show');

            $(element).slideDown(function () {
                setTimeout(function () {
                    $('#shopping-cart').popover('hide');
                }, 2000);
            });
        }, 100);
    };

    $('#shopping-cart').popover({
        html: true,
        content: function () {
            return $('#shopping-cart-summary').html();
        },
        title: 'Shopping Cart Details',
        placement: 'bottom',
        animation: true,
        trigger: 'manual'
    });
};

if (shoppingCartSummaryData !== undefined) {
    var shoppingCartSummaryViewModel = new ShoppingCartSummaryViewModel(shoppingCartSummaryData);
    ko.applyBindings(shoppingCartSummaryViewModel, document.getElementById('shopping-cart'));
} else {
    $('.body-content').prepend(`
        <div class="alert alert-danger">
            <strong>Error! </strong>Could not find shopping cart summary.
        </div>
    `);
}