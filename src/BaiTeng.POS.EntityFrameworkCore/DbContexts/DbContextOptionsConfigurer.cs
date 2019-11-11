using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using BaiTeng.POS.EntityFrameworkCore;

namespace BaiTeng.POS.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

    }
}
