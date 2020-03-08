// Start Bootstrap JS
// -----------------------------------

(function() {
    'use strict';

    $(initBootstrap);

    function initBootstrap() {

        // necessary check at least til BS doesn't require jQuery
        if (!$.fn || !$.fn.tooltip || !$.fn.popover) return;

        // POPOVER
        // -----------------------------------

        $('[data-toggle="popover"]').popover();

        // TOOLTIP
        // -----------------------------------

        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });

        // DROPDOWN INPUTS
        // -----------------------------------
        $('.dropdown input').on('click focus', function(event) {
            event.stopPropagation();
        });

    }

})();