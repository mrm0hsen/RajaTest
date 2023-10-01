using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Models
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="نام مدرک")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public string Name { get; set; }

        [Display(Name ="نوع مدرک")]
        [Required]
        public int Type { get; set; }

        public bool IsDeleted { get; set; } = false;

        #region Relations

        //Relation with CertificateType class
        [ForeignKey("Type")]
        public CertificateType CertificateType { get; set; }

        //Relation with User class
        public ICollection<UserCertificate> UserCertificates { get; set; }


        #endregion

        #region NOTMapped

        [NotMapped]
        public string TypeName { get; set; }

        #endregion

    }
}
