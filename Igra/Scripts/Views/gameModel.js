function GameModel() {

  //Make the self as 'this' reference
  var self = this;
  var serverProxy = new GameServerProxy();

  // login form
  self.login = function () {
    var callback = function (serverResponse, message) {
      if (serverResponse == 'success') {
        toastr.success("Uspešno ste se ulogovali.");
        window.location.href = "/tutorial";
      } else {
        toastr.error(message);
      }
    };
    serverProxy.callLogin(self.username(), self.password(), callback);
  };

  self.username = ko.observable("");
  self.password = ko.observable("");

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

  self.usernameRegister = ko.observable("");
  self.passwordRegister = ko.observable("");
  self.nameRegister = ko.observable("");
  self.lastnameRegister = ko.observable("");
  self.gender = ko.observable("male");


  // tutorial
  self.gotoGame = function () {
    window.location.href = "/game";
  };
  


    //Declare observable which will be bind with UI
    self.Id = ko.observable("cao");
    self.Name = ko.observable("");
    self.Price = ko.observable("");
    self.Category = ko.observable("");

    var Product = {
        Id: self.Id,
        Name: self.Name,
        Price: self.Price,
        Category: self.Category
    };

    self.Product = ko.observable();
    self.Products = ko.observableArray(); // Contains the list of products
  

    // Calculate Total of Price After Initialization
    self.Total = ko.computed(function () {
        var sum = 0;
        var arr = self.Products();
        for (var i = 0; i < arr.length; i++) {
            sum += arr[i].Price;
        }
        return sum;
    });

    //Add New Item
    self.create = function () {
        if (Product.Name() != "" && 
        Product.Price() != "" && Product.Category() != "") {
            $.ajax({
                url: 'Product/AddProduct',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(Product),
                success: function (data) {
                    self.Products.push(data);
                    self.Name("");
                    self.Price("");
                    self.Category("");
                }
            }).fail(
            function (xhr, textStatus, err) {
                alert(err);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    }
    // Delete product details
    self.delete = function (Product) {
        if (confirm('Are you sure to Delete "' + Product.Name + '" product ??')) {
            var id = Product.Id;

            $.ajax({
                url: 'Product/DeleteProduct/' + id,
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: id,
                success: function (data) {
                    self.Products.remove(Product);
                }
            }).fail(
            function (xhr, textStatus, err) {
                self.status(err);
            });
        }
    }

    // Edit product details
    self.edit = function (Product) {
        self.Product(Product);
    }

    // Update product details
    self.update = function () {
        var Product = self.Product();

        $.ajax({
            url: 'Product/EditProduct',
            cache: false,
            type: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(Product),
            success: function (data) {
                self.Products.removeAll();
                self.Products(data); //Put the response in ObservableArray
                self.Product(null);
                alert("Record Updated Successfully");
            }
        })
        .fail(
        function (xhr, textStatus, err) {
            alert(err);
        });
    }

    // Reset product details
    self.reset = function () {
        self.Name("");
        self.Price("");
        self.Category("");
    }

    // Cancel product details
    self.cancel = function () {
        self.Product(null);
    }

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