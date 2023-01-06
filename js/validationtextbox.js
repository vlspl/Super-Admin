var isatsymbol=false;var isdotsymbol=false;function onlyAlphabets(e,t){try{if(window.event){var charCode=window.event.keyCode;}
else if(e){var charCode=e.which;}
else{return true;}
if(charCode==8||charCode==0){}
else{if((charCode>64&&charCode<91)||(charCode>96&&charCode<123)||charCode==32)
return true;else
return false;}}
catch(err){alert(err.Description);}}
function valid(f){!(/^[A-z&#209;&#241;0-9]*$/i).test(f.value)?f.value=f.value.replace(/[^A-z&#209;&#241;0-9]/ig,''):null;}
function validEmailFormat(e,t,id){try{if(window.event){var charCode=window.event.keyCode;}
else if(e){var charCode=e.which;}
else{return true;}
if(charCode==8||charCode==0||charCode==64||charCode==46|| charCode == 95){var email=$("#"+id).val();if(email.indexOf('@')>=0){var temp=[];temp=email.split('@');email=temp[1];if(email.indexOf('.')>=0){}
else{isdotsymbol=false;}}
else{isatsymbol=false;isdotsymbol=false;}
if(isatsymbol&&charCode==64){return false;}
else if(isatsymbol&&isdotsymbol&&charCode==46){return false;}
if(charCode==64){isatsymbol=true;}
else if(isatsymbol&&charCode==46){isdotsymbol=true;}
return true;}
else if(charCode>=48&&charCode<=57&&!isdotsymbol){return true;}
else{if((charCode>64&&charCode<91)||(charCode>96&&charCode<123)||charCode==32|| charCode == 95)
return true;else
return false;}}
catch(err){}}
function validMobileNo(e,t){var charCode;try{if(window.event){charCode=window.event.keyCode;}
else if(e){charCode=e.which;}
else{return true;}
if(charCode==8||charCode==0){return true;}
else if(charCode>=48&&charCode<=57){return true;}
else{return false;}}
catch(err){}}
var name="",phone="",email="",state="",mobiletype="",internet="";function validateDhruForm(){name=$("#txtName").val();var tcount=0;if(name==""){tcount++;$("#spanTxtName").show();}
else{$("#spanTxtName").hide();}
phone=$("#txtPhone").val();if(phone==""){tcount++;$("#spantxtphone").show();}
else
$("#spantxtphone").hide();if(phone.length==10){$("#spantxtphone").hide();}
else{tcount++;$("#spantxtphone").show();}
email=$("#txtemail").val();if(email==""){tcount++;$("#spanEmailMsg").show();}
else
$("#spanEmailMsg").hide();if(!ValidEmailfn()){tcount++;$("#spanEmailMsg").show();}
else
$("#spanEmailMsg").hide();state=$("#ddlstate").val();if(state=="0"){tcount++;$("#spanddlstate").show();}
else
$("#spanddlstate").hide();mobiletype=$("#mpo").val();if(mobiletype=="0"){tcount++;$("#spanmpo").show();}
else{$("#spanmpo").hide();}
internet=$("#icm").val();if(internet=="0"){tcount++;$("#spanicm").show();}
else{$("#spanicm").hide();}
var chkagree=$("#chkagree").attr('v');if(chkagree==0){$("#spanchk").show();tcount++;}
else
$("#spanchk").hide();if(tcount>0)
return false;else
return true;}
function ValidEmailfn(){var email=$("#txtemail").val();if(email.indexOf("@")>=0){var tempemal=[];tempemal=email.split('@');if(tempemal.length==2){email=tempemal[1];tempemal=null;if(email.indexOf('.')>=0){tempemal=email.split('.');if(tempemal.length==2){return true;}
else
return false;}
else
return false;}
else{return false;}}
else{return false;}}
$(document).ready(function(){$(document).on('click','#chkagree',function(){var v=$(this).attr('v');if(v==1){$(this).attr('v',0);}
else if(v==0){$(this).attr('v',1);}});$(document).on('click',"#btnclosess",function(){$("#divmodels").hide();});$(document).on('click','#abtnsubmit',function(){var v=$(this).attr('v');if(v==0){}
else{return false;}
var url=window.location.href;var spliturls=url.split("/");var urls=spliturls[5];var languId=2;if(spliturls[3]=='hindi'){languId=1;}
var site_id=$('#ContentPlaceHolder1_site_id').val();if(site_id==undefined){site_id='';}
var utm_source=$('#ContentPlaceHolder1_utm_source').val();if(utm_source==undefined){utm_source='';}
$("#lblreqmsg").hide();if(validateDhruForm()){$(this).attr('v',1);$.ajax({url:"/services/dhurv.ashx",type:'Post',data:{sAction:'InsertDhruv',name:name,phone:phone,email:email,state:state,address:$("#txtAddress").val(),city:$("#txtcity").val(),mobiletype:mobiletype,internet:internet,languId:languId,urls:urls,site_id:site_id,utm_source:utm_source},success:function(result){$(this).attr('v',0);$("#txtemail").val('');$("#txtName").val('');$("#txtPhone").val('');$("#txtAddress").val('');$("#txtcity").val('');$("#ddlstate").val(0);$("#mpo").val(0);$("#icm").val(0);$("#divmodels").show();window.location.href="http://localhost:49234/tractor-mechanisation-solutions/tractor/request-a-demo/thank-you";},error:function(err){alert(err);}});}
else{}});});$(document).ready(function(e){$('#Btn_Submit').click(function(){var email=$('#TXT_EMAIL').val();if($.trim(email).length==0){alert('Please Enter Valid Email Address');return false;}
if(validateEmail(email)){alert('Valid Email Address');return false;}
else{alert('Invalid Email Address');return false;}});});function validateEmail(email){var reg=/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
if(reg.test(email)){return true;}
else{return false;}}