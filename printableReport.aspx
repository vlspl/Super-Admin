<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printableReport.aspx.cs" Inherits="printableReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
   
</head>
<body id="allpage" runat="server">
    <form id="form1" runat="server">
   
    <div class="row">
         <div class="col-lg-10">
        </div>
        <div class="col-lg-1">
          
        </div>
        <div class="col-lg-1">
          <asp:button ID="imgBtnPrint" style="margin-left:15px; margin-top:10px;"  
                CssClass="btn btn-color" runat="server" Text="Print" 
                onclick="imgBtnPrint_Click1"    />


        </div>
       
    </div>
    <div class="row">
    <table style="width:100%">
    <thead>
    <tr>
    <td colspan="3">
    <span id="headerdiv" runat="server"></span>
   
    
    </td>

    </tr>
     
    </thead>
    <tr>
     <td colspan="3">
    <table style="width:80%; margin-left:70px; border-bottom:1px solid gray;">
    <tr>
    <td colspan="3"> <b>Patient Name : </b>
        <asp:Label ID="lblpatientName" runat="server" Text="Label"></asp:Label><br />
    <b>Age/Sex :</b>
     <asp:Label ID="lblsex" runat="server" Text="Label"></asp:Label><br />
    <b>Referred By : </b>
    <asp:Label ID="lblrefby" runat="server" Text="Label"></asp:Label><br />
     <b>Report ID : </b>
    <asp:Label ID="lblreportId" runat="server" Text="Label"></asp:Label>
    </td>
    <td  colspan="3">
         <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>
    </td>
    <td colspan="3"> <b>Collection Date : </b>
        <asp:Label ID="lblcollected" runat="server" Text="Label"></asp:Label><br />
    <b>Booking Date :</b>
     <asp:Label ID="lblbooking" runat="server" Text="Label"></asp:Label><br />
    <b>Report Date : </b>
    <asp:Label ID="lblreported" runat="server" Text="Label"></asp:Label><br />
    
     <b>Reg No : </b>
    <asp:Label ID="lblregno" runat="server" Text="Label"></asp:Label>
    
    </td>
    </tr>
    </table>
    <span id="_ReportDiv" runat="server"></span>
    <br />
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
    </td>
    
    </tr>
    
    <tfoot>
    <tr>
    <td colspan="3">
    <span id="_remReport" runat="server"></span>
    </td>
    </tr>
    </tfoot>
    
    
    </table>





    </div>





    
   
    </form>
   
</body>
</html>
