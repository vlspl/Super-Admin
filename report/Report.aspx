<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <style>
        @media print
        {
            .panel-heading
            {
                display: none;
            }
        }
    </style>
    <script type="text/javascript" src="../js/jquery.js"></script>
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
    <div runat="server" clientidmode="static" id="_ReportDiv">
    </div>
    </form>
    <script>
        $("button").click(function () {
            $("button").hide();
            print()
        });
    </script>
</body>
</html>
