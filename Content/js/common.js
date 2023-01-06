$(document).ready(function () {
    $('.filterbutton .btn').on('click',function(e){
      e.preventDefault();
      $('.btn').removeClass('color');
      $(this).addClass('color')
    })


    $(".box1 select").change(function () {
      $(this).find("option:selected").each(function () {
        var optionValue = $(this).attr("value");
        if (optionValue) {
          $(".chart1").not("." + optionValue).hide();
          $("." + optionValue).show();
        } else {
          $(".chart1").hide();
        }
      });
    }).change();

    $(".box2 select").change(function () {
      $(this).find("option:selected").each(function () {
        var optionValue = $(this).attr("value");
        if (optionValue) {
          $(".chart2").not("." + optionValue).hide();
          $("." + optionValue).show();
        } else {
          $(".chart2").hide();
        }
      });
    }).change();

    $(".box3 select").change(function () {
      $(this).find("option:selected").each(function () {
        var optionValue = $(this).attr("value");
        if (optionValue) {
          $(".chart3").not("." + optionValue).hide();
          $("." + optionValue).show();
        } else {
          $(".chart3").hide();
        }
      });
    }).change();

    $(".box4 select").change(function () {
        $(this).find("option:selected").each(function () {
          var optionValue = $(this).attr("value");
          if (optionValue) {
            $(".chart4").not("." + optionValue).hide();
            $("." + optionValue).show();
          } else {
            $(".chart4").hide();
          }
        });
      }).change();

      $(".box5 select").change(function () {
        $(this).find("option:selected").each(function () {
          var optionValue = $(this).attr("value");
          if (optionValue) {
            $(".chart5").not("." + optionValue).hide();
            $("." + optionValue).show();
          } else {
            $(".chart5").hide();
          }
        });
      }).change();

      
      $(".box6 select").change(function () {
        $(this).find("option:selected").each(function () {
          var optionValue = $(this).attr("value");
          if (optionValue) {
            $(".chart6").not("." + optionValue).hide();
            $("." + optionValue).show();
          } else {
            $(".chart6").hide();
          }
        });
      }).change();

    //for filter button toggle
$("#filterBtn").click(function () 
    {
      $(".selectTime").toggle();
    });
    //close filter on close button
  $("#closeFilter").click(function () 
    {
      $(".selectTime").css("display", 'none');
    });
});
 