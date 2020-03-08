// Sweet Alert
// -----------------------------------

(function() {
    'use strict';

    $(initSweetAlert);

    function initSweetAlert() {

        $('#swal-demo1').on('click', function(e) {
            e.preventDefault();
            swal("Here's a message!")
        });

        $('#swal-demo2').on('click', function(e) {
            e.preventDefault();
            swal("Here's a message!", "It's pretty, isn't it?")
        });

        $('#swal-demo3').on('click', function(e) {
            e.preventDefault();
            swal("Good job!", "You clicked the button!", "success")
        });

        $('#swal-demo4').on('click', function(e) {
            e.preventDefault();
            swal({
                title: 'Are you sure?',
                text: 'Your will not be able to recover this imaginary file!',
                icon: 'warning',
                buttons: {
                    cancel: true,
                    confirm: {
                        text: 'Yes, delete it!',
                        value: true,
                        visible: true,
                        className: "bg-danger",
                        closeModal: true
                    }
                }
            }).then(function() {
                swal('Booyah!');
            });

        });

        $('#swal-demo5').on('click', function(e) {
            e.preventDefault();
            swal({
                title: 'Are you sure?',
                text: 'Your will not be able to recover this imaginary file!',
                icon: 'warning',
                buttons: {
                    cancel: {
                        text: 'No, cancel plx!',
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: false
                    },
                    confirm: {
                        text: 'Yes, delete it!',
                        value: true,
                        visible: true,
                        className: "bg-danger",
                        closeModal: false
                    }
                }
            }).then(function(isConfirm) {
                if (isConfirm) {
                    swal('Deleted!', 'Your imaginary file has been deleted.', 'success');
                } else {
                    swal('Cancelled', 'Your imaginary file is safe :)', 'error');
                }
            });

        });

    }

})();