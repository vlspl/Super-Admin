<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modelexp.aspx.cs" Inherits="SuperAdmin_modelexp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.js"></script>
    <script src="js/jquery.js"></script>
  
      <script type="text/javascript">
          function showModal() {
              $("#myModal").modal('show');
          }

      </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function RedirectSample() {
         var url = 'CreateReport.aspx';
         window.location.href = url;
     }
 </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Visinary LifeScience Says</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblMessage" runat="server"></asp:Label>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="RedirectSample()"  runat="server" Text="Close"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
    </form>
</body>
</html>
