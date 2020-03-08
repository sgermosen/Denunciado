// SLIMSCROLL
// -----------------------------------

(function() {
    'use strict';

    $(initSlimsSroll);

    function initSlimsSroll() {

        if (!$.fn || !$.fn.slimScroll) return;

        $('[data-scrollable]').each(function() {

            var element = $(this),
                defaultHeight = 250;

            element.slimScroll({
                height: (element.data('height') || defaultHeight)
            });

        });
    }

})();