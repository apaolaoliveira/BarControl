using BarControl.ProductModule;
using BarControl.WaiterModule;
using BarControl.AccountModule;
using BarControl.TableModule;

namespace BarControl.Application
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            // Object initialization --------------------------------------------------------------------------------
            ProductRepository   productRepository   = new ProductRepository(new List<Product>());
            ProductPresentation productPresentation = new ProductPresentation(productRepository);

            WaiterRepository   waiterRepository   = new WaiterRepository(new List<Waiter>());
            WaiterPresentation waiterPresentation = new WaiterPresentation(waiterRepository);

            TableRepository   tableRepository   = new TableRepository(new List<Table>());
            TablePresentation tablePresentation = new TablePresentation(tableRepository);

            Order orderEntity = new Order();

            AccountRepository accountRepository = new AccountRepository(new List<Account>());
            Account accountEntity = new Account();
            AccountPresentation accountPresentation = new AccountPresentation
            (accountRepository, tablePresentation, productPresentation, waiterPresentation,
             tableRepository, productRepository, waiterRepository, accountEntity, orderEntity);

            Notifier notifier = new Notifier();

            // SetMenu ----------------------------------------------------------------------------------------------
            ProductPresentation product = productPresentation;
            WaiterPresentation waiter = waiterPresentation;
            TablePresentation table = tablePresentation;
            AccountPresentation account = accountPresentation;

            // Add information --------------------------------------------------------------------------------------
            Product product1 = new Product
                (1, "Beer", "Drink", 20);
            productRepository.Add(product1);

            Waiter waiter1 = new Waiter
                (1, "Gary", "12236548955", 49999885665, "Street: 123 Main Street; Neighborhood: Downtown");
            waiterRepository.Add(waiter1);

            Table table1 = new Table
                (1, "Next to the window of front door");
            tableRepository.Add(table1);            

            Account account1 = new Account
                (1, table1, waiter1);
            account1.Status = "CLOSED";
            accountRepository.Add(account1);

            Order order1 = new Order(product1, 5);
            account1.ordersList.Add(order1);

            // Menus ------------------------------------------------------------------------------------------------                        
            bool proceedMain = true;

            while (proceedMain)
            {
                Console.Clear();

                notifier.Menu(
                  $"\n\nGALERA'S BAR"
                + $"\n-------------------"
                + $"\n[1] Product. "
                + $"\n[2] Table."
                + $"\n[3] Waiter."
                + $"\n[4] Account."
                + $"\n[5] Exit."
                + "\n\n→ ");

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        bool proceedProduct = true;

                        while (proceedProduct)
                        {
                            int productOption = product.SetMenu("product");

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
                            int tableOption = table.SetMenu("table");

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
                            int waiterOption = waiter.SetMenu("waiter");

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

                            notifier.Menu(
                              $"\n\nACCOUNT"
                            + $"\n-------------------"
                            + $"\n[1] Create account. "
                            + $"\n[2] Close account. "
                            + $"\n[3] Add an order to account. "
                            + $"\n[4] View account's table."
                            + $"\n[5] View order's table."
                            + $"\n[6] View just \"OPEN\" accounts."
                            + $"\n[7] View today's revenue."
                            + $"\n[8] Edit a account."
                            + $"\n[9] Delete a account."
                            + $"\n[10] Go back."
                            + "\n\n→ ");

                            int accountOption = Convert.ToInt32(Console.ReadLine());

                            switch (accountOption)
                            {
                                case 1:  account.Create();                   break;
                                case 2:  accountPresentation.CloseAccount(); break;
                                case 3:  accountPresentation.AddOrder();     break;
                                case 4:  account.Read();                     break;
                                case 5:  accountPresentation.OrderView();    break;
                                case 6:  accountPresentation.OpenView();     break;
                                case 7:  accountPresentation.Revenue();      break;
                                case 8:  account.Update();                   break;
                                case 9:  account.Delete();                   break;
                                case 10: proceedAccount = false;             break;
                            }
                        }

                        break;

                    case 5: proceedMain = false; break;
                }
            }
            Console.Clear();
            notifier.Text("\n\nHave a great day!");
            notifier.Text("\n\n<-'");

            Console.ReadKey();
        }
    }
}