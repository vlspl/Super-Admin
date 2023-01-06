<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GLOBALClinicalLaboratory.aspx.cs"
    Inherits="report_GLOBALClinicalLaboratory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css"
        integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp"
        crossorigin="anonymous">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"
        integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB"
        crossorigin="anonymous">
    <link href="ReportCSS/GLOBALClinicalLaboratorystyle.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <!-- START HERE -->
    <main class="container-fluid mt-3">
    <header class="container">
      <div class="d-flex">
        <div class="p-2">
          <p style="font-size: 50px; font-weight:bolder;margin-block-end:0em; color: #e6668a;font-family: serif ;">GLOBAL</p>
          <h6 class="ml-5 py-0" style="font-family: 'Times New Roman', Times, serif; font-weight: bolder;">CLINICAL LABORATORY</h6>
          <hr style="height:2px">
          <p>Near Siddhi Clinic, Surbhi Apartment, New <br>Court Road, Daund, Dist-Pune.-413801</p>
        </div>
       
        <div class="ml-auto p-2 justify-content-right">
          <p class="doc">Mr. Vijay S Mergal</p>
          <p class="text-right">(B.Sc. D.M.L.T)</p>
          <p class="doc">Home Visit Available</p>
          <p class="text-right">Mob-9881217250</p>
        </div>
      </div>
    </header>
    <section class="container" id="section">
      <div class="div1">
        <img src="../images/Logo/GLOBALClinicalLaboratoryLogo.png" alt="" style="height:auto; width:auto">
      </div>
       <div class="div2 mt-5">
        <div class="card">
          <div class="card-body">
           <div class="row  ">
             <div class="col col-md-4" style="width:33.3%"><p>Date:26/03/2020</p></div>
             <div class="col col-md-4" style="width:33.3%"><p>Sr. No.:23</p></div>
             <div class="col col-md-4" style="width:33.3%"></div>
           </div>
           <div class="row ">
            <div class="col col-md-4" style="width:33.3%"><p>Name: Ananbela Desuza</p></div>
            <div class="col col-md-4" style="width:33.3%"><p>Age:23yrs</p></div>
            <div class="col col-md-4" style="width:33.3%"><p>Sex:Female</p></div>
          </div>
          <div class="row">
            <div class="col col-md-4" style="width:33.3%"><p>Refd. By: Dr. Anil Pahawa</p></div>
            <div class="col col-md-4" style="width:33.3%"><p>LAB NO. :3290</p></div>
            <div class="col col-md-4" style="width:33.3%"></div>
          </div>
          </div>
        </div>
        <h6 class="text-center text-underline mt-5 pb-3"  style="color:#038086"><u><b>HAEMATOLOGY</b></u></h6>
        <table class="table table-borderless">
          <thead>
            <tr>
              <th scope="col">Test Name</th>
              <th scope="col">Result</th>
              <th scope="col">Normal Range</th>
              <th scope="col">Units</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td class="left">Haemoglobin</td>
              <td>12</td>
              <td>11.0-16.0</td>
              <td>@mdo</td>
            </tr>
            <tr>
              <td>RBC</td>
              <td>3.3</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>HTC</td>
              <td>36</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>MCV</td>
              <td>44</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>MCH</td>
              <td>13</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>MCHC</td>
              <td>12</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>RDW-CV</td>
              <td>11</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>RDW-SD</td>
              <td>3</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>NEU%</td>
              <td>1.1</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>LYM%</td>
              <td>2.2</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>MON%</td>
              <td>4.4</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>EOD%</td>
              <td>1.1</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>BAS%</td>
              <td>83</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>LYM#</td>
              <td>41</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>GRA#</td>
              <td>52</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>PLT</td>
              <td>63</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
            <tr>
              <td>ESR</td>
              <td>12</td>
              <td>11.0-16.0</td>
              <td>g/dL</td>
            </tr>
          </tbody>
        </table>
      </div> 
    </section>


    <footer class="container mb-5 footer" style="border-top:3px solid black">      
     <p>* This report is an opinion not final diagnosys & is not valid for medico-legal purposes.</p>
     <p>* In case if disparity repeat test. correlate with clinical findings & other Investigation</p>
    </footer>
  </main>
    <script src="http://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"
        integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T"
        crossorigin="anonymous"></script>
</body>
</html>
