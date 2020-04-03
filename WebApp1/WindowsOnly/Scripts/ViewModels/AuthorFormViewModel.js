function AuthorFormViewModel(author) {
    var self = this;

    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);

    self.isCreating = author.id == 0;

    self.author = {
        id: author.id,
        firstName: ko.observable(author.firstName),
        lastName: ko.observable(author.lastName),
        biography: ko.observable(author.biography)
    };

    self.validateAndSave = function (form) {
        if (!$(form).valid()) {
            return false;
        }

        self.sending(true);

        self.author.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: '/api/authors',
            method: self.isCreating ? 'post' : 'put',
            contentType: 'application/json',
            // dataType: 'json',
            data: ko.toJSON(self.author),
            done: self.successfulSave,
            // error: self.errorSave,
            complete: self.completeSave
        });

        return true;
    };

    self.successfulSave = function () {
        // console.log('success - data: ', data);
        // console.log('success - status: ', status);
        // console.log('success - xhr: ', xhr);

        self.saveCompleted(true);

        $('.body-content').prepend(`
            <div class="alert alert-success">
                <strong>Success!</strong> The new author has been saved.
            </div>
        `);

        setTimeout(function () {
            location.href = self.isCreating ? './' : '../';
        }, 1000);
    };

    self.errorSave = function () {
        // console.log('error - xhr: ', xhr);
        // console.log('error - status: ', status);
        // console.log('error - error: ', error);

        $('.body-content').prepend(`
            <div class="alert alert-danger">
                <strong>Error!</strong> There was an error creating the author.
            </div>
        `);
    };

    self.completeSave = function () {
        // console.log('complete - xhr: ', xhr);
        // console.log('complete - status:', status);

        var action = self.isCreating ? 'creation' : 'modificaition';

        $('.body-content').prepend(`
            <div class="alert alert-success">
                <strong>Success!</strong> The new author's ${action} is completed.
            </div>
        `);

        self.sending(false);
    };
}