<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="SubAnalyteMaster.aspx.cs" Inherits="SubAnalyteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
    function showModal() {
        $("#myModal").modal('show');
    }

</script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function goBack() {
         var url = 'SubAnalyteMaster.aspx';
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
        <a href="#" class="navbar-brand">Subanalyte Master </a>
      </div>
      <div class="mr-5">
       
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group mx-sm-3 mb-2 analytewrap">
                        <asp:DropDownList ID="selAnalyte" class="form-control" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSubAnalyteName" onkeypress="return onlyAlphabets(event,this);"
                        class="form-control" placeholder="Subanalyte
    name" runat="server" ClientIDMode="Static"></asp:TextBox>
                </div>
                <asp:Button ID="btnSubAnalyte" runat="server" class="btn btn-color mb-2" OnClientClick="javascript:return subAnalyteValidate()"
                    OnClick="btnAddSubAnalyte_click" Text="Add Subanalyte" />
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
                                        Subanalyte
                                    </th>
                                    <th style="text-align:center;">
                                        Analyte
                                    </th>
                                    <th style="text-align:center;">
                                       Delete
                                    </th>
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodySubAnalyte" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>



       
    </div>
  
    <!-- Modal Edit Sub Analyte Start-->
    <div id="modalEditSubAnalyte" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header"> <h4 class="modal-title">
                        Edit Sub Analyte</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                   
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <asp:DropDownList ID="selEditAnalyte" class="form-control" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:HiddenField ID="hiddenSubAnalyteId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox class="form-control" placeholder="Enter
    Sub Analyte *" ID="txtEditSubAnalyteName" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateSubAnalyte" class="btn btn-submit" runat="server" Text="Update"
                        OnClientClick="javascript:return
    editSubAnalyteValidate()" OnClick="btnUpdateSubAnalyte_click" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Edit Sub Analyte End-->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/SubAnalyteMasterValidation.js"></script>
    <script src="js/code.js"></script>
    <script src="js/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFilterSearch").keyup(function () {
                var input, filter, table, tr, td, i;
                input = document.getElementById("txtFilterSearch");
                filter = input.value.toUpperCase();

                table = document.getElementById("tbodySubAnalyte");
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

        function removeSubAnalyte(obj) {

            if (confirm("Are you sure you want to delete ?")) {
                var rowId = "rowSubAnalyte" + $(obj).attr('id');
                $("#" + rowId).hide();

                var parameter = { "subAnalyteId": $(obj).attr('id') };
                $.ajax({
                    type: "POST",
                    url: "SubAnalyteMaster.aspx/removeSubAnalyte",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {

                        if (response.d == "0") {
                            $("#" + rowId).show();
                        }
                        else if (response.d == "2") {
                            $("#" + rowId).show();
                            alert("This subanalyte has been defined in an existing test, hence it can't be deleted");
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
            }
            else { }
        }

        function updateSubAnalyte(obj) {
            var subAnalyteId = $(obj).attr('id');

            $("#hiddenSubAnalyteId").val($("#hidden" + subAnalyteId).val().split('|')[0]);
            $("#selEditAnalyte").val($("#hidden" + subAnalyteId).val().split('|')[2]);
            $("#txtEditSubAnalyteName").val($("#hidden" + subAnalyteId).val().split('|')[1]);
            $("#modalEditSubAnalyte").modal('show');
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
