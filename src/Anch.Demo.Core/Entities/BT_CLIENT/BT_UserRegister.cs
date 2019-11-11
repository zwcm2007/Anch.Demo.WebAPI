using Abp.Domain.Entities;
using System;

namespace Anch.Demo.Core
{
    /// <summary>
    /// BT_UserRegister
    /// </summary>
    public class BT_UserRegister : Entity<int>
    {
        public string CustomerId { get; set; }

        public string UserID { get; set; }

        public string FunctionsMOD { get; set; }

        public DateTime RegisterTime { get; set; }

        public string MachineID { get; set; }

        public int OnlineState { get; set; }
    }
}