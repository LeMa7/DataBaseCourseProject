using DataBaseCourseProject.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBaseCourseProject.Models.Action
{
    public class DifferenceModel
    {
        public int? FirstId { get; set; }

        public int? SecondId { get; set; }

        public List<Product> Produts { get; set; }

        public string Differences { get; set; }
    }
}