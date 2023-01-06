<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="ViewReportValuesinTemplate.aspx.cs" Inherits="ViewReportValuesinTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
	.header{width: 100%; background: #34383c; height: 66px; position: relative; padding:0;}
	.logo{position: absolute; left: 15px; top: 15px;}
	h1{font-size: 24px; text-align: center; color: #fff; line-height: 66px; font-weight: 300; margin: 0;}
	.patientinfo{padding:20px 15px;}
	.patientinfo p{font-size: 16px; font-weight: 500; color: #000;}
	.patientinfo p + p{margin-top: 10px;}
	.patientinfo p span{width: 100px; color: #2ac88e; font-weight: 700;}
	.footer{width: 100%; color: #000; font-size: 12px; padding:0 15px; text-align: center; border-top:2px solid #34383c; background: none;}
	.footer span{font-size: 16px; font-weight: 700;}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <a href=""  id="addHEadingbtn"  runat="server"  data-toggle="modal" data-target="#AddHeading" class="lab-btn-default">btn 1 </a>
             <a href=""  id="AddPatientbtn"  runat="server"  data-toggle="modal" data-target="#modalAddPatient" class="lab-btn-default">btn 2</a>
             <input type='button' id='btn' value='Print' onclick='printDiv();'>
             
<button id="cmd">Generate PDF</button>

             <script src="https://code.jquery.com/jquery-1.12.3.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/0.9.0rc1/jspdf.min.js"></script>

             <script>

                 function printDiv() {

                     var divToPrint = document.getElementById('TemplateContainer');

                     var newWin = window.open('', 'Print-Window');

                     newWin.document.open();

                     newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

                     newWin.document.close();

                     setTimeout(function () { newWin.close(); }, 10);

                 }



                 var doc = new jsPDF();
                 var specialElementHandlers = {
                     '#editor': function (element, renderer) {
                         return true;
                     }
                 };

                 $('#cmd').click(function () {
                     doc.fromHTML($('#TemplateContainer').html(), 15, 15, {
                         'width': 170,
                         'elementHandlers': specialElementHandlers
                     });
                     doc.save('sample-file.pdf');
                 });

                 // This code is collected but useful, click below to jsfiddle link.


             </script>


    <asp:Button ID="Button1" runat="server" OnClick="btnsendMail" Text="Button" />

<div class="container" id="TemplateContainer">
  <div class="row">
  
  <header>
		<div class="header">
			<div class="logo"><img src="images/labcare-logo.png" alt="" title="" /></div>
			<h1  id="spanlabname" runat="server" clientidmode="Static">Abc Pathalogy Lab</h1>
		</div>
	</header>
	<section>
		<div class="patientinfo">
			<p><span>Name :</span> <span id="spanpatname" runat="server" clientidmode="Static"></span> </p>
			<p><span>Gender :</span>  <span id="spanpatgender" runat="server" clientidmode="Static"></span> </p>
			<%--<p><span>Contact :</span>  <span id="spanpatcontact" runat="server" clientidmode="Static"></span> </p>
			<p><span>Address :</span>  <span id="spanpataddress" runat="server" clientidmode="Static"></span> </p>--%>
		</div>
	</section>
    <hr />
    	<section>
        <div class="header" id="tempheading" runat="server">
			<h1 >Heading Test Reports</h1>
		</div>
         <div id="tempsubheading" runat="server">
            <h4 >Sub Heading Test Reports</h4>
		</div>

        <div>

            <div class="createreporttable">
        <table class="table table-bordered">
<%--        <thead>
        <tr>
        <th>Analyte / Sub Analyte</th>
        <th>Value</th>
        <th>Result</th>
        </tr>
        </thead>--%>
            <tbody id="tbodyTemplateBuilder" runat="server">
            </tbody>
        </table>       
        <div class="reports-notes"  id="Notesdiv" runat="server" clientidmode="Static">
           <%-- <p>Notes: <span id="spanNotes" runat="server" clientidmode="Static"></span></p>--%>
        </div>
         
		<div class="reports-notes"  id="CommentDiv" runat="server" clientidmode="Static">
			<%--<p>Comment : <span id="spanComment" runat="server" clientidmode="Static"></span></p>--%>
		</div>		   
    </div>  

        </div>

	</section>

	<footer>
		<div class="footer">
			<p><span>Address : </span> <span id="labaddress" runat="server" clientidmode="Static"> </span> <span>, Contact No: - </span> <span  id="labcontactnumber" runat="server" clientidmode="Static"> </span></p>
		</div>
	</footer>
  </div>
  
  </div>

</asp:Content>

