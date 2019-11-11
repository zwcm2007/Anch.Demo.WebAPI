using Abp.Domain.Entities;
using System;

namespace Anch.Demo.Core
{
    /// <summary>
    /// BT_Customer
    /// </summary>
    public class BT_Customer : Entity<int>
    {
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime SvrBeginDate { get; set; }

        public DateTime SvrEndDate { get; set; }

        public int OnlineUser { get; set; }

        public DateTime CreateDate { get; set; }

        public string ServiceUrl { get; set; }

        public string vActID { get; set; }
    }
}