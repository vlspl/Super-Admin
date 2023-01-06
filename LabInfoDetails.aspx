<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="LabInfoDetails.aspx.cs" Inherits="LabInfoDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript">
     function showModal() {
         $("#myModal").modal('show');
     }

      </script>
  
 <script type="text/javascript">

     function goBack() {
         var url = 'LabInfoDetails.aspx';
         window.location.href = url;
     }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Howzu Says</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblMessage" runat="server"></asp:Label>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="goBack()"  runat="server" Text="Close"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5 ">
        <a href="#" class="navbar-brand">Lab Details</a>
      </div>

     
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <div class="card ">
                        <h5 class="card-header fa-color">
                            Lab Contact Details<span><a href="#" id="HideEditbtn1" runat="server" data-toggle="modal" data-target="#labcontact" class=" float-right"><i class="fa fa-edit fa-color mr-1 HideEditbtn"></i></a></span></h5>
                        <div class="card-body">
                            <p class=" text-15">
                                Email:   <span id="spanLabEmail" runat="server"></span></p>
                            <p class="text-15">
                                Contact: <span id="spanLabContact" runat="server"></span></p>
                            <p class="text-15">
                                Address:  <span id="spanLabAddress" runat="server"></span></p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card ">
                        <h5 class="card-header fa-color">
                           <span id="spanLogo" class="img-thumbnail" runat="server"  clientidmode="Static"></span><span id="spanLabName"
                                        style="font-size: larger" runat="server" clientidmode="Static"></span><span><a href="#" id="HideEditbtn"  runat="server" data-toggle="modal" data-target="#labdetails"  class="float-right"><i class="fa fa-edit fa-color mr-1 HideEditbtn"></i></a></span></h5>
                        <div class="card-body row">
                            <div class="col-md-6">
                                <p class=" text-15">
                                    Lab ID:  <span id="spanLabId" runat="server" clientidmode="Static"></span></p>
                                <p class="text-15">
                                    Lab Details:  <span id="spanLabDetails" runat="server" clientidmode="Static"></span></p>
                               
                            </div>
                            <div class="col-md-6">
                               
                                <p class="text-15">
                                    Lab Picture:  <span id="spanImages" runat="server" clientidmode="Static"></span></p>
                                <p class="text-15">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="wrapper">
        <div id="content">
            <!-- Lab Details -->
            <div class="container-fluid lab-details">
              
                <!-- MODAL START -->
                   <asp:HiddenField ID="hSessionId" runat="server" ClientIDMode="Static" />
                <div id="labdetails" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                              <h4 class="modal-title">
                                    Edit Lab Details</h4>
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                              
                            </div>
                            <div class="modal-body">
                                <div class="cus-form">
                                    <div class="">
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Lab Name *" ID="txtLabName" runat="server"
                                                ClientIDMode="Static"></asp:TextBox>
                                            <label id="lblLabName" class="form-error">
                                            </label>
                                             <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Lab id *" style="display:none" ID="txtLabRegID" runat="server"
                                                ClientIDMode="Static"></asp:TextBox>
                                            <label id="lblLabRegID" class="form-error">
                                            </label>
                                        </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:HiddenField ID="hiddenLogo" runat="server" ClientIDMode="Static" />
                                             <label > Choose Logo</label>
                                            <div class="fileUpload btn btn-color">
                                                <span>Browse</span>
                                                <input id="fileLogo" accept="image/*"  clientidmode="Static" onchange="javascript:logoSelect()"
                                                    type="file" class="upload" />
                                            </div>
                                            <div id="logoDiv" runat="server" clientidmode="Static">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:HiddenField ID="hiddenImages" runat="server" ClientIDMode="Static" />
                                              <label > Choose Images</label>
                                            <div class="fileUpload btn btn-color">
                                                <span>Browse</span>
                                                <input id="fileImages" accept="image/*" multiple="multiple" clientidmode="Static"
                                                    onchange="javascript:imageSelect()" type="file" class="upload" />
                                            </div>
                                            <div id="imageDiv" runat="server" clientidmode="Static">
                                            </div>
                                        </div>
                                     
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Lab Details" ID="txtLabDetails" runat="server"
                                                ClientIDMode="Static"></asp:TextBox>
                                            <label id="lblLabDetails" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnupdate" runat="server" Text="Update" 
                                    onclick="btnupdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="labcontact" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header"> <h4 class="modal-title">
                                    Edit Lab Contact</h4>
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                               
                            </div>
                            <div class="modal-body">
                                <div class="cus-form">
                                    <div class="">
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Enter Email id *" ID="txtLabEmail"
                                                runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <label id="lblLabEmail" class="form-error">
                                            </label>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Enter Contact Number" ID="txtLabContact"
                                                MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <label id="lblLabContact" class="form-error">
                                            </label>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Lab Address" ID="txtLabAddress" runat="server"
                                                ClientIDMode="Static"></asp:TextBox>
                                            <label id="lblLabAddress" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" class="btn btn-submit" runat="server" Text="Update"
                                    OnClientClick="javascript:return validateLabContact()" OnClick="btnUpdate_Click1"
                                    ClientIDMode="Static" />
                                <%--   <a href="" class="lab-btn-default" data-dismiss="modal">Close</a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                   <script type="text/javascript" src="js/jquery.js"></script>
                <script type="text/javascript" src="js/LabInfoValidation.js"></script>
                <script type="text/javascript">
                    $(document).ready(function () {

                        if ($("#hiddenImages").val() != "") {
                            $("#imageDiv").empty();
                            var files = $("#hiddenImages").val().split("@");
                            var content = "<table><tr>";
                            var count = 0;
                            for (var img = 0; img < files.length; img++) {
                                if (files[img] != "") {
                                    if (count >= 5) {
                                        content += "</tr><tr>";
                                        count = 0;
                                    }
                                    content += "<td id='" + files[img] + "' align='center' style='color:red;cursor:pointer' onclick='javascript:remove(this)'><img src='temp/" + files[img] + "' id='" + files[img] + "' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div></td>";
                                    count += 1;
                                }
                            }

                            var contentDisplay = "<table><tr>";
                            count = 0;
                            for (var img = 0; img < files.length; img++) {
                                if (files[img] != "") {
                                    if (count >= 5) {
                                        contentDisplay += "</tr><tr>";
                                        count = 0;
                                    }
                                    contentDisplay += "<td id='" + files[img] + "' align='center'><img src='temp/" + files[img] + "' id='" + files[img] + "' height='50px' width='50px'/></td>";
                                    count += 1;
                                }
                            }

                            content += "</tr>";
                            $("#imageDiv").append(content);
                            $("#spanImages").append(contentDisplay);
                        }

                        if ($("#hiddenLogo").val() != "") {
                            $("#logoDiv").html("<img title='" + $("#hiddenLogo").val() + "' src='temp/" + $("#hiddenLogo").val() + "' id='" + $("#hiddenLogo").val() + "' style='color:red;cursor:pointer' onclick='javascript:removeLogo(this)' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div>");
                            $("#spanLogo").html("<img title='" + $("#hiddenLogo").val() + "' src='temp/" + $("#hiddenLogo").val() + "' id='" + $("#hiddenLogo").val() + "' height='50px' width='50px'/>");
                        }

                        if ($("#HDMLTSignature").val() != "") {
                            $("#DMLTSignatureDiv").html("<img title='" + $("#HDMLTSignature").val() + "' src='temp/" + $("#HDMLTSignature").val() + "' id='" + $("#HDMLTSignature").val() + "' style='color:red;cursor:pointer' onclick='javascript:removeDMLTSign(this)' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div>");
                            $("#spanDMLT").html("<img title='" + $("#HDMLTSignature").val() + "' src='temp/" + $("#HDMLTSignature").val() + "' id='" + $("#HDMLTSignature").val() + "' height='70px' width='100px'/>");
                        }
                        if ($("#HMDPathologistSignature").val() != "") {
                            $("#MDPathologistSignatureDiv").html("<img title='" + $("#HMDPathologistSignature").val() + "' src='temp/" + $("#HMDPathologistSignature").val() + "' id='" + $("#HMDPathologistSignature").val() + "' style='color:red;cursor:pointer' onclick='javascript:removeMDSign(this)' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div>");
                            $("#spanMD").html("<img title='" + $("#HMDPathologistSignature").val() + "' src='temp/" + $("#HMDPathologistSignature").val() + "' id='" + $("#HMDPathologistSignature").val() + "' height='70px' width='100px'/>");
                        }
                    });

                    function logoSelect() {
                        var formData = new FormData();
                        var files = document.getElementById("fileLogo").files;
                        var hiddenLogoValue = "";

                        for (var i = 0; i < files.length; i++) {
                            formData.append(files[i].name, files[i]);
                            hiddenLogoValue += files[i].name;
                        }

                        formData.append("request", "XMLHttp");
                        $("#hiddenLogo").val(hiddenLogoValue);
                        var xhr = new XMLHttpRequest();
                        xhr.open("POST", "LabInfoDetails.aspx", false);
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4) {
                                //alert('successful');
                                $("#logoDiv").empty();
                                var logo = $("#hiddenLogo").val();
                                var content = "<table><tr>";

                                if (logo != "") {
                                    content += "<td id='" + logo + "' align='center' style='color:red;cursor:pointer' onclick='javascript:removeLogo(this)'><img title='" + logo + "' src='temp/" + logo + "' id='" + logo + "' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div></td>";
                                }
                                content += "</tr>";
                                $("#logoDiv").append(content);
                            }
                        }
                        xhr.send(formData);
                    }

                    function imageSelect() {
                        var formData = new FormData();
                        var files = document.getElementById("fileImages").files;
                        var hiddenImagesValue = ($("#hiddenImages").val() != "") ? $("#hiddenImages").val() + "@" : "";
                        for (var i = 0; i < files.length; i++) {
                            formData.append(files[i].name, files[i]);
                            hiddenImagesValue += files[i].name + "@";
                        }

                        formData.append("request", "XMLHttp");
                        $("#hiddenImages").val(hiddenImagesValue.replace("@@", "@"));
                        //alert($("#hiddenImages").val());

                        var xhr = new XMLHttpRequest();
                        xhr.open("POST", "LabInfoDetails.aspx", false);
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4) {
                                //alert('successful');
                                $("#imageDiv").empty();
                                var files = $("#hiddenImages").val().split("@");
                                var content = "<table><tr>";
                                var count = 0;
                                for (var img = 0; img < files.length; img++) {
                                    if (files[img] != "") {
                                        if (count >= 5) {
                                            content += "</tr><tr>";
                                            count = 0;
                                        }
                                        content += "<td id='" + files[img] + "' align='center' style='color:red;cursor:pointer' onclick='javascript:remove(this)' ><img title='" + files[img] + "' src='temp/" + files[img] + "' id='" + files[img] + "' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div></td>";
                                        count += 1;
                                    }
                                }
                                content += "</tr>";
                                $("#imageDiv").append(content);
                            }
                        }
                        xhr.send(formData);
                    }

                    function DMLTimageSelect() {

                        var formData = new FormData();
                        var files = document.getElementById("file1").files;
                        var hiddenDMLTSignValue = "";

                        for (var i = 0; i < files.length; i++) {
                            formData.append(files[i].name, files[i]);
                            hiddenDMLTSignValue += files[i].name;
                        }


                        formData.append("request", "XMLHttp");
                        $("#HDMLTSignature").val(hiddenDMLTSignValue.replace("@@", "@"));
                        //alert($("#hiddenImages").val());

                        var xhr = new XMLHttpRequest();
                        xhr.open("POST", "LabInfoDetails.aspx", false);
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4) {
                                //alert('successful');
                                $("#DMLTSignatureDiv").empty();
                                var Sign = $("#HDMLTSignature").val();
                                var content = "<table><tr>";
                                if (Sign != "") {
                                    content += "<td id='" + Sign + "' align='center' style='color:red;cursor:pointer' onclick='javascript:removeDMLTSign(this)'><img title='" + Sign + "' src='temp/" + Sign + "' id='" + Sign + "' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div></td>";
                                }
                                content += "</tr>";
                                $("#DMLTSignatureDiv").append(content);
                            }
                        }
                        xhr.send(formData);
                    }

                    function MDimageSelect() {

                        var formData = new FormData();
                        var files = document.getElementById("file2").files;
                        var hiddenMDSignValue = "";
                        for (var i = 0; i < files.length; i++) {
                            formData.append(files[i].name, files[i]);
                            hiddenMDSignValue += files[i].name;
                        }
                        formData.append("request", "XMLHttp");
                        $("#HMDPathologistSignature").val(hiddenMDSignValue.replace("@@", "@"));
                        //alert($("#hiddenImages").val());

                        var xhr = new XMLHttpRequest();
                        xhr.open("POST", "LabInfoDetails.aspx", false);
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4) {
                                //alert('successful');
                                $("#MDPathologistSignatureDiv").empty();
                                var MDSign = $("#HMDPathologistSignature").val();
                                var content = "<table><tr>";
                                if (MDSign != "") {
                                    content += "<td id='" + MDSign + "' align='center' style='color:red;cursor:pointer' onclick='javascript:removeMDSign(this)'><img title='" + MDSign + "' src='temp/" + MDSign + "' id='" + MDSign + "' height='50px' width='50px'/><div class='closeicon'><i class='fa fa-times-circle'></i></div></td>";
                                }
                                content += "</tr>";
                                $("#MDPathologistSignatureDiv").append(content);
                            }
                        }
                        xhr.send(formData);
                    }

                    function remove(img) {
                        $(img).remove();
                        $("#hiddenImages").val($("#hiddenImages").val().replace($(img).attr('id'), "").replace("@@", "@"));
                        // alert($("#hiddenImages").val());   
                    }
                    function removeLogo(img) {
                        $(img).remove();
                        $("#hiddenLogo").val($("#hiddenLogo").val().replace($(img).attr('id'), ""));
                        //alert($("#hiddenLogo").val());   
                    }

                    function removeDMLTSign(img) {
                        $(img).remove();
                        $("#HDMLTSignature").val($("#HDMLTSignature").val().replace($(img).attr('id'), ""));
                        //alert($("#hiddenLogo").val());   
                    }
                    function removeMDSign(img) {
                        $(img).remove();
                        $("#HMDPathologistSignature").val($("#HMDPathologistSignature").val().replace($(img).attr('id'), ""));
                        //alert($("#hiddenLogo").val());   
                    }
                </script>
                <script type="text/javascript">

                    function IsValidEmail(email) {
                        var re = /\S+@\S+\.\S+/;
                        return re.test(email);
                    }

                    function isNumber(evt) {
                        evt = (evt) ? evt : window.event;
                        var charCode = (evt.which) ? evt.which : evt.keyCode;
                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                            return false;
                        }
                        return true;
                    }
                </script>
</asp:Content>
