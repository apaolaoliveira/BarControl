using BarControl.ProductModule;
using BarControl.WaiterModule;
using BarControl.AccountModule;
using BarControl.TableModule;
using BarControl.Shared;
using System.Collections;

namespace BarControl.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product productEntity = new Product(1, "Beer", "Drink", 20);
            ProductRepository productRepository     = new ProductRepository(new ArrayList());
            productRepository.Add(productEntity);
            ProductPresentation productPresentation = new ProductPresentation(productRepository);
            ProductPresentation product = productPresentation;

            Waiter waiterEntity = new Waiter(1, "Gary", "12236548955", 49999885665, "Street: 123 Main Street; Neighborhood: Downtown");
            WaiterRepository waiterRepository     = new WaiterRepository(new ArrayList());
            waiterRepository.Add(waiterEntity);
            WaiterPresentation waiterPresentation = new WaiterPresentation(waiterRepository);
            WaiterPresentation waiter = waiterPresentation;

            Table tableEntity = new Table(1, "Next to the window of front door");
            TableRepository   tableRepository   = new TableRepository(new ArrayList());
            tableRepository.Add(tableEntity);
            TablePresentation tablePresentation = new TablePresentation(tableRepository);
            TablePresentation table = tablePresentation;

            Order orderEntity = new Order();
            AccountRepository accountRepository = new AccountRepository(new ArrayList());
            Account accountEntity = new Account(accountRepository, orderEntity);
            AccountPresentation accountPresentation = new AccountPresentation(accountRepository,
                                                      tablePresentation, productPresentation, waiterPresentation,
                                                      tableRepository, productRepository, waiterRepository, accountEntity, orderEntity);
            AccountPresentation account = accountPresentation;


            bool proceedMain = true;

            while (proceedMain)
            {
                Console.Clear();

                PresentationBase.ColorfulMessage(
                  $"\n\nGALERA'S BAR"
                + $"\n-------------------"
                + $"\n[1] Product. "
                + $"\n[2] Table."
                + $"\n[3] Waiter."
                + $"\n[4] Account."
                + $"\n[5] Exit."
                + "\n\n→ "
                , ConsoleColor.DarkYellow);

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        bool proceedProduct = true;

                        while (proceedProduct)
                        {
                            int productOption = ProductPresentation.SetMenu("product");

                            switch(productOption)
                            {
                                case 1: product.Create();       break;
                                case 2: product.Read();         break;
                                case 3: product.Update();       break;
                                case 4: product.Delete();       break;
                                case 5: proceedProduct = false; break;
                            }
                        }

                        break;

                    case 2:
                        bool proceedTable = true;

                        while (proceedTable)
                        {
                            int tableOption = TablePresentation.SetMenu("table");

                            switch (tableOption)
                            {
                                case 1: table.Create();       break;
                                case 2: table.Read();         break;
                                case 3: table.Update();       break;
                                case 4: table.Delete();       break;
                                case 5: proceedTable = false; break;
                            }
                        }

                        break;

                    case 3:
                        bool proceedWaiter = true;

                        while (proceedWaiter)
                        {
                            int waiterOption = WaiterPresentation.SetMenu("waiter");

                            switch (waiterOption)
                            {
                                case 1: waiter.Create();       break;
                                case 2: waiter.Read();         break;
                                case 3: waiter.Update();       break;
                                case 4: waiter.Delete();       break;
                                case 5: proceedWaiter = false; break;
                            }
                        }

                        break;

                    case 4:
                        bool proceedAccount = true;

                        while (proceedAccount)
                        {
                            Console.Clear();
                            
                            PresentationBase.ColorfulMessage(
                              $"\n\nACCOUNT"
                            + $"\n-------------------"
                            + $"\n[1] Create account. "
                            + $"\n[2] View account's table."
                            + $"\n[3] View just 'OPEN' accounts."
                            + $"\n[4] View day history and revenue."
                            + $"\n[5] Edit a account."
                            + $"\n[6] Delete a account."
                            + $"\n[7] Go back."
                            + "\n\n→ "
                            , ConsoleColor.DarkYellow);

                            int accountOption = Convert.ToInt32(Console.ReadLine());

                            switch (accountOption)
                            {
                                case 1: account.Create();               break;
                                case 2: account.Read();                 break;
                                case 3: accountPresentation.OpenView(); break;
                                case 4: accountPresentation.Revenue();  break;
                                case 5: account.Update();               break;
                                case 6: account.Delete();               break;
                                case 7: proceedAccount = false;         break;
                            }
                        }

                        break;

                    case 5: 
                        proceedMain = false;

                        break;
                }
            }
            PresentationBase.ColorfulMessage("\n\nHave a great day!", ConsoleColor.DarkCyan);
            PresentationBase.ColorfulMessage("\n\n<-'", ConsoleColor.DarkCyan);

            Console.ReadKey();
        }
    }
}