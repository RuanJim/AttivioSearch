// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttivioSearchView.cs" company="PerkinElmer Inc.">
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
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Spotfire.Dxp.Application;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Data.Import;

#endregion

namespace Com.PerkinElmer.Service.AttivioSearch
{
    public sealed class AttivioSearchView : CustomVisualView<AttivioSearch>
    {
        #region Constructors and Destructors

        public AttivioSearchView(AttivioSearch model)
            : base(model)
        {
            AddEventHandler(Render, model.GetRenderTrigger());
        }

        #endregion

        #region Constants and Fields

        private static readonly string DataFile = @"C:\attiviotmp\attivio.csv";
        private static List<string[]> data = new List<string[]>();

        #endregion

        #region Methods

        protected override HttpContent GetResourceCore(string path, NameValueCollection query, AttivioSearch snapshotNode)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "Attivio.html";

                StreamWriter writer = new StreamWriter(File.Open(DataFile, FileMode.Create), Encoding.UTF8);
                writer.Write("PlaceHolder");
                writer.Close();
            }

            var bytes = GetEmbeddedResource("SpotfireDeveloper.CustomVisualsExample.webroot." + path);
            var mimeType = ResolveContentType("SpotfireDeveloper.CustomVisualsExample.webroot." + path);

            // To get quick turn around time when developing your custom visual view you can for instance read the resource contents 
            // directly for the disk folder where you keep your source code. You can then edit the resource (html, .js, etc.) and then
            // press Control+Alt+Shift+F5 in spotfire to reload the UI and make your changes take effect.
            //if (path == "Search.html")
            //{
            //    var pathOnDisk = @"H:\Softwares\Spotfire 7.11\TIB_sfire_dev_7.11.0_win\SDK\Examples\Extensions\SpotfireDeveloper.CustomVisualsExample\webroot\Search.html";
            //    bytes = File.ReadAllBytes(pathOnDisk);
            //}

            return new HttpContent(mimeType, bytes);
        }

        protected override void ModifyCore(string method, string args, AttivioSearch liveNode)
        {
            string[] rows = args.Split(new char[] { ',' });

            data.Add(rows);

            var max = 0;

            foreach (string[] rs in data)
            {
                if (rs.Length > max)
                {
                    max = rs.Length;
                }
            }
            
            StreamWriter writer = new StreamWriter(File.Open(DataFile, FileMode.Create), Encoding.UTF8);

            for (var i = 0; i < max; i++)
            {
                List<string> line = new List<string>();

                foreach (string[] cell in data)
                {
                    if (i < cell.Length)
                    {
                        line.Add(cell[i].Replace(";", "；").Replace(",", "，"));
                    }
                    else
                    {
                        line.Add(string.Empty);
                    }
                }

                string lineString = String.Join(",", line.ToArray());
                writer.WriteLine(lineString);
            }

            writer.Close();

            TextFileDataSource dataSource = new TextFileDataSource(File.OpenRead(DataFile));

            var document = liveNode.Visual.Context.GetService<Document>();

            document.ActiveDataTableReference.ReplaceData(dataSource);
        }

        protected override string ReadCore(string method, string args, AttivioSearch snapshotNode)
        {
            return string.Empty;
        }

        protected override void OnUpdateRequiredCore()
        {
            Render();
        }

        private void Render()
        {
        }

        #endregion
    }
}