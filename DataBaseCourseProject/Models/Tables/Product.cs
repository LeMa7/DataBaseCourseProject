﻿namespace DataBaseCourseProject.Models.Tables
{
    public class Product : BaseTableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? SubcategoryId { get; set; }

        public int? ProducerId { get; set; }

        public int? Price { get; set; }

        public int? Quantity { get; set; }
    }
}