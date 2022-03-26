using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandsHTTPService.DTOModels
{
    public class AllowableSizeExistDTO
    {
        public bool IsSizeExist { get; set; }
        public string BrandName { get; set; }
        public string RfSize { get; set; }
    }
}
