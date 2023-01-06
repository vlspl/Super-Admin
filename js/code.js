//searchbar
$(".search-btn ").click(function(){
  $(".search-box .input").toggleClass("active").focus;
  $(this).toggleClass("animate");
  $("search-box .input").val("");
});

