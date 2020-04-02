using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WheyMen.Domain.Model;
using WheyMen.Infrastructure;

namespace WheyMenII.Test
{
    [TestClass]
    public class OrderRepoTest
    {
        OrderDAL Repo;
        LocationDAL LocDal;
        [TestInitialize]
        public void TestSetup()
        {
            Repo = new OrderDAL();
            LocDal = new LocationDAL();
        }
        [TestMethod]
        public async Task TestOrdtAdd()
        {
            var custs = await Repo.GetOrds();
            int initial_count = custs.ToList().Count;
            Order cust1 = new Order
            {
                CustId = 1,
                LocId = 1,
                Total = 0,
                Timestamp = DateTime.Now
            };
 
            int x = Repo.Add(cust1);
            custs = await Repo.GetOrds();
            int final_count = custs.ToList().Count;
            Assert.AreEqual(final_count,initial_count + 1);
            Repo.Remove(x);
        }
        [TestMethod]
        public async Task TestOrdEdit()
        {
            var ord = new Order
            {
                CustId = 1,
                Total = 0,
                Timestamp = DateTime.Now,
                LocId = 1
            };
            int target = Repo.Add(ord);
            var toEdit = Repo.FindByID(target);
            toEdit.Total = 0;
            Repo.Edit(toEdit);
            var editedCust = Repo.FindByID(target);
            Assert.AreEqual(0, editedCust.Total);
            Repo.Remove(target);
        }
        [TestMethod]
        public async Task TestOrdDelete()
        {

            var custs = await Repo.GetOrds();
            int initialCount = custs.ToList().Count;
            var newCust = new Order
            {
                CustId = 3,
                Total = 0,
                LocId = 1,
                Timestamp = DateTime.Now
            };
            int addedID = Repo.Add(newCust);

            Repo.Remove(addedID);
            custs = await Repo.GetOrds();
            int finalCount = custs.ToList().Count;
            Assert.AreEqual(initialCount, finalCount);
        }
        [TestMethod]
        public async Task TestOrdDeleteEmpty()
        {
     
            var newCust = new Order
            {
                CustId = 2,
                Total = 0,
                LocId = 1,
                Timestamp = DateTime.Now
            };
            Repo.Add(newCust);
            var custs = await Repo.GetOrds();
            int initialCount = custs.ToList().Count;
            var newCust1 = new Order
            {
                CustId = 2,
                Total = 0,
                LocId = 1,
                Timestamp = DateTime.Now
            };
            int rem = Repo.Add(newCust1);
            
            custs = await Repo.GetOrds();
            int current = custs.ToList().Count;
            Assert.AreEqual(initialCount, current);
            Repo.Remove(rem);
        }
        [TestMethod]
        public async Task TestOrdTotalUpdate()
        {
            var newCust = new Order
            {
                CustId = 3,
                Total = 0,
                LocId = 1,
                Timestamp = DateTime.Now
            };
            int addedID = Repo.Add(newCust);
            var orderItem = new OrderItem
            {
                Oid = addedID,
                Qty = 2,
                Pid = 5,
            };
            Repo.AddOrderItem(orderItem);
            var order = Repo.FindByID(addedID);
            Assert.AreEqual(24,order.Total);
            Repo.RemoveOrderItem(orderItem);
            Repo.Remove(addedID);

        }
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(100000000)]
        public async Task TestInvalidQty(int qty)
        {
            var orderItem = new OrderItem
            {
                Oid = 1,
                Qty = qty,
                Pid = 5,
            };
            Assert.IsFalse(orderItem.ValidateQuantity(LocDal.GetQty(5)));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public async Task TestValidQty(int qty)
        {
            var orderItem = new OrderItem
            {
                Oid = 1,
                Qty = qty,
                Pid = 5,
            };
            Assert.IsTrue(orderItem.ValidateQuantity(LocDal.GetQty(5)));
        }
    }
}
