using BarControl.Shared;
using System.Collections;

namespace BarControl.WaiterModule
{
    internal class Waiter : EntityBase<Waiter>
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }

        public Waiter(int waiterId, string name, string cpf, long phone, string address)
        {
            id = waiterId;
            Name = name;
            Cpf = cpf;
            Phone = phone;
            Address = address;
        }

        public override void UpdateData(Waiter updateWaiter)
        {
            Name = updateWaiter.Name;
            Cpf = updateWaiter.Cpf;
            Phone = updateWaiter.Phone;
            Address = updateWaiter.Address;
        }

        public override ArrayList Errors()
        {
            ArrayList ErrorsList = new ArrayList();

            if (string.IsNullOrEmpty(Name.Trim()))
                ErrorsList.Add("\nName is a required field!");

            if (string.IsNullOrEmpty(Cpf.Trim()))
                ErrorsList.Add("\nCpf is a required field!");

            if(Phone == 0)
                ErrorsList.Add("\nPhone is a required field!");

            return ErrorsList;
        }
    }
}
