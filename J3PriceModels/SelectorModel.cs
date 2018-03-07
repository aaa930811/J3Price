using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3PriceModels
{
    public class SelectorModel
    {
        public SelectorModel()
        {
            RegionModel _regionModel = new RegionModel();
            ServiceModel _serviceModel = new ServiceModel();
            this.regionModel = _regionModel;
            this.serviceModel = _serviceModel;
        }
        public RegionModel regionModel { get; set; }
        public ServiceModel serviceModel { get; set; }

    }

    public class RegionModel
    {
        public int region_id { get; set; }
        public string region_name { get; set; }
    }

    public class ServiceModel
    {
        public int service_id { get; set; }
        public int? region_id { get; set; }
        public string service_name { get; set; }
        public string service_nickname { get; set; }
    }
}
