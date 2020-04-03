function PagingService(resultList) {
    var self = this;

    self.queryOptions = {
        currentPage: ko.observable(),
        totalPages: ko.observable(),
        pageSize: ko.observable(),
        sortField: ko.observable(),
        sortOrder: ko.observable()
    };

    self.entities = ko.observableArray();

    self.updateResultList = function (resultList) {
        self.queryOptions.currentPage(resultList.queryOptions.currentPage);
        self.queryOptions.totalPages(resultList.queryOptions.totalPages);
        self.queryOptions.pageSize(resultList.queryOptions.pageSize);
        self.queryOptions.sortField(resultList.queryOptions.sortField);
        self.queryOptions.sortOrder(resultList.queryOptions.sortOrder);

        self.entities(resultList.results);
    };

    self.updateResultList(resultList);

    self.sortEntitiesBy = function (data, event) {
        var sortField = $(event.target).data('sortField');

        var sortOrder = self.queryOptions.sortOrder();

        if (self.queryOptions.sortOrder() == 0) {
            self.queryOptions.sortOrder(1);
        } else {
            self.queryOptions.sortOrder(0);
        }

        self.queryOptions.sortField(sortField);
        // self.queryOptions.currentPage(1);

        self.fetchEntities(event);
    };

    self.previousPage = function (data, event) {
        if (self.queryOptions.currentPage() > 1) {
            self.queryOptions.currentPage(self.queryOptions.currentPage() - 1);

            self.fetchEntities(event);
        }
    };

    self.nextPage = function (data, event) {
        if (self.queryOptions.currentPage() < self.queryOptions.totalPages()) {
            self.queryOptions.currentPage(self.queryOptions.currentPage() + 1);

            self.fetchEntities(event);
        }
    };

    self.fetchEntities = function (event) {
        var href = $(event.target).attr('href');
        var sortField = self.queryOptions.sortField();
        var sortOrder = self.queryOptions.sortOrder();
        var currentPage = self.queryOptions.currentPage();
        var pageSize = self.queryOptions.pageSize();

        var url = `/api${href}/?sortField=${sortField}&sortOrder=${sortOrder}&currentPage=${currentPage}&pageSize=${pageSize}`;

        $.ajax({
            url: url,
            method: 'get',
            dataType: 'json',
            complete: function (data) { console.log(data); self.updateResultList(data.responseJSON); }
            // error: self.errorFetch
        });
    };

    self.errorFetch = function () {
        $('.body-centent').prepend(`
            <div class="alert alert-danger">
                <strong>Error!</strong> There was an error fetching data.
            </div>
        `);
    };

    self.buildSortIcon = function (sortField) {
        return ko.pureComputed(function () {
            var sortIcon = 'sort';

            if (self.queryOptions.sortField() == sortField) {
                sortIcon += '-by-alphabet';

                if (self.queryOptions.sortOrder() == 1) {
                    sortIcon += '-alt';
                }
            }
            

            return 'glyphicon glyphicon-' + sortIcon;
        });
    };

    self.buildPreviousClass = ko.pureComputed(function () {
        var className = 'previous';

        if (self.queryOptions.currentPage() == 1) {
            className += ' disabled';
        }

        return className;
    });

    self.buildNextClass = ko.pureComputed(function () {
        var className = 'next';

        if (self.queryOptions.currentPage() == self.queryOptions.totalPages()) {
            className += ' disabled';
        }

        return className;
    });

};