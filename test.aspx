<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="SuperAdmin_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1"  runat="server">
    <title>page Break spacification </title>
    <link rel="shortcut icon" href="logo2.jpeg" type="image/jpeg" />
    <%--    logo and style sheet are here.--%>
    <style type="text/css">
        @page {
        margin-bottom:50px;
        margin-top:50px;
       
               
        }

         @media print {

            thead {
                
                display: table-header-group;
                top: 0px;
                color: red;
                font-size: 30px;
                position:static ;
                text-align:center;
            }

            tfoot {
                /*display:table-footer-group ;*/
                display:table-footer-group ;
                bottom: 0px;
                color: red;
                font-size: 15px;
                position:fixed;
                padding-bottom:0px;
                margin-bottom:0px;
            }

            h4 {
                page-break-after: always;
                color: pink;
                font-size:xx-large;
            }
            tr {
                display:table-row-group;
                bottom:100px;
            }
            
        }

        @media screen {
            thead {
                display: none;
            }

            tfoot {
                display: none;
            }

            h4 {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1"  runat="server">
        <div>
            <table style="width:500px">
                <thead>
                    <tr>
                        <th>MASTER</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <td>FOOTER 1</td>
                        <td> footer 2</td>
                    </tr>
                    <table><tbody><tr><td class="con">content 1</td><td>content 2</td></tr></tbody></table>
                </tfoot>
<tr><td>Content part.

when i am try to increase this part then it is overlapped with footer page.</td></tr>
</table>
</div>
</form>
</body>
</html>