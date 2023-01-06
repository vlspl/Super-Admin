<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/SuperAdminMaster.master" AutoEventWireup="true" CodeFile="Copy (2) of labsdrilldown.aspx.cs" Inherits="SuperAdmin_labsdrilldown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Register Labs</h4>
                     </div>
                      <div class="col-sm-6 text-right">
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
                                Lab Name
                            </th>
                            <th>
                                Owner Name
                            </th>
                            <th>
                               Mobile 
                            </th>
                            <th>
                                Email
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
            filters: [false, true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
</asp:Content>

