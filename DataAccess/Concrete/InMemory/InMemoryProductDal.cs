using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{ // ın memory product dal sanki veri varmış gibi bir ortamı simula ediyoruz
    public class InMemoryProductDal : IProductDal
    { 
        //bu projeyi başlatınca bellekte bir addet ürün listesini oluşturmasını istiyoruz

        //16. ve 27 satıra kader birtane bellekte ürün oluştrdu :.. sql server veri tabanından veri geliyormuş gibi simule ediyoruz:
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
             new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
             new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
             new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
             new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
             new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=5,UnitsInStock=1}

            };
        }

        public void Add(Product product)
        {
           _products.Add(product);
            
        }

        public void Delete(Product product)
        {
                                         //LINQ
            Product ProductToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
           
            
            _products.Remove(ProductToDelete);

            //SingelOrDefault = tek bir eleman bulmaya yarar
            //FirstOrDefault kulansakda olur 
            //lamda = p=>

            //ProductToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            //_products.Remove(ProductToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
          return _products.Where(p=>p.CategoryId==categoryId).ToList(); 

        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName=product.ProductName;
            productToUpdate.CategoryId=product.CategoryId;
            productToUpdate.UnitPrice=product.UnitPrice;
            productToUpdate.UnitsInStock=product.UnitsInStock;
        }
    }
}
