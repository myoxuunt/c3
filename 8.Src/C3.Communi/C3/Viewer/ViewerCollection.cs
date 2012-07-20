using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewerCollection : Xdgk.Common.Collection<ViewerBase>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="panel"></param>
        /// <returns></returns>
        public ViewerBase Find(Type type, Panel panel)
        {
            ViewerBase result = null;
            foreach (ViewerBase item in this)
            {
                if (item.GetType() == type)
                {
                    result = item;
                    break;
                }
            }

            if (result == null)
            {
                result = CreateViewer(type, panel);
                this.Add(result);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="panel"></param>
        /// <returns></returns>
        private ViewerBase CreateViewer(Type type, Panel panel)
        {
            ViewerBase v = null;
            if (type == typeof(StationViewer))
            {
                v = new StationViewer(panel);
            }
            else if (type == typeof(DeviceViewer))
            {
                v = new DeviceViewer(panel);
            }
            else
            {
                throw new ArgumentException(string.Format("cannot create view by '{0}'", type.Name));
            }
            return v;
        }
    }

}
