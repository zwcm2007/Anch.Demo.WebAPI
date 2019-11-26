using Abp.AutoMapper;
using Anch.Demo.Core;
using System.Collections.Generic;

namespace Anch.Demo.Application
{
    public class SearchUsersOutput
    {
        public int TotalCount { get; set; }

        public List<UserDto> Users { get; set; }
    }

    [AutoMapFrom(typeof(User))]
    public class UserDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FactName { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }
    }
}