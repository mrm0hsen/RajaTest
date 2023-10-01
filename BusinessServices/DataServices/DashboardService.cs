using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using Repositories.Data.Models;
using Repositories.Data;
using System.Runtime.ConstrainedExecution;

namespace BusinessServices.DataServices
{
    public class DashboardService
    {
        private readonly Context _context;

        public DashboardService(Context context)
        {
            _context = context;
        }


        #region User
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _context.Users.Find(id);

            if (userToDelete != null)
            {
                userToDelete.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public async Task AddUser(User user)
        {
            if (user != null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task EditUser(User user)
        {
            var oldUser = await GetUserById(user.Id);
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();
        }

        public async Task<List<UserCertificate>> GetUserCertificates(int id)
        {
            var certs = await _context.UserCertificate.Where(x => x.UserId == id).ToListAsync();
            return certs;
        }

        public async Task<string> GetUserName(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var name = user.FirstName + " " + user.LastName;
            return name;
        }

        public async Task AssignCertificateToUser(int userId, int certId)
        {
            UserCertificate uc = new UserCertificate();
            uc.UserId = userId;
            uc.CertificateId = certId;
            await _context.UserCertificate.AddAsync(uc);
            await _context.SaveChangesAsync();
        }
        public async Task UnAssignCertificateFromUser(int userId, int certId)
        {
            var uc = await _context.UserCertificate.SingleOrDefaultAsync(x => x.UserId == userId && x.CertificateId == certId);

            if (uc != null)
            {
                _context.UserCertificate.Remove(uc);
                await _context.SaveChangesAsync();
            }
        }



        #endregion

        #region Certificates

        public async Task<List<Certificate>> GetAllCertificates()
        {
            return await _context.Certificates.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task AddCertificate(Certificate certificate)
        {
            if (certificate != null)
            {
                await _context.Certificates.AddAsync(certificate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Certificate>> GetAllCertificatesWithTypeName()
        {
            var certificates = await _context.Certificates
                .Where(x => x.IsDeleted == false)
                .Join(
                    _context.CertificateTypes,
                    certificate => certificate.Type,
                    certificateType => certificateType.Id,
                    (certificate, certificateType) => new Certificate
                    {
                        Id = certificate.Id,
                        Name = certificate.Name,
                        Type = certificate.Type,
                        TypeName = certificateType.Name
                    }
                )
                .ToListAsync();

            return certificates;
        }

        public async Task DeleteCertificate(int id)
        {
            var cert = await _context.Certificates.SingleOrDefaultAsync(x => x.Id == id);
            if (cert != null)
            {
                cert.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Certificate> GetCertificateById(int id)
        {
            return await _context.Certificates.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task EditCertificate(Certificate certificate)
        {
            var oldCertificate = await GetCertificateById(certificate.Id);
            oldCertificate.Name = certificate.Name;
            oldCertificate.Type = certificate.Type;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetCertificateName(int id)
        {
            var cert = await _context.Certificates.SingleOrDefaultAsync(x => x.Id == id);
            return cert.Name;
        }


        public async Task<List<UserCertificate>> GetUserWithCertificates(int id)
        {
            //var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var certs = await _context.UserCertificate.Where(x => x.UserId == id).ToListAsync();
            foreach (var cert in certs)
            {
                cert.CertificateName = await GetCertificateName(cert.CertificateId);
                cert.CertificateTypeName = await GetCertificateTypeName(cert.CertificateId);
            }
            return certs;
        }

        #endregion

        #region CertificateTypes

        public async Task<List<CertificateType>> GetAllTypes()
        {
            return await _context.CertificateTypes.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task DeleteType(int id)
        {
            var typeToDelete = _context.CertificateTypes.Find(id);

            if (typeToDelete != null)
            {
                typeToDelete.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddType(CertificateType certificateType)
        {
            if (certificateType != null)
            {
                await _context.CertificateTypes.AddAsync(certificateType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CertificateType> GetTypeById(int id)
        {
            return await _context.CertificateTypes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task EditType(CertificateType certificateType)
        {
            var oldcertificateType = await GetTypeById(certificateType.Id);
            oldcertificateType.Name = certificateType.Name;

            await _context.SaveChangesAsync();
        }

        public async Task<string> GetCertificateTypeName(int id)
        {
            var cert = await _context.Certificates.SingleOrDefaultAsync(x => x.Id == id);
            var type = await _context.CertificateTypes.SingleOrDefaultAsync(x => x.Id == cert.Type);
            return type.Name;
        }

        #endregion

    }
}