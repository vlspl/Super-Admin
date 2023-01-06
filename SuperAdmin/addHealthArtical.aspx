<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="addHealthArtical.aspx.cs" Inherits="SuperAdmin_ChannelPartner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Health Articles</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                        <a href="healthArtical.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Health Articles</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 25px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                       Title<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txttitle"
                        placeholder="Enter Title Here"></asp:TextBox>
                  
                </div>
                  <div class="form-group">
                    <label>
                        Description<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None; height:100px;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtdescription" placeholder="Description"></asp:TextBox>
                </div>
              <hr />
              <div class="form-group">
               <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary" OnClientClick="ShowProgress()"
                        Text="Save" OnClick="BtnSave_Click" />
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
