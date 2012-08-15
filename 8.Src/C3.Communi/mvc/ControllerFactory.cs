
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class ControllerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public IController Create(IParameter p)
        {
            IController c = null;

            if (p is StringParameter)
            {
                c = new StringParameterController((StringParameter)p);
            }
            else if (p is CommuniPortConfigParameter)
            {
                CommuniPortConfigParameter p2 = (CommuniPortConfigParameter)p;
                c = new CommuniPortConfigController(p2);
            }
            else if (p is NumberParameter)
            {
                NumberParameter numP = (NumberParameter)p;
                c = new NumberParameterController(numP);
            }
            if (c == null)
            {
                throw new ArgumentException(p.ToString());
            }
            return c;
        }

        static public IController Create(IGroup g)
        {
            GroupController c = new GroupController();
            c.Model = g;
            return c;
        }
    }

}
