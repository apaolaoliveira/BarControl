using BarControl.Shared;
using System.Collections;

namespace BarControl.WaiterModule
{
    internal class WaiterRepository : RepositoryBase 
    {
        public WaiterRepository(ArrayList arrayList)
        {
            records = arrayList;
        }
    }
}
