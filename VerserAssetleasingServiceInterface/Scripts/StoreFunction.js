<script type="text/javascript">
    $(document).ready(function () {

        $('#mainTable').DataTable({
            "initComplete": function (settings, json) {
                $("#mainTable tr").children('td:first').click();
            }
        });
    
    $('#mainTable1').DataTable({
        stateSave: true,
    "pageLength": 5,
    "lengthMenu": [5,10, 15, 20, 25],
    "aoColumnDefs": [
                    {"bSearchable": true, "bVisible": false, "aTargets": [1] }

]
});

$('#mainTable2').DataTable();
$('#mainTable3').DataTable();
$('#mainTable4').DataTable();
$('#mainTable5').DataTable();
            if ($("#image").src == null) {
        $("#image").hide();
    }
            else {
        $("#image").show();
    }

            $('#mainTable1,#mainTable2,#mainTable3,#mainTable4,#mainTable5 tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
                else {
        $('#mainTable1,#mainTable2,#mainTable3,#mainTable4,#mainTable5').DataTable().$('tr.selected').removeClass('selected');
    $(this).addClass('selected');
}
});


});
//  ExportTimesSheetToExcel", "Candidates"
        function LoadAddressData(theId,companyName,login,addressLine1,phone,fax,email,logo,state,suburb,postcode) {
        // alert(data);
        $("#spinner").show();
    $("#txtCompany").val(companyName);
    $("#txtAddress").val(addressLine1);
    $("#txtPhone").val(phone);
    $("#txtFax").val(fax);
    $("#txtEmail").val(email);
    $("#hiddenId").val(theId);
    $("#hiddenAssetId").val(theId);
    $("#hiddenState").val(state);
    $("#hiddenSuburb").val(suburb);
    $("#txtState").val(state);
    $("#txtSuburb").val(suburb);
    $("#txtPostCode").val(postcode);
    $("#hiddenPostcode").val(postcode);
    $("#image").show();
    $("#image").attr("src", "data:image/jpeg;base64," + logo + "").height(80).width(80);

    var loadUrl = '@Url.Action("GetCompanyData", "Company")';
    var loadAssetsUrl = '@Url.Action("GetAssetsData", "Assets")';
   var loadContractsUrl = '@Url.Action("GetContractsData", "Contracts")';
    var loadCatalogueUrl = '@Url.Action("GetCatalogueData", "Catalogue")';
    var loadEndusersUrl = '@Url.Action("GetEndusersData", "Endusers")';

            $.ajax({

        type: "GET",
    url: loadUrl,
    contentType: "application/json; charset=utf-8",
                data: {"Id": theId },
    datatype: "json",
                success: function (data) {

        populateDataTable(data);
    },
                error: function () {
        alert("Dynamic content load failed.");
    }
});
var obj = null;
            $.ajax({

        type: "GET",
    url: loadAssetsUrl,
    contentType: "application/json; charset=utf-8",
                data: {"Id": theId },
    datatype: "json",
                beforeSend: function () {
        obj = $.alert({
            boxWidth: '50px',
            useBootstrap: false,
            title: '',
            content: $("#spinner").html(),
            buttons: {
                yes: { isHidden: true }
            }
        })
    },
                success: function (data) {
        $("#spinner").hide();
    obj.close();
    populateAssetsDataTable(data);
},
                error: function () {
        //  alert("Dynamic content load failed.");
    }
    });

            $.ajax({

        type: "GET",
    url: loadContractsUrl,
    contentType: "application/json; charset=utf-8",
                data: {"CompanyId": theId },
    datatype: "json",

                success: function (data) {

        populateContractsDataTable(data);
    },
                error: function () {
        alert("Dynamic content load failed.");
    }
});

            $.ajax({

        type: "GET",
    url: loadEndusersUrl,
    contentType: "application/json; charset=utf-8",
                data: {"CompanyId": theId },
    datatype: "json",

                success: function (data) {

        populateEndusersDataTable(data);
    },
                error: function () {
        alert("Dynamic content load failed.");
    }
});
}
        $('#image').click(function () {$('#imgupload').trigger('click'); });

        $('#btnCertificates').click(function () {$('#Certificateimgupload').trigger('click'); });

        function populateDataTable(data) {
        console.log("populating data table...");
    // clear the table before populating it with more data
    $("#mainTable1").DataTable().clear();
    var length = Object.keys(data).length;
            if (length == 0) {
        $('#mainTable1').dataTable().fnAddData(["", "", "", "", "", "", ""]);
    }
            else {
                for (var i = 0; i <= length; i++) {
                    var customer = data[i];

    // You could also use an ajax property on the data table initialization
    $('#mainTable1').dataTable().fnAddData([
        customer.siteName,
        customer.addressLine1,
        customer.suburb,
        customer.state,
        customer.postCode,
        customer.site_Company,
          customer.id
    ]);
}
}
}

        function populateAssetsDataTable(data) {
        console.log("populating data table...");

    // clear the table before populating it with more data
    $("#mainTable2").DataTable().clear();
       var length = Object.keys(data).length;
               if (length == 0) {
        $('#mainTable2').dataTable().fnAddData(["", "", "", "", "", "", "", "", "", "", "", "", ""]);
    }
               else {
                   for (var i = 0; i <= length; i++) {
                       var customer = data[i];

    // You could also use an ajax property on the data table initialization
    $('#mainTable2').dataTable().fnAddData([

        customer.SSN,
        customer.Make,
        customer.Model,
        customer.SerialNo,
        customer.DeviceName,
        customer.IMEI,
        customer.SIM,
        customer.Status,
        customer.CustomerName,
        customer.ConnectTechTicket,
        customer.SalesForceOpportunity,
        customer.Connote,
        new Date(moment(customer.Date)).toLocaleDateString(),
 ]);


}
}
            //$('#mainTable2').DataTable({
        //    "pageLength": 20,
        //    "lengthMenu": [10, 15, 20, 25, 50],
        //    "aoColumnDefs": [
        //        { "bSearchable": true, "bVisible": false, "aTargets": [1] }

        //    ]
        //});
    }
    function populateContractsDataTable(data) {
        console.log("populating data table...");
    // clear the table before populating it with more data
    $("#mainTable3").DataTable().clear();
    var length = Object.keys(data).length;
            if (length == 0) {
        $('#mainTable3').dataTable().fnAddData(["", "", "", "", "", "", ""]);
    }
            else {
                for (var i = 0; i <= length; i++) {
                    var customer = data[i];

    // You could also use an ajax property on the data table initialization
    $('#mainTable3').dataTable().fnAddData([
        customer.id,
        new Date(moment(customer.startDate)).toLocaleDateString(),
        new Date(moment(customer.endDate)).toLocaleDateString(),
        customer.serviceNo,
        customer.contract_Company,
        customer.contract_EndUser,
        customer.contract_Plan

    ]);


}
}
}

        function populateEndusersDataTable(data) {
        console.log("populating data table...");

    // clear the table before populating it with more data
    $("#mainTable5").DataTable().clear();
    var length = Object.keys(data).length;
            if (length == 0) {
        $('#mainTable5').dataTable().fnAddData(["", "", "", "", "", "", "", "", "", ""]);
    }
            else {
                for (var i = 0; i <= length; i++) {
                    var customer = data[i];

    // You could also use an ajax property on the data table initialization
    $('#mainTable5').dataTable().fnAddData([
        customer.Id,
        customer.FirstName,
        customer.LastName,
        customer.EmployeeNo,
        customer.Department,
        customer.CostCode,
        customer.EndUserStatus,
        new Date(moment(customer.CommencementDate)).toLocaleDateString(),
        new Date(moment(customer.TerminationDate)).toLocaleDateString(),
        customer.UserName
    ]);

}
}
}

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
        $('#image')
            .attr('src', e.target.result);
    };

    reader.readAsDataURL(input.files[0]);
}
}

$('#Certificateimgupload').on('change', function (event) {
var formdata = new FormData(); //FormData object
    var fileInput = document.getElementById('Certificateimgupload');
    //Iterating through each files selected in fileInput
for (i = 0; i < fileInput.files.length; i++) {
        //Appending each file to FormData object
        formdata.append(fileInput.files[i].name, fileInput.files[i]);
    }
    
    formdata.append("FileUpload", "CertificateUpload");
    //Creating an XMLHttpRequest and sending
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Assets/ImportAssets');
    xhr.send(formdata);
xhr.onreadystatechange = function (data) {
if (xhr.readyState == 4 && xhr.status == 200) {
        $.confirm({

            content: 'The file has been uploaded successfully.',
            boxWidth: '370px',
            useBootstrap: false,
            buttons: {
                OK: function () {

                }
            }
        });
    }
    }
    });
    
$('#updateBtn').on('click', function (e) {
        {
            // alert(data);
            var CompanyEdit = {};

            CompanyEdit.companyName = $("#txtCompany").val();
            CompanyEdit.addressLine1 = $("#txtAddress").val();
            CompanyEdit.phone = $("#txtPhone").val();
            CompanyEdit.fax = $("#txtFax").val();
            CompanyEdit.email = $("#txtEmail").val();
            CompanyEdit.logo = $("#image")[0].src.split(',')[1];
            CompanyEdit.Id = $("#hiddenId").val();
            CompanyEdit.state = $("#hiddenState").val();
            CompanyEdit.suburb = $("#hiddenSuburb").val();
            e.preventDefault();
            e.stopPropagation();

            var loadUrl = '@Url.Action("UpdateCompanyData", "Company")';

            $.ajax({

                type: "POST",
                url: loadUrl,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(CompanyEdit),
                datatype: "json",
                success: function (result) {
                    debugger;
                    $("#txtCompany").text(result);
                },
                error: function () {
                    // alert("Dynamic content load failed.");
                }
            });
        }
    });
                            $('#addSite').click(function () {
                            var AddSiteURL = '@Url.Action("AddSite", "Sites")'
                            $.ajax({
        type: "GET",
    url: AddSiteURL,

                            success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            },
        });
    }
    });
    });
                        $('#editSite').click(function () {
                        var updateSiteURL = '@Url.Action("UpdateSite", "Sites")'
                        $.ajax({
        type: "GET",
    url: updateSiteURL,

                        success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            },
            onOpen: function () {
                // after the modal is displayed.
                var table = $('#mainTable1').DataTable();
                var selData = table.rows(".selected").data();
                $(".jconfirm").find($("#siteName")).val(selData[0][0]);
                $(".jconfirm").find($("#address1")).val(selData[0][1]);
                $(".jconfirm").find($("#suburb")).val(selData[0][2]);
                $(".jconfirm").find($("#state")).val(selData[0][3]);
                $(".jconfirm").find($("#postcode")).val(selData[0][4]);
                $(".jconfirm").find($("#siteCompany")).val(selData[0][5]);
                $(".jconfirm").find($("#siteId")).val(selData[0][6]);
            }
        });
    }
    });

    });
                        $('#addEndUser').click(function () {

                        var AddSiteURL = '@Url.Action("AddEnduser", "RegisterEndUsers")'
                        $.ajax({
        type: "GET",
    url: AddSiteURL,

                        success: function (data) {
        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            },
        });

    }
    });
    });
$('#editEndUser').click(function () {
var updateSiteURL = '@Url.Action("UpdateEndUser", "RegisterEndusers")'
    var table1 = $('#mainTable5').DataTable();
    var selData1 = table1.rows(".selected").data();
    
$.ajax({
        type: "GET",
    url: updateSiteURL,
data: {"Id": (selData1[0][0]) },
success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            }

        });


    },
error: function (data) {

        alert("Data cannot be loaded.");


    }
    });
    
    });
$('#addAsset').click(function () {
var AddSiteURL = '@Url.Action("AddAssets", "RegisterAssets")'
$.ajax({
        type: "GET",
    url: AddSiteURL,
    
success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            },
        });

    }
    });
    });
$('#editAsset').click(function () {
var updateSiteURL = '@Url.Action("UpdateAssets", "RegisterAssets")'
    var table1 = $('#mainTable2').DataTable();
    var selData1 = table1.rows(".selected").data();
$.ajax({
        type: "GET",
    url: updateSiteURL,
data: {"AssetId": (selData1[0][0]), "companyId": $("#hiddenId").val()},
success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            }

        });


    }
    });
    
    });
        $('#addContract').click(function ()
    {
var AddContractURL = '@Url.Action("AddContract", "RegisterContract")'
$.ajax({
        type: "GET",
    url: AddContractURL,
    
success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            }
        });

    }
    });
    });
    $('#editContract').click(function () {
    var table1 = $('#mainTable3').DataTable();
    var selData1 = table1.rows(".selected").data();
    var updateSiteURL = '@Url.Action("UpdateContract", "RegisterContract")'
    $.ajax({
        type: "GET",
    url: updateSiteURL,
    data: {"Id": (selData1[0][0])},
    success: function (data) {

        $.confirm({
            boxWidth: '800px',
            useBootstrap: false,
            title: '',
            content: data,
            closeIcon: true,

            buttons: {
                yes: { isHidden: true }
            }

        });
    },
    error: function (data) {

        alert("Data cannot be loaded.");
    }
    });
    });
        function getAge(dateString) {

            var val = $("#txtDOB").val();
    var birthYear = val.split('-')[2];
    var birthMonth = val.split('-')[1];
    var birthDay = val.split('-')[0];


    todayDate = new Date();
    todayYear = todayDate.getFullYear();
    todayMonth = todayDate.getMonth();
    todayDay = todayDate.getDate();
    age = todayYear - birthYear;

            if (todayMonth < birthMonth - 1) {
        age--;
    }

            if (birthMonth - 1 == todayMonth && todayDay < birthDay) {
        age--;
    }
    $("#txtAge").val(age);
//            return age;
}
    </script>