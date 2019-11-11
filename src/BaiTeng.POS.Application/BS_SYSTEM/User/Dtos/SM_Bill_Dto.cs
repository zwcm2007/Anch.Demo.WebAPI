using Abp.AutoMapper;
using System.Collections.Generic;
using BaiTeng.POS.Core;

namespace BaiTeng.POS.Application
{

    //[AutoMapFrom(typeof(User))]
    public class SM_Bill_Dto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string StaffName { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
    }
}