$(function () {
    $.validator.addMethod('date',
    function (value, element) {
        if (this.optional(element)) {
            return true;
        }
        var valid = true;
        try {
            $.datepicker.parseDate('yy年mm月dd日', value);
        }
        catch (err) {
            valid = false;
        }
        return valid;
    });
    $(".datetype").datepicker({ dateFormat: 'yy年mm月dd日' });
});