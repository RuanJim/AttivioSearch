// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearchVisualFactory.cs" company="PerkinElmer Inc.">
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
    internal sealed class AttivioSearchVisualFactory : CustomVisualFactory<AttivioSearchVisual>
    {
        internal AttivioSearchVisualFactory()
            : base(AttivioSearchVisualIdentifiers.AttivioSearch, VisualCategory.Visualization, Properties.Resources.Attivio, null)
        {
        }
    }
}
