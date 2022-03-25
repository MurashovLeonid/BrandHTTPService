using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BrandsHTTPService.EntityModels
{
    public class AllowableSize
    {
        
        public int AllowableSizeId { get; set; }
        public string BrandSize { get; set; }
        public string RFSize { get; set; }
        public bool IsDeletedSize { get; set; }
        public int? BrandId { get; set; }

        //public Brand Brand { get; set; }


    }
}
