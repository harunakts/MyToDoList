$(document).ready(function () {

    $.ajax({
        url: '/YapilanIslers/DerleYapilanisTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    });
});