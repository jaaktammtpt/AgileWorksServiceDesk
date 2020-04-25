using AgileWorksServiceDesk.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesc.IntegrationTests
{
    public class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Requests.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static List<Request> GetSeedingMessages()
        {
            var requestList = new List<Request>();

            for (int i = 0; i < 10; i++)
            {
                requestList.Add(new Request
                {
                    Description = "Request no " + i,
                    DueDateTime = DateTime.Now.AddHours(-5 + i)
                });
            }

            return requestList;
        }
    }
}
