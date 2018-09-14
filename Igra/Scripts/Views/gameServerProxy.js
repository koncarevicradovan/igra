

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
        callback(textStatus, data);
      },
      error: function (xhr, textStatus, errorThrown) {
        callback(textStatus, xhr.responseJSON);
      }
    });
  };

  self.registerCall = function (username, password, firstname, lastname, gender, question1, question2, question3, question4, question5, question6, question7, question8, question9, question10, callback) {
    var isFemale = gender == 'female';
    $.ajax({
      url: '/api/loginapi/register',
      type: 'POST',
      dataType: 'json',
      data: {
        username: username,
        password: password,
        firstname: firstname,
        lastname: lastname,
        isFemale: isFemale,
        question1: question1,
        question2: question2,
        question3: question3,
        question4: question4,
        question5: question5,
        question6: question6,
        question7: question7,
        question8: question8,
        question9: question9,
        question10: question10,
      },
      success: function (data, textStatus, xhr) {
        callback(textStatus);
      },
      error: function (xhr, textStatus, errorThrown) {
        callback(textStatus, xhr.responseJSON);
      }
    });
  };
  self.checkForOpponent = function (callback) {
    $.ajax({
      url: '/api/gameapi/checkForOpponent',
      type: 'POST',
      dataType: 'json',
      success: function (data, textStatus, xhr) {
        callback(textStatus, data);
      },
      error: function (xhr, textStatus, errorThrown) {
        callback(textStatus, xhr.responseJSON);
      }
    });

    self.playCard1 = function (gameId, cardNumber, callback) {
      $.ajax({
        url: '/api/gameapi/playCard1',
        type: 'POST',
        dataType: 'json',
        data: {
          gameId: gameId,
          cardNumber: cardNumber
        },
        success: function (data, textStatus, xhr) {
          callback(textStatus, data.OpponentName, data.OpponentPoints, data.MyPoints);
        },
        error: function (xhr, textStatus, errorThrown) {
          callback(textStatus, xhr.responseJSON);
        }
      });
    };

    self.playCard2 = function (gameId, cardNumber, callback) {
      $.ajax({
        url: '/api/gameapi/playCard2',
        type: 'POST',
        dataType: 'json',
        data: {
          gameId: gameId,
          cardNumber: cardNumber
        },
        success: function (data, textStatus, xhr) {
          callback(textStatus, data.OpponentPoints, data.MyPoints);
        },
        error: function (xhr, textStatus, errorThrown) {
          callback(textStatus, xhr.responseJSON);
        }
      });
    };

    self.playCard3 = function (gameId, cardNumber, callback) {
      $.ajax({
        url: '/api/gameapi/playCard3',
        type: 'POST',
        dataType: 'json',
        data: {
          gameId: gameId,
          cardNumber: cardNumber
        },
        success: function (data, textStatus, xhr) {
          callback(textStatus, data.OpponentPoints, data.MyPoints);
        },
        error: function (xhr, textStatus, errorThrown) {
          callback(textStatus, xhr.responseJSON);
        }
      });
    };

    self.playCard4 = function (gameId, cardNumber, callback) {
      $.ajax({
        url: '/api/gameapi/playCard4',
        type: 'POST',
        dataType: 'json',
        data: {
          gameId: gameId,
          cardNumber: cardNumber
        },
        success: function (data, textStatus, xhr) {
          callback(textStatus, data.OpponentPoints, data.MyPoints);
        },
        error: function (xhr, textStatus, errorThrown) {
          callback(textStatus, xhr.responseJSON);
        }
      });
    };
  };
}