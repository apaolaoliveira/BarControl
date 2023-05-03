using BarControl.ProductModule;
using BarControl.Shared;
using BarControl.TableModule;
using BarControl.WaiterModule;
using System.Collections;

namespace BarControl.AccountModule
{
    internal class AccountPresentation : PresentationBase
    {
        private TablePresentation tablePresentation = null;
        private ProductPresentation productPresentation = null;
        private WaiterPresentation waiterPresentation = null;
        private TableRepository tableRepository = null;
        private ProductRepository productRepository = null;
        private WaiterRepository waiterRepository = null;

        private Account entity = null;
        private Order orderEntity = null;

        public AccountPresentation(AccountRepository accountRepository, TablePresentation tablePresentation,
                                   ProductPresentation productPresentation, WaiterPresentation waiterPresentation,
                                   TableRepository tableRepository, ProductRepository productRepository,
                                   WaiterRepository waiterRepository, Account entity, Order order)
        {
            repository = accountRepository;
            entityName = "Account";
            this.entity = entity;
            this.orderEntity = order;

            // Presentation ---------------------------------
            this.tablePresentation = tablePresentation;
            this.productPresentation = productPresentation;
            this.waiterPresentation = waiterPresentation;

            // Repository ----------------------------------- 
            this.tableRepository = tableRepository;
            this.productRepository = productRepository;
            this.waiterRepository = waiterRepository;
        }

        protected override EntityBase GetRecordProperties()
        {
            // Status and Date ---------------------------------------------------

            int statusInput = SetField<int>(
                  "Status:"
                + "\n[1] OPEN"
                + "\n[2] CLOSED\n"
                , ConsoleColor.Cyan);

            string status = AccountRepository.GetStatus(statusInput);

            string dateTime = DateTime.Today.Date.ToString("dd/MM/yyyy");

            // Table -------------------------------------------------------------

            tablePresentation.Read();

            int tableInput = SetField<int>("\nTable ID:", ConsoleColor.Cyan);
            int validTable = tableRepository.isValidId(tableInput);

            Table table = (Table)tableRepository.GetSelectedId(validTable);

            // Waiter ------------------------------------------------------------

            waiterPresentation.Read();

            int waiterInput = SetField<int>("\nWaiter ID:", ConsoleColor.Cyan);
            int validWaiter = waiterRepository.isValidId(waiterInput);

            Waiter waiter = (Waiter)waiterRepository.GetSelectedId(validWaiter);

            // Order --------------------------------------------------------------

            productPresentation.Read();

            int productInput = SetField<int>("\nProduct ID:", ConsoleColor.Cyan);
            int validProduct = productRepository.isValidId(productInput);

            Product product = (Product)productRepository.GetSelectedId(validProduct);

            int quantityInput = SetField<int>("Quantity:", ConsoleColor.Cyan);

            Order newOrder = new Order(product, quantityInput);
            newOrder.CalculatePrice(quantityInput, product.Price);

            Account newAccount = new Account(repository.idCounter, table, waiter, newOrder, status, dateTime);

            return newAccount;
        }

        protected override void DisplayTable()
        {
            SetHeader("accounts' view");

            string[] columnNames = { "id", "Table", "waiter", "product", "quantity", "price", "status", "date" };
            int[] columnWidths = { 4, 8, 15, 15, 9, 9, 10, 12 };

            List<object> data = new List<object>();

            ArrayList records = repository.GetRecords();

            foreach (Account account in records)
            {
                data.Add(new object[] { account.id, account.Table.id, account.Waiter.Name, account.Order.Product.Name,account.Order.Quantity,
                orderEntity.CalculatePrice(account.Order.Quantity, account.Order.Product.Price).ToString("C2"), account.Status, account.TodayDate  });
            }

            SetTable(columnNames, columnWidths, data);

            SetFooter();
        }

        public void OpenView()
        {
            SetHeader("'Open' accounts' view");

            string[] columnNames = { "id", "Table", "waiter", "product", "quantity", "price", "status", "date" };
            int[] columnWidths = { 4, 8, 15, 15, 9, 9, 10, 12 };

            List<object> data = new List<object>();

            ArrayList records = repository.GetRecords();

            bool noRecords = repository.NoRecords();

            if (noRecords == false)
            {
                foreach (Account account in records)
                {
                    if (account != null &&
                        account.Status == "OPEN")
                    {
                        data.Add(new object[] { account.id, account.Table.id, account.Waiter.Name, account.Order.Product.Name,account.Order.Quantity,
                        orderEntity.CalculatePrice(account.Order.Quantity, account.Order.Product.Price).ToString("C2"), account.Status, account.TodayDate  });
                    }
                }

            }
            else
            {
                ColorfulMessage("You don't have open accounts!", ConsoleColor.Red);
            }

            SetTable(columnNames, columnWidths, data);

            SetFooter();
        }

        public void Revenue()
        {
            SetHeader("history and revenue");

            ArrayList SetHistory = repository.GetRecords();

            string[] columnNames = { "id", "product", "quantity", "final price" };
            int[] columnWidths = { 4, 15, 9, 12 };

            List<object> data = new List<object>();

            foreach (Account account in SetHistory)
            {
                data.Add(new object[] { account.id, account.Order.Product.Name, account.Order.Quantity,
                    orderEntity.CalculatePrice(account.Order.Quantity, account.Order.Product.Price).ToString("C2") });
            }

            decimal totalDayPrice = entity.CalculateTotalPrice();

            SetTable(columnNames, columnWidths, data);

            ColorfulMessage($"\nTotal Amount: +{totalDayPrice.ToString("C2")} "
                            + $"\nDate:{DateTime.Today.Date.ToString("dd/MM/yyyy")}"
                            , ConsoleColor.Green);

            SetFooter();
        }
    }
}
