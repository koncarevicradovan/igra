

function GameServerProxy() {
  var self = this;


  self.callLogin = function (username, password, callback) {

    $.ajax({
      url: '/api/loginapi/login',
      type: 'POST',
      dataType: 'json',
      data: {
        username: username,
        password: password
      },
      success: function (data, textStatus, xhr) {
        callback(textStatus);
      },
      error: function (xhr, textStatus, errorThrown) {
        callback(textStatus, xhr.responseJSON);
      }
    });
  };

  self.registerCall = function (username, password, firstname, lastname, callback) {

    $.ajax({
      url: '/api/loginapi/register',
      type: 'POST',
      dataType: 'json',
      data: {
        username: username,
        password: password,
        firstname: firstname,
        lastname: lastname
      },
      success: function (data, textStatus, xhr) {
        callback(textStatus);
      },
      error: function (xhr, textStatus, errorThrown) {
        callback(textStatus, xhr.responseJSON);
      }
    });
  };
}