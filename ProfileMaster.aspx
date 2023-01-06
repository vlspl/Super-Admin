<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="ProfileMaster.aspx.cs" Inherits="ProfileMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript">
      function showModal() {
          $("#myModal").modal('show');
      }

    </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function goBack() {
         var url = 'ProfileMaster.aspx';
         window.location.href = url;
     }
 </script>
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
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
        <a href="#" class="navbar-brand">Profile Master </a>
      </div>

      <div class="mr-5">
       
      </div>
    </div>
  </nav>


    <div class="table_div">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group mx-sm-3 mb-2 analytewrap">
                        <asp:DropDownList ID="selSection" class="form-control"  runat="server" ClientIDMode="Static"> </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">  <asp:TextBox ID="txtProfileName" onkeypress="return onlyAlphabets(event,this);" class="form-control" placeholder="Profile name" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div>
                      <asp:Button ID="btnAddProfile" class="btn btn-color mb-2" runat="server" OnClientClick="javascript:return profileValidate()" OnClick="btnAddProfile_click" Text="Add Profile" />
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
                                      Profile
                                    </th>
                                    <th style="text-align:center;">
                                        Section
                                    </th>
                                    <th style="text-align:center;">
                                        Delete
                                    </th>
                                   
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodyProfile" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>


      
    </div>
  
<!-- Modal Edit Profile Start-->
<div id="modalEditProfile" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header"> <h4 class="modal-title">Edit Profile</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
       
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">                   
                  <div class="form-group">
                    <asp:DropDownList ID="selEditSection" class="form-control" runat="server" ClientIDMode="Static"> </asp:DropDownList>
                  </div>
                  <div class="form-group">
                    <asp:HiddenField ID="hiddenProfileId" runat="server" ClientIDMode="Static"/>
                    <asp:TextBox class="form-control" placeholder="Enter Profile Name *" id="txtEditProfileName" runat="server" ClientIDMode="Static"></asp:TextBox>
                  </div>
              </div>
            </div>
      </div>
      <div class="modal-footer">
         <asp:Button ID="btnUpdateProfile" class="btn btn-submit" runat="server" Text="Update" OnClientClick="javascript:return editProfileValidate()" onclick="btnUpdateProfile_click" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 
<!-- Modal Edit Profile End-->

<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/ProfileMasterValidation.js"></script>
  <script src="js/code.js"></script>
    <script src="js/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFilterSearch").keyup(function () {
                var input, filter, table, tr, td, i;
                input = document.getElementById("txtFilterSearch");
                filter = input.value.toUpperCase();

                table = document.getElementById("tbodyProfile");
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

        function removeProfile(obj) {

            if (confirm("Are you sure you want to delete?")) {
                var rowId = "rowProfile" + $(obj).attr('id');
                $("#" + rowId).hide();

                var parameter = { "profileId": $(obj).attr('id') };
                $.ajax({
                    type: "POST",
                    url: "ProfileMaster.aspx/removeProfile",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {

                        if (response.d == "0") {
                            $("#" + rowId).show();
                        }
                        else if (response.d == "2") {
                            $("#" + rowId).show();
                            alert("There exists tests belonging to this profile, hence it can't be deleted");
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

        function updateProfile(obj) {
            var profileId = $(obj).attr('id');

            $("#hiddenProfileId").val($("#hidden" + profileId).val().split('|')[0]);
            $("#selEditSection").val($("#hidden" + profileId).val().split('|')[2]);
            $("#txtEditProfileName").val($("#hidden" + profileId).val().split('|')[1]);
            $("#modalEditProfile").modal('show');
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

