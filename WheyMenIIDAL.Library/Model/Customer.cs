﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMenDAL.Library.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get;}

        public string Name { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Pwd { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
