using BarControl.Shared;
using System.Collections;

namespace BarControl.AccountModule
{
    internal class AccountRepository : RepositoryBase
    {
        public AccountRepository(ArrayList arrayList)
        {
            records = arrayList;
        }

        public static string GetStatus(int statusChoice)
        {
            switch (statusChoice)
            {
                case 1: return "OPEN";
                case 2: return "CLOSED";
                default: throw new ArgumentException("Invalid status choice.");
            }
        }
    }
}
