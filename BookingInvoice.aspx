<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingInvoice.aspx.cs" Inherits="BookingInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css"
        integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp"
        crossorigin="anonymous">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"
        integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB"
        crossorigin="anonymous">
    <!-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/paper-css/0.3.0/paper.css"> -->
    <link rel="stylesheet" href="css/BookingInvoice.css">
  
    <style>
        @media print
        {
            .panel-heading
            {
                display: none;
            }
        }
        .btn-color {
    background: #eeecec;
    color: #91c740;
}
.btn-color:hover
{
    background: #2c3e50;
    color: white;
    font-weight: bold;
}
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
      <div class="row">
        <div class="col-lg-10">
        </div>
        <div class="col-lg-2" style="argin-top:10px;">
            <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-color pull-right" 
                onclick="btnprint_Click" />
                <a href="" runat="server" id="btnback" class="btn btn-color"><i class="fa fa-arrow-left mr-2"  aria-hidden="true"></i>Back</a>
          <%--  <button class="btn btn-color pull-right" style="margin-top:10px" id="btnPrint"  clientidmode="static">
                <i class="fa fa-print"> Print</i>
            </button>
              <a href="" runat="server" id="btnback" class="btn btn-color"><i class="fa fa-arrow-left mr-2"  aria-hidden="true"></i>Back</a>--%>
        </div>
        <div class="col-lg-1">
        
        </div>
    </div>
    <main id="maindiv" runat="server" class="container-fluid ">
    <header class="container">
      <div class="row py-3">
       <asp:Image ID="Image1" runat="server" style="width:100%; height:220px;"></asp:Image>
      <%--  <div class="col col-md-12 text-left">
        
        </div>--%>
        
      <%--  <div class="col col-md-6 text-right" style="height:auto;width:50%">
          <p><span runat="server" id="spanLabAddress"></span></p>
          <p><span runat="server" id="spanLabEmail"></span></p>
          <p><span runat="server" id="spanLabContact"></span></p>
        </div>--%>
      </div>
      <div class="row m-4 text-center">
        <div class="col" style="height:auto;width:100%">
          <h5>INVOICE</h5>
          <p style="color:rgb(116, 117, 117);font-size: 15px;">#INV-<span runat="server" id="spanLabInvoiceNo"></span></p>
          <p style="font-size: 15px;"><span runat="server" id="spanInvoiceDate"></span></p>
        </div>
      </div>
    </header>
    <section class="container">
      <div class="row pt-2">
        <div class="col-md-4 col" style="height:auto;width:33.3%">
          <p><b>Bill To :</b></p>
        </div>
        <div class="col col-md-4"style="height:auto;width:33.3%"></div>
         <div class="col-md-4 col" style="height:auto;width:33.3%">
          <p><b>Referral Doctor</b></p>
        </div>
        <hr style="height:2px; width:100%; margin:2px">
      </div>

      <div class="row pt-2">
      <div class="col-md-4 col" style="height:auto;width:33.3%">
          <div>
            <p><b><span runat="server" id="spanPatientName"></span></b></p>
          </div>
          <div>
            <p>Phone: <span runat="server" id="spanPatientContactNumber"></span></p>
          </div>
            <div>
            <p><span runat="server" id="spanPatientAddress"></span></p>
          </div>
        </div>
        
        <div class="col col-md-4" style="height:auto;width:33.3%"></div>
        <div class="col-md-4 col" style="height:auto;width:33.3%">
          <div>
            <p><b><span runat="server" id="spanLabDoctor"></span></b></p>
          </div>
          <div class="pb-1">
            <p><span runat="server" id="spanLabDoctorDegree"></span></p>
          </div>
          <div>
            <p><b><span runat="server" id="spanLabDoctorName1"></span></b></p>
          </div>
          <div>
            <p><span runat="server" id="spanLabDoctorDegree1"></span></p>
          </div>
        </div>
      </div>

      <div class="table-responsive-sm mt-4">
        <table class="table tbl-1">
          <thead>
            <tr>
              <th class="center">Sr. No.</th>
              <th>Test Code</th>
              <th class="center">Test Name</th>
              <th class="right">TestStatus</th>
              <th class="right">Price</th>
            </tr>
          </thead> <tbody id="tbodyTestDetails" runat="server" clientidmode="Static">
                       </tbody>
        </table>
      </div>
      <div class="row mt-5">
        <div class="col col-md-4"style="height:auto;width:33.3%"></div>
        <div class="col col-md-4" style="height:auto;width:33.3%"></div>
        <div class="col-md-4  ml-auto" style="height:auto;width:33.3%">
          <table class="table tbl-2">
            <tbody>
              <tr>
                <td class="left">
                  <strong class="text-dark">Date:</strong>
                </td>
                <td class="right"><span runat="server" id="PaymentReceiveDate"></span></td>
              </tr>
               <tr>
                <td class="left">
                  <strong class="text-dark">Payment Method</strong>
                </td>
                <td class="right"><span runat="server" id="PaymentMode"></span></td>
              </tr>
              <tr>
                <td class="left">
                  <strong class="text-dark">Total</strong>
                </td>
                <td class="right"><span runat="server" id="TotalAmount"></span> INR</td>
              </tr>
              <tr>
                <td class="left">
                  <strong class="text-dark">Paid</strong>
                </td>
                <td class="right"><span runat="server" id="PaidAmount"></span> INR</td>
              </tr>
              <tr>
                <td class="left">
                  <strong class="text-dark">Balance</strong> </td>
                <td class="right">
                  <strong class="text-dark"><span runat="server" id="BalanceAmount"></span> INR</strong>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="col mt-5">
        <p><b>Stamp & Signature</b></p>
      
      </div>
      <div style="margin-top:80px;">
       <p style="text-align:center;"><span runat="server" id="spanLabAddress"></span></p>
          <p style="text-align:center;"><b>Email :</b> <span runat="server" id="spanLabEmail"></span>&nbsp;&nbsp;&nbsp;&nbsp;<b>Contact No : </b><span runat="server" id="spanLabContact"></span></p>
      </div>
    </section>

    <footer class="mt-4">
      <div style="height: 20px;width:100%; background: #ececec!important;"></div>
    </footer>
  </main>
    <script src="http://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"
        integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T"
        crossorigin="anonymous"></script>
          <script>
              $("button").click(function () {
                  $("button").hide();
                  print()
              });
    </script>
    </form>
</body>
</html>
