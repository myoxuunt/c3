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
        private Timer _timer;

        public Soft()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(_timer_Tick);
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timer_Tick(object sender, EventArgs e)
        {
            Doit();
        }

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

        private void Do(IStation station)
        {
            foreach (IDevice device in station.Devices)
            {
                Do(device);
            }
        }

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

                        IDevice device =head.Device;
                        IOpera opera = head.Opera;

                        // TODO:
                        //
                        byte[] send = opera.CreateSend(device);

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

        private ICommuniPort GetCommuniPort(ITask task)
        {
            IDevice device = task.Device;
            IStation station = device.Station;
            return station.CommuniPort;
        }


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
    }
}
