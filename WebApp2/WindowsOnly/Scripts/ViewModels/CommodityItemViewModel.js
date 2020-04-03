function CommodityItemViewModel(params) {
    var self = this;

    self.sending = ko.observable(false);

    self.commodityItem = params.commodityItem;
    self.showButton = params.showButton;

    self.changeQuantity = function (sign) {
        return function () {
            if (sign === 1) {
                self.commodityItem.quantity(self.commodityItem.quantity() + 1);
            }

            if (sign === -1) {
                var currentQuantity = self.commodityItem.quantity();
                if (currentQuantity !== undefined && currentQuantity > 0) {
                    self.commodityItem.quantity(currentQuantity - 1);
                }
            }
        };
    };

    self.upsertCommodityItem = function (form) {
        if (!$(form).valid()) {
            return false;
        }

        self.sending(true);

        var data = {
            id: self.commodityItem.id,
            shoppingCartId: self.commodityItem.shoppingCartId,
            bookId: self.commodityItem.book.id,
            quantity: self.commodityItem.quantity()
        };

        $.ajax({
            url: '/api/commodityitems',
            method: self.commodityItem.id === undefined ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(data),
            error: self.errorSave,
            complete: self.completeSave
        });

        return true;
    };

    self.completeSave = function (data) {
        var action = self.commodityItem.id === undefined ? 'added to' : 'updated in';
        var message = `<div class="alert alert-success"><string>Success!</strong> The item has been ${action} your cart.</div>`;
        $('.body-content').prepend(message);

        self.commodityItem.id = data.id;

        shoppingCartSummaryViewModel.updateCommodityItem(ko.toJS(self.commodityItem));
    };

    self.errorSave = function () {
        var action = self.commodityItem.id === undefined ? 'adding' : 'updating';
        var message = `<div><strong>Error!<strong> There wa an error ${action} the item to your cart.</div>`;
        $('.body-content').prepend(message);
    };
};

ko.components.register('upsert-commodity-item', {
    viewModel: CommodityItemViewModel,
    template: { element: document.getElementById('commodity-item-form') }
});