using BarControl.ProductModule;
using BarControl.Shared;
using BarControl.TableModule;
using System.Collections;

namespace BarControl.WaiterModule
{
    internal class WaiterPresentation : PresentationBase<WaiterRepository, Waiter>
    {
        public WaiterPresentation(WaiterRepository waiterRepository)
        {
            repository = waiterRepository;
            entityName = "Waiter";
        }

        protected override Waiter GetRecordProperties()
        {
            string name = SetField<string>("Name:", ConsoleColor.Cyan);

            string cpf = SetField<string>("CPF:", ConsoleColor.Cyan);

            long phone = SetField<long>("Phone:", ConsoleColor.Cyan);

            string address = SetField<string>("Address:", ConsoleColor.Cyan);

            Waiter newWaiter = new Waiter(repository.idCounter, name, cpf, phone, address);

            return newWaiter;
        }

        protected override void DisplayTable()
        {
            string[] columnNames = { "id", "name", "cpf", "phone" , "address" };
            int[] columnWidths = { 4, 15, 15, 15, 50 };

            List<object> data = new List<object>();

            List<Waiter> records = repository.GetRecords();

            foreach (Waiter waiter in records)
            {
                data.Add(new object[] { waiter.id, waiter.Name, waiter.Cpf, waiter.Phone, waiter.Address });
            }

            SetTable(columnNames, columnWidths, data);

            SetFooter();
        }
    }
}
