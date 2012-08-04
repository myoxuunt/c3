using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;

namespace C3.Communi
{
    #region 
    public class DeviceInfoAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
            set { _propertyInfo = value; }
        } private PropertyInfo _propertyInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        public DeviceInfoAttribute(string name, int orderNumber)
            : this(name, null, orderNumber, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="format"></param>
        public DeviceInfoAttribute(string name, int orderNumber, string format)
            : this(name, null, orderNumber, format)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="orderNumber"></param>
        /// <param name="format"></param>
        public DeviceInfoAttribute(string name, string description, int orderNumber, string format)
        {
            this.Name = name;
            this.Description = description;
            this.OrderNumber = orderNumber;
            this.Format = format;
        }

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region Description
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Empty;
                }
                return _description;
            }
            set
            {
                _description = value;
            }
        } private string _description;
        #endregion //Description

        #region OrderNumber
        /// <summary>
        /// 
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                _orderNumber = value;
            }
        } private int _orderNumber;
        #endregion //OrderNumber

        #region Format
        /// <summary>
        /// 
        /// </summary>
        public string Format
        {
            get
            {
                if (_format == null)
                {
                    _format = string.Empty;
                }
                return _format;
            }
            set
            {
                _format = value;
            }
        } private string _format;
        #endregion //Format
        
    }
    #endregion //

    public interface IOrderNumber
    {
        int OrderNumber {get;set;}
    }

    public class OrderNumberCollection<T> : Xdgk.Common.Collection<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public void Sort()
        {
            if (this.Count > 1)
            {
                T[] array = new T[this.Count];
                this.CopyTo(array, 0);

                Array.Sort(array, new Comparer());
                this.Clear();

                foreach (T item in array)
                {
                    this.Add(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Comparer : System.Collections.IComparer 
        {
            #region IComparer 成员

            public int Compare(object x, object y)
            {
                if (x == null)
                {
                    throw  new ArgumentNullException("x");
                }

                if (y == null)
                {
                    throw new ArgumentNullException("y");
                }

                IOrderNumber x1 = x as IOrderNumber;
                IOrderNumber y1 = y as IOrderNumber;

                if (x1 == null)
                {
                    throw new ArgumentException("x is not IOrderNumber");
                }

                if (y1 == null)
                {
                    throw new ArgumentException("y is not IOrderNumber");
                }

                return x1.OrderNumber - y1.OrderNumber;
            }

            #endregion
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceInfoAttributeCollection : Xdgk.Common.Collection<DeviceInfoAttribute>
    {
        public void Sort()
        { 
            DeviceInfoAttribute[] array = new DeviceInfoAttribute[this.Count];
            this.CopyTo(array, 0);

            Array.Sort(array, new Comparer());

            this.Clear();

            foreach (DeviceInfoAttribute item in array)
            {
                this.Add(item);
            }
        }

        #region Comparer
        /// <summary>
        /// 
        /// </summary>
        private class Comparer : IComparer<DeviceInfoAttribute >
        {


            public int Compare(DeviceInfoAttribute x, DeviceInfoAttribute y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException("x == null or y == null");
                }
                int r = x.OrderNumber - y.OrderNumber;
                if (r == 0)
                {
                    r = string.Compare(x.Name, y.Name);
                }
                return r;
            }
        }
        #endregion //Comparer
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceUI
    {
        //IStation Station { get; }
        //IDevice Device { get; }
        DialogResult Add(Type deviceType, IStation station, out IDevice newDevice);
        DialogResult Edit(IDevice device);

        // TODO: need delete(...)?
        //
        // void Delete(Device);
    }

    abstract public class DeviceUIBase : IDeviceUI
    {

        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get { return _device; }
            protected set { _device = value; }
        } private IDevice _device;

        #region


        public DialogResult Add(Type deviceType, IStation station, out IDevice newDevice)
        {
            if (deviceType == null)
            {
                throw new ArgumentNullException("deviceType");
            }

            if (station == null)
            {
                throw new ArgumentNullException("station");
            }

            return OnAdd(deviceType, station, out newDevice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="newDevice"></param>
        /// <returns></returns>
        abstract protected DialogResult OnAdd(Type deviceType, IStation station, out IDevice newDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public DialogResult Edit(IDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }
            return OnEdit(device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        abstract protected DialogResult OnEdit(IDevice device);

        #endregion
    }

    public class DeviceUI : DeviceUIBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="newDevice"></param>
        /// <returns></returns>
        protected override DialogResult OnAdd(Type deviceType, IStation station, out IDevice newDevice)
        {
            return FrmDeviceUI.Add(deviceType, station, out newDevice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        protected override DialogResult OnEdit(IDevice device)
        {
            return FrmDeviceUI.Edit(device);
        }
    }
}
