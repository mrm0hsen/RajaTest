using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام الزامی است")]
        [MaxLength(30, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است")]
        [MaxLength(30, ErrorMessage = "{0} نمیتواند بیشتر از {1} حرف باشد")]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} رقم باشد")]
        [MinLength(11, ErrorMessage = "{0} نمیتواند کمتر از {1} رقم باشد")]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; } = false;

        //Relation with Certificate class
        public ICollection<UserCertificate> UserCertificates { get; set; }

        [NotMapped]
        public List<UserCertificate> UserCerts { get; set;}
    }
}
