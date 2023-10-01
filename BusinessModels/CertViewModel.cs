using Repositories.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class CertViewModel
    {
        public string CertificateName { get; set; }
        public string CertificateTypeName { get; set; }

    }
}
