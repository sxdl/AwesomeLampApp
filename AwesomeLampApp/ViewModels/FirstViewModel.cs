using AwesomeLampApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeLampApp.ViewModels
{
    public class FirstViewModel
    {
        public List<CarouseItemModel> Carouses { get; set; }

        public List<CommodityCategoryModel> Category { get; set; }
        public FirstViewModel()
        {
            Carouses = new List<CarouseItemModel>();
            Carouses.Add(new CarouseItemModel { ImageName = "b01.png" });
            Carouses.Add(new CarouseItemModel { ImageName = "b02.png" });
            Carouses.Add(new CarouseItemModel { ImageName = "b03.png" });

            Category = new List<CommodityCategoryModel>();
            Category.Add(new CommodityCategoryModel { ImageName = "i01.jpg", Header = "item01" });
            Category.Add(new CommodityCategoryModel { ImageName = "i02.jpg", Header = "item02" });
            Category.Add(new CommodityCategoryModel { ImageName = "i03.jpg", Header = "item03" });
            Category.Add(new CommodityCategoryModel { ImageName = "i04.jpg", Header = "item04" });
            Category.Add(new CommodityCategoryModel { ImageName = "i05.jpg", Header = "item05" });
            Category.Add(new CommodityCategoryModel { ImageName = "i06.jpg", Header = "item06" });
            Category.Add(new CommodityCategoryModel { ImageName = "i07.jpg", Header = "item07" });
            Category.Add(new CommodityCategoryModel { ImageName = "i08.jpg", Header = "item08" });
            Category.Add(new CommodityCategoryModel { ImageName = "i09.jpg", Header = "item09" });
            Category.Add(new CommodityCategoryModel { ImageName = "i10.jpg", Header = "item10" });
        }
    }
}
