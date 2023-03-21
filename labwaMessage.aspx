<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="labwaMessage.aspx.cs" Inherits="SuperAdmin_labwaMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Labwise Whatsapp Message</h4>
                     </div>
                      <div class="col-sm-6 text-right">

                      </div>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px">
    	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
    <div class="row">
     <div class="col-md-3" >
     <div class="form-group">
                    <label  >Select Lab <span style="color: Red">*</span></label></label>
                       
                    <asp:DropDownList ID="drplablist" runat="server" CssClass="form-control" 
                        AutoPostBack="True" onselectedindexchanged="drplablist_SelectedIndexChanged" 
                                >
                   
                    </asp:DropDownList>
                   
                </div>
                </div>
                <div class="col-md-3">
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
          <div class="col-md-3">
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
          <div class="col-md-3">
            <asp:Button ID="BtnSave" runat="server" style="margin-top:30px;" type="submit" class="fa fa-save btn btn-lg btn-primary" 
                        Text="Search" onclick="BtnSave_Click"  />
          </div>
    </div>
             <div class="row">
             <div class="col-md-8">
             <div class="col-lg-12 col-md-12" >
             <div class="box-body" style="padding: 20px; overflow:auto; height:635px;">
                    <table class="table text-center booking" id="userOtp">
                        <thead>
                            <tr>
                                <th>
                                   Sr No
                                </th>
                                <th>
                                    Lab Name
                                </th>
                                <th>
                                    Owner Name
                                </th>
                                <th>
                                   Mobile No
                                </th>
                               
                            </tr>
                        </thead>
                        <tbody id="lablistbody" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                   
                </div>
               
   
              <br />
          </div>
             </div>
              <div class="col-md-4">
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
                     <asp:TextBox ID="txtbody" TextMode="MultiLine"  style="height:410px; resize:none;"  CssClass="form-control"  runat="server" ></asp:TextBox>
                  
                    </div>
                     <div class="form-group">
                      <asp:Button ID="btnsendMsg" runat="server" type="submit" 
                             class="fa fa-save btn btn-lg btn-primary" OnClientClick="ShowProgress()"
                        Text="Send Message"  style="margin-top:30px;" onclick="btnsendMsg_Click"/>
                     </div>
              </div>

          
           </div>
           </div>
           <br /> 
          
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
