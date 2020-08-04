$(document).ready(function () {
    
    if ($('#IsAddingManual').val() == 'True' && $('#IsAdding').val() == 'False') {
        $('#CurrentParticipants,#ListOfPeople,#ManualParticipant').hide();
        $('#ManualPanel').show();
    }
    else {
        $('#ListOfPeople,#ManualPanel,#ManualParticipant').hide();

        if ($('#IsAdding').val() == 'True' && $('#IsAddingManual').val() == 'False') {
            $('#CurrentParticipants,#ManualPanel').hide();
            $('#ListOfPeople').show();
        }
        else {
            $('#ListOfPeople,#ManualPanel').hide();
        }

    }
 
    $('#addFiltered').click(function () {
       
        urlFiltered += "&programId=" + $("#ProjectId option:selected").val();
        urlFiltered += "&signatureId=" + $("#SignatureId option:selected").val();
        window.location.href = urlFiltered;

    });

    $('#filter').click(function () {
       
        urlFilter += "&programId=" + $("#ProjectId option:selected").val();
        urlFilter += "&signatureId=" + $("#SignatureId option:selected").val();
        window.location.href = urlFilter;

    });

    $('#filterDate').click(function () {

        urlFilter += "?dateFrom=" + $("#dateFrom").val();
        urlFilter += "&dateTo=" + $("#dateTo").val();
        window.location.href = urlFilter;
    });

    $('#makeManualParticipant').click(function () {

        urlMakeParticipantManual += "?participant=" + $("#ParticipantString").val() ;
        // urlMakeParticipantManual += "&SignatureId=" + $("#SignatureId option:selected").val();
         
        window.location.href = urlMakeParticipantManual;
         
    });
    
    $('#btnAddParticipant').click(function () {
        $('#CurrentParticipants,#ManualPanel').hide();
        $('#ListOfPeople').show();
    });

    $('#btnAddParticipantManual').click(function () {
        $('#CurrentParticipants').hide();
        $('#ManualPanel').show();
    });

    $('#btnAddManualParticipant').click(function () {
        $('#CurrentParticipants,#ListOfPeople').hide();
        $('#ManualParticipant').show();
    });

    $('#btnShowParticipants').click(function () {
        $('#CurrentParticipants').show();
        $('#ListOfPeople,#ManualPanel').hide();
    });

    $('#btnShowParticipants2').click(function () {
        $('#CurrentParticipants').show();
        $('#ListOfPeople,#ManualPanel').hide();
    });

});
