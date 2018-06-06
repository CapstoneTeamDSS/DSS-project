WISKY.advertising = function () {
    function init() {
        $(document).ready(function () {
            $(document).on('scroll', '#createAdvertisingModal', function () {
                $('.daterangepicker').daterangepicker('place')
            });

        });
    };


    return {
        init: init
    };
}();
