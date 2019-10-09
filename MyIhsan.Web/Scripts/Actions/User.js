
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
});

function drawUserDatatable(first) {
    if (!first) {
        var table = $('#drawTable');
        table.empty();
        table.append('<table id="myDatatable" class="table table-striped table-bordered table-advance table-hover" width="100%" cellspacing="0"><thead> <tr> <th>' + gridHeader[0] + '</th> <th>' + gridHeader[2] + '</th> <th>' + gridHeader[3] + '</th> <th>' + gridHeader[4] + '</th> <th class="all">' + gridHeader[5] +'</th></tr> </thead> </table>');
    }
    var param = { "UserName": $('#userNametxt').val(), "Name": $('#ddl_Name').val(), "EntityInfo": $('#ddl_EntityInfoSelect').val() };
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
                "data": "result.data.id", render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            //{ "data": gridcolumns["name"], "name": "Name", "autoWidth": true },
            { "data": "UserName", "name": "UserName", "autoWidth": true },
            { "data": "Email", "name": "Email", "autoWidth": true },
            { "data": "Roles", "name": "Name", "autoWidth": true },
            {
                "data": "Id", "orderable": false, "autoWidth": true, "render": function (data, type, row) {
                    /*return '<button id="' + row.Id + '" data-id="' + data + '" onclick="AddorUpdateDepartment(this)" class="btn btn-link btn-Department text-primary" data-toggle="tooltip" data-placement="top"  title="' + addDept+'"><i class="fas fa-plus-circle"></i></button>'*/
                        //+
                        //'<button id="' + row.Id + '" data-id="' + data + '" onclick="AddorUpdateEntity(this)" class="btn btn-link btn-Entity text-primary" data-toggle="tooltip" data-placement="top" title="'+addEntity+'"><i class="fas fa-file-medical"></i></button>'
                        //+
                    return '<button id="' + row.Id + '" data-id="' + data + '" onclick="UpdateUser(this)" class="btn btn-link btn-Update  text-primary" data-toggle="tooltip" data-placement="top"  title="'+edit+'"><i class="fas fa-edit"></i></button>'
                        +
                        '<button id="' + row.Id + '" onclick="deleteClick(this)" class="btn btn-link btn-Delete  text-danger" data-toggle="tooltip" data-placement="top"  title="'+remove+'"><i class="fas fa-trash-alt "></i></button>'
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
    cleartxt();
    $('#model').modal('show');
});
function UpdateUser(obj) {
    var id = $(obj).attr('Id');
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                $.ajax({
                    url: `${getUser}/${id}`,
                    type: 'get',
                    cache: false,
                    success: function (data) {
                        cleartxt();
                        $('#hiddenId').val(data.Id);
                        $('#nametxt').val(data.Name);
                        $('#nameEntxt').val(data.NameEn);
                        $('#usernametxt').val(data.UserName);
                        $('#emailtxt').val(data.Email);
                        $('#phonetxt').val(data.PhoneNumber);
                        $('#ddl_Country').val(data.CountryId);
                        var $newOption = $("<option selected='selected'></option>").val(data.CountryId).text(data.CountryName);
                        $("#ddl_Country").append($newOption).trigger('change');
                        $('#ddl_EntityInfo').val(data.EntityIdInfo);
                        var $newOption1 = $("<option selected='selected'></option>").val(data.EntityIdInfo).text(data.EntityIdInfoName);
                        $("#ddl_EntityInfo").append($newOption1).trigger('change');

                        $('#teltxt').val(data.TelNumber);
                        $('#addresstxt').val(data.Address);
                        var $newOption2 = $("<option selected='selected'></option>").val(data.Gender === true ? '1' : '0').text(data.GenderName);
                        //data.GenderType
                        $("#ddl_Gender").append($newOption2).trigger('change');
                        $('#model').modal('show');
                    }
                });
            } else {
                location.reload();
            }
        }
    });
}
function AddorUpdateEntity(obj) {
    var id = $(obj).attr('Id');
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                sessionStorage.setItem("UserIdItem", id);
                $.ajax({
                    url: `${getUserEntity}/${id}`,
                    type: 'get',
                    cache: false,
                    success: function (res) {
                        $("#ddl_Entities").empty();
                        $("#ddl_Entities").select2('val', '');
                        $.each(res, function (k, v) {
                            var $newOption = $("<option selected='selected'></option>").val(v.id).text(v.text);
                            $("#ddl_Entities").append($newOption).trigger('change');
                        });
                        $('#myEntityModal').modal('show');
                    }
                });
            } else {
                location.reload();
            }
        }
    });
}
function deleteClick(obj) {
    var rowID = $(obj).attr('Id');
    sessionStorage.setItem("delId", rowID)
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
function cleartxt() {
    $('#popupForm').closest('form').find("input[type=text],input[type=password], textarea").val("");
    $('#hiddenId').val('');
}
$(document).ready(function () {
   
    $('#ddl_Country').select2({
        ajax: {
            url: optionListUrl2,
            dataType: 'json',
            data: function (params) {
                params.page = params.page || 1;
                return {
                    pageSize: pageSize,
                    pageNumber: params.page,
                    searchTerm: params.term
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
    $('#ddl_Entities').select2(
        {
            ajax: {
                url: optionListUrl1,
                sync: false,
                dataType: 'json',
                headers: {
                    'Authorization': `Bearer ${$.cookie("tokenA")}`
                },
                data: function (params) {
                    params.page = params.page || 1;
                    return {
                        searchTerm: params.term,
                        pageSize: pageSize,
                        pageNumber: params.page,
                        Lang: lang
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
    cmdLookupLoad($('#ddl_EntityInfo'), "EntityInfo");
    cmdLookupLoad($('#ddl_Gender'),"Genders");
    
});
function cmdLookupLoad(s, name) {
    /// ===========
    s.select2(
        {
            ajax: {
                url: optionListUrl100,
                dataType: 'json',
                data: function (params) {
                    params.page = params.page || 1;
                    return {
                        pageSize: pageSize,
                        pageNumber: params.page,
                        lookupTypeName: name,
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
    ///=============
}
function AddorUpdateDepartment(obj) {
    var id = $(obj).attr('Id');
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                sessionStorage.setItem("UserIdItem", id);
                $.ajax({
                    url: `${getUserDept}/${id}`,
                    type: 'get',
                    cache: false,
                    success: function (res) {
                        $("#ddl_Departments").empty();
                        $("#ddl_Departments").select2('val', '');
                        $.each(res, function (k, v) {
                            var $newOption = $("<option selected='selected'></option>").val(v.id).text(v.text);
                            $("#ddl_Departments").append($newOption).trigger('change');
                        });
                        $('#myDepartmentModal').modal('show');
                    }
                });
            } else {
                location.reload();
            }
        }
    });
}
$('#saveDepartmentBtn').on('click', function () {
    jQuery.ajax({
        url: sessionExpireUrl,
        success: function (result) {
            if (result) {
                //if ($('#ddl_Departments').val().length == 0) {
                //    toastr.warning("برجاء اختيار قسم");
                //    return;
                //}
                var param = { "UserId": sessionStorage.getItem("UserIdItem"), "AssignedEntity": $('#ddl_Departments').val() };
                $.ajax({
                    url: saveUserDept,
                    type: 'post',
                    cache: false,
                    data: param,
                    success: function (res) {
                        console.log(res);
                        if (res.data.isSuccess) {
                            toastr.success(savedSuccessfully);
                            sessionStorage.removeItem("UserIdItem");
                            $('#myDepartmentModal').modal('hide');
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