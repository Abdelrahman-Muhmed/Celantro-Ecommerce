using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceCoffe_DAL.Models.Product
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }

        //I will Handle Nagational Prop By Fluint Api 
        public string PictureUrl { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string Description { get; set; }

    }
}
