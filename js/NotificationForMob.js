function sendnotification(mobnotid, Message) {

    var Data = {
        sAction: "GCMNotification",
        regKey: mobnotid,
        sMobMessage: Message
    }
  //  alert('inserted');
    $.ajax({
        type: "GET",
        // url: "http://192.168.4.15:8011/service/NotificationFromLab.ashx",
        url: "mobileapp/service/NotificationFromLab.ashx",


        data: Data,
        //  contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
          //  alert("successs");
          //  alert(response);
            //                localStorage.setItem('UserregistrationId', response);

        },
        failure: function (error) {
            //                alert("error");
            //                alert(error);
        }
    });
}