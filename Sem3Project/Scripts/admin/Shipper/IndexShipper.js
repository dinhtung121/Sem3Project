$(document).ready(function () {

    if ($.fn.DataTable.isDataTable('#data-table')) {
        $('#data-table').dataTable().fnDestroy();
        $('#data-table').dataTable().empty();

    }
    var count = 0;
    var complete = $('#data-table').DataTable(
        {

            "serverSide": true,
            "destroy": true,
            "processing": true,
            "ajax":
                {
                    url: "/Admin/Shipper/GetShipper",
                    method: "POST",
                    dataType: 'json'
                },
            "columns":
                [
                    { "data": "ShipperID", "searchable": true },
                    { "data": "CompanyName", "searchable": true },
                    { "data": "Phone", "searchable": true },
                    {
                        "render": function (data, type, full, meta) {
                            count++;
                            return '<a class="btn btn-lg btn-primary" href="#" onClick="showEdit(' + full.ShipperID + ')" title="Edit this shipper"><span class="glyphicon glyphicon-edit"><i></i></span></a>' +
                                '&nbsp;&nbsp;<a class="btn btn-lg btn-success" onClick="showDetail(' + full.ShipperID + ')" href="#" title="See details this shipper"><span class="glyphicon glyphicon-align-justify"></span></a>' +
                                '&nbsp;&nbsp;<button class="btn btn-lg btn-danger delete-button"  value="' + full.ShipperID + '" title="Delete this shipper"><span class="glyphicon glyphicon-trash"></span></button>';
                        }
                    }
                ]
        }

    );

    //function convertNETDateTime(sNetDate) {
    //    if (sNetDate == null) return null;
    //    if (sNetDate instanceof Date) return sNetDate;
    //    var r = /\/Date\(([0-9]+)\)\//i
    //    var matches = sNetDate.match(r);
    //    if (matches.length == 2) {
    //        return new Date(parseInt(matches[1]));
    //    }
    //    else {
    //        return sNetDate;
    //    }
    //};
    //function convert(str) {
    //    var date = new Date(str),
    //        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    //        day = ("0" + date.getDate()).slice(-2);
    //    return [day, mnth, date.getFullYear()].join("-");
    //};
    var itm = $("#data-table_filter input");

    itm.unbind();
    itm.keyup(function (e) {
        //enter or tab0
        if (e.keyCode == 13 || this.value.length >= 3) {
            complete.search(this.value).draw();
        }
        if (this.value.length == 0) {
            complete.search(this.value).draw();
        }
    });

    $('#data-table').on('click', 'button', function (e) {
        e.preventDefault();
        var self = this.value;

        if (confirm('Are you sure you want to delete this?')) {
            $.ajax({
                url: '/Admin/Shipper/Delete?id=' + self,
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
    });
//iiii


});

