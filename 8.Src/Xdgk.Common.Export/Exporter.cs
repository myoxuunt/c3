using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace Xdgk.Common.Export
{
    public class Exporter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        static public void Export(DataGridView dgv)
        {
            string extension = "xls";
            string filename = Path.GetTempFileName(extension);

            Export(filename, dgv, DataFormatterCollection.DefaultDataFormatterCollection, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        static public void Export(string filename, 
            DataGridView dgv, 
            DataFormatterCollection dataFormatters, 
            bool isOpenFile)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException("dgv");
            }

            DataGridViewExcelExporter ee = new DataGridViewExcelExporter(dgv, null, dataFormatters);
            ee.Export();
            ee.Save(filename);

            if (isOpenFile)
            {
                ProcessStartInfo si = new ProcessStartInfo(filename);
                si.ErrorDialog = true;

                Process process = new Process();
                process.StartInfo = si;
                try
                {
                    process.Start();
                }
                catch (Exception ex)
                {
                    NUnit.UiKit.UserMessage.DisplayFailure(ex.Message);
                }
                process.Dispose();
            }
        }
    }

}