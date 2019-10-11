
var ddl_BasicScreenOpen = 1;
var ddl_SecondScreenOpen = 1;
$(document).ready(function () {
    $('#collapseTwo').collapse();
    drawUserDatatable(true);
    $('#search').on('click', function () {
        jQuery.ajax({
            url: sessionExpireUrl,
            success: function (result) {
                if (result) {
                    drawUserDatatable(false);
                } else {
                    location.reload();
                }
            }
        });
    });
    //=================================
    $('#ddl_Users').select2({
        ajax: {
            url: optionListUrl1,
            sync: false,
            dataType: 'json',
            headers: {
                'Authorization': `Bearer ${$.cookie("token")}`
            },
            data: function (params) {
                params.page = params.page || 1;
                return {
                    searchTerm: params.term,
                    pageSize: pageSize,
                    pageNumber: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                return {
                    results: data.results,
                    pagination: {
                        more: (params.page * pageSize) < data.total
                    }
                };
            }
        },
        placeholder: select2Placeholder,
        minimumInputLength: 0,
        allowClear: true, multiple: true,
        maximumSelectionLength: 250,
        language: select2Lang

    }).select2('val', '');
    //=================================  
    //=================================
    $('#ddl_BasicScreen').select2(
        {
            ajax: {
                url: baseUrl + "Page/GetScreensSelect2",
                dataType: 'json',
                headers: {
                    'Authorization': `Bearer ${$.cookie("token")}`
                },
                data: function (params) {
                    params.page = params.page || 1;
                    return {
                        pageSize: pageSize,
                        pageNumber: params.page,
                        searchTerm: params.term,
                        lang:lang
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    return {
                        results: data.results,
                        pagination: {
                            more: (params.page * pageSize) < data.total
                        }
                    };
                }
            },
            placeholder: select2Placeholder,
            minimumInputLength: 0,
            allowClear: true,
            language: select2Lang
        });
    //=================================
    $('#ddl_SecondScreen').select2(
        {
            ajax: {
                url: baseUrl + "Page/GetChildScreensSelect2",
                dataType: 'json',
                headers: {
                    'Authorization': `Bearer ${$.cookie("token")}`
                },
                data: function (params) {
                    params.page = params.page || 1;
                    return {
                        pageSize: pageSize,
                        pageNumber: params.page,
                        parentId: $('#ddl_BasicScreen').val(),
                        searchTerm: params.term,
                        lang: lang
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    return {
                        results: data.results,
                        pagination: {
                            more: (params.page * pageSize) < data.total
                        }
                    };
                }
            },
            placeholder: select2Placeholder,
            minimumInputLength: 0,
            allowClear: true,
            language: select2Lang
        });
    //=================================
    
});

function drawUserDatatable(first) {
    if (!first) {
        var table = $('#drawTable');

        table.empty();
        table.append(' <div id="drawTable" class=" "> <table id="myDatatable" class="table table-striped table-bordered dt-responsive nowrap" width="100%" cellspacing="0"> <thead > <tr> <th>' + gridHeader[0] + '</th> <th>' + gridHeader[1] + '</th> <th>' + gridHeader[2] + '</th> <th class="all">' + gridHeader[3] +'</th></tr> </thead> </table> </div>');
    }
    var param = { "RoleName": $('#nametxt').val() };
    var oTable = $('#myDatatable').DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once          
        "language": dataTableLang,
        "ajax": {
            "url": serverUri,
            "type": "POST",
            "datatype": "json",
            "data": param
        },
        "columns": [
            {
                "data": "Id", render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "Name", "name": "Name", "autoWidth": true },
            { "data": "AspNetUsersRoleCount", "name": "Id", "autoWidth": true },
            {
                "data": "Id", "orderable": false, "render": function (data, type, row) {
                    return '<button id="' + row.Id + '"onclick="addClick(this)" class="btn btn-link btn-Add text-primary" data-toggle="tooltip" data placement="top" title="' + addUsers+'" ><i class="fas fa-user-plus"></i></button >'
                        +
                        '<button id="' + row.Id + '" data-id="' + data + '"onclick="ScreenClick(this)" class="btn btn-link btn-Screen text-primary" data-toggle="tooltip" data-placement="top" title="' + addPages+'"><i class="fa fa-tasks "></i></button>'
                        //+
                        //'<button id="' + row.Id + '"onclick="EntityClick(this)" class="btn btn-link text-primary btn-Entity" data-toggle="tooltip" data-placement="top" title="' + addEntity+'"><i class="fas fa-file-medical "></i></button>'
                        +
                        '<button id="' + row.Id + '"onclick="UpdateClick(this)" class="btn btn-link text-primary btn-Update"  data-toggle="tooltip" data-placement="top"title="'+edit+'"><i class="fas fa-edit"></i></button>'
                        +
                        '<button id="' + row.Id + '" onclick="deleteClick(this)" class="btn btn-link btn-Delete text-danger"  data-toggle="tooltip" data-placement="top" title="'+remove+'"><i class="fas fa-trash-alt "></i></button>'
                        ;

                }
            },
        ],
        "initComplete": function () {
            $('[data-toggle="tooltip"]').tooltip();

        }
    }).on('draw', function () {
        $('[data-toggle="tooltip"]').tooltip();

    });
}

$('#addbtn').on('click', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                $.ajax({
                    url: saveUri,
                    type: 'get',
                    cache: false,
                    success: function (value) {
                        $('#partialView').empty();
                        $('#partialView').html(value);
                    }
                }).done(function () {
                    $('#model').modal('show');
                    $('#nametxt').focus();
                });
            } else {
                location.reload();
            }
        }
    });

});

function addClick(obj) {
    var id = $(obj).attr('Id');
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                sessionStorage.setItem("RoleIdItem", id);
                $.ajax({
                    url: `${getUserRole}/${id}`,
                    type: 'get',
                    headers: {
                        'Authorization': `Bearer ${$.cookie("token")}`
                    },
                    success: function (res) {
                        $("#ddl_Users").empty();
                        $("#ddl_Users").select2('val', '');
                        $.each(res, function (k, v) {
                            var $newOption = $("<option selected='selected'></option>").val(v.id).text(v.text);
                            $("#ddl_Users").append($newOption).trigger('change');
                        });
                        $('#myUserModal').modal('show');
                    }
                });
            } else {
                location.reload();
            }
        }
    });
}

function UpdateClick(obj) {
    var id = $(obj).attr('Id');
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                $.ajax({
                    url: `${saveUri}/${id}`,
                    type: 'get',
                    cache: false,
                    success: function (value) {
                        $('#partialView').empty();
                        $('#partialView').html(value);
                    }
                }).done(function () {
                    $('#model').modal('show');
                    $('#nametxt').focus();
                });
            } else {
                location.reload();
            }
        }
    });
}

function ScreenClick(obj) {
    var id = $(obj).attr('Id');
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                sessionStorage.setItem("RoleIdItem", id);
                ddl_BasicScreenOpen = 1; ddl_SecondScreenOpen = 1;
                $('#ddl_BasicScreen').select2('val', '');
                $('#ddl_SecondScreen').select2('val', '');
                RenderData(id);
                $('#myScreenModal').modal('show');
            } else {
                location.reload();
            }
        }
    });
}
function deleteClick(obj) {
    var rowID = $(obj).attr('Id');
    sessionStorage.setItem("delId", rowID);
    $('#myModal').modal('show');
}

$('#delBtn').on('click', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                $.ajax({
                    url: deleteUri + "/" + sessionStorage.getItem("delId"),
                    type: 'get',
                    cache: false,
                    success: function (value) {
                        if (value) {
                            location.reload();
                        } else {
                            toastr.warning(cannotRemove);
                        }
                        sessionStorage.removeItem("delId");
                    }
                });
            } else {
                location.reload();
            }
        }
    });
});

$('#saveBtn').on('click', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                //if ($('#ddl_Users').val().length == 0) {
                //    toastr.warning("برجاء اختيار مستخدمين");
                //    return;
                //}
                var param = { "RoleId": sessionStorage.getItem("RoleIdItem"), "AssignedUser": $('#ddl_Users').val() };
                $.ajax({
                    url: saveUserRole,
                    type: 'post',
                    headers: {
                        'Authorization': `Bearer ${$.cookie("token")}`
                    },
                    data: param,
                    success: function (res) {
                        if (res.data) {
                            toastr.success(savedSuccessfully);
                            sessionStorage.removeItem("RoleIdItem");
                            $('#myUserModal').modal('hide');
                        } else {
                            toastr.warning(Retry);
                        }

                    }
                });
            } else {
                location.reload();
            }
        }
    });

});
var table;
var table1;
function RenderData(id) {
    $('#drawmyDataTable').empty();
    $('#drawmyDataTable').append('<table id="myDatatableS" class="table table-bordered table-striped" cellspacing="0" width="100%"> <thead> <tr><th>' + gridHeader[4]+'</th></tr> </thead> </table>');
    $('#drawmyDataTable1').empty();
    $('#drawmyDataTable1').append('<table id="myDatatableS1" class="table table-bordered table-striped" cellspacing="0" width="100%"> <thead> <tr><th>' + gridHeader[4] +'</th></tr> </thead> </table>');
    table = $('#myDatatableS').DataTable({
        "language": dataTableLang,
        "ajax": {
            "url": `${baseUrl}Page/GetScreenData/${id}/${$('#ddl_BasicScreen').val()}`,
            "headers": {
                'Authorization': `Bearer ${$.cookie("token")}`
            },
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                { "data": screenNameCol, "autoWidth": true }
            ],
        "processing": true,
        'select': 'multi',
        'order': [[0, 'asc']],
        "info": false
    });
    table1 = $('#myDatatableS1').DataTable({
        "language": dataTableLang,
        "ajax": {
            "url": `${baseUrl}Page/GetScreenDataSelected/${id}/${$('#ddl_BasicScreen').val()}`,
            "headers": {
                'Authorization': `Bearer ${$.cookie("token")}`
            },
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                { "data": screenNameCol, "autoWidth": true }
            ],
        "processing": true,
        'select': 'multi',
        'order': [[0, 'asc']],
        "info": false
    });
}
$('#saveScreenBtn').on('click', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                var menuIds = [];
                var i = 0;
                $.each(table.rows('.selected').data(), function (k, v) {
                    menuIds[i] = v.id; i++;
                });
                var menuIdsSelected = [];
                var j = 0;
                $.each(table1.rows('.selected').data(), function (k, v) {
                    menuIdsSelected[j] = v.id; j++;
                });
                if (table.rows('.selected').data().length === 0 && table1.rows('.selected').data().length === 0) {
                    toastr.warning(choose);
                    return;
                }
                var param = { "RoleId": sessionStorage.getItem("RoleIdItem"), "ScreenAssigned": menuIds, "ScreenAssignedRemove": menuIdsSelected };
                $.ajax({
                    url: baseUrl + "Page/SaveScreens",
                    type: 'post',
                    headers: {
                        'Authorization': `Bearer ${$.cookie("token")}`
                    },
                    data: param,
                    success: function (res) {
                        console.log(res);
                        if (res.data) {
                            toastr.success(savedSuccessfully);
                            $('#ddl_BasicScreen').select2('val', '');
                            $('#ddl_SecondScreen').select2('val', '');
                            //sessionStorage.removeItem("RoleIdItem");
                            //$('#myScreenModal').modal('hide');
                        } else {
                            toastr.warning(Retry);
                        }

                    }
                });
                //alert(table.rows('.selected').data().length + ' row(s) selected');
            } else {
                location.reload();
            }
        }
    });
});
$('#ddl_BasicScreen').on('change', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                if (ddl_BasicScreenOpen == 0) {
                    $.ajax({
                        url: baseUrl + "Page/GetScreenData/" + sessionStorage.getItem("RoleIdItem") + "/" + $('#ddl_BasicScreen').val(),
                        type: "get",
                        headers: {
                            'Authorization': `Bearer ${$.cookie("token")}`
                        },
                        success: function (result) {
                            table.clear().draw();
                            table.rows.add(result.data); // Add new data
                            table.columns.adjust().draw(); // Redraw the DataTable
                        }
                    });
                    $.ajax({
                        url: baseUrl + "Page/GetScreenDataSelected/" + sessionStorage.getItem("RoleIdItem") + "/" + $('#ddl_BasicScreen').val(),
                        type: "get",
                        headers: {
                            'Authorization': `Bearer ${$.cookie("token")}`
                        },
                        success: function (result) {
                            table1.clear().draw();
                            table1.rows.add(result.data); // Add new data
                            table1.columns.adjust().draw(); // Redraw the DataTable
                        }
                    });
                }
                ddl_BasicScreenOpen = 0;
                //RenderData(sessionStorage.getItem("RoleIdItem"));
            } else {
                location.reload();
            }
        }
    });
});
$('#ddl_SecondScreen').on('change', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                if (ddl_SecondScreenOpen == 0) {
                    $.ajax({
                        url: baseUrl + "Page/GetScreenData/" + sessionStorage.getItem("RoleIdItem") + "/" + $('#ddl_BasicScreen').val() + "/" + $('#ddl_SecondScreen').val(),
                        type: "get",
                        headers: {
                            'Authorization': `Bearer ${$.cookie("token")}`
                        },
                        success: function (result) {
                            table.clear().draw();
                            table.rows.add(result.data); // Add new data
                            table.columns.adjust().draw(); // Redraw the DataTable
                        }
                    });
                    $.ajax({
                        url: baseUrl + "Page/GetScreenDataSelected/" + sessionStorage.getItem("RoleIdItem") + "/" + $('#ddl_BasicScreen').val() + "/" + $('#ddl_SecondScreen').val(),
                        type: "get",
                        headers: {
                            'Authorization': `Bearer ${$.cookie("token")}`
                        },
                        success: function (result) {
                            table1.clear().draw();
                            table1.rows.add(result.data); // Add new data
                            table1.columns.adjust().draw(); // Redraw the DataTable
                        }
                    });
                }
                ddl_SecondScreenOpen = 0;
                //RenderData(sessionStorage.getItem("RoleIdItem"));
            } else {
                location.reload();
            }
        }
    });
});