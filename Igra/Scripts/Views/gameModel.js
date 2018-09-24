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
  self.question1 = ko.observable(1);
  self.gender = ko.observable("male");
  self.question1 = ko.observable("1");
  self.question2 = ko.observable("1");
  self.question3 = ko.observable("1");
  self.question4 = ko.observable("1");
  self.question5 = ko.observable("1");
  self.question6 = ko.observable("1");
  self.question7 = ko.observable("1");
  self.question8 = ko.observable("1");
  self.question9 = ko.observable("1");
  self.question10 = ko.observable("1");
  self.question11 = ko.observable("1");
  self.question12 = ko.observable("1");
  self.question13 = ko.observable("1");
  self.question14 = ko.observable("1");
  self.question15 = ko.observable("1");
  self.question16 = ko.observable("1");
  self.question17 = ko.observable("1");
  self.question18 = ko.observable("1");
  self.question19 = ko.observable("1");

  self.login = function () {
    var callback = function (serverResponse, data) {
      if (serverResponse == 'success') {
        toastr.success("Uspešno ste se ulogovali.");
        localStorage.setItem("username", self.username());
        localStorage.setItem("fullName", data);
        self.myFullName(data);
        window.location.href = "/tutorial";
      } else {
        toastr.error(data);
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
    serverProxy.registerCall(self.usernameRegister(), self.passwordRegister(), self.nameRegister(), self.lastnameRegister(), self.gender(), self.question1(), self.question2(), self.question3(), self.question4(), self.question5(), self.question6(), self.question7(), self.question8(), self.question9(), self.question10(), self.question11(), self.question12(), self.question13(), self.question14(), self.question15(), self.question16(), self.question17(), self.question18(), self.question19(), callback);
  };

  self.clearLocalStorage = function () {
    localStorage.clear();
  };

  self.downloadFile = function () {
    window.location.href = "game/download";
  };

  self.backToTutorial = function () {
    window.location.href = "/tutorial";
  };

  // tutorial
  self.gotoGame = function () {
    window.location.href = "/game";
  };

  self.isGameStarted = false;
  self.gameNumber = ko.observable(0);
  self.myFullName = ko.observable('');
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
      self.myFullName(localStorage.getItem("fullName"));
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

  self.opponentName = ko.observable('???');
  self.opponentPoints1 = ko.observable(0);
  self.myPoints1 = ko.observable(0);
  self.isImageDisplayed1 = ko.observable(true);
  self.changeIsImageDisplayed1 = function () {
    self.isImageDisplayed1(false);
  };

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
  self.isImageDisplayed2 = ko.observable(true);
  self.changeIsImageDisplayed2 = function () {
    self.isImageDisplayed2(false);
  };

  var playCard2 = function (cardNumber) {
    var callback = function (serverResponse, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
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

  $.connection.tasks.client.opponentPlayed2 = function (receiver, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentPoints2(opponentPoints);
      self.myPoints2(myPoints);
      self.displayLoader(false);
      self.gameNumber(3);
    }
  };

  // igra 3
  self.opponentPoints3 = ko.observable(0);
  self.myPoints3 = ko.observable(0);
  self.isImageDisplayed3 = ko.observable(true);
  self.changeIsImageDisplayed3 = function () {
    self.isImageDisplayed3(false);
  };

  var playCard3 = function (cardNumber) {
    var callback = function (serverResponse, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        self.opponentPoints3(opponentPoints);
        self.myPoints3(myPoints);
        self.displayLoader(false);
        self.gameNumber(4);
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

  $.connection.tasks.client.opponentPlayed3 = function (receiver, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentPoints3(opponentPoints);
      self.myPoints3(myPoints);
      self.displayLoader(false);
      self.gameNumber(4);
    }
  };

  // igra 4
  self.opponentPoints4 = ko.observable(0);
  self.myPoints4 = ko.observable(0);
  self.winner = ko.observable('');
  self.isImageDisplayed4 = ko.observable(true);
  self.changeIsImageDisplayed4 = function () {
    self.isImageDisplayed4(false);
  };

  var playCard4 = function (cardNumber) {
    var callback = function (serverResponse, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        self.opponentPoints4(opponentPoints);
        self.myPoints4(myPoints);
        self.displayLoader(false);
        self.gameNumber(5);
        if ((self.myPoints1() + self.myPoints2() + self.myPoints3() + self.myPoints4()) > (self.opponentPoints1() + self.opponentPoints2() + self.opponentPoints3() + self.opponentPoints4())) {
          self.winner(self.myFullName());
        } else {
          self.winner(self.opponentName());
        }
        localStorage.removeItem("gameId");
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

  $.connection.tasks.client.opponentPlayed4 = function (receiver, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      self.opponentPoints4(opponentPoints);
      self.myPoints4(myPoints);
      self.displayLoader(false);
      self.gameNumber(5);
      if ((self.myPoints1() + self.myPoints2() + self.myPoints3() + self.myPoints4()) > (self.opponentPoints1() + self.opponentPoints2() + self.opponentPoints3() + self.opponentPoints4())) {
        self.winner(self.myFullName());
      } else {
        self.winner(self.opponentName());
      }
      localStorage.removeItem("gameId");
    }
  };

  self.goToFifthGame = function () {
    var callback = function (serverResponse, gameId) {
      if (serverResponse == 'success') {
        self.gameNumber(6);
        self.opponentName('???');
        localStorage.setItem("gameId", gameId);
      } else {
        self.displayLoader(true);
      }
    };
    self.myFullName(localStorage.getItem("fullName"));
    serverProxy.checkForOpponent2(callback);
  };

  $.connection.tasks.client.tellOpponentThatGameIsCreated2 = function (receiver, gameId) {
    if (receiver == localStorage.getItem("username")) {
      localStorage.setItem("gameId", gameId);
      self.displayLoader(false);
      self.gameNumber(6);
    }
  };

  // igra 5, ali se vodi kao 6 zbog toga sto je peta prikaz rezultata
  self.opponentPoints51 = ko.observable(0);
  self.opponentPoints52 = ko.observable(0);
  self.opponentPoints53 = ko.observable(0);
  self.opponentPoints54 = ko.observable(0);
  self.opponentPoints55 = ko.observable(0);

  self.myPoints51 = ko.observable(0);
  self.myPoints52 = ko.observable(0);
  self.myPoints53 = ko.observable(0);
  self.myPoints54 = ko.observable(0);
  self.myPoints55 = ko.observable(0);

  self.game5NumberOfPlay = ko.observable(1);
  self.winnerGame5 = ko.observable('');
  self.isImageDisplayed5 = ko.observable(true);
  self.changeIsImageDisplayed5 = function () {
    self.isImageDisplayed5(false);
  };

  var playCard5 = function (cardNumber) {
    var callback = function (serverResponse, opponentPoints, myPoints) {
      if (serverResponse == 'success') {
        if (self.game5NumberOfPlay() == 1) {
          self.opponentPoints51(opponentPoints);
          self.myPoints51(myPoints);
        }
        if (self.game5NumberOfPlay() == 2) {
          self.opponentPoints52(opponentPoints);
          self.myPoints52(myPoints);
        }
        if (self.game5NumberOfPlay() == 3) {
          self.opponentPoints53(opponentPoints);
          self.myPoints53(myPoints);
        }
        if (self.game5NumberOfPlay() == 4) {
          self.opponentPoints54(opponentPoints);
          self.myPoints54(myPoints);
        }
        if (self.game5NumberOfPlay() == 5) {
          self.opponentPoints55(opponentPoints);
          self.myPoints55(myPoints);
        }

        self.game5NumberOfPlay(self.game5NumberOfPlay() + 1);
        self.displayLoader(false);
        
        if (self.game5NumberOfPlay() > 5) {
          self.gameNumber(7); // strana sa proglasenim pobednikom pete igre
          if ((self.myPoints51() + self.myPoints52() + self.myPoints53() + self.myPoints54() + self.myPoints55()) > (self.opponentPoints51() + self.opponentPoints52() + self.opponentPoints53() + self.opponentPoints54() + self.opponentPoints55())) {
            self.winnerGame5(self.myFullName());
          } else {
            self.winnerGame5(self.opponentName());
          }
          localStorage.removeItem("gameId");
        }
      } else {
        self.displayLoader(true);
      }
    };
    serverProxy.playCard5(localStorage.getItem("gameId"), cardNumber, self.game5NumberOfPlay(), callback);
  };

  self.playCard51 = function () {
    playCard5(1);
  };

  self.playCard52 = function () {
    playCard5(2);
  };

  $.connection.tasks.client.opponentPlayed5 = function (receiver, opponentPoints, myPoints) {
    if (receiver == localStorage.getItem("username")) {
      if (self.game5NumberOfPlay() == 1) {
        self.opponentPoints51(opponentPoints);
        self.myPoints51(myPoints);
      }
      if (self.game5NumberOfPlay() == 2) {
        self.opponentPoints52(opponentPoints);
        self.myPoints52(myPoints);
      }
      if (self.game5NumberOfPlay() == 3) {
        self.opponentPoints53(opponentPoints);
        self.myPoints53(myPoints);
      }
      if (self.game5NumberOfPlay() == 4) {
        self.opponentPoints54(opponentPoints);
        self.myPoints54(myPoints);
      }
      if (self.game5NumberOfPlay() == 5) {
        self.opponentPoints55(opponentPoints);
        self.myPoints55(myPoints);
      }
      self.game5NumberOfPlay(self.game5NumberOfPlay() + 1);
      self.displayLoader(false);
      if (self.game5NumberOfPlay() > 5) {
        self.gameNumber(7); // strana sa proglasenim pobednikom pete igre
        if ((self.myPoints51() + self.myPoints52() + self.myPoints53() + self.myPoints54() + self.myPoints55()) > (self.opponentPoints51() + self.opponentPoints52() + self.opponentPoints53() + self.opponentPoints54() + self.opponentPoints55())) {
          self.winnerGame5(self.myFullName());
        } else {
          self.winnerGame5(self.opponentName());
        }
        localStorage.removeItem("gameId");
      }
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