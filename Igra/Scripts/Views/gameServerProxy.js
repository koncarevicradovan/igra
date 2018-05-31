

function GameServerProxy() {
  var self = this;


  self.callLogin = function(username, password) {
    //$.post("/api/values",
    //  {
    //    value: username,
    //    //city: "Duckburg"
    //  },
    //  function (data, status) {
    //    alert("Data: " + data + "\nStatus: " + status);
    //  });

    //$.ajax({
    //  url: '/api/values',
    //  cache: false,
    //  type: 'POST',
    //  contentType: 'application/json; charset=utf-8',
    //  data: {
    //    value: username
    //  },
    //  success: function (data) {
    //    alert('ok');
    //  }
    //})

    $.ajax({

      url: '/api/values',

      type: 'POST',

      dataType: 'json',

      data: {
        value: username
      },

      success: function (data, textStatus, xhr) {

        console.log(data);

      },

      error: function (xhr, textStatus, errorThrown) {

        console.log('Error in Operation');

      }

    });
  };
}