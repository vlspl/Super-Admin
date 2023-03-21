<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="SuperAdmin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<style>
    
#easyPaginate {width:300px;}
#easyPaginate img {display:block;margin-bottom:10px;}
.easyPaginateNav a {padding:5px;}
.easyPaginateNav a.current {font-weight:bold;text-decoration:underline;}
.easyPaginateNav { width: 100% !important; }
</style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Lab Active / Deactive</h4>
                  </div>                 
               </div>
            </div>
         </nav>
    <div class="container-fluid">
      
      	  <div style="height:545px; overflow:auto;">
    <asp:TextBox ID="txtSearch" placeholder="Search Here" runat="server" CssClass="col-md-12" style="margin-top:12px;" Font-Size="16px" onkeyup="Search_Gridview(this, 'Labgrid')"></asp:TextBox><br />	 
 
        <asp:GridView  class="table"  ID="Labgrid" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="Labgrid_SelectedIndexChanged"
        AutoGenerateColumns="false" CellPadding="4" OnRowDataBound="Labgrid_RowDataBound" ShowHeader="true"
        DataKeyNames="sLabName">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>   
          
            <asp:BoundField DataField="sLabCode" HeaderText="Lab Code" />
            <asp:BoundField DataField="sLabName" HeaderText="Lab Name" />
            <asp:BoundField DataField="sLabStatus" HeaderText="Status" />
            <asp:TemplateField HeaderText="Active/Deactive">
                <ItemTemplate>
                    <asp:ImageButton ID="img_user" runat="server" CommandName="Select" ImageUrl='<%# Eval("sLabStatus") %>'
                        Width="30px" Height="30px"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>    
      </div>		
    </div>

    
<%--    <script type="text/javascript">
        $(document).ready(function () {
            $('#YoCommonPopup').addClass('in');
            $('#YoCommonPopup').show();
        });
    </script>--%>


  <%--start Pagination and sorting data --%>
     <%-- <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript" src="js/jquery.easyPaginate.js"></script>

    <script type="text/javascript">
        $('#Labgrid').datatable({
            pageSize: 10,
            filters: [true, true, ],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
--%>

        <script type="text/javascript">

            function Search_Gridview(strKey, strGV) {
                var strData = strKey.value.toLowerCase().split(" ");
                var tblData = document.getElementById(strGV);
                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            }



            $('#Labgrid').easyPaginate({
                    paginateElement: 'tr',
                    elementsPerPage: 10,
                    effect: 'climb'
                });

</script>




      <%--end Pagination and sorting data --%>

</asp:Content>
