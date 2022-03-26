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
using Microsoft.AspNetCore.Authorization;

namespace BrandsHTTPService.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly StoreContext _context;
        private readonly IBrandsService _brandService;

        public BrandsController(StoreContext context, IBrandsService brandService)
        {
            _brandService = brandService;
            _context = context;

        }
       
        // GET: /Brands/All
        [HttpGet("All")]  
        public async Task<IActionResult> GetBrandsAsync()
        {
            return Ok(await _brandService.GetBrandsAsync(_context));
        }

        // GET: /Brands/List
        [HttpGet("List")]
        
        public async Task<IActionResult> GetBrandByIdAsync([FromBody] BrandListDTO brand)
        {
            return Ok(await _brandService.GetBrandByIdAsync(_context, brand));
        }

        // Post: /Brands/Post
        [HttpPost("Post")]       
        public async Task<IActionResult> PostBrandAsync([Bind("BrandName")] BrandPostDTO brand)
        {
            return new JsonResult(await _brandService.PostBrandAsync(_context, brand));
        }


        // POST: /Brands/Edit
        [HttpPost("Edit")]
        
        public async Task<IActionResult> EditBrandAsync([Bind("BrandId,BrandName")] BrandEditDTO brand)
        {

            return new JsonResult(await _brandService.EditBrandAsync(_context, brand));
        }

        // POST: /Brands/Delete
        [HttpPost("Delete")]
        
        public async Task<IActionResult> DeleteBrandAsync([Bind("BrandId")] BrandListDTO brands)
        {
            return new JsonResult(await _brandService.DeleteBrandAsync(_context, brands));
        }

        // POST: /Brands/AllowableSizes/Edit
        [HttpPost("AllowableSizes/Edit")]
        
        public async Task<IActionResult> EditAllowableSizeAsync([Bind("AllowableSizeId,BrandSize, RFSize")] AllowableSizeDTO allowableSize)
        {

            return new JsonResult(await _brandService.EditAllowableSizeAsync(_context, allowableSize));
        }

        //POST: /Brands/AllowableSizeExist
        [HttpPost("IsAllowableSizeExist")]
        public async Task<AllowableSizeExistDTO> IsAllowableSizeExist([Bind("BrandName, RFSize")] AllowableSizeExistDTO allowableSize)
        {
            var allowableSizeExist = new AllowableSizeExistDTO();
            var response = await _brandService.IsAllowableSizeExist(_context, allowableSize);
            allowableSizeExist.IsSizeExist = response.IsSizeExist;
            return allowableSizeExist;
        }

    }
}
