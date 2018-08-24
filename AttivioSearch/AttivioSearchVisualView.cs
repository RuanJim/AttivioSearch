using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotfire.Dxp.Application;
using Spotfire.Dxp.Application.Extension;

namespace Com.PerkinElmer.Service.AttivioSearch
{
    public sealed class AttivioSearchVisualView : CustomVisualView<AttivioSearchVisual>
    {
        public AttivioSearchVisualView(AttivioSearchVisual model) : base(model)
        {
        }

        protected override HttpContent GetResourceCore(string path, NameValueCollection query, AttivioSearchVisual snapshotNode)
        {
            return new HttpContent("text/html", @"<!DOCTYPE html>
<html style=""width: 100%; height: 100%; overflow: hidden;"">
<head>
    <script>
        window.addEventListener(""message"", function (event) {
            Spotfire.modify("""", event.data);
        }, false);
    </script>
</head>
<body style=""width: 100%; height: 100%; margin: 0px;"">
<iframe id=""att"" src=""http://192.168.20.113:8080/searchui/"" style=""width: 100%; height: 100%; border: 0px solid black;"">
    
</iframe>
</body>
</html>
");
        }

        protected override void ModifyCore(string method, string args, AttivioSearchVisual liveNode)
        {
            base.ModifyCore(method, args, liveNode);
        }

        protected override void OnUpdateRequiredCore()
        {
        }
    }
}
