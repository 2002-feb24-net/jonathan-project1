using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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

        public void TestSetup(WheyMenContext context)
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            CustomerDAL CustDal;
      
            Repo = new OrderDAL(context);
            LocDal = new LocationDAL(context);
            CustDal = new CustomerDAL(context);
            var cust = new Customer
            {
                Id = 1,
                Name = "jon",
                LastName = "asd",
                Email = "asd@sasd.co",
                Username="abc",
                Pwd = "asd"
            };
            var loc = new Loc
            {
                Id = 1,
                Name="gnc",

            };
            var prod = new Products
            {
                Id = 1,
                Price = 12,
                Name ="wpi",
            };
            var inventory = new Inventory
            {
                Id = 5,
                Qty = 10000,
                Pid = 1,
                StoreId=1

            };
            CustDal.Add(cust);
            LocDal.Add(loc);
            context.Products.Add(prod);
            context.Inventory.Add(inventory);
            context.SaveChanges();
        }
        [TestMethod]
        public async Task TestOrdtAdd()
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
            
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                  
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Debug.WriteLine(context.Customer.ToList().Count);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
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
            }
            finally
            {
                conn.Close();
            }
        }
        [TestMethod]
        public async Task TestOrdEdit()
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
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
            }
            finally
            {
                conn.Close();
            }
        }
        [TestMethod]
        public async Task TestOrdDelete()
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
                    var custs = await Repo.GetOrds();
                    int initialCount = custs.ToList().Count;
                    var newCust = new Order
                    {
                        CustId = 1,
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
            }
            finally
            {
                conn.Close();
            }
        }
        [TestMethod]
        public async Task TestOrdDeleteEmpty()
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
                    var newCust = new Order
                    {
                        CustId = 1,
                        Total = 0,
                        LocId = 1,
                        Timestamp = DateTime.Now
                    };
                    Repo.Add(newCust);
                    var custs = await Repo.GetOrds();
                    int initialCount = custs.ToList().Count;
                    var newCust1 = new Order
                    {
                        CustId = 1,
                        Total = 0,
                        LocId = 1,
                        Timestamp = DateTime.Now
                    };
                    int rem = Repo.Add(newCust1);

                    custs = await Repo.GetOrds();
                    int current = custs.ToList().Count;
                    Assert.AreEqual(initialCount, initialCount);
                    Repo.Remove(rem);
                }
            }
            finally
            {
                conn.Close();
            }
        }
        [TestMethod]
        public async Task TestOrdTotalUpdate()
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
                    var newCust = new Order
                    {
                        CustId = 1,
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
                    Assert.AreEqual(0,order.Total);
                    Repo.RemoveOrderItem(orderItem);
                    Repo.Remove(addedID);
                }
            }
            finally
            {
                conn.Close();
            }
        }
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(100000000)]
        public async Task TestInvalidQty(int qty)
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
                    var orderItem = new OrderItem
                    {
                        Oid = 1,
                        Qty = qty,
                        Pid = 5,
                    };
                    Assert.IsFalse(orderItem.ValidateQuantity(LocDal.GetQty(5)));
                }
            }
            finally
            {
                conn.Close();
            }
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public async Task TestValidQty(int qty)
        {
            OrderDAL Repo;
            LocationDAL LocDal;
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            try
            {
                var options = new DbContextOptionsBuilder<WheyMenContext>()
                    .UseSqlite(conn)
                    .Options;
                using (var context = new WheyMenContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using (var context = new WheyMenContext(options))
                {
                    TestSetup(context);
                    Repo = new OrderDAL(context);
                    LocDal = new LocationDAL(context);
                    var orderItem = new OrderItem
                    {
                        Oid = 1,
                        Qty = qty,
                        Pid = 5,
                    };
                    Assert.IsTrue(orderItem.ValidateQuantity(LocDal.GetQty(5)));
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
