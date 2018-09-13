
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

        let employee = {};
        employee.EmployeeID = $('#edit').val();
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
        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            if (image != '') {
                var file = $('#employee-image').get(0).files[0];
                formData.append("File", file);
            }

            formData.append('EmployeeID', employee.EmployeeID);
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
                url: '/Admin/Employee/Update',
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

