using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrandsHTTPService.EntityModels;
using BrandsHTTPService.DTOModels;
using BrandsHTTPService.Abstracts;

namespace BrandsHTTPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly StoreContext _context;
        private readonly IBrandService _brandService;

        public BrandsController(StoreContext context, IBrandService brandService)
        {
            _brandService = brandService;
            _context = context;

        }

        // GET: api/Brands/All
        [HttpGet("All")]
        public async Task<IActionResult> GetBrandsAsync()
        {
            return Ok(await _brandService.GetBrandsAsync(_context));
        }

        // GET: api/Brands/List
        [HttpGet("List")]
        public async Task<IActionResult> GetBrandByIdAsync([FromBody]BrandListDTO brand)
        {
            return Ok(await _brandService.GetBrandByIdAsync(_context, brand));
        }

        // GET: api/Brands/Post
        [HttpPost("Post")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostBrandAsync([Bind("BrandName")]BrandPostDTO brand)
        {  
            return new JsonResult(await  _brandService.PostBrandAsync( _context, brand));
        }


        // POST: api/Brands/Edit
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBrandAsync([Bind("BrandId,BrandName")] BrandUpdateDTO brand)
        {
            
            return new JsonResult(await _brandService.EditBrandAsync(_context, brand));
        }


        // POST: api/Brands/AllowableSizes/Edit
        [HttpPost("AllowableSizes/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllowableSizeAsync([Bind("AllowableSizeId,BrandSize, RFSize")] AllowableSizeDTO allowableSize)
        {

            return new JsonResult(await _brandService.EditAllowableSizeAsync(_context, allowableSize));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBrandAsync(int[] id)
        {
            return new JsonResult(await _brandService.DeleteBrandAsync(_context, id));
        }


        
    }
}
