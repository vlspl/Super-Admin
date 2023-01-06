<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true"
    CodeFile="ViewLabs.aspx.cs" Inherits="SuperAdmin_ViewLabs" %>

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
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>View Labs</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="LabRegister.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add New Lab</a>
                     </div>
                  </div>
               </div>
            </nav>
<div class="container-fluid">
    
      	  
    <asp:TextBox ID="txtSearch" placeholder="Search Here" runat="server" CssClass="col-md-12" style="margin-top:12px;" Font-Size="16px" onkeyup="Search_Gridview(this, 'Labgrid')"></asp:TextBox><br />	 
    <asp:GridView  class="table table-striped booking admintable"  ID="Labgrid" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="Labgrid_SelectedIndexChanged"
        AutoGenerateColumns="false" CellPadding="4" OnRowDataBound="Labgrid_RowDataBound"
        DataKeyNames="sLabName">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>     
          
            <asp:BoundField DataField="sLabCode" HeaderText="LabCode" />
            <asp:BoundField DataField="sLabName" HeaderText="LabName" />
            <asp:BoundField DataField="sLabStatus" HeaderText="Status" />
            <asp:TemplateField HeaderText="Active/Deactive">
                <ItemTemplate>
                    <asp:ImageButton ID="img_user" runat="server" CommandName="Select" ImageUrl='<%# Eval("sLabStatus") %>'
                        Width="30px" Height="30px"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>    
      		 <div class="paging"></div>
    </div>

    
<%--    <script type="text/javascript">
        $(document).ready(function () {
            $('#YoCommonPopup').addClass('in');
            $('#YoCommonPopup').show();
        });
    </script>--%>


  <%--start Pagination and sorting data --%>
      <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript" src="js/jquery.easyPaginate.js"></script>
<%--
    <script>
        $('#Labgrid').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [true, true, true, true, true, true, 'select', true, 'select', 'select', 'select', false],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>--%>


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
