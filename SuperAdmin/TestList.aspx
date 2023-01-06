<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="TestList.aspx.cs" Inherits="SuperAdmin_TestList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Test List</h4>
                     </div>
                    <%-- <div class="col-sm-6 text-right">
                        <a href="AddOrgBranch.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Branch</a>
                     </div>--%>
                  </div>
               </div>
            </nav>
  <div class="box-body" style="padding:20px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Organization Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <!-- tab content -->
        <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table  booking table-bordered table-hover" id="MyTestList">
                    <thead>
                        <tr>
                            <th>
                                Test Code
                            </th>
                            <th>
                                Test Name
                            </th>
                            <th>
                                Profile
                            </th>
                            <th>
                                Section
                            </th>
                            <th>
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbodyMyTestList" class="text-center" runat="server" clientidmode="Static">
                    </tbody>
                </table>
                <div class="paging">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">
        function edit(testIddata) {
            $.ajax({
                url: "TestList.aspx/TestEdit",
                data: JSON.stringify({ "testId": testIddata }),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (msg) {
                    if (msg.d == 1) {
                        alert('Test Added Successfully.')
                        location.reload();

                    };
                    if (msg.d == -2) {
                        alert('Test Already Added in Your List.')
                    };
                },
                error: function (data) {
                    alert('Something Went Wrong,Please Try again.')
                }
            });
        }
        $('#MyTestList').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [true, true, true, true, false],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
</asp:Content>
