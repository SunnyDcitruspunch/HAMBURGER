using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hamburger13.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {

            //represent customers waiting outside
            Queue<string> myQueue = new Queue<string>();

            //hold info about each customer
            Dictionary<string, int> myDictionary = new Dictionary<string, int>();

            //add 100 customers into queue (customers waiting inline)
            for(int i = 0; i < 100; i++)
            {
                myQueue.Enqueue(randomName());                
            }            

            //iterate over customers in the queue
            IEnumerator<string> MyQueueEnumerator = myQueue.GetEnumerator();

            //sort dictionary with LINQ
            var items = from customer in myDictionary
                        orderby customer.Value ascending
                        select customer;

            while (MyQueueEnumerator.MoveNext())
            {
                if (myDictionary.ContainsKey(MyQueueEnumerator.Current) == false)
                {
                    myDictionary.Add(MyQueueEnumerator.Current, 0);
                } else
                {
                    myDictionary[MyQueueEnumerator.Current] += randomNumberInRange();
                }              
            }

            //sort customer names inside of dictionary
            var descDictionary = myDictionary.OrderByDescending(aCustomer => aCustomer.Value);
            var ascDictionary = myDictionary.OrderBy(aCustomer => aCustomer.Value);

            ViewBag.descCustomers = descDictionary.ToArray();
            ViewBag.ascCustomers = ascDictionary.ToArray();          

            return View();
        }

        public static Random random = new Random();

        public static string randomName()
        {
            string[] names = new string[8] {"Dan Morain", "Emily Bell", "Carol Roche", "Ann Rose", "John Miller", "Greg Anderson", "Arthur McKinney", "Joann Fisher" };
            int randomIndex = Convert.ToInt32(random.NextDouble() * 7);
            return names[randomIndex];
        }

        public static int randomNumberInRange()
        {
            return Convert.ToInt32(random.NextDouble() * 20);
        }

        public class OrderComparer : IComparer<string>
        {
            public int Compare (string x, string y)
            {
                return string.Compare(x, y, true); 
            }
        }
    }
}