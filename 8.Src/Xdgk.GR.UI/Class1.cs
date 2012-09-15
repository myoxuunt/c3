using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
namespace Xdgk.GR.UI
{
<<<<<<< HEAD:8.Src/Xdgk.GR.UI/Class1.cs
  
=======
    public interface IStationSource
    {
        Guid Guid { get; set; }
    }

    public interface IStationSourceProvider
    {
        SourceConfigCollection SourceConfigs { get; set; }
        IStationSource[] GetStationSources();
    }

    public interface IStation
    {
        string Name { get; set; }
        DeviceCollection Devices { get; set; }
        ICommuniPort CommuniPort { get; set; }
        ICommuniPortConfig CommuniPortConfig{ get; set; }
        IStationSource StationSource { get; set; }
        StationCollection Stations { get; set; }
    }

    public interface IStationPersister
    {
        void Add(IStation station);
        void Update(IStation station);
        void Delete(IStation station);
    }

    public interface IStationFactory
    {
        IStation Create(IStationSource stationSource);
    }

    public class StationCollection : Xdgk.Common.Collection<IStation>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, IStation item)
        {
            if (this.CheckExist(item.Name))
            {
                throw new ArgumentException("Exist Station" + item.Name);
            }
            base.InsertItem(index, item);
            item.Stations = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IStation Find(Guid guid)
        {
            IStation r = null;
            foreach (IStation station in this)
            {
                IStationSource stationSource = station.StationSource;
                if (stationSource.Guid == guid)
                {
                    r = station;
                    break;
                }
            }

            return r;
        }
        #region CheckExist
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public bool CheckExist(string stationName)
        {
            return ExistName(stationName, null);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public bool ExistName(string name, IStation ignore)
        {
            name = name.Trim();
            foreach (IStation st in this)
            {
                if (string.Compare(st.Name, name, true) == 0 &&
                    st != ignore)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public interface ISPU
    {
        Type StationType {get;set;}
        IStationFactory StationFactory { get; set; }
        IStationPersister StationPersister { get; set; }
        IStationSourceProvider StationSourceProvider { get; set; }
    }

    public class ISPUCollection : Collection<ISPU>
    {
    }
>>>>>>> parent of b869a63... split class:8.Src/C3.Communi/C3.Communi/StationInterface.cs
}
