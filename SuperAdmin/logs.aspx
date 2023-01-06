<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="logs.aspx.cs" Inherits="SuperAdmin_logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">  
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#txtPassword').attr('type', 'text');
               
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#txtPassword').attr('type', 'password');
               
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox Show Password  
            $('#ShowPassword').click(function () {
                $('#txtPassword').attr('type', $(this).is(':checked') ? 'text' : 'password');
              
            });
        });  
    </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Log Master</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                         <a href="ViewLog.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Log</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
            <div class="form-group">
                    <label>
                        Log Date</label>
                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <asp:TextBox ID="txtlogDate"   runat="server" ClientIDMode="Static" CssClass="form-control"
                          ReadOnly="true" ></asp:TextBox>
                    </div>
                    
     
                </div>
                <div class="form-group">
                    <label>
                        Exception Msg</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtexceptionMsg"
                       ReadOnly="true" TextMode="MultiLine" style="resize:none; height:100px;"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>
                        Exception Type</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None; height:100px;" TextMode="MultiLine"   ReadOnly="true"
                        CssClass="form-control" ID="txtexceptionType" ></asp:TextBox>
                </div>
                 <div class="form-group">
                    <label>
                        Exception URL</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None; height:100px;" TextMode="MultiLine"   ReadOnly="true"
                        CssClass="form-control" ID="txtexceptionURL" ></asp:TextBox>
                </div>
               
            </div>
            <div class="col-lg-6 col-md-6">
             
                 <div class="form-group">
                    <label>
                        Exception Source</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtexceptionSource"
                       ReadOnly="true" TextMode="MultiLine" style="resize:none; height:465px;"></asp:TextBox>
                </div>

              
             
            </div>
        </div>
    </div>

</asp:Content>
