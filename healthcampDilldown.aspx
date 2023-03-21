<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="healthcampDilldown.aspx.cs" Inherits="SuperAdmin_healthcampDilldown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Health Camp</h4>
                     </div>
                       <div class="col-sm-6 text-right" >
                       <a  href="Dash.aspx" class="btn btn-danger" style="margin-right:60px;"><i class="fa fa-arrow-left"></i>Back</a>
                       </div>
                     
                  </div>
               </div>
            </nav><br />
  
<div class="container"></div>
<div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive" style="width:93%;">
                <table class="table text-center table-bordered table-hover booking" id="Labgrid">
                    <thead>
                        <tr>
                           <th>
                                View
                            </th>
                            <%-- <th>
                                Sr No
                            </th>--%>
                            <th>
                                Health Camp Name
                            </th>
                            <th>
                                Owner Name
                            </th>
                            <th>
                               Description 
                            </th>
                         
                         
                        </tr>
                    </thead>
                    <tbody id="tbodyAllLabList" runat="server" clientidmode="Static">
                    </tbody>
                </table>
                <div class="paging">
                </div>
            </div>
        </div>
         <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">
       
        $('#Labgrid').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [false, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
</asp:Content>

