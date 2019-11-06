using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class FoodEditViewModel : FoodCreateViewModel
    {
        public int FoodId { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
