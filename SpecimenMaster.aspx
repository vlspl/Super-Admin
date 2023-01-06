<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="SpecimenMaster.aspx.cs" Inherits="SpecimenMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript">
     function showModal() {
         $("#myModal").modal('show');
     }

 </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function goBack() {
         var url = 'SpecimenMaster.aspx';
         window.location.href = url;
     }
 </script>
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Howzu Says</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblMessage" runat="server"></asp:Label>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="goBack()"  runat="server" Text="Closed"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Specimen Master </a>
      </div>

      <div class="mr-5">
       
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group mx-sm-3 mb-2 analytewrap">
                        <asp:TextBox ID="txtSampleType" onkeypress="return onlyAlphabets(event,this);" class="form-control"
                    placeholder="Sample Type" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                  
                <asp:TextBox ID="txtQuantity" class="form-control" placeholder="Quantity" runat="server"
                    ClientIDMode="Static"></asp:TextBox>
                   </div>
                  <div class="col-md-2">
                    <asp:TextBox ID="txtTimePeriod" class="form-control" placeholder="Time Period" runat="server"
                    ClientIDMode="Static"></asp:TextBox>
                   </div>
                   <asp:Button ID="btnAddSpecimen" runat="server" class="btn btn-color mb-2" OnClientClick="javascript:return specimenValidate()"
                    OnClick="btnAddSpecimen_click" Text="Add Specimen" />
            </div>
        </div>

        <div class="container">
        <div class="row">
          
            <div class="col-lg-12">
  <div class="table-responsive">
                        <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                            <thead>
                                <tr>
                                 <th style="text-align:center;">
                                        Sr No
                                    </th>
                                    <th style="text-align:center;">
                                      Specimen
                                    </th>
                                    <th style="text-align:center;">
                                        Quantity
                                    </th>
                                    <th style="text-align:center;">
                                        Time Period
                                    </th>
                                     <th style="text-align:center;">
                                        Delete
                                    </th>
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodySpecimen" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>


        
    </div>
  
 
    <!-- Modal Edit Specimen Start-->
    <div id="modalEditSpecimen" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">  <h4 class="modal-title">
                        Edit Specimen</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                  
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <asp:HiddenField ID="hiddenSpecimenId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txtEditSampleType" class="form-control" placeholder="Sample Type"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtEditQuantity" class="form-control" placeholder="Quantity" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtEditTimePeriod" class="form-control" placeholder="Time Period"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateSpecimen" class="btn btn-submit" runat="server" Text="Update"
                        OnClientClick="javascript:return editSpecimenValidate()" OnClick="btnUpdateSpecimen_click"
                        ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Edit Analyte End-->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/SpecimenMasterValidation.js"></script>
      <script src="js/code.js"></script>
    <script src="js/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFilterSearch").keyup(function () {
                var input, filter, table, tr, td, i;
                input = document.getElementById("txtFilterSearch");
                filter = input.value.toUpperCase();

                table = document.getElementById("tbodySpecimen");
                tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                    td = tr[i].getElementsByTagName("td")[0];

                    if (td) {
                        if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {
                            tr[i].style.display = "none";
                        }
                    }
                }
            });
        });

        function removeSpecimen(obj) {

            if (confirm("Are you sure you want to delete?")) {
                var rowId = "rowSpecimen" + $(obj).attr('id');
                $("#" + rowId).hide();

                var parameter = { "specimenId": $(obj).attr('id') };
                $.ajax({
                    type: "POST",
                    url: "SpecimenMaster.aspx/removeSpecimen",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {

                        if (response.d == "0") {
                            $("#" + rowId).show();
                        }
                        else if (response.d == "2") {
                            $("#" + rowId).show();
                            alert("This specimen is being used in an existing test, hence it can't be deleted");
                        }

                        console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
                location.reload();
            }
            else
            { }
        }

        function updateSpecimen(obj) {
            var specimenId = $(obj).attr('id');
            $("#hiddenSpecimenId").val($("#hidden" + specimenId).val().split('|')[0]);
            $("#txtEditSampleType").val($("#hidden" + specimenId).val().split('|')[1]);
            $("#txtEditQuantity").val($("#hidden" + specimenId).val().split('|')[2]);
            $("#txtEditTimePeriod").val($("#hidden" + specimenId).val().split('|')[3]);
            $("#modalEditSpecimen").modal('show');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#page li").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
</script>
</asp:Content>
