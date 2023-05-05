using BarControl.Shared;
using System.Collections;

namespace BarControl.TableModule
{
    internal class TableRepository : RepositoryBase<Table>
    {
        public TableRepository(List<Table> tableList)
        {
            records = tableList;
        }
    }
}
