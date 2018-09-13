$(document).ready(function () {
    $('#delete-button').click(function (e) {
        e.preventDefault();
        var self = $('#delete-button').val();

        if (confirm('Are you sure you want to delete this?')) {
            $.ajax({
                url: '/Admin/Employee/Delete?id=' + self,
                type: "POST",
                data: '{id : ' + self + '}',
                success: function (data) {
                    if (data.Ok) {
                        
                        alert('Delete successfully!!');
                        $('#data-table').DataTable().ajax.reload();
                    }
                    else {
                        alert('Delete failed!!');
                    }
                },
                error: function () {
                    alert('Server error!!');
                }
            });
        }
    })
})