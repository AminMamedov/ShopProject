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


bool isContinue = true;
while (isContinue)
{
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

    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 25)
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
                        switch (optionNumber)
                        {

                            case (int)UserMethods.DeleteUser:
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
                        string? password =Console.ReadLine();
                        userServices.CreateUser(firstName, lastName, phone, deliveryAdd, userName,email, password);
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