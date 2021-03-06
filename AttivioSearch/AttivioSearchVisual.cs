﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearchVisual.cs" company="PerkinElmer Inc.">
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

using System;
using System.Runtime.Serialization;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Data;
using Spotfire.Dxp.Framework.DocumentModel;
using Spotfire.Dxp.Framework.Persistence;

#endregion

namespace Com.PerkinElmer.Service.AttivioSearch
{
    [Serializable]
    [PersistenceVersion(2, 0)]
    public sealed class AttivioSearchVisual : CustomVisual
    {
        internal AttivioSearchVisual()
        {
        }

        private AttivioSearchVisual(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected override bool RenderUsingViewCore()
        {
            return true;
        }
    }
}
