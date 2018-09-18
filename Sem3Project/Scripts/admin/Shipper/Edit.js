

$(document).ready(function () {
    //$('#cancel').click(function (e) {
    //    e.preventDefault();
    //    var self = $('#cancel').val();

    //    if (confirm('Are you sure you want to cancel')) {
    //        window.location = '/Admin/Employee/Index';
    //    }

    //});

    $('#edit').click(function (e) {
        e.preventDefault();

        let shipper = {};
        shipper.ShipperID = $('#edit').val();
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

            formData.append('ShipperID', shipper.ShipperID);
            formData.append('CompanyName', shipper.CompanyName);
            formData.append('Phone', shipper.Phone);

            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Shipper/Update',
                type: 'POST',
                data: formData, // modify
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.Ok) {
                        alert('Update success!!');
                        $('#data-table').DataTable().ajax.reload();
                    }
                    else {
                        $('#update-valid').text('Update error');
                    }
                },
                error: function () {
                    $('#update-valid').text('Server error');
                }

            });

        }

    });
})

