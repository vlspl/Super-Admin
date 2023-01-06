<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AtomPaymentFail.aspx.cs"
    Inherits="AtomPaymentFail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
            background: white;
        }
        center
        {
            color: Red;
            border-radius: 1em;
            padding: 1em;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            transform: translate(-50%, -50%);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <img src="images/icons/11004-cancel-or-error-animation.gif" width="200" height="155" />
        <br />
        <h3>
            Payment Failed. Try again later...</h3>
    </center>
    </form>
</body>
</html>
