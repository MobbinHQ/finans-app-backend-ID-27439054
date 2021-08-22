using System;
using System.Threading.Tasks;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace FinansApp.Job
{
    class Program
    {

        static async Task Main(string[] args)
        {
            Console.Write("zxczxczxc");
            using (var db = new FinansAppDbContext())
            {
                var a = await db.Users.ToListAsync();
                foreach (var item in a)
                {
                    Console.WriteLine(item.Name);
                }
            }

        }
    }
}