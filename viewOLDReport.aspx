<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewOLDReport.aspx.cs" Inherits="viewQrReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
     <meta name="google" content="notranslate" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"
        name="viewport" />
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <style>
        @media print
        {
            .panel-heading
            {
                display: none;
            }
        }
    </style>
  
    <script type="text/javascript" src="js/jquery.js"></script>
</head>
<body id="allpage" runat="server">
    <form id="form1" runat="server">
   
    <div class="row">
        <div class="col-lg-10">
        </div>
        <div class="col-lg-1">
            <asp:Button ID="btnprintReport" runat="server" CssClass="btn btn-color"  style="margin-left:-15px; margin-top:10px; display:none;"
                Text="Customize Report" onclick="btnprintReport_Click" />
        </div>
        <div class="col-lg-1">
        <asp:button ID="imgBtnPrint" style="margin-left:15px; margin-top:10px; display:none;"  CssClass="btn btn-color" runat="server" Text="Print" onclick="imgBtnPrint_Click"  />

        </div>
    </div>
    <div runat="server" clientidmode="static" id="_ReportDiv">
    </div>
    <asp:DataList ID="DataList1" runat="server"  RepeatDirection="Horizontal" style="float:right; " >
        <ItemTemplate>
           <div style="padding:10px;" > 
            <asp:Label ID="DSIdLabel" runat="server" Text='<%# Eval("DSId") %>'  Visible="false"/> 
            <asp:Image ID="Image1" style="width:170px;" runat="server" ImageUrl='<%# Eval("SignImage") %>' />
            <br />            
            <asp:Label ID="SignHolderLabel" runat="server"  style="margin-left:30px;" 
                Text='<%# Eval("SignHolder") %>' />
                 <br />
                 <asp:Label ID="lbldept" runat="server" style="margin-left:30px;" 
                Text='<%# Eval("Department") %>' />
          </div>
            
            
 
        </ItemTemplate>
    </asp:DataList>
     <div runat="server" clientidmode="static" id="_remReport">
    </div>
    
    </form>
   
</body>
</html>
