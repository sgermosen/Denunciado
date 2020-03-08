(function() {
    'use strict';

    if (typeof Dropzone === 'undefined') return;

    // Prevent Dropzone from auto discovering
    // This is useful when you want to create the
    // Dropzone programmatically later
    Dropzone.autoDiscover = false;

    $(initDropzone);

    function initDropzone() {

        // Dropzone settings
        var dropzoneOptions = {
            autoProcessQueue: false,
            uploadMultiple: true,
            parallelUploads: 100,
            maxFiles: 100,
            dictDefaultMessage: '<em class="fa fa-upload text-muted"></em><br>Drop files here to upload', // default messages before first drop
            paramName: 'file', // The name that will be used to transfer the file
            maxFilesize: 2, // MB
            addRemoveLinks: true,
            accept: function(file, done) {
                if (file.name === 'justinbieber.jpg') {
                    done('Naha, you dont. :)');
                } else {
                    done();
                }
            },
            init: function() {
                var dzHandler = this;

                this.element.querySelector('button[type=submit]').addEventListener('click', function(e) {
                    e.preventDefault();
                    e.stopPropagation();
                    dzHandler.processQueue();
                });
                this.on('addedfile', function(file) {
                    console.log('Added file: ' + file.name);
                });
                this.on('removedfile', function(file) {
                    console.log('Removed file: ' + file.name);
                });
                this.on('sendingmultiple', function() {

                });
                this.on('successmultiple', function( /*files, response*/ ) {

                });
                this.on('errormultiple', function( /*files, response*/ ) {

                });
            }

        };

        var dropzoneArea = new Dropzone('#dropzone-area', dropzoneOptions);

    }

})();