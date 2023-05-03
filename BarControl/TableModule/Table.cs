using BarControl.Shared;
using System.Collections;

namespace BarControl.TableModule
{
    internal class Table : EntityBase
    {
        public string Description { get; set; }

        public Table(int tableId, string description)
        {
            id = tableId;
            Description = description;
        }

        public override void UpdateData(EntityBase record)
        {
            Table table = (Table)record;

            Description = table.Description;
        }

        public override ArrayList Errors()
        {
            ArrayList ErrorsList = new ArrayList();

            if (string.IsNullOrEmpty(Description.Trim()))
                ErrorsList.Add("\nDescription is a required field!");

            return ErrorsList;
        }

    }
}
