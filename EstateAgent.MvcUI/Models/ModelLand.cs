using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAgent.MvcUI.Models
{
    public class ModelLand
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public List<string> Pictures { get; set; }
        public string RealEstate { get; set; }
        public string Type { get; set; }
        public bool WithinaBuildingComplex { get; set; }
    }
}
