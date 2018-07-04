function GameModel() {

  //Make the self as 'this' reference
  var self = this;
  var serverProxy = new GameServerProxy();

  // login form

  self.username = ko.observable("");
  self.password = ko.observable("");
  self.usernameRegister = ko.observable("");
  self.passwordRegister = ko.observable("");
  self.nameRegister = ko.observable("");
  self.lastnameRegister = ko.observable("");
  self.gender = ko.observable("male");

  self.login = function () {
    var callback = function (serverResponse, message) {
      if (serverResponse == 'success') {
        toastr.success("Uspešno ste se ulogovali.");
        localStorage.setItem("username", self.username());
        window.location.href = "/tutorial";
      } else {
        toastr.error(message);
      }
    };
    serverProxy.callLogin(self.username(), self.password(), callback);
  };

  self.register = function () {
    var callback = function (serverResponse, message) {
      if (serverResponse == 'success') {
        toastr.success("Uspešno ste se registrovali.");
        window.location.href = "/tutorial";
      } else {
        toastr.error(message);
      }
    };
    serverProxy.registerCall(self.usernameRegister(), self.passwordRegister(), self.nameRegister(), self.lastnameRegister(), self.gender(), callback);
  };

  self.clearLocalStorage = function () {
    localStorage.clear();
  };

  // tutorial
  self.gotoGame = function () {
    window.location.href = "/game";
  };

  self.isGameStarted = false;
  self.gameNumber = ko.observable(0);
  self.displayLoader = ko.observable(false);

  // uspostavljanje igre
  self.checkForOpponent = function () {
    var callback = function (serverResponse, gameId) {
      if (serverResponse == 'success') {
        self.isGameStarted = true;
        self.gameNumber(1);
        localStorage.setItem("gameId", gameId);
      } else {
        //setTimeout(function () { self.checkForOpponent(); }, 2000);
      }
    };
    if (!self.isGameStarted) {
      serverProxy.checkForOpponent(callback);
    }
  };

  $.connection.tasks.client.tellOpponentThatGameIsCreated = function (receiver, gameId) {
    if (receiver == localStorage.getItem("username")) {
      localStorage.setItem("gameId", gameId);
      self.isGameStarted = true;
      self.gameNumber(1);
    }
  };

  // igra 1

  self.opponentName = ko.observable('');
  self.opponentPoints1 = ko.observable(0);
  self.myPoints1 = ko.observable(0);

  var playCard1 = function (cardNumber) {
    var callback = function (serverResponse, opponentName, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        self.opponentName(opponentName);
        self.opponentPoints1(opponentPoints);
        self.myPoints1(myPoints);
        self.displayLoader(false);
        self.gameNumber(2);
      } else {
        self.displayLoader(true);
      }
    };
    serverProxy.playCard1(localStorage.getItem("gameId"), cardNumber, callback);
  };

  $.connection.tasks.client.opponentPlayed1 = function (receiver, opponentName, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentName(opponentName);
      self.opponentPoints1(opponentPoints);
      self.myPoints1(myPoints);
      self.displayLoader(false);
      self.gameNumber(2);
    }
  };

  self.playCard11 = function () {
    playCard1(1);
  };

  self.playCard12 = function () {
    playCard1(2);
  };

  // igra 2
  self.opponentPoints2 = ko.observable(0);
  self.myPoints2 = ko.observable(0);

  var playCard2 = function (cardNumber) {
    var callback = function (serverResponse, opponentName, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        self.opponentName(opponentName);
        self.opponentPoints2(opponentPoints);
        self.myPoints2(myPoints);
        self.displayLoader(false);
        self.gameNumber(3);
      } else {
        self.displayLoader(true);
      }
    };
    serverProxy.playCard2(localStorage.getItem("gameId"), cardNumber, callback);
  };

  self.playCard21 = function () {
    playCard2(1);
  };

  self.playCard22 = function () {
    playCard2(2);
  };

  $.connection.tasks.client.opponentPlayed2 = function (receiver, opponentName, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentName(opponentName);
      self.opponentPoints2(opponentPoints);
      self.myPoints2(myPoints);
      self.displayLoader(false);
      self.gameNumber(3);
    }
  };

  // igra 3
  self.opponentPoints3 = ko.observable(0);
  self.myPoints3 = ko.observable(0);

  var playCard3 = function (cardNumber) {
    var callback = function (serverResponse, opponentName, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        self.opponentName(opponentName);
        self.opponentPoints3(opponentPoints);
        self.myPoints3(myPoints);
        self.displayLoader(false);
        self.gameNumber(5);
      } else {
        self.displayLoader(true);
      }
    };
    serverProxy.playCard3(localStorage.getItem("gameId"), cardNumber, callback);
  };

  self.playCard31 = function () {
    playCard3(1);
  };

  self.playCard32 = function () {
    playCard3(2);
  };

  $.connection.tasks.client.opponentPlayed3 = function (receiver, opponentName, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentName(opponentName);
      self.opponentPoints3(opponentPoints);
      self.myPoints3(myPoints);
      self.displayLoader(false);
      self.gameNumber(5);
    }
  };

  // igra 4
  self.opponentPoints4 = ko.observable(0);
  self.myPoints4 = ko.observable(0);

  var playCard4 = function (cardNumber) {
    var callback = function (serverResponse, opponentName, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        self.opponentName(opponentName);
        self.opponentPoints4(opponentPoints);
        self.myPoints4(myPoints);
        self.displayLoader(false);
        self.gameNumber(5);
      } else {
        self.displayLoader(true);
      }
    };
    serverProxy.playCard4(localStorage.getItem("gameId"), cardNumber, callback);
  };

  self.playCard41 = function () {
    playCard4(1);
  };

  self.playCard42 = function () {
    playCard4(2);
  };

  $.connection.tasks.client.opponentPlayed4 = function (receiver, opponentName, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentName(opponentName);
      self.opponentPoints4(opponentPoints);
      self.myPoints4(myPoints);
      self.displayLoader(false);
      self.gameNumber(5);
    }
  };

  // login and register form behaviour
  $(function () {
    $('#login-form-link').click(function (e) {
      $("#login-form").delay(100).fadeIn(100);
      $("#register-form").fadeOut(100);
      $('#register-form-link').removeClass('active');
      $(this).addClass('active');
      e.preventDefault();
    });
    $('#register-form-link').click(function (e) {
      $("#register-form").delay(100).fadeIn(100);
      $("#login-form").fadeOut(100);
      $('#login-form-link').removeClass('active');
      $(this).addClass('active');
      e.preventDefault();
    });

  });
}
var viewModel = new GameModel();
ko.applyBindings(viewModel);
$.connection.hub.start(function () {/* vm.init()*/; });