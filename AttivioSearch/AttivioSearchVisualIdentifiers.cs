using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotfire.Dxp.Application.Extension;

namespace Com.PerkinElmer.Service.AttivioSearch
{
    public sealed class AttivioSearchVisualIdentifiers : CustomTypeIdentifiers
    {
        public static readonly CustomTypeIdentifier ConfiguredBarChart = CreateTypeIdentifier(
            "Com.PerkinElmer.Service.AttivioSearch", Properties.Resources.AttivioSearchDisplayName, Properties.Resources.AttivioSearchDescription);
    }
}
