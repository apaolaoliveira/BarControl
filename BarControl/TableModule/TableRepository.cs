using BarControl.Shared;
using System.Collections;

namespace BarControl.TableModule
{
    internal class TableRepository : RepositoryBase
    {
        public TableRepository(ArrayList arrayList)
        {
            records = arrayList;
        }
    }
}
