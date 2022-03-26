using BrandsHTTPService.DTOModels;
using BrandsHTTPService.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandsHTTPService.Abstracts
{
    public interface IBrandService
    {
      Task<IEnumerable<BrandDTO>> GetBrandsAsync(StoreContext context);
      Task<IEnumerable<BrandDTO>> GetBrandByIdAsync(StoreContext context, BrandListDTO brand);
      Task<IActionResult> PostBrandAsync(StoreContext context, BrandPostDTO brand);
      Task<IActionResult> EditBrandAsync(StoreContext context, BrandEditDTO brand);
      Task<IActionResult> DeleteBrandAsync(StoreContext context, BrandListDTO brands);
      Task<IActionResult> EditAllowableSizeAsync(StoreContext context, AllowableSizeDTO allowableSizes);

    }
}
