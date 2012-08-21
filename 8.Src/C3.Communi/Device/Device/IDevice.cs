
using System;
using Xdgk.Common;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDevice : ITag 
    {
        UInt64 Address { get; set; }

        string Name { get; set; }
        DeviceType DeviceType { get; set; }
        string Text { get; }

        IStation Station { get; set; }

        //IDeviceData LastData { get; set; }
        //DeviceDataCollection DeviceDatas { get; }
        //event EventHandler LastDataChanged;

        DeviceDataManager DeviceDataManager { get; }

        IDeviceSource DeviceSource { get; set; }

        //TaskQueue Tasks { get; set; }
        //ITask Current { get; set; }
        TaskManager TaskManager { get; }

        IDPU Dpu { get; set; }

        Guid Guid { get; set; }
        Guid StationGuid { get; set; }
        CommuniDetailCollection CommuniDetails { get; set; }

        //ReportItemCollection GetDeviceInfos();
        //ParameterCollection Parameters { get; /*set;*/ }
        GroupCollection Groups { get; }


        object GetLazyDataFieldValue(string name);
        byte[] ProcessUpload(byte[] bs);

        PickerCollection Pickers { get; set; }
    }

}
