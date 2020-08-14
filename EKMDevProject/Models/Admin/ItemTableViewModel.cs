using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKMDevProject.Models.Admin
{
    public class ItemTableViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CommonCode { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }

        public ItemTableViewModel() { }
        public ItemTableViewModel(Item dbEntity)
        {
            ID = dbEntity.ID;
            Name = dbEntity.Name;
            CommonCode = dbEntity.CommonCode;
            Price = "£" + dbEntity.Price.ToString();
            Quantity = dbEntity.Stock;
        }
    }
}