// See https://aka.ms/new-console-template for more information
using Shop.Business.Services;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

Console.WriteLine("Shop project");
UserServices userServices = new UserServices();
AdminServices adminServices = new AdminServices();
WalletServices walletServices = new WalletServices();
ProductServices productServices = new ProductServices();
BasketServices basketServices = new BasketServices();
InvoiceServices invoiceServices = new InvoiceServices();
ShopDbContext context = new ShopDbContext();


bool isContinue = true;
while (isContinue)
{
Beginning:
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Choose the option:");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(
                     "-------------REGISTER--------------\n" +
                     " 1 - Login\n" +
                     " 2 - Login as admin\n" +
                     " 3 - If you don't have account, create it:)\n" +
                     "----------------------------------");

    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);

    if (isInt == true)
    {
        if (optionNumber >= 0 && optionNumber <= 3)
        {
            switch (optionNumber)
            {
                case (int)Register.Login:
                    try
                    {
                        Console.WriteLine("Enter your username:");
                        string? username = Console.ReadLine();
                        Console.WriteLine("Enter your email adress:");
                        string? email = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        string? password = Console.ReadLine();
                        userServices.LogIn(username, email, password);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("             Welcome!           ");
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();

                        bool isContinue1 = true;
                        while (isContinue1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Choose the option:");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                             "-------------UserMethods--------------\n" +
                                             " 1 - Show all products\n" +
                                             " 2 - Show products in basket\n" +
                                             " 3 - Add product to basket\n" +
                                             " 4 - Delete product from basket\n" +
                                             " 5 - Show wallets\n" +
                                             " 6 - Add new wallet\n" +
                                             " 7 - Buy product\n" +
                                             " 8 - Show invoices\n" +
                                             " 9 - Go to settings\n" +
                                             " 10 - LogOut\n" +
                                             "----------------------------------");

                        Start:
                            string? option1 = Console.ReadLine();
                            int optionNumber1;
                            bool isInt1 = int.TryParse(option1, out optionNumber1);
                            if (isInt1 == true)
                            {
                                if (optionNumber1 >= 0 && optionNumber1 <= 25)
                                {

                                    switch (optionNumber1)
                                    {

                                        case (int)UserMethods.ShowAllProducts:
                                            productServices.ShowAllProducts();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("--------------------------------");
                                            Console.WriteLine("      Process is successful     ");
                                            Console.WriteLine("--------------------------------");
                                            Console.ResetColor();
                                            break;

                                        case (int)UserMethods.ShowBasketProducts:

                                            try
                                            {
                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                basketServices.ShowBasketProducts(userId);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;

                                            }
                                            break;
                                        case (int)UserMethods.AddProductToBasket:
                                            try
                                            {

                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                productServices.ShowAllProducts();
                                                Console.WriteLine("Enter product id:");
                                                int productId = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter product count:");
                                                var pro = context.Products.Find(productId);
                                                Console.WriteLine($"The product's count in stock ={pro.ProductCount}");
                                                int proCount = Convert.ToInt32(Console.ReadLine());
                                                productServices.AddProductToBasket(productId, userId, proCount);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();

                                            }
                                            catch (Exception ex)
                                            {

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;
                                            }
                                            break;
                                        case (int)UserMethods.DeleteProductFromBasket:
                                            try
                                            {
                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                basketServices.ShowBasketProducts(userId);
                                                Console.WriteLine("Enter product id:");
                                                int productId = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter product count:");
                                                var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
                                                var bp = context.BasketProducts.FirstOrDefault(bp => bp.ProductId == productId && bp.BasketID == bas.Id);
                                                Console.WriteLine($"The product's count in basket ={bp.ProductCount}");
                                                int proCount = Convert.ToInt32(Console.ReadLine());
                                                productServices.DeleteProductFromBasket(productId, userId, proCount);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;

                                            }
                                            break;
                                        case (int)UserMethods.GetUserWallets:
                                            try
                                            {
                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                userServices.GetUserWallets(userId);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();

                                            }
                                            catch (Exception ex)
                                            {

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;
                                            }
                                            break;
                                        case (int)UserMethods.AddWallet:
                                            try
                                            {
                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                Console.WriteLine("Enter card number:");
                                                string cardNumber = Console.ReadLine();
                                                Console.WriteLine("Enter card balance:");
                                                decimal cardBalance = Convert.ToDecimal(Console.ReadLine());
                                                walletServices.AddWallet(userId, cardNumber, cardBalance);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                            }
                                            catch (Exception ex)
                                            {

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;
                                            }
                                            break;
                                        case (int)UserMethods.BuyProduct:
                                            try
                                            {
                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                Console.WriteLine("Enter product id:");
                                                basketServices.ShowBasketProducts(userId);
                                                int productId = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter wallet Id:");
                                                userServices.GetUserWallets(userId);
                                                int walletId = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter product count :");
                                                int proCount = Convert.ToInt32(Console.ReadLine());
                                                basketServices.BuyProduct(userId, productId, walletId, proCount);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                            }
                                            catch (Exception ex)
                                            {

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;
                                            }
                                            break;
                                        case (int)UserMethods.ShowUserInvoices:
                                            try
                                            {
                                                User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                int userId = user.Id;
                                                invoiceServices.ShowUserInvoices(userId);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      Process is successful     ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();
                                                goto Start;

                                            }
                                            break;
                                        case (int)UserMethods.GoToSettings:
                                            bool isContinue2 = true;
                                            while (isContinue2)
                                            {

                                            Settings:
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("Choose the option:");
                                                Console.ResetColor();
                                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                Console.WriteLine(
                                                                 "-------------REGISTER--------------\n" +
                                                                 " 1 - Login\n" +
                                                                 " 2 - Login as admin\n" +
                                                                 " 3 - If you don't have account, create it:)\n" +
                                                                 "----------------------------------");
                                                string? option2 = Console.ReadLine();
                                                int optionNumber2;
                                                bool isInt2 = int.TryParse(option2, out optionNumber2);
                                                if (isInt2 == true)
                                                {
                                                    if (optionNumber2 >= 0 && optionNumber2 <= 25)
                                                    {
                                                        switch (optionNumber2)
                                                        {
                                                            case (int)Settings.UpdateUser:
                                                                try
                                                                {
                                                                    User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                                    int userId = user.Id;
                                                                    string passwordd = user.Password;
                                                                    Console.WriteLine("Enter new username:");
                                                                    string userName = Console.ReadLine();
                                                                    Console.WriteLine("Enter new password:");
                                                                    string newPassword = Console.ReadLine();
                                                                    Console.WriteLine("Enter new email:");
                                                                    string newEmail = Console.ReadLine();
                                                                    userServices.UpdateUser(userId, passwordd, userName, newPassword, newEmail);
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.WriteLine("      Process is successful     ");
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.ResetColor();


                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.WriteLine(ex.Message);
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.ResetColor();
                                                                    goto Settings;
                                                                }
                                                                break;
                                                            case (int)Settings.UpdateUserDetails:
                                                                try
                                                                {
                                                                    User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                                    int userId = user.Id;                                                                   
                                                                    Console.WriteLine("Enter new phone number:");
                                                                    string newPhone = Console.ReadLine();
                                                                    Console.WriteLine("Enter new delivery addres:");
                                                                    string newdeliveryAdd = Console.ReadLine();
                                                                    userServices.UpdateUserDetails(userId, newPhone, newdeliveryAdd);
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.WriteLine("      Process is successful     ");
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.ResetColor();


                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.WriteLine(ex.Message);
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.ResetColor();
                                                                    goto Settings;
                                                                }

                                                                break;
                                                            case (int)Settings.DeleteWallet:
                                                                try
                                                                {

                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    throw;
                                                                }
                                                                break;

                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case (int)UserMethods.LogOut:
                                            try
                                            {
                                                userServices.LogOut();
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine("      See you later :)    ");
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("--------------------------------");
                                                Console.WriteLine(ex.Message);
                                                Console.WriteLine("--------------------------------");
                                                Console.ResetColor();

                                                goto Beginning;
                                            }
                                            isContinue1 = false;
                                            //goto Beginning;

                                            break;

                                    }
                                }
                            }

                        }


                    }
                    catch (Exception ex)
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();
                        goto case (int)Register.Login;
                    }
                    break;
                case (int)Register.LoginAsAdmin:
                    try
                    {
                        Console.WriteLine("Enter your username:");
                        string? username = Console.ReadLine();
                        Console.WriteLine("Enter your email adress:");
                        string? email = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        string? password = Console.ReadLine();
                        adminServices.AdminUserLogin(username, email, password);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("             Welcome!           ");
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();
                        switch (optionNumber)
                        {
                            case (int)AdminMethods.ShowAllUsers:
                                try
                                {

                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();
                        goto case (int)Register.LoginAsAdmin;
                    }
                    break;
                case (int)Register.CreateAccount:
                    try
                    {
                        Console.WriteLine("Etner First name:");
                        string? firstName = Console.ReadLine();
                        Console.WriteLine("Etner Last name:");
                        string? lastName = Console.ReadLine();
                        Console.WriteLine("Etner Phone number :");
                        string? phone = Console.ReadLine();
                        Console.WriteLine("Etner Delivery address:");
                        string? deliveryAdd = Console.ReadLine();
                        Console.WriteLine("Etner username :");
                        string? userName = Console.ReadLine();
                        Console.WriteLine("Etner email :");
                        string? email = Console.ReadLine();
                        Console.WriteLine("Etner password :");
                        string? password = Console.ReadLine();
                        userServices.CreateUser(firstName, lastName, phone, deliveryAdd, userName, email, password);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("  Successfully created.Welcome! ");
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();
                        goto case (int)Register.CreateAccount;
                    }
                    break;
            }
        }
    }











}