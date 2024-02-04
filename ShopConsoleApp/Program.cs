// See https://aka.ms/new-console-template for more information
using Shop.Business.Services;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

Console.WriteLine("Shop project");
UserServices userServices = new UserServices();
ProductServices productServices = new ProductServices();
BasketServices basketServices = new BasketServices();

//productServices.ShowAllProducts();
//basketServices.ShowBasketProducts(2);
//productServices.AddProductToBasket(2, 10, 10);
productServices.DeleteProductFromBasket(2, 10, 10);
//productServices.DeleteProductFromBasket(2, 5);
//productServices.AddProductToBasket(2, 5);
//userServices.CreateUser("oktay", "oktay1", "12123");
//userServices.LogIn("oktay", "oktay1", "12123");