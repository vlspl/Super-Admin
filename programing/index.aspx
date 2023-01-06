<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>
<html lang="en">
<head>
<title>TVS Build your Bike</title>
<meta name="description" content="TVS Expo 2018">
<meta name="keywords" content="TVS Expo 2018">
<meta name="author" content="TVS">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
<link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="css/owl.carousel.min.css">
<link rel="stylesheet" type="text/css" href="css/owl.theme.default.css">
<link rel="stylesheet" type="text/css" href="css/tvs.css">
<!-- Favicon -->
<link rel="shortcut icon" href="images/favicon.png">
<link rel="apple-touch-icon" href="images/apple-touch-icon.png">
<link rel="apple-touch-icon" sizes="72x72" href="images/apple-touch-icon-72x72.png">
<link rel="apple-touch-icon" sizes="114x114" href="images/apple-touch-icon-114x114.png">


    <script src="http://code.jquery.com/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="jquery.timer.js"></script>
    <script type="text/javascript" src="res/demo.js"></script>
 
 <style>
 span#stopwatch {
    margin: 79px 0 0 0;
    position: absolute;
    color: red;
}
 </style>

</head>
<body class="buildBike">
<form runat="server">

<div id="jquery-script-menu">
<div class="jquery-script-center">


</div>
<div class="container">
    <span id="stopwatch" runat="server">00:00:00</span>
<!--    <p>
        <input class='btn btn-primary' type='button' value='Play/Pause' onclick='Example1.Timer.toggle();' />
        <input class='btn btn-primary' type='button' value='Stop/Reset' onclick='Example1.resetStopwatch();' />
    </p>-->
    <br/>
    <br/>


</div>


<nav id="navigation" class="navbar navbar-default ir-navbar navbar-fixed-top">
  <div class="container">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#ir-navbar"> <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </button>
      <a class="navbar-brand ir-logo" href="index.html"><img src="images/autoExpo.png" alt="TVS Expo 2018"> </a> </div>
    <div id="ir-navbar" class="navbar-collapse collapse">
      <ul class="nav navbar-nav">
        <li><a href="#">Videos</a></li>
        <li><a href="#">Photos</a></li>
      </ul>
    </div>
  </div>
</nav>
<div class="gameContainer paddingTop">
  <div class="container">
    <h4>Build your very own <span><img src="images/jupiter/jupiter.png"></span></h4>
    <div class="bike">
      <div id="cardSlots"> </div>
      <img src="images/jupiter/jupiter-outlines.png"></div>
    <p>Drag and drop the parts to build your bike</p>
    <div class="owl-carousel owl-theme" id="cardPile"> </div>
  </div>
  <div class="rotateMob text-center"> <img src="images/rotate.png"> </div>
</div>


<!-- Modal -->
<div class="modal fade" id="successMessage" runat="server" role="dialog">
  <div class="modal-dialog"> 
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body text-center" id="mydiv">
        <h3 class="margintop-0"><i class="fa fa-smile-o" aria-hidden="true"></i> Congratulations!</h3>
        <p>Completed in <span id="mytime" runat="server"></span></p>
          <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" onclick="init();">Play Again</button>
        <div id="yosharebtn"></div>
        <asp:Button ID="Button1" Text="Share on Facebook" runat="server" UseSubmitBehavior="false"
    OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />
    <span runat="server" id="sharefbid"></span>

        <a href="#" data-href="http://visionarylifescience.com/programing/index.aspx" data-caption="An example caption" data-picture="http://visionarylifescience.com/images/myimg2.png" data-description="description" data-name="name" data-redirect_uri="http://visionarylifescience.com/programing/index.aspx" class="soc-facebook dhe_fb_share">yes</a>
       
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>








<!--<script src="js/jquery.js"></script> -->
<script src="js/jquery-ui.min.js"></script> 
<script src="js/jquery.ui.touch-punch.js"></script>
<script src="js/bootstrap.min.js"></script> 
<script src="js/owl.carousel.min.js"></script> 
<script type="text/javascript">
$('#cardPile div').draggable();
var correctCards = 0;
$(init);

function init() {

Example1.resetStopwatch();
  // Hide the success message
  $('#successMessage').modal('hide');

  // Reset the game
  correctCards = 0;
  $('#cardPile').html( '' );
  $('#cardSlots').html( '' );

  // Create the pile of shuffled cards
  var numbers = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 ];
  var terms = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11'];
  <!--numbers.sort( function() { return Math.random() - .5 } );-->

  for ( var i=0; i<11; i++ ) {
    $('<div class="item"><img src="images/jupiter/' + numbers[i] + '.png"/></div>').data( 'number', numbers[i] ).attr( 'id', 'card'+numbers[i] ).appendTo( '#cardPile' ).draggable( {
      
      stack: '#cardPile div',
      cursor: 'move',
      revert: true
    } );
  }

  // Create the card slots
  var words = [ '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11'];
  for ( var i=1; i<=11; i++ ) {
    $('<div><img src="images/jupiter/' + numbers[i-1] + '.png"/></div>').data( 'number', i ).appendTo( '#cardSlots' ).droppable( {
      accept: '#cardPile div',
      hoverClass: 'hovered',
      drop: handleCardDrop
    } );
  }

}

function handleCardDrop( event, ui ) {

  var slotNumber = $(this).data( 'number' );
  var cardNumber = ui.draggable.data( 'number' );

  // If the card was dropped to the correct slot,
  // change the card colour, position it directly
  // on top of the slot, and prevent it being dragged
  // again

  if ( slotNumber == cardNumber ) {
    ui.draggable.addClass( 'correct' );
    ui.draggable.draggable( 'disable' );
    $(this).droppable( 'disable' );
    ui.draggable.position( { of: $(this), my: 'center center', at: 'center center' } );
    ui.draggable.draggable( 'option', 'revert', false );
    correctCards++;
  } 
  
  // If all the cards have been placed correctly then display a message
  // and reset the cards for another go
  if ( correctCards >= 0 )
  {
  if(timer.isActive)
  {
  Example1.Timer.play();
  }
  }
  if ( correctCards == 2 ) {
    var compltime = $('#stopwatch').html();
	$('#successMessage').modal('show');Label1
	$('#mytime').html(compltime.toString() +" sec");
//	$('#sharefbid').html("<a href='#' data-href='http://visionarylifescience.com/programing/index.aspx' data-caption='An example caption' data-picture='http://visionarylifescience.com/images/" + compltime.toString() + ".png' data-description='description' data-name='name' data-redirect_uri='http://visionarylifescience.com/programing/index.aspx' class='soc-facebook dhe_fb_share'>yes</a>");
//    var imgdata = "<a href='#' data-href='http://visionarylifescience.com/programing/index.aspx' data-caption='An example caption' data-picture='http://visionarylifescience.com/images/labcare-logo.png' data-description='description' data-name='name' data-redirect_uri='http://visionarylifescience.com/programing/index.aspx' class='soc-facebook dhe_fb_share'>fb sha</a>"
//	$('#yosharebtn').html(imgdata);
    Example1.resetStopwatch();
  }

}
</script> 
<script>
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        dot: true,
        mouseDrag: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 5
            }
        }
    })	
</script>



<%--<a href="#" data-href="http://visionarylifescience.com/programing/index.aspx" data-caption="An example caption" data-picture="http://visionarylifescience.com/images/labcare-logo.png" data-description="description" data-name="name" data-redirect_uri="http://visionarylifescience.com/programing/index.aspx" class="soc-facebook dhe_fb_share">fb sha</a>--%>

<asp:HiddenField ID="hfImageData" runat="server" ClientIDMode="Static" />

<%--<asp:Button ID="btnExport" Text="Export to Image" runat="server" UseSubmitBehavior="false"
    OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />--%>
   <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/html2canvas.min.js"></script>
<script type="text/javascript">
    function ConvertToImage(btnExport) {
        html2canvas($("#mydiv")[0]).then(function (canvas) {
            var base64 = canvas.toDataURL();
            $("[id*=hfImageData]").val(base64);
            __doPostBack(btnExport.name, "");
        });
        return false;
    }
</script>



<script>

    /*
    * facebook SDK
    */
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));

    /*
    * facebook APP init
    */
    window.fbAsyncInit = function () {
        FB.init({
            appId: '544730365893171',
            xfbml: true,
            version: 'v2.5'
        });
    };



    /*
    * Use facebook.com/sharer/sharer.php and graph.facebook.com/
    */
    var dhe = dhe || {};
    dhe.fb_share2 = (function () {
        /*
        * 
        * return following type of json object.
        * {
        *     "id": "http://news.ycombinator.com",
        *     "shares": 8868,
        *     "comments": 3
        * }
        */
        function loadFBCount(url, updateCallBack) {
            var xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function () {
                if (xhttp.readyState == 4 && xhttp.status == 200) {
                    updateCallBack(JSON.parse(xhttp.responseText));
                }
            };
            xhttp.open("GET", "http://graph.facebook.com/" + url, true);
            xhttp.send();
        }

        /*
        * update fb fshare count to UI
        */
        function updateCount(fbCount) {
            var fbShare = document.querySelector(".soc_count.fb");
            if (fbShare) {
                fbShare.innerHTML = fbCount.shares;
            }
        }

        /*
        * get current url, remove the hash or querystring
        */
        function GetCurrentUrl() {
            var _location = window.location;
            return _location.origin + _location.pathname;
        }

        /*
        * bind click event for facebook share
        * load facebook total count
        */
        function init() {
            var all = document.querySelectorAll(".dhe_fb_share");
            for (var i = 0; i < all.length; i++) {
                all[i].onclick = fbShareEvent;
            };

            loadFBCount(encodeURIComponent(GetCurrentUrl()), updateCount);
        }

        /*
        * get meta tag
        */
        function getMetaTag(tagName) {
            var metas = document.getElementsByTagName('meta');
            for (i = 0; i < metas.length; i++) {
                if (metas[i].getAttribute("property") == tagName) {
                    return metas[i].getAttribute("content");
                }
            }
        }

        /*
        * facebook share event
        */
        function fbShareEvent(e) {
            var _this = this;
            var data = {};
            data["app_id"] = "544730365893171";

            data["method"] = "feed";
            data["href"] = _this.dataset.href ? encodeURIComponent(_this.dataset.href) : encodeURIComponent(GetCurrentUrl());
            data["caption"] = (_this.dataset.caption) ? _this.dataset.caption : getMetaTag("og:title");
            data["name"] = (_this.dataset.name) ? _this.dataset.name : getMetaTag("og:type");
            data["description"] = (_this.dataset.description) ? _this.dataset.description : getMetaTag("og:description");
            data["picture"] = (_this.dataset.picture) ? _this.dataset.picture : getMetaTag("og:image");
            if (_this.dataset.redirect_uri) {
                data["redirect_uri"] = _this.dataset.redirect_uri;
            }
            console.info(data);
            FB.ui(data, function (response) {
                console.info(response);
            });
        }

        init();

        return {
            loadFBCount: loadFBCount
        };
    })();

</script>

</form>
</body>
</html>
