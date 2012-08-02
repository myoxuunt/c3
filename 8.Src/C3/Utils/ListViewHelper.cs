
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C3.Communi;


namespace C3
{
    public class ListViewHelper
    {
        private ListViewHelper()
        {

        }

#region GetSelectedIndex
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        static public int GetSelectedIndex(ListView lv)
        {
            int r = -1;
            if (lv.SelectedIndices.Count > 0)
            {
                r = lv.SelectedIndices[0];
            }
            return r;
        }
#endregion //GetSelectedIndex

#region SetSelectedIndex
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        static public bool SetSelectedIndex(ListView lv, int index)
        {
            bool r = false;
            if (index >= 0 && index < lv.Items.Count)
            {
                lv.SelectedIndices.Add(index);
                r = true;
            }
            return r;
        }
#endregion //SetSelectedIndex

    }

}
