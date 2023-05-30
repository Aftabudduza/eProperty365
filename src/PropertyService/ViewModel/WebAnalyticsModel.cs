using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.ViewModel
{
    public class WebAnalyticsModel
    {
        public string OwnerId { get; set; }
        public string PropertyManagerId { get; set; }
        public string LocationId { get; set; }
        public string UnitId { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string TotalViews { get; set; }
        public string MTDViews { get; set; }
        public string TotalSchedules { get; set; }
        public string MTDSchedules { get; set; }
        public string TotalStart { get; set; }
        public string MTDStart { get; set; }
        public string TotalCompleted { get; set; }
        public string MTDCompleted { get; set; }

        //public string OwnerId { get; set; }
        //public string PropertyManagerId { get; set; }
        //public string LocationId { get; set; }
        //public string UnitId { get; set; }
        //public string from { get; set; }
        //public string to { get; set; }
        //public string Postarea { get; set; }
        //public string Nolinksposted { get; set; }
        //public string NOViews { get; set; }
        //public string NoSchedules { get; set; }
        //public string NOApplication { get; set; }
        //public string MTDShowings { get; set; }
        //public string TotalShowings { get; set; }
        //public string MTDViews { get; set; }
        //public string TotalViews { get; set; }
        //public string TotalApplicationFees { get; set; }

    }

    public class Barchart
    {
        public string January { get; set; }
        public string February { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string August { get; set; }
        public string September { get; set; }
        public string October { get; set; }
        public string November { get; set; }
        public string December { get; set; }
        public string TOTAL { get; set; }
        public string Name { get; set; }
    }
}
