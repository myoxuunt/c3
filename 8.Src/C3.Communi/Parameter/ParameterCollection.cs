using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;

namespace C3.Communi
{
    public class ParameterCollection : OrderNumberCollection<IParameter>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public IParameter this[string parameterName]
        {
            get
            {
                VerifyParameterName(parameterName);

                int index = this.Find(parameterName);
                if (index != -1)
                {
                    return this[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                VerifyParameterName(parameterName);

                int index = this.Find(parameterName);
                if (index != -1)
                {
                    this.SetItem(index, value);
                }
                else
                {
                    this.Add(value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        private void VerifyParameterName(string parameterName)
        {
            if (parameterName == null || parameterName.Trim().Length == 0)
            {
                throw new ArgumentException("parameterName is null or empty");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        private int Find(string parameterName)
        {
            int index = -1;
            for (int i = 0; i < this.Count; i++)
            {
                IParameter item = this[i];
                if (StringHelper.Equal(item.Name, parameterName))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }

}
