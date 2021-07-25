using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rpm.Mailing;
using Rpm.Various;

namespace Rpm.Productie
{
    public interface IQueue
    {
        bool IsPrioritize { get; }
        bool ReQueue { get; }
        int ID { get; }
        public RemoteMessage[] Results { get; set; }

        /// <summary>
        ///     Dont use async
        /// </summary>
        /// <returns></returns>
        Task DoWork();

        bool CheckEquals(IQueue queue);

        void Cancel();
    }

    public static class EventHandlerExtensions
    {
        public static void SafeInvoke<T>(RunInstanceCompleteHandler evt, T e) where T : IQueue
        {
            evt?.Invoke(e);
        }
    }

    public class TaskQueues
    {
        private readonly List<IQueue> Queues = new();
        private readonly List<IQueue> Runnings = new();
        private readonly object someEventLock = new();

        private bool _IsStart = true;

        private int _MaxRun = 1;

        private EventHandler<EventArgs> _RunInstanceComplete;

        public int MaxRun
        {
            get => _MaxRun;
            set
            {
                var flag = value > _MaxRun;
                _MaxRun = value;
                if (flag && Queues.Count != 0) RunNewQueue();
            }
        }

        public bool RunRandom { get; set; } = false;

        public bool IsStart
        {
            get => _IsStart;
            set
            {
                _IsStart = value;
                if (value) RunNewQueue();
            }
        }

        public event RunComplete OnRunComplete;

        public event EventHandler<EventArgs> RunInstanceComplete
        {
            add
            {
                lock (someEventLock)
                {
                    _RunInstanceComplete += value;
                }
            }

            remove
            {
                lock (someEventLock)
                {
                    _RunInstanceComplete -= value;
                }
            }
        }

        protected virtual void OnRunInstanceComplete(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler;

            lock (someEventLock)
            {
                handler = _RunInstanceComplete;
            }

            handler?.Invoke(sender, e);
        }

        public void RunComplete()
        {
            OnRunComplete?.Invoke();
        }

        public void OnRunInstanceComplete(object sender)
        {
            OnRunInstanceComplete(sender, EventArgs.Empty);
        }

        //need lock Queues first
        private void StartQueue(IQueue queue)
        {
            if (null != queue)
            {
                Queues.Remove(queue);
                lock (Runnings)
                {
                    Runnings.Add(queue);
                }

                queue.DoWork().ContinueWith(ContinueTaskResult, queue);
            }
        }

        private void RunNewQueue()
        {
            if (!IsStart) return;
            lock (Queues) //Prioritize
            {
                foreach (var q in Queues.Where(x => x.IsPrioritize)) StartQueue(q);
            }

            if (Runnings.Count >= MaxRun) return; //other

            if (Queues.Count == 0)
            {
                if (Runnings.Count == 0) OnRunComplete?.Invoke(); //on completed
                else return;
            }
            else
            {
                lock (Queues)
                {
                    IQueue queue;
                    if (RunRandom) queue = Queues.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    else queue = Queues.FirstOrDefault();
                    StartQueue(queue);
                }

                if (Queues.Count > 0 && Runnings.Count < MaxRun) RunNewQueue();
            }
        }

        private void ContinueTaskResult(Task Result, object queue_obj)
        {
            var queue = queue_obj as IQueue;
            lock (Queues)
            {
                OnRunInstanceComplete(queue);
            }

            lock (Runnings)
            {
                Runnings.Remove(queue);
            }

            if (queue.ReQueue)
                lock (Queues)
                {
                    Queues.Add(queue);
                }

            RunNewQueue();
        }

        public void Add(IQueue queue)
        {
            if (null == queue) throw new ArgumentNullException(nameof(queue));
            lock (Queues)
            {
                Queues.Add(queue);
            }

            RunNewQueue();
        }

        public void Cancel(IQueue queue)
        {
            if (null == queue) throw new ArgumentNullException(nameof(queue));
            lock (Queues)
            {
                Queues.RemoveAll(o => o.CheckEquals(queue));
            }

            lock (Runnings)
            {
                Runnings.ForEach(o =>
                {
                    if (o.CheckEquals(queue)) o.Cancel();
                });
            }
        }

        public void Reset(IQueue queue)
        {
            if (null == queue) throw new ArgumentNullException(nameof(queue));
            Cancel(queue);
            Add(queue);
        }

        public void ShutDown()
        {
            MaxRun = 0;
            lock (Queues)
            {
                Queues.Clear();
            }

            lock (Runnings)
            {
                Runnings.ForEach(o => o.Cancel());
            }
        }
    }
}