using BrandsHTTPService.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandsHTTPService.DTOModels
{
    public class BrandGetDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public List<AllowableSizeDTO> AllowableSizes { get; set; }

    }
}
