﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BaiTeng.POS.Core;
using System.Collections.Generic;

namespace BaiTeng.POS.Application
{
    /// <summary>
    /// 查询用户输入
    /// </summary>
    public class SearchUsersInput : IPagedResultRequest
    {
        public string UserName { get; set; }
        public bool IsExactMatch { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; } = 20;
    }

    public class SearchUsersOutput
    {
        public int TotalCount { get; set; }

        public List<UserDto> Users { get; set; }
    }


    [AutoMapFrom(typeof(SM_User))]
    public class UserDto
    {
        public string UserCode { get; set; }

        public string UserName { get; set; }
    }
}