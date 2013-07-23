
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common.Export
{
    public class LinesAttache : Attache 
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Lines
        {
            get { return _lines; }
        } private List<string> _lines = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xls"></param>
        public override void Add(DataGridViewExcelExporter ee)
        {
            int row = ee.CurrentRow;
            foreach( string line in this.Lines )
            {
                ee.XlsFile.SetCellValue(row++, 1, line);
            }
        }
    }

}
