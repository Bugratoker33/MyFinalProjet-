using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{ 
    //burda polimorfizim var 
    public class BussinessRules
    {
        // BussinessRules.Run(CheckIfProductCountCategoryCorrect(product.CategoryId),CheckIfProductNameExists(product.ProductName), CheckIfCategoryLimitExceded());
        //burada bu methodu busnisda çağırdığımızaman ıresult arayine gönderir oda logics e foreach ile arayi gez eğer successdurumu false olan varsa mesajı döndür:
        public static IResult Run(params IResult[] logics)
            
        {
            foreach (var logic in logics)
            { 
                if (!logic.Succeess)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
