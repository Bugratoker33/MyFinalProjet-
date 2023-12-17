using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //DTO MEANS= DATA TRANSFORMATİON OBJET (BENİM TAŞIYACAĞIM OBJELER ASLINDA);:;:
            // GetAllbyCategory();
            // GetAllByUnitPrice_();
            //CategoryTest();
            //IOS conteyner öğrendikten sonra newlemeyi bırakcağız
            // CategoryTestEEngin();

          //  ProductTest();

        }

        //private static void ProductTest()
        //{
        //    ProductManeger productManeger = new ProductManeger(new EfProductDal());

        //    var result = productManeger.GetProductDetails();

        //    if (result.Succeess == true)
        //    {
        //        foreach (var product in productManeger.GetProductDetails().Data)
        //        {
        //            Console.WriteLine(product.ProductName + " / " + product.CategoryName);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine(result.Message);

        //    }
        //}

        private static void CategoryTestEEngin()
        {
            CategoryManeger categoryManeger = new CategoryManeger(new EfCategoryDal());

            foreach (var category in categoryManeger.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName + "   " + category.CategoryId);
            }
        }

        //private static void CategoryTest()
        //{
        //    ProductManeger productManeger = new ProductManeger(new EfProductDal() );

        //    foreach (var product in productManeger.GetAllByCategoryId(2).Data)
        //    {
        //        Console.WriteLine(product.ProductName + product.ProductId + "        " + product.CategoryId + "   " + product.UnitPrice);
        //        Console.WriteLine(product.ProductName);
        //    }

        //    Console.WriteLine("Hello, World!");
        //}

        //public static void GetAllbyCategory()
        //{
        //    ProductManeger productManeger = new ProductManeger(new EfProductDal());

        //    foreach (var product in productManeger.GetAllByCategoryId(2).Data)
        //    {
        //        //Console.WriteLine(product.ProductName + product.ProductId +"        "+ product.CategoryId+"   "+product.UnitPrice);
        //        Console.WriteLine(product.ProductName+product.ProductId);
        //    }

        //}
        //public static void GetAllByUnitPrice_()
        //{
        //    ProductManeger productManeger = new ProductManeger(new EfProductDal());
        //    foreach (var prdduct in productManeger.GetAllByUnitPrice(15, 46).Data)
        //    {
        //        Console.WriteLine(prdduct.UnitPrice +" = "+ prdduct.ProductName);
        //    }
        //}
    }
}