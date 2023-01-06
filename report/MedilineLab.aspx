<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedilineLab.aspx.cs" Inherits="report_MedilineLab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css"
    integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp" crossorigin="anonymous">
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"
    integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB" crossorigin="anonymous">
 
    <link href="../report/ReportCSS/MedilineLab.css" rel="stylesheet" type="text/css" />
     <style>
        @media print
        {
            .panel-heading
            {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="row">
        <div class="col-lg-10">
        </div>
        <div class="col-lg-1">
            <button class="btn btn-success pull-right" style="margin-top:10px" id="btnPrint" clientidmode="static">
                <i class="fa fa-print">Print Report</i>
            </button>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <main class="container-fluid mt-3">
    <header class="container">
      <div class="header_top "></div>

      <div class="row">
        <div class="col col-md-5 mt-1" id="top-left">
          <div class="row">
            <div class="col col-md-7 ml-3" style="width:60%; height:30px; background: #038086;"></div>
            <div class="col col-md-4" style="width:40%;height:30px; background: #2d1573;"></div>
          </div>
        </div>
        <div class="col col-md-2"><img src="../images/MedilineLabLogo708202092600.png" class="d-block mx-auto mt-1" alt=""
            style="height:140px;width:140px; margin:0px;">

        </div>

        <div class="col col-md-5 " id="top-right" style="color:#2d1573;">
          <div class="row">
            <div class="col" style="width: 100%;">
              <img src="../images/Medilinelablogo2708202092700.png" class="ml-auto mt-1" alt=""
                style="height:30px; width:150px;float:right;display:block;"></div>
          </div>

          <div class="row">
            <div class="col text-right" style="width: 100%;">
              <h1 style="font-weight: bold; font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; margin:0px;padding:0px">
                Mediline Pathalogy </h1>
            </div>
          </div>

            <div class="row">
              <div class="col text-right" style="width: 100%;">
                <h1
                  style="padding-top:0px;font-weight: bold; font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;">
                  Laboratory</h1>
              </div>
            </div>

          </div>
        </div>
    </header>
    <section class="container" id="section">
      <div class="div1">
        <img src="../images/MedilineLabLogo708202092600.png" alt="" style="height:200px; width:200px;">
      </div>
      <div class="div2 mt-5">
        <div class="card">
          <div class="card-body">
           <div class="row  ">
             <div class="col col-md-4" style="width:33.3%"><p>Date: <span id="spanTestDate" runat="server" clientidmode="Static"></span></p></div>
             <div class="col col-md-4" style="width:33.3%"><p>Sr. No.:<span id="spanBookId" runat="server" clientidmode="Static"></span></p></div>
             <div class="col col-md-4" style="width:33.3%"></div>
           </div>
           <div class="row ">
            <div class="col col-md-4" style="width:33.3%"><p>Name: <span id="spanPatientName" runat="server" clientidmode="Static"></span></p></div>
            <div class="col col-md-4" style="width:33.3%"><p>Age:<span id="spanPatientAge" runat="server" clientidmode="Static"></span></p></div>
            <div class="col col-md-4" style="width:33.3%"><p>Sex:<span id="spanGender" runat="server" clientidmode="Static"></span></p></div>
          </div>
          <div class="row">
            <div class="col col-md-4" style="width:33.3%"><p>Refd. By: <span id="spanDoctorName" runat="server" clientidmode="Static"></span></p></div>
            <div class="col col-md-4" style="width:33.3%"><p>LAB NO. :<span id="spanLabName" runat="server" clientidmode="Static"></span></p></div>
            <div class="col col-md-4" style="width:33.3%"></div>
          </div>
          </div>
        </div>
        <h6 class="text-center text-underline mt-5 pb-3"  style="color:#038086"><u><b><span id="spanTestname" runat="server" clientidmode="Static"></span></b></u></h6>
        <table class="table table-borderless">
          <thead>
            <tr>
              <th scope="col">Test Name</th>
              <th scope="col">Result</th>
              <th scope="col">Normal Range</th>
              <th scope="col">Units</th>
            </tr>
          </thead>
           <tbody id="tbodyReport" runat="server" clientidmode="Static">
                                </tbody>
        </table>
      </div>
    </section>


    <footer class="container mb-5 footer ">      
      <div class="row text-center " style="height:50px;">
        <div class="col col-md-6 p-2" style="width:50%; color:#2d1573;font-weight:bold">
          <div>
            <p>Dr. Umeshkumar Bapte (M.B.B.S. DCP)</p>         
          </div>
          <div>   <p>Reg No. 2001/07/2539</p></div>
        </div>
        <div class="col col-md-6 text-white p-2" style="width:50%; background:#038086">
          <div>
            <p>Shakuntala Chambers, 3rd floor, Near Bus Stop,Susgaon, Pune,411-021 </p>         
          </div>
          <div>   <p>Timing : Mon to Sat -9am to 9pm, Sunday -9am to 2pm</p></div>
          <div>   <p>Call : 7058310023 | 8830115902</p></div>
        </div>
      </div>
    </footer>
  </main>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"
    integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"
    integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T"
    crossorigin="anonymous"></script>
    </form>
    <script>
        $("button").click(function () {
            $("button").hide();
            print()
        });
    </script>
</body>
</html>
