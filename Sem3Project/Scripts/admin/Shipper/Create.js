$(document).ready(function () {
    $('#create').click(function (e) {
        e.preventDefault();

        let shipper = {};
        shipper.CompanyName = $('#company-name').val();
        shipper.Phone = $('#phone').val();
        
        let check = true;
        if (shipper.CompanyName == '') {
            check = false;
            $('#company-name-valid').text('This field can not empty');
        }
        else {
            $('#company-name-valid').text('');
        }

        if (shipper.Phone == '') {
            check = false;
            $('#phone-valid').text('This field can not empty');
        }
        else {
            $('#phone-valid').text('');
        }

        
        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            formData.append('CompanyName', shipper.CompanyName);
            formData.append('Phone', shipper.Phone);

            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Shipper/Insert',
                type: 'POST',
                data: formData, // modify
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.Ok) {
                        alert('Insert success!!');
                        $('#data-table').DataTable().ajax.reload();
                        $('#dashboardModalWrapper').modal('toggle');
                    }
                    else {
                        $('#create-valid').text('Insert error');
                    }
                },
                error: function () {
                    $('#create-valid').text('Server error');
                }

            });
        }
    });





});
var loadFile = function (event) {
    var output = document.getElementById('image-upload');
    output.src = URL.createObjectURL(event.target.files[0]);
};
