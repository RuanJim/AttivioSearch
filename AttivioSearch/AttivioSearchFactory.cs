// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearchFactory.cs" company="PerkinElmer Inc.">
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

using System.Linq;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Data;

#endregion

namespace Com.PerkinElmer.Service.AttivioSearch
{
    /// <summary>
    /// The factory class for <see cref="AttivioSearch"/>.
    /// </summary>
    internal sealed class AttivioSearchFactory : CustomVisualFactory<AttivioSearch>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of class <see cref="AttivioSearchFactory"/>.
        /// </summary>
        internal AttivioSearchFactory()
            : base(
                AttivioSearchVisualsIdentifiers.CustomDonutChart, // Type identifier
                VisualCategory.Visualization, // Visual category
                null, // Icon
                null)
        {
            // License
            // Empty
        }

        #endregion

        #region Methods

        /// <summary>Override this method to configure data-dependent properties.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <remarks>The default implementation does nothing.</remarks>
        protected override void AutoConfigureCore(AttivioSearch visual)
        {
            base.AutoConfigureCore(visual);

            DataManager dataManager = visual.Context.GetService<DataManager>();

            if (visual.DataTable == null)
            {
                visual.DataTable = dataManager.Tables.DefaultTableReference;
            }

            if (string.IsNullOrEmpty(visual.ValueExpression))
            {
                if (visual.DataTable != null)
                {
                    var stringColumn = visual.DataTable.Columns.FindAll(column => column.Properties.DataType == DataType.String).FirstOrDefault();
                    var numericColumn = visual.DataTable.Columns.FindAll(column => column.Properties.DataType.IsNumeric).FirstOrDefault();

                    if (stringColumn != null && numericColumn != null)
                    {
                        visual.ValueExpression = "Avg(" + numericColumn.NameEscapedForExpression + ")";
                        visual.StringColumn = stringColumn;
                    }
                }
            }
        }

        #endregion
    }
}