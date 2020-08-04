// Select2
// -----------------------------------

(function() {
    'use strict';

    $(initSelect2);

    function initSelect2() {

        if (!$.fn.select2) return;

        // Select 2

        $('#select2-1').select2({
            theme: 'bootstrap4'
        });
        $('#select2-2').select2({
            theme: 'bootstrap4'
        });
        $('#select2-3').select2({
            theme: 'bootstrap4'
        });
        $('#select2-4').select2({
            placeholder: 'Select a state',
            allowClear: true,
            theme: 'bootstrap4'
        });

    }

})();