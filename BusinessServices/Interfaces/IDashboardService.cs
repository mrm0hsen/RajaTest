using Repositories.Data.Models;

namespace BusinessServices.Interfaces.IDashboardService
{
    public interface IDashboardService
    {

        #region Users

        List<User> GetAllUsers();

        void DeleteUser(int id);

        void AddUser(User user);

        User GetUserById(int id);

        void EditUser(User user);

        Task<List<UserCertificate>> GetUserCertificates(int id);

        Task<string> GetUserName(int id);

        Task AssignCertificateToUser(int userId, int certificateId);
        Task UnAssignCertificateFromUser(int userId, int certId);

        #endregion

        #region Certificates

        Task<List<Certificate>> GetAllCertificates();

        Task AddCertificate(Certificate certificate);

        Task<List<Certificate>> GetAllCertificatesWithTypeName();

        Task DeleteCertificate(int id);

        Task<Certificate> GetCertificateById(int id);

        Task EditCertificate(Certificate certificate);

        Task<string> GetCertificateName(int id);

        Task<List<UserCertificate>> GetUserWithCertificates(int id);

        #endregion

        #region CertificateTypes

        Task<List<CertificateType>> GetAllTypes();

        Task DeleteType(int id);

        Task AddType(CertificateType certificateType);

        Task<CertificateType> GetTypeById(int id);

        Task EditType(CertificateType certificateType);

        Task<string> GetCertificateTypeName(int id);

        #endregion
    }
}