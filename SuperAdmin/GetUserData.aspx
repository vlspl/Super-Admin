<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="GetUserData.aspx.cs" Inherits="SuperAdmin_GetUserData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>User OTP Details</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                      </div>
                  </div>
               </div>
            </nav>
   
    <div class="box-body" style="padding: 20px">
                    <table class="table text-center booking" id="userOtp">
                        <thead>
                            <tr>
                                <th>
                                   Sr No
                                </th>
                                <th>
                                    User Name
                                </th>
                                <th>
                                   Mobile No
                                </th>
                                <th>
                                    OTP
                                </th>
                              
                            </tr>
                        </thead>
                        <tbody id="tbodyAlluserOTP" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                    <div class="paging">
                </div>
                </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script>
        $('#userOtp').datatable({
            pageSize: 50,
            pagination:[true],
            //sort: [true, true, true, true],
            filters: [false,true, true ],
            filterText: 'Type to filter ... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>

      
</asp:Content>
