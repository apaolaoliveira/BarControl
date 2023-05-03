﻿using BarControl.TableModule;
using BarControl.ProductModule;
using BarControl.WaiterModule;
using BarControl.Shared;
using System.Collections;

namespace BarControl.AccountModule
{
    internal class Account : EntityBase
    {
        private AccountRepository repository = null;
        private Order order = null;

        public Table Table { get; set; }
        public Waiter Waiter { get; set; }
        public Order Order { get; set; }
        public string Status { get; set; }
        public string TodayDate { get; set; }

        public decimal totalDayPrice;       

        public Account(AccountRepository accountRepository, Order order)
        {
            repository = accountRepository;
            this.order = order;
        }

        public Account(int accountId, Table table, Waiter waiter, Order order, string status, string date)
        {
            id = accountId;
            Table = table;
            Waiter = waiter;
            Order = order;
            Status = status;
            TodayDate = date;
        }

        public decimal CalculateTotalPrice()
        {
            totalDayPrice = 0;

            ArrayList record = repository.GetRecords();

            foreach (Account account in record)
            {
                totalDayPrice += order.CalculatePrice(account.Order.Quantity, account.Order.Product.Price);
            }

            return totalDayPrice;
        }

        public override void UpdateData(EntityBase record)
        {
            Account account= (Account)record;

            Table.id = account.Table.id;
            Waiter.id = account.Waiter.id;   
            Order = account.Order;
            Status = account.Status;
        }

        public override ArrayList Errors()
        {
            ArrayList ErrorsList = new ArrayList();

            if (Table.id == 0)
                ErrorsList.Add("\nTable is a required field!");

            if (Waiter.id == 0)
                ErrorsList.Add("\nWaiter is a required field!");

            if (Order.Product == null)
                ErrorsList.Add("\nOrder is a required field!");

            return ErrorsList;
        }
    }
}