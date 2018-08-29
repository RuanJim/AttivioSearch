// --------------------------------------------------------------------------------------------------------------------
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
        private readonly UndoableProperty<string> attivioServerUrl;

        public string AttivioServerUrl
        {
            get { return this.attivioServerUrl.Value; }
            set { this.attivioServerUrl.Value = value; }
        }

        internal AttivioSearchVisual()
        {
            this.CreateProperty(PropertyNames.AttivioServerUrl, out this.attivioServerUrl, "http://192.168.20.113:8080/searchui/");
        }

        private AttivioSearchVisual(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.DeserializeProperty<string>(info, context, PropertyNames.AttivioServerUrl, out this.attivioServerUrl);
        }

        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            this.SerializeProperty(info, context, this.attivioServerUrl);
        }

        protected override bool RenderUsingViewCore()
        {
            return true;
        }

        public new abstract class PropertyNames : CustomVisual.PropertyNames
        {
            public static readonly PropertyName AttivioServerUrl = CreatePropertyName("AttivioServerUrl");
        }
    }
}
