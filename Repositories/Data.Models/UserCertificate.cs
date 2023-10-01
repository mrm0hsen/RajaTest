using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Models
{
    public class UserCertificate
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }

        #region NotMapped

        [NotMapped]
        public string CertificateName { get; set; }

        [NotMapped]
        public string CertificateTypeName { get; set; }


        #endregion
    }
}
