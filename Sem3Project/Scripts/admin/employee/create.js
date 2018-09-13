$(document).ready(function () {
    $('#create').click(function (e) {
        e.preventDefault();

        let employee = {};
        employee.LastName = $('#last-name').val();
        employee.FirstName = $('#first-name').val();
        employee.Title = $('#title').val();
        employee.TitleOfCourtesy = $('#title-of-courtesy').val();
        employee.BirthDate = $('#birth-date').val();
        employee.HireDate = $('#hire-date').val();
        employee.Address = $('#address').val();
        employee.City = $('#city').val();
        employee.Region = $('#region').val();
        employee.PostalCode = $('#postal-code').val();
        employee.Country = $('#country').val();
        employee.HomePhone = $('#home-phone').val();
        employee.Extension = $('#extension').val();
        employee.Notes = $('#notes').val();
        employee.ReportsTo = $('#report-to').val();
        let image;
        let check = true;
        if (employee.LastName == '') {
            check = false;
            $('#last-name-valid').text('This field can not empty');
        }
        else {
            $('#last-name-valid').text('');
        }

        if (employee.FirstName == '') {
            check = false;
            $('#first-name-valid').text('This field can not empty');
        }
        else {
            $('#first-name-valid').text('');
        }

        if (employee.BirthDate == '') {
            check = false;
            $('#birth-date-valid').text('This field can not empty');
        }
        else {
            $('#birth-date-valid').text('');
        }
        if (employee.HireDate == '') {
            check = false;
            $('#hire-date-valid').text('This field can not empty');
        }
        else {
            $('#hire-date-valid').text('');
        }

        image = $('#employee-image').val();
        if (image == '') {
            7
            check = false;
            $('#employee-image-valid').text('This field can not empty');
        }
        else {
            $('#employee-image-valid').text('');
        }
        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            var file = $('#employee-image').get(0).files[0];
            formData.append("File", file);
            formData.append('LastName', employee.LastName);
            formData.append('FirstName', employee.FirstName);
            formData.append('Title', employee.Title);
            formData.append('TitleOfCourtesy', employee.TitleOfCourtesy);
            formData.append('BirthDate', employee.BirthDate);
            formData.append('HireDate', employee.HireDate);
            formData.append('Address', employee.Address);
            formData.append('City', employee.City);
            formData.append('Region', employee.Region);
            formData.append('PostalCode', employee.PostalCode);
            formData.append('Country', employee.Country);
            formData.append('HomePhone', employee.HomePhone);
            formData.append('Extension', employee.Extension);
            formData.append('Notes', employee.Notes);
            formData.append('ReportsTo', employee.ReportsTo);

            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Employee/Insert',
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
