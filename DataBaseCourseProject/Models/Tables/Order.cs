using System;

namespace DataBaseCourseProject.Models.Tables
{
    public class Order : BaseTableEntity
    {
        public int? UserId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string Adress { get; set; }
    }
}