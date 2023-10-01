using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Data;
using BusinessServices.DataServices;
using Repositories.Data.Models;
using BusinessModels;

namespace RajaTest.Areas.Raja.Controllers
{
    [Area("Raja")]
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;
        private readonly Context _context;

        public DashboardController(Context context, DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            _context = context;
        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            var users = await _dashboardService.GetAllUsers();
            return View(users);
        }

        // Delete
        public ActionResult Delete(int id)
        {
            _dashboardService.DeleteUser(id);
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Create User
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            #region DataValidation
            if (user.FirstName == null)
            {
                TempData["ErrorMessage"] = "نام را وارد کنید.";
                return View();
            }
            if (user.LastName == null)
            {
                TempData["ErrorMessage"] = "نام خانوادگی را وارد کنید.";
                return View();
            }
            if (user.PhoneNumber == null || !user.PhoneNumber.StartsWith("09"))
            {
                TempData["ErrorMessage"] = "شماره موبایل نامعتبر است.";
                return View();
            }
            var tekrari = _context.Users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
            if (tekrari != null)
            {
                TempData["ErrorMessage"] = "شماره موبایل تکراری است.";
                return View();
            }
            #endregion
            await _dashboardService.AddUser(user);
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _dashboardService.GetUserById(id);
            return View(user);
        }

        // POST: Edit User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            #region DataValidation
            if (user.FirstName == null)
            {
                TempData["ErrorMessage"] = "نام را وارد کنید.";
                return View();
            }
            if (user.LastName == null)
            {
                TempData["ErrorMessage"] = "نام خانوادگی را وارد کنید.";
                return View();
            }
            if (user.PhoneNumber == null || !user.PhoneNumber.StartsWith("09"))
            {
                TempData["ErrorMessage"] = "شماره موبایل نامعتبر است.";
                return View();
            }
            #endregion

            await _dashboardService.EditUser(user);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCertificates(int userId)
        {
            var certificates = await _dashboardService.GetUserWithCertificates(userId);
            List<CertViewModel> certs = new List<CertViewModel>();
            foreach (var item in certificates)
            {
                var cert = new CertViewModel();
                cert.CertificateName = item.CertificateName;
                cert.CertificateTypeName = item.CertificateTypeName;
                certs.Add(cert);
            }
            var x = Json(certs);
            return x;

        }

        [HttpGet]
        public async Task<ActionResult> CertEdit(int id)
        {
            ViewBag.UserId = id;
            var userName = await _dashboardService.GetUserName(id);
            ViewBag.UserName = userName;
            var certs = await _dashboardService.GetAllCertificatesWithTypeName();
            var userCerts = await _dashboardService.GetUserWithCertificates(id);
            var userCertIds = userCerts.Select(item => item.CertificateId).ToList();
            certs.RemoveAll(cert => userCertIds.Contains(cert.Id));
            ViewBag.Certs = certs;


            return View(userCerts);
        }

        public async Task<IActionResult> AssignCert(int userId, int certId)
        {
            await _dashboardService.AssignCertificateToUser(userId, certId);
            return RedirectToAction("CertEdit", "Dashboard", new { id = userId });
        }

        public async Task<IActionResult> UnAssignCert(int userId, int certId)
        {
            await _dashboardService.UnAssignCertificateFromUser(userId, certId);
            return RedirectToAction("CertEdit", "Dashboard", new { id = userId });
        }




    }
}