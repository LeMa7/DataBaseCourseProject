using System;

namespace DataBaseCourseProject.Models.Views
{
    public class ActiveOrderView
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShipDate { get; set; }

        public string Adress { get; set; }
    }
}