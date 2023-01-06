<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="waMessage.aspx.cs" Inherits="SuperAdmin_waMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Whatsapp Message Filters</h4>
                     </div>
                      <div class="col-sm-6 text-right">

                      </div>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px">
        <div class="row">
          <div class="col-lg-12 col-md-12">
         <div class="col-lg-6 col-md-6">
           <div class="form-group">
                    <label>
                        User Type<span style="color: Red">*</span></label>
                      <asp:DropDownList ID="drpuserType" runat="server" CssClass="form-control" 
                        AutoPostBack="True" onselectedindexchanged="drpuserType_SelectedIndexChanged">
                      <asp:ListItem>-Select Type Here-</asp:ListItem>
                      <asp:ListItem>Patient</asp:ListItem>
                      <asp:ListItem>Doctor</asp:ListItem>
                      <asp:ListItem>Employee</asp:ListItem>
                      </asp:DropDownList>
                </div>
         </div>
           <div class="col-lg-6 col-md-6">
            <div class="form-group" id="org" runat="server" visible="false">
                    <label>
                        Organization Name</label>
                      <asp:DropDownList ID="drporgnizationName" runat="server" CssClass="form-control">
                      <asp:ListItem>-Select-</asp:ListItem>
                    
                      </asp:DropDownList>
                </div>
         </div>
          </div>
          </div>
            <div class="row">
          <div class="col-lg-12 col-md-12">
         <div class="col-lg-3 col-md-3">
         <div class="form-group">
                    <label>
                        On board From</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtonboardFrom"
                         placeholder="Select From Date Here"></asp:TextBox>
                    <asp:CalendarExtender ID="txtonboardFrom_CalendarExtender" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtonboardFrom" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
             <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
             </asp:ToolkitScriptManager>
                </div>
         </div>
          <div class="col-lg-3 col-md-3">
           <div class="form-group">
                    <label>
                        On board To</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txttodate"
                         placeholder="Select To Date Here"></asp:TextBox>
                      
                    <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txttodate" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                      
                </div>
         </div>
          <div class="col-lg-3 col-md-3">
          <div class="form-group">
                    <label>
                        From Age</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtfromage"
                         placeholder="Enter from Age"></asp:TextBox>
                      
                </div>
         </div>
          <div class="col-lg-3 col-md-3">
           <div class="form-group">
                    <label>
                        To Age</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txttoage"
                         placeholder="Enter To Age"></asp:TextBox>
                      
                </div>
         </div>
         </div>
         </div>
           <div class="row">
          <div class="col-lg-12 col-md-12">
         <div class="col-lg-3 col-md-3">
           <div class="form-group">
                    <label>
                        Mobile No</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtmobileNo"
                         placeholder="Enter Mobile No"></asp:TextBox>
                      
                </div>
         </div>
         <div class="col-lg-3 col-md-3" style="display:none;">
           <div class="form-group" >
                    <label>
                        Gender</label>
                      <asp:DropDownList ID="drpgender" runat="server" CssClass="form-control">
                      <asp:ListItem>-Select-</asp:ListItem>
                      <asp:ListItem>Male</asp:ListItem>
                      <asp:ListItem>Female</asp:ListItem>
                      </asp:DropDownList>
                </div>
         </div>
         <div class="col-lg-3 col-md-3">
          <div class="form-group">
                    <label>
                        Area</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtarea"
                         placeholder="Enter Area"></asp:TextBox>
                      
                </div>
         </div>
         <div class="col-lg-3 col-md-3">
          <div class="form-group">
                    <label>
                        Pin Code</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtpincode"
                         placeholder="Enter Pincode"></asp:TextBox>
                      
                </div>
         </div>
           </div>
             </div>


              <div class="row">
          <div class="col-lg-12 col-md-12">
         <div class="col-lg-3 col-md-3"></div>
          <div class="col-lg-3 col-md-3"></div>
           <div class="col-lg-3 col-md-3"></div>
            <div class="col-lg-3 col-md-3">
               <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary" 
                        Text="Search" onclick="BtnSave_Click"  />
            </div>
          </div>
           </div>
           <hr />
             <div class="row">
          <div class="col-lg-12 col-md-12" style="height:400px; overflow:auto;">
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
            pagination: [true],
            //sort: [true, true, true, true],
            filters: [false, false, false],
            filterText: 'Type to filter ... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
              <br />
          </div>
           </div>
           <br /> 
             <div class="row">
          <div class="col-lg-12 col-md-12">
        <div class="col-lg-6 col-md-6">
           <div class="form-group">
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
                     <asp:TextBox ID="txtbody" TextMode="MultiLine"  style="height:350px; resize:none;"  CssClass="form-control"  runat="server" ></asp:TextBox>
                  
                    </div>

      </div>
          <div class="col-lg-6 col-md-6">
          
             
                     <div class="form-group">
                      <asp:Button ID="btnsendMsg" runat="server" type="submit" 
                             class="fa fa-save btn btn-lg btn-primary" OnClientClick="ShowProgress()"
                        Text="Send Message"  style="margin-top:30px;" onclick="btnsendMsg_Click"/>
                     </div>
          </div>
       </div>
       </div>
   
      <div class="loading" align="center">
    Loading. Please wait.
    <img src="../images/2.gif" />
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
    $('form').live("submit", function () {
        ShowProgress();
    });
</script> 
</asp:Content>
