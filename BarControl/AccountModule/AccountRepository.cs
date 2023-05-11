
namespace BarControl.AccountModule
{
    internal class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(List<Account> accountList)
        {
            records = accountList;
        }

        public decimal CalculateTodayPrice()
        {
            decimal totalDayPrice = 0;

            foreach (Account account in records)
            {
                if(account.TodayDate == DateTime.Now.Date)
                {
                    totalDayPrice += account.SumTotalOrders();
                }
            }

            return totalDayPrice;
        }
    }
}
