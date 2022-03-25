using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BrandsHTTPService.EntityModels
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool IsDeletedBrand { get; set; }
        public ICollection<AllowableSize> AllowableSizes { get; set; }


    }
}
