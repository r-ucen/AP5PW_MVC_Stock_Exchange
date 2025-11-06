$.validator.addMethod('allletterscapitalized',
    function (value, element, params) {
        if (!value || value == '')
            return true;

        if (value.toUpperCase() == value) {
            return true;
        }

        return false;
    });


$.validator.unobtrusive.adapters.add('allletterscapitalized', '',
    function (options) {
        var element = $(options.form).find('#Description')[0];

        options.rules['allletterscapitalized'] = [element, ''];
        options.messages['allletterscapitalized'] = options.message;
    });