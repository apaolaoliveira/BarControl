using BarControl.TableModule;
using BarControl.WaiterModule;

namespace BarControl.AccountModule
{
    internal class Account : EntityBase<Account>
    {
        public Table Table { get; set; }
        public Waiter Waiter { get; set; }
        public string Status { get; set; }
        
        public DateTime TodayDate;

        public List<Order> ordersList = new List<Order>();

        public Account () { }

        public Account(int accountId, Table table, Waiter waiter)
        {
            id = accountId;
            Table = table;
            Waiter = waiter;
            Status = "OPEN";
            TodayDate = DateTime.Today.Date;
        }        

        public override void UpdateData(Account updateAccount)
        {
            Table.id = updateAccount.Table.id;
            Waiter.id = updateAccount.Waiter.id;
        }

        public decimal SumTotalOrders()
        {
            decimal total = 0;

            foreach (Order order in ordersList)
            {
                total += order.CalculatePrice(order.Quantity, order.Product.Price);
            }

            return total;
        }

        public override ArrayList Errors()
        {
            ArrayList ErrorsList = new ArrayList();

            if (Table.id == 0)
                ErrorsList.Add("\nTable is a required field!");

            if (Waiter.id == 0)
                ErrorsList.Add("\nWaiter is a required field!");

            return ErrorsList;
        }
    }
}
