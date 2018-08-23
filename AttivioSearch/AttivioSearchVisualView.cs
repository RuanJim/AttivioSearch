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
            return base.GetResourceCore(path, query, snapshotNode);
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
