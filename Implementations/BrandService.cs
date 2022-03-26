using BrandsHTTPService.Abstracts;
using BrandsHTTPService.DTOModels;
using BrandsHTTPService.EntityModels;
using BrandsHTTPService.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandsHTTPService.Implementations
{
    public class BrandService : IBrandsService
    {  
       
       public async Task<IEnumerable<BrandDTO>> GetBrandsAsync(StoreContext context)
       {
            var brands = await context.Brands?
                .AsNoTracking()
                .Where(x => x.IsDeletedBrand != true)
                .Select(x =>
                new BrandDTO
                {
                    BrandId = x.BrandId,
                    BrandName = x.BrandName,
                    AllowableSizes = x.AllowableSizes.Select(y => new AllowableSizeDTO
                    {
                        AllowableSizeId = y.AllowableSizeId,
                        RFSize = y.RFSize,
                        BrandSize = y.BrandSize
                    })
                .ToList()
                })
               .ToListAsync();

            return brands;
       }
       public async Task<IEnumerable<BrandDTO>> GetBrandByIdAsync(StoreContext context, BrandListDTO brand)
       {
            var brands = new List<BrandDTO>();
            if (brand.BrandId.Any())
            {
                var validModelIdArray = brand.BrandId.Where(x => x > 0).ToArray();
                brands = await context.Brands?
                .AsNoTracking()
                .Where(x => validModelIdArray.Contains(x.BrandId) && x.IsDeletedBrand != true)
                .Select(x => new BrandDTO
                {
                    BrandId = x.BrandId,
                    BrandName = x.BrandName,
                    AllowableSizes = x.AllowableSizes.Select(y => new AllowableSizeDTO
                    {
                        AllowableSizeId = y.AllowableSizeId,
                        RFSize = y.RFSize,
                        BrandSize = y.BrandSize
                    }).ToList()
                })
                .ToListAsync();
            }
            
            return brands;
       }

      public async Task<IActionResult> PostBrandAsync(StoreContext context, BrandPostDTO brand)
      {
            if(String.IsNullOrWhiteSpace(brand.BrandName))
            {
                return new JsonResult(new { mes = "Пожалуйста, укажите название бренда, которое необходимо внести" });
            }

            if (await context.Brands.AnyAsync(x => x.BrandName == brand.BrandName))
            {
                return new JsonResult(new { mes = "Данный бренд уже существует в базе данных!" });
            }
            await context.Brands.AddAsync(new Brand { BrandName = brand.BrandName, IsDeletedBrand = false });

            await context.SaveChangesAsync();

            return new JsonResult(new { mes = "Добавление бренда произошло успешно!" });
      }
       public async Task<IActionResult> EditBrandAsync(StoreContext context, BrandEditDTO brand)
       {
            try 
            {
                if(brand.BrandId < 0)
                {
                    return new JsonResult(new { mes = "ID бренда не может быть меньше 0!" });
                }
                var _brand = await context.Brands.Where(x => x.BrandId == brand.BrandId).FirstAsync();
                if (String.IsNullOrEmpty(_brand.BrandId.ToString()))
                {
                    return new JsonResult(new { mes = "В базе данных нет бренда с указанным ID" });
                }
                _brand.BrandName = brand.BrandName;
                await context .SaveChangesAsync();
            }
            catch
            {
                return new JsonResult(new { mes = "Во время выполнения обновления произошла ошибка!" });
            }
                     
            return new JsonResult(new { mes = "Обновление названия бренда успешно завершено!" });
       }
        public async Task<IActionResult> DeleteBrandAsync(StoreContext context, BrandListDTO brands)
        {
            for (int i = 0; i<= brands.BrandId.Length - 1; i++)
            {
                if(brands.BrandId[i] < 0)
                {
                    continue;
                }
                var brand = await context.Brands.Where(x => x.BrandId == brands.BrandId[i]).FirstAsync();
               
                if (brand.IsDeletedBrand == true )
                {
                    continue;
                }
                else
                {
                   brand.IsDeletedBrand = true;                  
                }
                context.SaveChanges();
            }
            await context.SaveChangesAsync();
            return new JsonResult(new { mes = "Операция удаления выполнена" });
        }

        public async Task<IActionResult> EditAllowableSizeAsync(StoreContext context, AllowableSizeDTO allowableSize)
        {
            var sizeId = await context.AllowableSizes.AsNoTracking().Where(x => x.AllowableSizeId == allowableSize.AllowableSizeId).FirstAsync();
            if(String.IsNullOrEmpty(Convert.ToString(sizeId)))
            {
                return new JsonResult(new { mes = "Списка размеров с заданным ID в базе не существует" });
            }

            var _allowableSize = await context.AllowableSizes.Where(x => x.AllowableSizeId == sizeId.AllowableSizeId).FirstAsync();

            _allowableSize.BrandSize = allowableSize.BrandSize;

            _allowableSize.RFSize = allowableSize.RFSize;

            await context.SaveChangesAsync();

            return new JsonResult(new { mes = "Операция обновления списка размеров завершена" });
        }
        public async Task<AllowableSizeExistDTO> IsAllowableSizeExist(StoreContext context, AllowableSizeExistDTO product)
        {
            var brandId = await context.Brands.AsNoTracking().Where(x => x.BrandName == product.BrandName).Select(x => x.BrandId).FirstAsync();
            var allowableSize =  context.AllowableSizes
                .AsNoTracking()
                .Where(x => x.RFSize == product.RfSize && x.BrandId == brandId)
                .First();
            if(String.IsNullOrWhiteSpace(allowableSize.BrandId.ToString()))
            {
                return new AllowableSizeExistDTO { IsSizeExist = false };
            }

            return new AllowableSizeExistDTO { IsSizeExist = true };
        }


    }
}
