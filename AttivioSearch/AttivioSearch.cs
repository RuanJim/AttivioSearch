// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearch.cs" company="PerkinElmer Inc.">
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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Spotfire.Dxp.Application;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Data;
using Spotfire.Dxp.Data.Expressions;
using Spotfire.Dxp.Framework.DocumentModel;
using Spotfire.Dxp.Framework.Persistence;

#endregion

namespace Com.PerkinElmer.Service.AttivioSearch
{
    /// <summary>This is an example of a custom visualization.
    /// </summary>
    [Serializable]
    [PersistenceVersion(2, 0)]
    public sealed class AttivioSearch : CustomVisual
    {
        /// <summary>Contains property name constants for the public properties of <see cref="AttivioSearch"/>.
        /// </summary>
        public new abstract class PropertyNames : CustomVisual.PropertyNames
        {
            #region Constants and Fields

            /// <summary>The name of the property DataTable.</summary>
            public static readonly PropertyName DataTable = CreatePropertyName("DataTable");

            /// <summary>The name of the property ValueExpression.</summary>
            public static readonly PropertyName ValueExpression = CreatePropertyName("ValueExpression");

            /// <summary>The name of the property NameExpression.</summary>
            public static readonly PropertyName StringColumn = CreatePropertyName("StringColumn");

            /// <summary>The name of the property View.</summary>
            public static readonly PropertyName View = CreatePropertyName("View");

            #endregion
        }

        #region Constants and Fields

        /// <summary>
        /// A reference to the data table that the plot is based on.
        /// </summary>
        private readonly UndoableCrossReferenceProperty<DataTable> dataTable;

        /// <summary>
        /// The expression used to calculate the values for the plot.
        /// </summary>
        private readonly UndoableProperty<string> valueExpression;

        /// <summary>
        /// The expression used to calculate the values for the plot.
        /// </summary>
        private readonly UndoableCrossReferenceProperty<DataColumn> stringColumn;

        /// <summary>
        /// The data view that is used to calculate the values in the plot.
        /// </summary>
        private readonly UndoableProperty<PersistentDataView> view;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the AttivioSearch class.
        /// </summary>
        internal AttivioSearch()
        {
            CreateProperty(PropertyNames.DataTable, out dataTable, null);
            CreateProperty(PropertyNames.View, out view, null);
            CreateProperty(PropertyNames.ValueExpression, out valueExpression, null);
            CreateProperty(PropertyNames.StringColumn, out stringColumn, null);
        }

        /// <summary>Initializes a new instance of the AttivioSearch class.
        /// Implements ISerializable.</summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The serialization context.</param>
        private AttivioSearch(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            DeserializeProperty<PersistentDataView>(info, context, PropertyNames.View, out view);
            DeserializeProperty<DataTable>(info, context, PropertyNames.DataTable, out dataTable);
            DeserializeProperty<string>(info, context, PropertyNames.ValueExpression, out valueExpression);
            DeserializeProperty<DataColumn>(info, context, PropertyNames.StringColumn, out stringColumn);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the data table that this plot is based on.
        /// </summary>
        public DataTable DataTable
        {
            get
            {
                return dataTable.Value;
            }

            set
            {
                dataTable.Value = value;
            }
        }

        /// <summary>
        /// Gets the data view.
        /// </summary>
        public DataView View
        {
            get
            {
                if (PersistentDataView == null)
                {
                    return null;
                }

                return PersistentDataView.DataView;
            }
        }

        /// <summary>Gets or sets the value expression.
        /// </summary>
        public string ValueExpression
        {
            get
            {
                return valueExpression.Value;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The expression is null or empty.");
                }

                // Try to parse the expression to see if it is valid.
                ColumnExpression columnExpression = ColumnExpression.Create(value);

                if (!columnExpression.IsValid)
                {
                    throw new ArgumentException("The expression is not valid.");
                }

                valueExpression.Value = value;
            }
        }

        /// <summary>Gets or sets the name expression.
        /// </summary>
        public DataColumn StringColumn
        {
            get
            {
                return stringColumn.Value;
            }

            set
            {
                stringColumn.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the persistent data view.
        /// </summary>
        private PersistentDataView PersistentDataView
        {
            get
            {
                return view.Value;
            }

            set
            {
                view.Value = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds event handlers to rebuild the data view when the model changes.
        /// </summary>
        /// <param name="eventManager">The event manager.</param>
        protected override void DeclareInternalEventHandlers(InternalEventManager eventManager)
        {
            base.DeclareInternalEventHandlers(eventManager);

            // If the data table, expression, or filtering changes we need to rebuild the view.
            eventManager.AddEventHandler(
                delegate
                {
                    RebuildView();
                },
                Trigger.CreatePropertyTrigger(this, PropertyNames.DataTable, PropertyNames.ValueExpression, PropertyNames.StringColumn),
                Trigger.CreatePropertyTrigger(Context.GetAncestor<Page>(), Page.PropertyNames.ActiveFilteringSelectionReference));
        }

        /// <summary>Implements ISerializable.</summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The serialization context.</param>
        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            SerializeProperty(info, context, dataTable);
            SerializeProperty(info, context, valueExpression);
            SerializeProperty(info, context, stringColumn);
            SerializeProperty(info, context, view);
        }

        /// <summary>
        /// Override to create and return a render trigger.
        /// The render trigger is used to signal when the visualization is invalid and need to re-render.
        /// Note that it should not fire when highlight changes.
        /// The default implementation returns a <see cref="F:Spotfire.Dxp.Framework.DocumentModel.Trigger.NeverTrigger"/>.
        /// </summary>
        /// <returns>The trigger.</returns>
        /// <seealso cref="M:Spotfire.Dxp.Application.Visuals.VisualContent.GetRenderTrigger"/>
        protected override Trigger GetRenderTriggerCore()
        {
            return Trigger.CreateMutablePropertyTrigger<PersistentDataView>(
                this,
                PropertyNames.View,
                delegate(PersistentDataView persistentView)
                {
                    return Trigger.CreateCompositeTrigger(
                        Trigger.CreatePropertyTrigger(persistentView, PersistentDataView.PropertyNames.DataView),
                        Trigger.CreateSubTreeTrigger(persistentView, typeof(DataView), DataTable.PropertyNames.RowCount),
                        Trigger.CreateSubTreeTrigger(persistentView, typeof(DataColumn), DataColumn.PropertyNames.RowValues, DataColumn.PropertyNames.Hierarchy));
                });
        }

        /// <summary>Ensure we build the view when first added to the document.
        /// </summary>
        protected override void OnConfigure()
        {
            base.OnConfigure();

            RebuildView();
        }

        /// <summary>Ensure we rebuild the view when moved between pages since
        /// the filtering could have changed.
        /// </summary>
        protected override void OnReconfigure()
        {
            base.OnReconfigure();

            RebuildView();
        }

        /// <summary>
        /// Gets a value indicating whether the rendering of this visualization during print and export
        /// operations should be performed via a CustomWebView.
        /// </summary>
        protected override bool RenderUsingViewCore()
        {
            // We have not implemeted RenderCore to perform rendering for print and
            // export. Instead we rely on our AttivioSearchView to handle this.
            return true;
        }

        /// <summary>
        /// Rebuild the data view when the model changes.
        /// </summary>
        private void RebuildView()
        {
            Visual.ShowTitle = false;

            ColumnExpression valueExpr = ColumnExpression.Create(ValueExpression);
            string valueExpression = valueExpr.Expression + " AS [Value]";

            DataFilteringSelection filtering = Context.GetAncestor<Page>().ActiveFilteringSelectionReference;
            if (string.IsNullOrEmpty(valueExpression) || stringColumn == null || filtering == null || DataTable == null)
            {
                PersistentDataView = null;
                return;
            }

            try
            {
                var columns = new List<DataColumn>();
                if (StringColumn != null)
                {
                    columns.Add(StringColumn);
                }

                PersistentDataView =
                    new PersistentDataView(
                        "AttivioSearchView",
                        new string[] { valueExpression }.Concat(columns.Select(c => c.NameEscapedForExpression)).ToArray(),
                        columns.ToArray(),
                        DataTable,
                        filtering);
            }
            catch (Exception)
            {
                // cannot create view, set to null.
                PersistentDataView = null;
            }
        }

        #endregion
    }
}
