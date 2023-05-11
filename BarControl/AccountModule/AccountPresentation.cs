using BarControl.ProductModule;
using BarControl.TableModule;
using BarControl.WaiterModule;
using System.Security.Principal;

namespace BarControl.AccountModule
{
    internal class AccountPresentation : PresentationBase<AccountRepository, Account>
    {
        private TablePresentation tablePresentation = null;
        private ProductPresentation productPresentation = null;
        private WaiterPresentation waiterPresentation = null;
        private TableRepository tableRepository = null;
        private ProductRepository productRepository = null;
        private WaiterRepository waiterRepository = null;

        private Account entity = null;
        private Order orderEntity = null;

        public AccountPresentation
          (AccountRepository accountRepository, TablePresentation tablePresentation,
           ProductPresentation productPresentation, WaiterPresentation waiterPresentation,
           TableRepository tableRepository, ProductRepository productRepository,
           WaiterRepository waiterRepository, Account entity, Order order)
        {
            repository = accountRepository;
            entityName = "Account";
            this.entity = entity;
            orderEntity = order;

            // Presentation ---------------------------------
            this.tablePresentation = tablePresentation;
            this.productPresentation = productPresentation;
            this.waiterPresentation = waiterPresentation;

            // Repository ----------------------------------- 
            this.tableRepository = tableRepository;
            this.productRepository = productRepository;
            this.waiterRepository = waiterRepository;
        }

        public void CloseAccount()
        {
            SetHeader("Close account");

            int accountId = SetField<int>("Account ID:", ConsoleColor.Cyan);

            Account account = repository.GetSelectedId(accountId);

            account.Status = "CLOSED";

            notifier.Success("\nAccount's status suscessfuly changed!");
            SetFooter();
        }

        protected override Account GetRecordProperties()
        {
            // Table -------------------------------------------------------------

            tablePresentation.Read();

            int tableInput = SetField<int>("\nTable ID:", ConsoleColor.Cyan);
            int validTable = tableRepository.isValidId(tableInput);

            Table table = tableRepository.GetSelectedId(validTable);

            // Waiter ------------------------------------------------------------

            waiterPresentation.Read();

            int waiterInput = SetField<int>("\nWaiter ID:", ConsoleColor.Cyan);
            int validWaiter = waiterRepository.isValidId(waiterInput);

            Waiter waiter = waiterRepository.GetSelectedId(validWaiter);

            Account newAccount = new Account(repository.idCounter, table, waiter);
            return newAccount;
        }

        public void AddOrder()
        {
            SetHeader("add order");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            int accountId = SetField<int>("Account ID:", ConsoleColor.Cyan);
            int validAccount = repository.isValidId(accountId);

            Account account = repository.GetSelectedId(validAccount);
            productPresentation.Read();

            int productInput = SetField<int>("\nProduct ID:", ConsoleColor.Cyan);
            int validProduct = productRepository.isValidId(productInput);

            Product product = productRepository.GetSelectedId(validProduct);

            int quantityInput = SetField<int>("Quantity:", ConsoleColor.Cyan);

            Order newOrder = new Order(product, quantityInput);
            newOrder.CalculatePrice(quantityInput, product.Price);
            account.ordersList.Add(newOrder);
        }

        protected override void DisplayTable()
        {
            SetHeader("accounts' view");

            string[] columnNamesAccount = { "id", "Table", "waiter", "status", "date" };
            int[] columnWidthsAccount = { 4, 8, 15, 10, 12 };

            List<object> dataAccount = new List<object>();

            List<Account> records = repository.GetRecords();

            foreach (Account account in records)
            {
                dataAccount.Add(new object[] { account.id, account.Table.id, account.Waiter.Name, account.Status, account.TodayDate.ToString("dd/MM/yyyy") });
            }

            SetTable(columnNamesAccount, columnWidthsAccount, dataAccount);
            SetFooter();
        }

        public void OrderView()
        {
            SetHeader("orders' view");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            int accountId = SetField<int>("Account ID:", ConsoleColor.Cyan);
            int validAccount = repository.isValidId(accountId);

            Account account = repository.GetSelectedId(validAccount);

            string[] columnNamesOrder = { "id", "product", "quantity", "price" };
            int[] columnWidthsOrder = { 4, 15, 9, 11 };

            List<object> dataOrder = new List<object>();

            foreach (Order order in account.ordersList)
            {
                dataOrder.Add(new object[] { account.id, order.Product.Name, order.Quantity,
                     orderEntity.CalculatePrice(order.Quantity, order.Product.Price).ToString("C2") });
            }

            SetTable(columnNamesOrder, columnWidthsOrder, dataOrder);
            SetFooter();
        }

        public void OpenView()
        {
            SetHeader("\"Open\" accounts' view");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            string[] columnNames = { "id", "Table", "waiter", "status", "date" };
            int[] columnWidths = { 4, 8, 15, 10, 12 };

            List<object> data = new List<object>();

            List<Account> records = repository.GetRecords();

            bool noRecords = repository.NoRecords();

            if (noRecords == false)
            {
                foreach (Account account in records)
                {
                    if (account != null && account.Status == "OPEN")
                    {
                        data.Add(new object[] { account.id, account.Table.id, account.Waiter.Name, account.Status, account.TodayDate.ToString("dd/MM/yyyy") });
                    }
                }
            }
            else
            {
                notifier.Error("\nYou don't have \"OPEN\" accounts!\n");
                SetFooter();
                return;
            }

            SetTable(columnNames, columnWidths, data);

            SetFooter();
        }

        public void Revenue()
        {
            SetHeader("revenue");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            decimal totalDayPrice = repository.CalculateTodayPrice();

            notifier.Success($"\nTotal Amount: +{totalDayPrice.ToString("C2")}. "
                            + $"\nDate: {DateTime.Today.Date.ToString("dd/MM/yyyy")}.");

            SetFooter();
        }
    }
}
