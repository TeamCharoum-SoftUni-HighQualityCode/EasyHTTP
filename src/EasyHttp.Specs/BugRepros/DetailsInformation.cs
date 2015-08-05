using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyHttp.Specs.BugRepros
{
    public class DetailsInformation
    {
        public string Name { get; set; }

        public string FormattedAddress { get; set; }
        public string FormattedPhoneNumber { get; set; }

        public string Icon { get; set; }
        public string Id { get; set; }
        public AddressComponent[] AddressComponents { get; set; }

        public PlaceGeometry Geometry { get; set; }
        public string Reference { get; set; }
        public string[] Types { get; set; }
        public string Url { get; set; }
        public string Vicinity { get; set; }
        public string Website { get; set; }
    }
}
