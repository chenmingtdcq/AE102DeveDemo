using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineToPolygonByWidthAttribute
{
    public class BuildLineBuffer : BaseCommand
    {
        public IHookHelper m_HookHelper;
        public IMapControl3 m_MapControl;

        public BuildLineBuffer()
        {
            this.m_caption = "线转面";
        }

        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;
            if (m_HookHelper == null)
                m_HookHelper = new HookHelperClass();
            m_HookHelper.Hook = hook;
            m_MapControl = (IMapControl3)hook;
        }

        public override void OnClick()
        {
            BufferForm frm = new BufferForm(m_HookHelper);
            frm.ShowDialog();
        }

        
    }
}
