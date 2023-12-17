using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //static verdiğimizde clası newlemiyoruz 
        //direk messages. devam ediyoruz 
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz"; //ınvalid = geçersiz:
        public static string MaintenanceTime = "Sistem Bakında";
        public static string ProductsListed = "ürünler Listelendi";
        public static string ProductCountCategoryError = " bir kategoryde en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Aynı isimden 2 adet eklenemz";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok ";
        public static string CateoriListed = "Category Listelendi";

    }
}
