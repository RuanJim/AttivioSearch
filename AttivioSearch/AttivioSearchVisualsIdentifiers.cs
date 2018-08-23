// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearchVisualsIdentifiers.cs" company="PerkinElmer Inc.">
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

#endregion

namespace Com.PerkinElmer.Service.AttivioSearch
{
    /// <summary>
    /// Type identifiers for the custom visuals in this add-in.
    /// </summary>
    public sealed class AttivioSearchVisualsIdentifiers : CustomTypeIdentifiers
    {
        #region Constants and Fields

        /// <summary>
        /// Type identifier for the custom donut chart.
        /// </summary>
        public static readonly CustomTypeIdentifier CustomDonutChart = CreateTypeIdentifier(
            "SpotfireDeveloper.CustomDonutChart", "AttivioSearch", "");

        #endregion
    }
}
