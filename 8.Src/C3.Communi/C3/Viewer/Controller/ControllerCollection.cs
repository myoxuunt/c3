using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class ControllerCollection : Xdgk.Common.Collection<Controller>
    {
        public Controller Find(Type controllerType)
        {
            foreach (Controller item in this)
            {
                if (item.GetType() == controllerType)
                {
                    return item;
                }
            }

            throw new ArgumentException("not find controller by type: " + controllerType.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="panel"></param>
        /// <returns></returns>
        public Controller Find(Type type, Panel panel)
        {
            Controller result = null;
            foreach (Controller item in this)
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
        private Controller CreateViewer(Type type, Panel panel)
        {
            Controller v = null;
            if (type == typeof(StationController))
            {
                //v = new StationController(panel);
            }
            else if (type == typeof(DeviceController))
            {
                //v = new DeviceController(panel);
            }
            else
            {
                throw new ArgumentException(string.Format("cannot create view by '{0}'", type.Name));
            }
            return v;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public void OnlyShow(Controller controller)
        {
            foreach (Controller item in this)
            {
                item.View.ViewControl.Visible = false;
            }

            controller.View.ViewControl.Visible = true;
        }
    }

}
