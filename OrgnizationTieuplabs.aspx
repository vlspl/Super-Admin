<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="OrgnizationTieuplabs.aspx.cs" Inherits="SuperAdmin_OrgnizationTieuplabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Lab List</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                    <%--    <a href="LabRegister.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Lab</a>
                    --%> </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:20px">
    	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
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
                <table class="table text-center table-bordered table-hover booking" id="Labgrid">
                    <thead>
                        <tr>
                            <th>
                                Lab Name
                            </th>
                            <th>
                                Lab Address
                            </th>
                            <th>
                                Lab Manager
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                                Contact Number
                            </th>
                            <th>
                                Action
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
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">
        function edit(testIddata) {
            $.ajax({
                url: "OrgnizationTieuplabs.aspx/AddLab",
                data: JSON.stringify({ "LabId": testIddata }),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (msg) {
                    if (msg.d == 1) {
                        alert('Lab added successfully.')
                        location.reload();

                    };
                    if (msg.d == -2) {
                        alert('Lab already added in your list.')
                    };
                },
                error: function (data) {
                    alert('Something went wrong,please try again.')
                }
            });
        }
        $('#Labgrid').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [true, true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
</asp:Content>
