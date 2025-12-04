using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trouble_Ticket_Manager.Services
{
    public class MockWarrantyChecker
    {
        public static bool GetMockWarranty(string serviceTag)
        {
            // Use the Service Tag (or just a fixed date) to determine the outcome.
            Random random = new Random(serviceTag.GetHashCode()); // Seed the random based on the tag

            if (random.Next(0, 3) == 0) // 1 in 3 chance the warranty is expired
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}