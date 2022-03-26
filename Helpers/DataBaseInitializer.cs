using BrandsHTTPService.EntityModels;
using BrandsHTTPService.EntityModels.AuthentificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandsHTTPService.Helpers
{
    public static class DataBaseInitializer
    {
        public static void UserInitialize(UserContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }
            var user = new User { UserId = 1, UserEmail = "lenja-d@yandex.ru", UserPassword = "12345678" };
            context.Users.Add(user);
            context.SaveChanges();
        }
        public static void StoreInitialize(StoreContext context)
        {
            
            context.Database.EnsureCreated();
            if (context.Brands.Any())
            {
                return;
            }
            var brands = new Brand[]
            {
                new Brand { BrandId = 1, BrandName = "Nike", AllowableSizes = new List<AllowableSize>(){new AllowableSize{ AllowableSizeId = 10,RFSize = "1", BrandSize = "10", BrandId = 1},
                new AllowableSize{AllowableSizeId = 20, RFSize = "2", BrandSize = "40", BrandId = 1}}},

                new Brand { BrandId = 2, BrandName = "Adidas", AllowableSizes = new List<AllowableSize>(){  new AllowableSize{ AllowableSizeId = 30,RFSize = "3", BrandSize = "55", BrandId = 2},
                new AllowableSize{ AllowableSizeId = 40, RFSize = "5", BrandSize = "78", BrandId = 2}}},

                new Brand { BrandId = 3,BrandName = "Puma", AllowableSizes = new List<AllowableSize>(){new AllowableSize{ AllowableSizeId = 50, RFSize = "6", BrandSize = "19", BrandId = 3},
                new AllowableSize{ AllowableSizeId = 60,RFSize = "18", BrandSize = "43", BrandId = 3}}}
            };
            //var allowableSizes = new AllowableSize[]
            //{
            //    new AllowableSize{ RFSize = "1", BrandSize = "10", BrandId = 1},
            //    new AllowableSize{ RFSize = "2", BrandSize = "40", BrandId = 1},
            //    new AllowableSize{ RFSize = "3", BrandSize = "55", BrandId = 2},
            //    new AllowableSize{ RFSize = "5", BrandSize = "78", BrandId = 2},
            //    new AllowableSize{ RFSize = "6", BrandSize = "19", BrandId = 3},
            //    new AllowableSize{ RFSize = "18", BrandSize = "43", BrandId = 3}
            //};
            foreach(var item in brands)
            {
                context.Brands.Add(item);

                foreach (var size in item.AllowableSizes)
                {
                    context.AllowableSizes.Add(size);
                }
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}
