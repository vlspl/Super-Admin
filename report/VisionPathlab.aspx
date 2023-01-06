<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisionPathlab.aspx.cs" Inherits="report_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css"
    integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp" crossorigin="anonymous">
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"
    integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB" crossorigin="anonymous">
    <link href="../report/ReportCSS/VisionPathlab.css" rel="stylesheet" type="text/css" />

  <title></title>
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
     <div id="main" style="margin:10px 100px; ">
    <asp:Literal ID="ltrReport" runat="server"></asp:Literal> 
  </div>
    <script src="http://code.jquery.com/jquery-3.3.1.min.js"
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
