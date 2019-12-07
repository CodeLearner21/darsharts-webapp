(function ($) {
    $(document).ready(function () {
        $("#customer-name").keyup(function () {
            var $this = $(this);
            var term = $this.val();
            var baseUrl = document.location.protocol + '//' + document.location.host;
            var apiUrl = '/Admin/Customers/Find?term=' + term;
            $.ajax({
                url: apiUrl,
                method: 'get'
            }).done(function (result) {
                console.log(result)
            }).fail(function (xhr) {
                console.log(xhr)
            });
        });
    });
}(jQuery));