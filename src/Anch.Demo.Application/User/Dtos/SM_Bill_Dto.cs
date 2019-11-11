using Abp.AutoMapper;
using System.Collections.Generic;
using Anch.Demo.Core;

namespace Anch.Demo.Application
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