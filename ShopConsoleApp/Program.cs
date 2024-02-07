// See https://aka.ms/new-console-template for more information
using Shop.Business.Services;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using System.Transactions;

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
                     " 4 - Quit\n" +
                     "----------------------------------");

    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);

    if (isInt == true)
    {
        if (optionNumber >= 0 && optionNumber <= 4)
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
                                                int userIdd = user.Id;
                                                productServices.ShowAllProducts();
                                                Console.WriteLine("Enter product id:");
                                                int productIdd = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter product count:");
                                                var pro = context.Products.Find(productIdd);
                                                Console.WriteLine($"The product's count in stock ={pro.ProductCount}");
                                                int proCountt = Convert.ToInt32(Console.ReadLine());
                                                productServices.AddProductToBasket(productIdd, userIdd, proCountt);
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
                                                Console.WriteLine("Enter product id:");
                                                basketServices.ShowBasketProducts(userId);
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
                                                Console.WriteLine("Enter card balance:");
                                                string bankName = Console.ReadLine();
                                                Console.WriteLine("Enter card number:");
                                                string cardNumber = Console.ReadLine();
                                                Console.WriteLine("Enter card balance:");
                                                decimal cardBalance = Convert.ToDecimal(Console.ReadLine());
                                                walletServices.AddWallet(userId, bankName, cardNumber, cardBalance);
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
                                                                 " 1 - Update Login information\n" +
                                                                 " 2 - Update your information\n" +
                                                                 " 3 - Delete wallet\n" +
                                                                 " 4 - Delete your account\n" +
                                                                 " 5 - Get back\n" +
                                                                 "----------------------------------");
                                                string? option2 = Console.ReadLine();
                                                int optionNumber2;
                                                bool isInt2 = int.TryParse(option2, out optionNumber2);
                                                if (isInt2 == true)
                                                {
                                                    if (optionNumber2 >= 0 && optionNumber2 <= 5)
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
                                                                    User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                                    int userId = user.Id;
                                                                    Console.WriteLine("Enter wallet Id:");
                                                                    userServices.GetUserWallets(userId);
                                                                    int walletID = Convert.ToInt32(Console.ReadLine());
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
                                                            case (int)Settings.DeleteUser:
                                                                try
                                                                {
                                                                    User? user = context.Users.FirstOrDefault(u => u.URegistr == true);
                                                                    int userId = user.Id;
                                                                    string passwordd = user.Password;
                                                                    userServices.DeleteUser(userId, passwordd);
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    Console.WriteLine("--------------------------------");
                                                                    Console.WriteLine("Account was successfully deleted");
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
                                                                break;
                                                            case (int)Settings.Back:
                                                                isContinue2 = false;
                                                                goto Start;


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

                        Console.WriteLine("Enter password:");
                        string? password = Console.ReadLine();
                        adminServices.AdminUserLogin(username, password);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("             Welcome!           ");
                        Console.WriteLine("--------------------------------");
                        Console.ResetColor();
                        bool isContinueAd = true;
                        while (isContinueAd == true)
                        {

                        AdminMethodss:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Choose the option:");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                             "----------AdminMethods-----------\n" +
                                             " 1 - Show all users\n" +
                                             " 2 - Show all wallets\n" +
                                             " 3 - Create discount\n" +
                                             " 4 - Disable discount\n" +
                                             " 5 - Add discount to product\n" +
                                             " 6 - Show all baskets\n" +
                                             " 7 - Create product\n" +
                                             " 8 - Delete product\n" +
                                             " 9 - Activate product\n" +
                                             "10 - Activate discount\n" +
                                             "11 - Show deactive products\n" +
                                             "12 - Show Deactive discounts\n" +
                                             "13 - Show all products\n" +
                                             "14 - Show all invoices\n" +
                                             "15 - Create Category\n" +
                                             "16 - Create Brand\n" +
                                             "17- LogOut\n" +
                                             "----------------------------------");
                            string? option3 = Console.ReadLine();
                            int optionNumber3;
                            bool isInt3 = int.TryParse(option3, out optionNumber3);
                            if (isInt3 == true)
                            {
                                if (optionNumber3 >= 0 && optionNumber3 <= 17)
                                {
                                    switch (optionNumber3)
                                    {
                                        case (int)AdminMethods.ShowAllUsers:
                                            try
                                            {
                                                adminServices.ShowAllUsers();
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.ShowAllWallets:
                                            try
                                            {
                                                adminServices.ShowAllWallets();
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.CreateDiscount:
                                            try
                                            {
                                                Console.WriteLine("Enter discount name:");
                                                string name = Console.ReadLine();
                                                Console.WriteLine("Enter discount percentage:");
                                                int percentage = Convert.ToInt32(Console.ReadLine());
                                                adminServices.CreateDiscount(name, percentage);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.DisableDiscount:
                                            try
                                            {
                                                Console.WriteLine("Enter discount id:");
                                                adminServices.ShowAllDiscounts();
                                                int discountId = Convert.ToInt32(Console.ReadLine());
                                                adminServices.DisableDiscount(discountId);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.AddDiscountToProduct:
                                            try
                                            {
                                                Console.WriteLine("Enter discount Id:");
                                                adminServices.ShowAllDiscounts();
                                                int discountId = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter product Id:");
                                                adminServices.ShowAllProducts();
                                                int productId = Convert.ToInt32(Console.ReadLine());
                                                adminServices.UpdateProductDiscount(discountId, productId);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.ShowAllBaskets:
                                            try
                                            {
                                                adminServices.ShowAllBaskets();
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.CreateProduct:
                                            try
                                            {
                                                Console.WriteLine("Enter product name:");
                                                string proName = Console.ReadLine();
                                                Console.WriteLine("Enter product price:");
                                                decimal price = Convert.ToDecimal(Console.ReadLine());                                                
                                                Console.WriteLine("Enter product count:");
                                                int proCount = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter brand id:");
                                                adminServices.ShowAllBrands();
                                                int brandId = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Enter category id:");
                                                adminServices.ShowAllCategory();
                                                int categoryId = Convert.ToInt32(Console.ReadLine());
                                                adminServices.CreateProduct(proName, price,  proCount, brandId, categoryId);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.DeleteProduct:
                                            try
                                            {
                                                Console.WriteLine("Enter product Id:");
                                                adminServices.ShowAllProducts();
                                                int productId = Convert.ToInt32(Console.ReadLine());
                                                adminServices.DeleteProduct(productId);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.ActivateProduct:
                                            try
                                            {
                                                Console.WriteLine("Enter product Id:");
                                                adminServices.ShowDeactiveProducts();
                                                int productId = Convert.ToInt32(Console.ReadLine());
                                                adminServices.ActivateProduct(productId);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.ActivateDiscount:
                                            try
                                            {
                                                Console.WriteLine("Enter discount Id:");
                                                adminServices.ShowDeactiveDiscounts();
                                                int discountId = Convert.ToInt32(Console.ReadLine());
                                                adminServices.ActivateDiscount(discountId);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.ShowDeactiveProducts:
                                            adminServices.ShowDeactiveProducts();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("--------------------------------");
                                            Console.WriteLine("      Process is successful     ");
                                            Console.WriteLine("--------------------------------");
                                            Console.ResetColor();
                                            break;
                                        case (int)AdminMethods.ShowDeactiveDiscounts:
                                            adminServices.ShowDeactiveDiscounts();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("--------------------------------");
                                            Console.WriteLine("      Process is successful     ");
                                            Console.WriteLine("--------------------------------");
                                            Console.ResetColor();
                                            break;

                                        case (int)AdminMethods.ShowAllProducts:
                                            adminServices.ShowAllProducts();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("--------------------------------");
                                            Console.WriteLine("      Process is successful     ");
                                            Console.WriteLine("--------------------------------");
                                            Console.ResetColor();
                                            break;
                                        case (int)AdminMethods.ShowAllInvoices:
                                            adminServices.ShowAllInvoices();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("--------------------------------");
                                            Console.WriteLine("      Process is successful     ");
                                            Console.WriteLine("--------------------------------");
                                            Console.ResetColor();
                                            break;
                                        case (int)AdminMethods.CreateCategory:
                                            try
                                            {
                                                Console.WriteLine("Enter Category name:");
                                                string name = Console.ReadLine();
                                                adminServices.CreateCategory(name);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.CreateBrand:

                                            try
                                            {
                                                Console.WriteLine("Enter Brand name:");
                                                string name = Console.ReadLine();
                                                adminServices.CreateBrand(name);
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
                                                goto AdminMethodss;
                                            }
                                            break;
                                        case (int)AdminMethods.AdminUserLogout:

                                            adminServices.AdminUserLogout();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("--------------------------------");
                                            Console.WriteLine("      See you later :)    ");
                                            Console.WriteLine("--------------------------------");
                                            Console.ResetColor();
                                            isContinueAd = false;


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
                        goto Beginning;
                    }
                    break;
                case (int)Register.Quit:
                    isContinue = false;
                    break;
            }
        }
    }











}