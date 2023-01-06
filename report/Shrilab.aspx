<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shrilab.aspx.cs" Inherits="report_Shrilab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css"
    integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp" crossorigin="anonymous">
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"
    integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB" crossorigin="anonymous">
     <link href="../report/ReportCSS/VisionPathlab.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <main class="container-fluid">
    <header class="container">
      <div class="row">
        <div class="col col-md-2"><img class="size float-right" src="../images/img-2.png"></div>
        <div class="col col-md-3 text-right col2 mt-4">
          <p style="font-size: 14px; font-weight:bold; margin-bottom:0px">Dr. Indrani Khandelwal</p>
          <p>MD, PG, Chandigarh </p>
          <hr class="intro">
          <p><b>Formerly</b></p>
          <p>Assistant Professor,virology, PGIMER</p>
          <p>Research Associate, BI Hospital</p>
          <p>Harward Medical School,Boston (USA)</p>
        </div>
        <div class="col col-md-4" style="margin-top:-10px">
          <hr id="name1">
          <hr id="name2">
          <hr id="name3">
          <hr id="name4">
          <hr id="name5">
          <h4 class="text-center" style="color:rgb(247, 70, 70); padding-bottom: 0px;margin: 0px;">Khandelwal Diagnostic
          </h4>
          <div class="lab-info text-center">
            <p>AUTOMATED CLINICAL LAB & MOLECULAR DIAGNOSTIC CENTER</p>
            <p><b>SCO 108-109, Sector 8-C, Madhya Marge Chandigarh</b> </p>
            <p><b>Phones: 277230, 277234, RES:2714323</b></p>
            <p>Branch:SCF-5, Sector 11-D,Chandigarh,Phone :5035589</p>
          </div>
        </div>
        <div class="col col-md-3">
          <img src="../images/img-1.png" alt="" style="height:130px; width:20">
        </div>
      </div>
    </header>

    <section class="justify-content-center container pt-4">    
      <div class="card">
        <div class="card-body">
         <div class="row pl-5 ">
           <div class="col col-md-4"><p>Date:<span id="spanTestDate" runat="server" clientidmode="Static"></span></p></div>
           <div class="col col-md-4"><p>Sr. No.:<span id="spanBookId" runat="server" clientidmode="Static"></span></p></div>
           <div class="col col-md-4"></div>
         </div>
         <div class="row pl-5">
          <div class="col col-md-4"><p>Name: <span id="spanPatientName" runat="server" clientidmode="Static"></span></p></div>
          <div class="col col-md-4"><p>Age:<span id="spanPatientAge" runat="server" clientidmode="Static"></span></p></div>
          <div class="col col-md-4"><p>Sex:<span id="spanGender" runat="server" clientidmode="Static"></span></p></div>
        </div>
        <div class="row pl-5">
          <div class="col col-md-4"><p>Refd. By: <span id="spanDoctorName" runat="server" clientidmode="Static"></span></p></div>
          <div class="col col-md-4"><p>LAB NO. :<span id="spanLabName" runat="server" clientidmode="Static"></span></p></div>
          <div class="col col-md-4"></div>
        </div>
        </div>
      </div>
      <h6 class="text-center text-underline pt-4 pb-3"><u><span id="spanTestname" runat="server" clientidmode="Static"></span></u></h6>
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
      <div class="col col-md3 offset-md-9 p-5"><img src="../images/sign.png" alt="" style="height:50px;width:100px"></div>
    </section>

    <div class="data container pb-5">
      <p>Note:</p>
      <p>
        <li>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quaerat quos repellendus architecto nihil blanditiis, quas.</li>
     <li>Lorem ipsum dolor sit amet consectetur adipisicing elit. Numquam nulla ipsa et. Quas possimus dicta fugit sint tempora. Non iste doloribus aliquam quidem nam? </li>
    <li>Lorem ipsum dolor sit amet consectetur adipisicing elit. Dolor ipsam commodi dolore sunt eligendi. Impedit, totam nostrum</li> 
    </p>
     
    </div>
   
  </main>
   <script src="http://code.jquery.com/jquery-3.3.1.min.js"
    integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"
    integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T"
    crossorigin="anonymous"></script>
    </form>
</body>
</html>
