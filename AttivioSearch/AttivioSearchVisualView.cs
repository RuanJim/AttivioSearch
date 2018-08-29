﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotfire.Dxp.Application;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Data.Import;

namespace Com.PerkinElmer.Service.AttivioSearch
{
    public sealed class AttivioSearchVisualView : CustomVisualView<AttivioSearchVisual>
    {
        private string dataFile;
        private List<string[]> data = new List<string[]>();

        public AttivioSearchVisualView(AttivioSearchVisual model) : base(model)
        {
        }

        protected override HttpContent GetResourceCore(string path, NameValueCollection query, AttivioSearchVisual snapshotNode)
        {
            var bytes = GetEmbeddedResource("Com.PerkinElmer.Service.AttivioSearch.web.Attivio.html");
            var mimeType = ResolveContentType("Com.PerkinElmer.Service.AttivioSearch.web.Attivio.html");

            dataFile = Path.GetTempFileName();

            string html = Encoding.UTF8.GetString(bytes).Replace("##ATTIVIO_SEARCHUI##", "http://192.168.20.113:8080/searchui/");

            return new HttpContent(mimeType, html);
        }

        protected override void ModifyCore(string method, string args, AttivioSearchVisual liveNode)
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

            StreamWriter writer = new StreamWriter(File.Open(dataFile, FileMode.Create), Encoding.UTF8);

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

            TextFileDataSource dataSource = new TextFileDataSource(File.OpenRead(dataFile));

            var document = liveNode.Visual.Context.GetService<Document>();

            document.ActiveDataTableReference.ReplaceData(dataSource);
        }

        protected override void OnUpdateRequiredCore()
        {
        }
    }
}
