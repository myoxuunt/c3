
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;


namespace Xdgk.UI.Forms
{
    public class FormHelper
    {
        #region ShowAndActiveFluxQuery
        static public void ShowAndActiveFluxQuery(Form parentForm, Type typeOfForm)
        {
            Form f = GetOrCreateForm(parentForm, typeOfForm);
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            f.Activate();
        }
        #endregion //ShowAndActiveFluxQuery

        #region GetOrCreateForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formType"></param>
        /// <returns></returns>
        static private Form GetOrCreateForm(Form parentform, Type formType)
        {
            Form r = null;
            foreach (Form f in parentform.MdiChildren)
            {
                if (f.GetType() == formType)
                {
                    r = f;
                    break;
                }
            }

            if (r == null)
            {
                r = (Form)Activator.CreateInstance(formType);
                r.MdiParent = parentform;
            }
            return r;
        }
        #endregion //GetOrCreateForm
    }

}
