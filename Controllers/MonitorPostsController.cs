using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedditListenerApp.Controllers
{
    public class MonitorPostsController : Controller
    {
        // GET: MonitorPosts
        public ActionResult Index()
        {
            return View();
        }


        public void MonitorNewPosts()
        {
            Console.WriteLine("Monitoring funny for new posts....");

            //funny.Posts.NewUpdated += C_NewPostsUpdated;
            //funny.Posts.MonitorNew();  // Toggle on.
        }

        // GET: MonitorPosts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}