using System;
using System.Xml;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace C3.Communi
{
    public class Soft
    {
        #region Members
        private Timer _timer;
        #endregion //Members

        #region Constructor
        internal Soft()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(_timer_Tick);
        }
        #endregion //Constructor

        #region SocketListenerManager
        /// <summary>
        /// 
        /// </summary>
        public SocketListenerManager SocketListenerManager
        {
            get
            {
                if (_socketListenerManager == null)
                {
                    _socketListenerManager = new SocketListenerManager(this);
                }
                return _socketListenerManager;
            }
        } private SocketListenerManager _socketListenerManager;
        #endregion //SocketListenerManager

        #region CommuniPortManager
        public CommuniPortManager CommuniPortManager
        {
            get 
            {
                if (_communiPortManager == null)
                {
                    _communiPortManager = new CommuniPortManager(this);
                }
                return _communiPortManager;
            }
        } private CommuniPortManager _communiPortManager;
        #endregion //CommuniPortManager

        #region _timer_Tick
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timer_Tick(object sender, EventArgs e)
        {
            Doit();
        }
        #endregion //_timer_Tick

        #region Doit
        /// <summary>
        /// 
        /// </summary>
        private void Doit()
        {
            foreach (IStation station in Hardware.Stations)
            {
                Do(station);
            }
        }
        #endregion //Doit

        #region Do
        private void Do(IStation station)
        {
            foreach (IDevice device in station.Devices)
            {
                Do(device);
            }
        }
        #endregion //Do

        #region Do
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        private void Do(IDevice device)
        {
            // 1. current task
            //
            ITask current = device.CurrentTask;
            if (current != null)
            {
                Do(current);
            }
            else
            {
                Do(device.Tasks);
            }
        }
        #endregion //Do

        #region Do - task
        private void Do(ITask current)
        {
            if (current.IsTimeOut())
            {
                byte[] received = GetReceived(current);
                // TOOD: log received
                //
                // log(received);

                IParseResult pr = current.Parse(received);
                ITaskProcessor processor = GetTaskProcessor(current);
                processor.Process(pr);

                IDevice device = current.Device;
                device.CurrentTask = null;

                if (!current.IsComplete)
                {
                    device.Tasks.Enqueue(current);
                }
            }
        }
        #endregion //Do

        #region Do
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskCollection"></param>
        private void Do(TaskQueue tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            TaskCollection tempTasks = new TaskCollection();
            //IDevice device = tasks;

            while (tasks.Count > 0)
            {

                ITask head = tasks.Dequeue();

                DateTime dt = DateTime.Now;
                if (head.NeedExecute(dt))
                {
                    ICommuniPort cp = GetCommuniPort(head);
                    if ((cp != null) &&
                        (!cp.IsOccupy))
                    {
                        // TODO: move to task class?
                        //

                        IDevice device =head.Device;
                        IOpera opera = head.Opera;

                        byte[] send = opera.CreateSendBytes(device);

                        // byte[] send = head.opera
                        cp.Write(send);
                        head.LastExecute = DateTime.Now;

                        device.CurrentTask = head;
                        break;
                    }
                    else
                    {
                        tempTasks.Add(head);
                    }
                }
                else
                {
                    tempTasks.Add(head);
                }
            }
            tasks.Enqueue(tempTasks);
        }
        #endregion //Do

        #region GetTaskProcessor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private ITaskProcessor GetTaskProcessor(ITask current)
        {
            IDevice device = current.Device;
            IDPU dpu = device.Dpu;
            ITaskProcessor processor = dpu.Processor;
            return processor;
        }
        #endregion //GetTaskProcessor

        #region GetReceived
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private byte[] GetReceived(ITask current)
        {
            IDevice device = current.Device;
            IStation station = device.Station;
            ICommuniPort cp = station.CommuniPort;

            byte[] received = new byte[0];
            if (cp != null)
            {
                received = cp.Read();
            }
            return received;
        }
        #endregion //GetReceived


        #region ErrorManager
        public ErrorManager ErrorManager
        {
            get 
            {
                if (_errorManager == null)
                {
                    _errorManager = new ErrorManager(this);
                }
                return _errorManager;
            }
        } private ErrorManager _errorManager;
        #endregion //

        #region GetCommuniPort
        private ICommuniPort GetCommuniPort(ITask task)
        {
            IDevice device = task.Device;
            IStation station = device.Station;
            return station.CommuniPort;
        }
        #endregion //GetCommuniPort


        #region Hardware
        /// <summary>
        /// 
        /// </summary>
        public Hardware Hardware
        {
            get
            {
                if (_hardware == null)
                {
                    HardwareFactory factory = new HardwareFactory();
                    factory.SourceConfigs = ReadSourceConfigs();
                    _hardware = factory.Create();
                }
                return _hardware;
            }
            set
            {
                _hardware = value;
            }
        } private Hardware _hardware;
        #endregion //Hardware

       
        #region SourceConfigs 
        private SourceConfigCollection SourceConfigs
        {
            get
            {
                if (_sourceConfigs == null)
                {
                    _sourceConfigs = ReadSourceConfigs();
                }
                return null;
            }
        } private SourceConfigCollection _sourceConfigs;
        #endregion //SourceConfigs

        #region ReadSourceConfigs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SourceConfigCollection ReadSourceConfigs()
        {
            SourceConfigCollection sourceConfigs = new SourceConfigCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(PathUtils.SourceConfigFileName);

            XmlNode sourcesNode = doc.SelectSingleNode("sources");
            foreach (XmlNode item in sourcesNode.ChildNodes)
            {
                XmlElement sourceNode = item as XmlElement;
                string key = sourceNode["key"].ChildNodes[0].Value;
                string value = sourceNode["value"].ChildNodes[0].Value;

                SourceConfig sourceConfig = new SourceConfig(key, value);
                sourceConfigs.Add(sourceConfig);
            }

            return sourceConfigs;
        }
        #endregion //ReadSourceConfigs
    }
}
