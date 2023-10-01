using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Data;
using BusinessServices.DataServices;
using Repositories.Data.Models;

namespace RajaTest.Areas.Raja.Controllers
{
    [Area("Raja")]
    public class TypeController : Controller
    {
        private readonly DashboardService _dashboardService;
        private readonly Context _context;

        public TypeController(Context context, DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            _context = context;
        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            var types = await _dashboardService.GetAllTypes();
            return View(types);
        }

        // Delete
        public async Task<ActionResult> Delete(int id)
        {
            await _dashboardService.DeleteType(id);
            return RedirectToAction("Index", "Type");
        }

        // GET: Create Type
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create Type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CertificateType certificateType)
        {
            #region DataValidation
            if (certificateType.Name == null)
            {
                TempData["ErrorMessage"] = "نام را وارد کنید.";
                return View();
            }
            var tekrari = _context.CertificateTypes.FirstOrDefault(x => x.Name == certificateType.Name);
            if (tekrari != null)
            {
                TempData["ErrorMessage"] = "نوع مدرک تکراری است.";
                return View();
            }
            #endregion
            await _dashboardService.AddType(certificateType);
            return RedirectToAction("Index", "Type");
        }

        // GET: Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var type = await _dashboardService.GetTypeById(id);
            return View(type);
        }

        // POST: Edit Type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CertificateType certificateType)
        {
            #region DataValidation
            if (certificateType.Name == null)
            {
                TempData["ErrorMessage"] = "نام را وارد کنید.";
                return View();
            }
            var tekrari = _context.CertificateTypes.FirstOrDefault(x => x.Name == certificateType.Name);
            if (tekrari != null)
            {
                TempData["ErrorMessage"] = "نوع مدرک تکراری است.";
                return View();
            }
            #endregion

            await _dashboardService.EditType(certificateType);
            return RedirectToAction("Index", "Type");
        }

    }
}