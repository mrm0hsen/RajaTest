using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Models
{
    public class CertificateType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نوع مدرک")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        //Relation with Certificate class
        public List<Certificate> Certificates { get; set; }

    }
}
