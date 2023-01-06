<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="AnalyteMaster.aspx.cs" Inherits="AnalyteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript">
     function showModal() {
         $("#myModal").modal('show');
     }

     </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function goBack() {
         var url = 'AnalyteMaster.aspx';
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
        <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="goBack()"  runat="server" Text="Close"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Analyte Master </a>
      </div>

      <div class="mr-7">
       
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
          <div class="row">
           <div class="col-md-6">
            <div class="form-group mx-sm-3 mb-2 analytewrap">
                <label for="analyte" class="sr-only">
                    Password</label>
                <asp:TextBox ID="txtAnalyteName" class="form-control" placeholder="Analyte"  runat="server"
                    ClientIDMode="Static"></asp:TextBox>
                    </div>
            </div>
            <asp:Button ID="btnAddAnalyte" runat="server" class="btn btn-color mb-2"  OnClientClick="javascript:return analyteValidate()"
                OnClick="btnAddAnalyte_click" ClientIDMode="Static" Text="Add Analyte" />
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
                                       Analyte Name
                                    </th>
                                    <th style="text-align:center;">
                                        Delete
                                    </th>
                                   
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodyAnalyte" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>


            
        </div>
    </div>
   
   
    <!-- Modal Edit Analyte Start-->
    <div id="modalEditAnalyte" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">  <h4 class="modal-title">
                        Edit Analyte</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                  
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <asp:HiddenField ID="hiddenAnalyteId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox class="form-control" placeholder="Enter Analyte *" ID="txtAnalyte" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateAnalyte" class="btn btn-submit" runat="server" Text="Update"
                        OnClientClick="javascript:return editAnalyteValidate()" OnClick="btnUpdateAnalyte_click"
                        ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Edit Analyte End-->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/AnalyteMasterValidation.js"></script>
    <script src="js/code.js"></script>
    <script src="js/pagination.js"></script>
    <script type="text/javascript">
       

        function removeAnalyte(obj) {

            if (confirm("Are you sure you want to delete ?")) {
                var rowId = "rowAnalyte" + $(obj).attr('id');
                $("#" + rowId).hide();

                var parameter = { "analyteId": $(obj).attr('id') };
                $.ajax({
                    type: "POST",
                    url: "AnalyteMaster.aspx/removeAnalyte",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {

                        if (response.d == "1") {
                            $("#" + rowId).show();
                            alert("Deleted Successfully.");

                        }
                        else if (response.d == "2") {
                            $("#" + rowId).show();
                            alert("This analyte has been defined in an existing test, hence it can't be deleted");
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
            else {

            }
        }

        function updateAnalyte(obj) {
            var analyteId = $(obj).attr('id');
            $("#hiddenAnalyteId").val($("#hidden" + analyteId).val().split('|')[0]);
            $("#txtAnalyte").val($("#hidden" + analyteId).val().split('|')[1]);
            $("#modalEditAnalyte").modal('show');
        }
    </script>
    <script type="text/javascript" src="js/jquery.js"></script>
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
