using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BowlingApi_Service.Controllers
{
    public class LaneController : Controller
    {
        // GET: LaneController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LaneController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LaneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LaneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaneController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LaneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LaneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
