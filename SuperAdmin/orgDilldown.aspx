<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="orgDilldown.aspx.cs" Inherits="SuperAdmin_orgDilldown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Organization Details</h4>
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
                <table class="table text-center table-bordered table-hover booking" id="org">
                    <thead>
                        <tr>
                           <th>
                                View
                            </th>
                            <%-- <th>
                                Sr No
                            </th>--%>
                            <th>
                                 ID
                            </th>
                            <th>
                                 Name
                            </th>
                            <th>
                               Address 
                            </th>
                          <th>
                               Contact  
                            </th>
                             <th>
                               Email 
                            </th>
                         
                        </tr>
                    </thead>
                    <tbody id="tbodyAllorgList" runat="server" clientidmode="Static">
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

        $('#org').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [false, false,true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
</asp:Content>

