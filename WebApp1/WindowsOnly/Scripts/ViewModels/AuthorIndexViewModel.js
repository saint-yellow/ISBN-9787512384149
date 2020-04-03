function AuthorIndexViewModel(resultList) {
    var self = this;

    self.pagingService = new PagingService(resultList);

    self.showModal = function (modalId) {
        return function (data, event) {
            self.sending = ko.observable(false);
            $.get($(event.target).attr('href'), function (d) {
                $('.body-content').prepend(d);
                $(`#${modalId}`).modal('show');

                ko.applyBindings(self, document.getElementById(modalId));
            });
        };
    };

    self.deleteAuthor = function (form) {
        self.sending(true);
        return true;
    };
};