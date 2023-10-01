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
    public class CertificateController : Controller
    {
        private readonly DashboardService _dashboardService;
        private readonly Context _context;

        public CertificateController(Context context,DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            _context = context;
        }

        // GET: Certificate
        public async Task<ActionResult> Index()
        {
            var certs = await _dashboardService.GetAllCertificatesWithTypeName();
            return View(certs);
        }

        // Delete
        public async Task<ActionResult> Delete(int id)
        {
            await _dashboardService.DeleteCertificate(id);
            return RedirectToAction("Index", "Certificate");
        }

        // GET: Create Certificate
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.Types = await _dashboardService.GetAllTypes();
            return View();
        }

        // POST: Create Certificate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Certificate certificate)
        {
            #region DataValidation
            if (certificate.Name == null)
            {
                TempData["ErrorMessage"] = "نام را وارد کنید.";
                return View();
            }
            if (certificate.Type == 0)
            {
                TempData["ErrorMessage"] = "نوع مدرک را وارد کنید.";
                return View();
            }
            #endregion
            await _dashboardService.AddCertificate(certificate);
            return RedirectToAction("Index", "Certificate");
        }

        // GET: Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var types = await _dashboardService.GetAllTypes();
            var cert = await _dashboardService.GetCertificateById(id);
            foreach (var type in types)
            {
                if (cert.Type == type.Id)
                    cert.TypeName = type.Name;
            }
            ViewBag.Types = types;
            return View(cert);
        }

        // POST: Edit Certificate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Certificate certificate)
        {
            #region DataValidation
            if (certificate.Name == null)
            {
                TempData["ErrorMessage"] = "نام را وارد کنید.";
                return View();
            }
            if (certificate.Type == 0)
            {
                TempData["ErrorMessage"] = "نوع را وارد کنید.";
                return View();
            }
            #endregion

            await _dashboardService.EditCertificate(certificate);
            return RedirectToAction("Index", "Certificate");
        }

    }
}