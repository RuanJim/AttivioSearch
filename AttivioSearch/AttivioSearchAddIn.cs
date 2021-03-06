// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearchAddIn.cs" company="PerkinElmer Inc.">
//   Copyright (c) 2013 PerkinElmer Inc.,
//     940 Winter Street, Waltham, MA 02451.
//     All rights reserved.
//     This software is the confidential and proprietary information
//     of PerkinElmer Inc. ("Confidential Information"). You shall not
//     disclose such Confidential Information and may not use it in any way,
//     absent an express written license agreement between you and PerkinElmer Inc.
//     that authorizes such use.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Framework.Preferences;

#endregion

namespace Com.PerkinElmer.Service.AttivioSearch
{
    /// <summary>
    /// </summary>
    public sealed class AttivioSearchAddIn : AddIn
    {
        protected override void RegisterVisuals(VisualRegistrar registrar)
        {
            base.RegisterVisuals(registrar);

            registrar.Register(new AttivioSearchVisualFactory());
        }

        protected override void RegisterViews(ViewRegistrar registrar)
        {
            base.RegisterViews(registrar);

            registrar.Register(typeof(CustomVisualView), typeof(AttivioSearchVisual), typeof(AttivioSearchVisualView));
        }

        protected override void RegisterPreferences(PreferenceRegistrar registrar)
        {
            base.RegisterPreferences(registrar);

            registrar.Register<AttivioSearchPreference>();
        }

        protected override void OnAnalysisServicesRegistered(ServiceProvider serviceProvider)
        {
            base.OnAnalysisServicesRegistered(serviceProvider);

            AttivioServerUrl = serviceProvider.GetService<PreferenceManager>().GetPreference<AttivioSearchPreference>().AttivioServerUrl;
            DataFile = System.IO.Path.GetTempFileName();
        }

        public static string AttivioServerUrl = string.Empty;
        public static string DataFile = string.Empty;
    }
}
