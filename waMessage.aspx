<%@ Page Title="" Language="C#" MasterPageFile="~/CRMMasterPage.master"
    AutoEventWireup="true" CodeFile="waMessage.aspx.cs" Inherits="waMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:UpdatePanel ID="uppage" runat="server">
<ContentTemplate>


<nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Whatsapp Message Filters </span> </h3>
        
                    <!-- Topbar Navbar -->
                       
        </nav>
    <div class="wrapper" style="margin-left:20px;">
        <div id="content">
       <div class="table_div">
                    <div class="container">
                        <div class="row testDetail ">
                            <div class="col col-md-3">
                             <div class="form-group">
                    <label>
                        User Type<span style="color: Red">*</span></label>
                      <asp:DropDownList ID="drpuserType" runat="server" CssClass="form-control" 
                        AutoPostBack="True" onselectedindexchanged="drpuserType_SelectedIndexChanged">
                      <asp:ListItem>-Select-</asp:ListItem>
                      <asp:ListItem>Patient</asp:ListItem>
                      <asp:ListItem>Doctor</asp:ListItem>
                 
                      </asp:DropDownList>
                </div>
                            </div>
                             <div class="col col-md-3">
                             <div class="form-group">
  <label for="drpgender"> Channel Partner </label>
    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtchanelpartner"
                        ReadOnly="true"></asp:TextBox>
   </div>
   </div>
   <div class="col col-md-3">
                               <div class="form-group">
                    <label>
                        Mobile No</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtmobileNo"
                         placeholder="Enter Mobile No"></asp:TextBox>
                      
                </div>
                             </div>
                               <div class="col col-md-3">
                                <div class="form-group" >
                    <label>
                        Gender</label>
                      <asp:DropDownList ID="drpgender" runat="server" CssClass="form-control">
                      <asp:ListItem>All</asp:ListItem>
                      <asp:ListItem>Male</asp:ListItem>
                      <asp:ListItem>Female</asp:ListItem>
                      </asp:DropDownList>
                </div>
                              </div>
                            </div>
                             <div class="row ">
                              <div class="col col-md-3" style="display:none;">
                              <div class="form-group">
                    <label>
                        On board From</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtonboardFrom" ReadOnly="true"
                         placeholder="Select From Date Here"></asp:TextBox>
                    <asp:CalendarExtender ID="txtonboardFrom_CalendarExtender" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtonboardFrom" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
            <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
             </asp:ToolkitScriptManager>--%>
                </div>
                              </div>
                              <div class="col col-md-3" style="display:none;">
                               <div class="form-group">
                    <label>
                        On board To</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txttodate" ReadOnly="true"
                         placeholder="Select To Date Here"></asp:TextBox>
                      
                    <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txttodate" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                      
                </div>
                              </div>
                                <div class="col col-md-3">
                              <div class="form-group">
                    <label>
                       Age From</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtfromage"
                         placeholder="Enter From Age"></asp:TextBox>
                  
                </div>
                              </div>
                              <div class="col col-md-3">
                               <div class="form-group">
                    <label>
                        Age To</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txttoage"
                         placeholder="Enter To Age"></asp:TextBox>
                      
                  
                      
                </div>
                              </div>
                             <div class="col col-md-3">
                                <div class="form-group">
                    <label>
                        Pin Code</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtpincode"
                         placeholder="Enter Pincode"></asp:TextBox>
                      
                </div>
                             </div>
                               <div class="col col-md-3">
                               <asp:Button ID="BtnSave" runat="server" type="submit" style="margin-top:25px;" class="fa fa-save btn btn-lg btn-primary" 
                        Text="Search" onclick="BtnSave_Click"  />
                             </div>
                             </div>
                             <br />
                           
                                 <div class="row">
                                  <div class="col col-md-8">
                                    <div class="box-body" style="padding: 20px; height:443px; overflow:auto;">
                   
                  
             
                                  <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b; " 
                            cellspacing="0">
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
                               
                            </tr>
                        </thead>
                        <tbody id="tbodyAlluserOTP" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                       </div>
                                  </div>
                                    <div class="col col-md-4">
                                     <div class="form-group" >
                    <label>
                        Type <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drptype"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" ontextchanged="drptype_TextChanged"  AutoPostBack="true"
                        >
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>Name</asp:ListItem>
                        <asp:ListItem>Parameter</asp:ListItem>
                      
                    </asp:DropDownList>
                    <br />
                       <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtname" Visible="false"
                         placeholder="Enter Day Name"></asp:TextBox>
                </div>
             <div class="form-group">
                    <label>
                        Template <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drptemplate"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" 
                        OnSelectedIndexChanged="drptemplate_SelectedIndexChanged" AutoPostBack="True" >
                      
                    </asp:DropDownList>
                </div>
                  <div class="form-group">
                    <label >
                       Message Body </label>
                       
                    <asp:TextBox ID="txtremark"  style="display:none;"  CssClass="form-control"  runat="server" ></asp:TextBox>
                     <asp:TextBox ID="txtbody" TextMode="MultiLine"  style="height:200px; resize:none;"  CssClass="form-control"  runat="server" ></asp:TextBox>
                  
                    </div>
                   
                     <asp:Button ID="btnsendMsg" runat="server" type="submit" 
                             class="fa fa-save btn btn-lg btn-primary" 
                        Text="Send Message"  style="margin-top:30px;" onclick="btnsendMsg_Click"/>
                  

                                  </div>





       
  
              <br />
          </div>
           </div>
                             
                            </div>
                            </div>
                            </div>
                            </div>
  
   
      <div class="loading" align="center">
    Loading. Please wait.
    <img src="images/2.gif" />
</div>
<style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #2ac88e;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
//    $('form').live("submit", function () {
//        ShowProgress();
//    });
</script> 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
