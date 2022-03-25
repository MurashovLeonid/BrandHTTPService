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
      Task<IEnumerable<BrandGetDTO>> GetBrandsAsync(StoreContext context);
      Task<IEnumerable<BrandGetDTO>> GetBrandByIdAsync(StoreContext context, BrandListDTO brand);
      Task<IActionResult> PostBrandAsync(StoreContext context, BrandPostDTO brand);
      Task<IActionResult> EditBrandAsync(StoreContext context, BrandUpdateDTO brand);
      Task<IActionResult> DeleteBrandAsync(StoreContext context, int[] id);
      Task<IActionResult> EditAllowableSizeAsync(StoreContext context, AllowableSizeDTO allowableSizes);

    }
}
