using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Enums
{
    public enum RiskRating
    {
        [Description("Red")]
        HighRisk,
        [Description("Yellow")]
        MediumRisk,
        [Description("Green")]
        LowRisk,
    }
}
