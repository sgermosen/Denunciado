$(function () {
    // $("#Rnc").mask("999-9999999-9");
    $('#Age').attr('readonly', true);
    $('#manualAge').change(function () {
        if ($(this).is(":checked")) {
            var returnVal = confirm("Seguro que desea insertar una edad manual?");
            $(this).attr("checked", returnVal);
            if (returnVal == true) {
                $('#Age').attr('readonly', false);
                $('#BornDate').attr('readonly', true);
                $('#BornDate').val('');

            } else {
                $('#Age').attr('readonly', true);
                $('#BornDate').attr('readonly', false);
                $('#Age').val('');
            }

        }
        else {
            $('#Age').attr('readonly', true);
            $('#BornDate').attr('readonly', false);
            $('#Age').val('');
        }
        $('#textbox1').val($(this).is(':checked'));
    });

    function daysInMonth(m, y) {
        switch (m) {
            case "2":
                return (y % 4 === 0 && y % 100) || y % 400 === 0 ? 29 : 28;
            case "9": case "4": case "6": case "11":
                return 30;
            default:
                return 31
        }
    }

    function isValidDate(d, m, y) {
        return m >= 0 && m < 12 && d > 0 && d <= daysInMonth(m, y);
    }

    $('.dateVal').change(function () {
        if (($("#Day").val() != "" && $("#Day").val() != "0") && $("#Month").val() != "" && $("#Month").val() != "0" && $("#Year").val() != "" && $("#Year").val() != "0") {

            if (daysInMonth($("#Month").val(), $("#Year").val()) < $("#Day").val()) {
                $('#Day option[value=0]').prop('selected', true);
                alert("Fecha invalida");
                return;
            }

            var myDate = ('0' + $("#Day").val()).slice(-2) + "/" + ('0' + $("#Month").val()).slice(-2) + "/" + $("#Year").val();
            console.log(myDate);
            return $('#Age').val(getAge(myDate));

        }
    });

});
$("#BornDate").focusout(function (e) {
    // e.preventDefault();
    //var today = new Date();
    //var mydate = $('#BornDate').val();
    //var birthDate = new Date($('#BornDate').val());
    //var age = today.getFullYear() - birthDate.getFullYear();
    //var m = today.getMonth() - birthDate.getMonth();
    //if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
    //    age--;
    //}
    if ($("#BornDate").val() != '') {
        return $('#Age').val(getAge($('#BornDate').val()));
    }

    //  $("#Age").val(getAge($("#BornDate").val()));

});

function getAge(dateString) {
    var now = new Date();
    var today = new Date(now.getYear(), now.getMonth(), now.getDate());

    var yearNow = now.getYear();
    var monthNow = now.getMonth();
    var dateNow = now.getDate();

    var dob = new Date(dateString.substring(6, 10),
        dateString.substring(3, 5) - 1,
        dateString.substring(0, 2)
    );

    var yearDob = dob.getYear();
    var monthDob = dob.getMonth();
    var dateDob = dob.getDate();
    var age = {};
    var ageString = "";
    var yearString = "";
    var monthString = "";
    var dayString = "";
    var monthAge;
    var dateAge;
    var yearAge;
    yearAge = yearNow - yearDob;

    if (monthNow >= monthDob)
        monthAge = monthNow - monthDob;
    else {
        yearAge--;
        monthAge = 12 + monthNow - monthDob;
    }

    if (dateNow >= dateDob)
        dateAge = dateNow - dateDob;
    else {
        monthAge--;
        dateAge = 31 + dateNow - dateDob;

        if (monthAge < 0) {
            monthAge = 11;
            yearAge--;
        }
    }

    age = {
        years: yearAge,
        months: monthAge,
        days: dateAge
    };

    if (age.years > 1) yearString = " Años";
    else yearString = " Año";
    if (age.months > 1) monthString = " Meses";
    else monthString = " Mes";
    if (age.days > 1) dayString = " Dias";
    else dayString = " Dia";


    //if ((age.years > 0) && (age.months > 0) && (age.days > 0))
    //    ageString = age.years + yearString + ", " + age.months + monthString + ", y " + age.days + dayString + "";
    //else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
    //    ageString = "" + age.days + dayString + "";
    //else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
    //    ageString = age.years + yearString + "";
    //else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
    //    ageString = age.years + yearString + " y " + age.months + monthString + "";
    //else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
    //    ageString = age.months + monthString + " y " + age.days + dayString + "";
    //else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
    //    ageString = age.years + yearString + " y " + age.days + dayString + "";
    //else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
    //    ageString = age.months + monthString + "";
    //else ageString = "Oops! Escoja una fecha inferior a la del dia!";

    if ((age.years > 0) && (age.months > 0) && (age.days > 0))
        ageString = age.years + yearString;
    else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
        ageString = age.days + dayString;
    else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
        ageString = age.years + yearString;
    else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
        ageString = age.years + yearString;
    else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
        ageString = age.months + monthString;
    else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
        ageString = age.years + yearString;
    else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
        ageString = age.months + monthString;
    else ageString = "Oops! Escoja una fecha inferior a la del dia!";

    return ageString;
}