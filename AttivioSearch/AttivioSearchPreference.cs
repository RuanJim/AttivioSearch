using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Framework.Persistence;
using Spotfire.Dxp.Framework.Preferences;

namespace Com.PerkinElmer.Service.AttivioSearch
{
    [Serializable]
    [PersistenceVersion(1, 0)]
    public class AttivioSearchPreference : CustomPreference
    {
        private readonly PreferenceProperty<string> _attivioServerUrl;

        public AttivioSearchPreference()
        {
            _attivioServerUrl = AddPreference(new PreferenceProperty<string>(
                "AttivioServerUrl",
                "1.0",
                PreferencePersistenceScope.Server, 
                PreferenceUsage.UserGroup
                ));
        }

        public string AttivioServerUrl
        {
            get { return _attivioServerUrl.Value; }
            set { _attivioServerUrl.Value = value; }
        }

        public override string Category => "Attivio";

        public override string SubCategory => "SearchUI Server Url";
    }
}
