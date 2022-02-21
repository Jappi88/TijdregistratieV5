using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rpm.Mailing;
using Rpm.Various;

namespace Rpm.Misc
{
    /// <summary>
    ///     Een rij taak dat bevat de functionaliteit info
    /// </summary>
    public interface IQueue
    {
        /// <summary>
        ///     prioriteren van de taak
        /// </summary>
        bool IsPrioritize { get; }

        /// <summary>
        ///     Blijf de taak herhalen als die klaar is
        /// </summary>
        bool ReQueue { get; }

        /// <summary>
        ///     De Id van de taak
        /// </summary>
        int Id { get; }

        /// <summary>
        ///     Resultaat berichten van de uitgevoerde taak
        /// </summary>
        public List<RemoteMessage> Results { get; set; }

        /// <summary>
        ///     Dont use async
        /// </summary>
        /// <returns></returns>
        Task DoWork();

        /// <summary>
        ///     Controleer of de rij hetzelfde is als deze
        /// </summary>
        /// <param name="queue">De rij waarvan vergeleken moet worden</param>
        /// <returns>True als de rij hetzelfde is</returns>
        bool CheckEquals(IQueue queue);

        /// <summary>
        ///     Annuleer de taak
        /// </summary>
        void Cancel();
    }

    /// <summary>
    ///     Een extensie om een event veilig te kunnen oproepen
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        ///     Veilig oproepen
        /// </summary>
        /// <param name="evt">De event die veilig opgeroepen moet worden</param>
        /// <param name="e">De argument wat doorgegeven moet worden door de event</param>
        /// <typeparam name="T">De type van de argument</typeparam>
        public static void SafeInvoke<T>(RunInstanceCompleteHandler evt, T e) where T : IQueue
        {
            evt?.Invoke(e);
        }
    }

    /// <summary>
    ///     Een taken rij beheerder
    /// </summary>
    public class TaskQueues
    {
        private readonly List<IQueue> _queues = new();
        private readonly List<IQueue> _runnings = new();
        private readonly object _someEventLock = new();

        private bool _isStart = true;

        private int _maxRun = 1;

        private EventHandler<EventArgs> _runInstanceComplete;

        /// <summary>
        ///     De maximaal aantal taken die je kan uitvoeren
        /// </summary>
        public int MaxRun
        {
            get => _maxRun;
            set
            {
                var flag = value > _maxRun;
                _maxRun = value;
                lock (_queues)
                {
                    if (flag && _queues.Count != 0) RunNewQueue();
                }
            }
        }

        /// <summary>
        ///     Voer taken door elkaar uit
        /// </summary>
        public bool RunRandom { get; set; } = false;

        /// <summary>
        ///     Of de taken moeten worden uitgevoerd.
        /// </summary>
        public bool IsStart
        {
            get => _isStart;
            set
            {
                _isStart = value;
                if (value) RunNewQueue();
            }
        }

        /// <summary>
        ///     Een event voor als alle taken op zijn
        /// </summary>
        public event RunComplete OnRunComplete;

        /// <summary>
        ///     Een event voor als een taak klaar is
        /// </summary>
        public event EventHandler<EventArgs> RunInstanceComplete
        {
            add
            {
                lock (_someEventLock)
                {
                    _runInstanceComplete += value;
                }
            }

            remove
            {
                lock (_someEventLock)
                {
                    _runInstanceComplete -= value;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnRunInstanceComplete(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler;

            lock (_someEventLock)
            {
                handler = _runInstanceComplete;
            }

            handler?.Invoke(sender, e);
        }

        /// <summary>
        ///     Geef aan dat alle taken gereed zijn
        /// </summary>
        public void RunComplete()
        {
            OnRunComplete?.Invoke();
        }

        /// <summary>
        ///     Geef aan dat een bepaalde taak klaar is
        /// </summary>
        /// <param name="sender">De taak die klaar is</param>
        public void OnRunInstanceComplete(object sender)
        {
            OnRunInstanceComplete(sender, EventArgs.Empty);
        }

        //need lock Queues first
        private void StartQueue(IQueue queue)
        {
            if (null != queue)
            {
                _queues.Remove(queue);
                lock (_runnings)
                {
                    _runnings.Add(queue);
                }

                queue.DoWork().ContinueWith(ContinueTaskResult, queue);
            }
        }

        private void RunNewQueue()
        {
            if (!IsStart) return;
            lock (_queues) //Prioritize
            {
                foreach (var q in _queues.Where(x => x.IsPrioritize)) StartQueue(q);
            }

            lock (_runnings)
            {
                if (_runnings.Count >= MaxRun) return; //other
            }

            lock (_queues)
            {
                if (_queues.Count == 0)
                {
                    lock (_runnings)
                    {
                        if (_runnings.Count == 0) RunComplete(); //on completed
                    }
                }
                else
                {
                    IQueue queue;
                    if (RunRandom) queue = _queues.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
                    else queue = _queues.FirstOrDefault();
                    StartQueue(queue);

                    lock (_runnings)
                    {
                        if (_queues.Count > 0 && _runnings.Count < MaxRun) RunNewQueue();
                    }
                }
            }
        }

        private void ContinueTaskResult(Task result, object queueObj)
        {
            var queue = queueObj as IQueue;
            lock (_queues)
            {
                OnRunInstanceComplete(queue);
            }

            lock (_runnings)
            {
                _runnings.Remove(queue);
            }

            if (queue is {ReQueue: true})
                lock (_queues)
                {
                    _queues.Add(queue);
                }

            RunNewQueue();
        }

        /// <summary>
        ///     Voeg een nieuwe taak toe
        /// </summary>
        /// <param name="queue">Een taak als ITaskQueue</param>
        /// <exception cref="ArgumentNullException">Geeft een fout als de taak niet wordt gegeven</exception>
        public void Add(IQueue queue)
        {
            if (null == queue) throw new ArgumentNullException(nameof(queue));
            lock (_queues)
            {
                _queues.Add(queue);
            }

            RunNewQueue();
        }

        /// <summary>
        ///     Annuleer een taak
        /// </summary>
        /// <param name="queue">De taak die geannuleerd moet worden</param>
        public void Cancel(IQueue queue)
        {
            if (null == queue) throw new ArgumentNullException(nameof(queue));
            lock (_queues)
            {
                _queues.RemoveAll(o => o.CheckEquals(queue));
            }

            lock (_runnings)
            {
                _runnings.ForEach(o =>
                {
                    if (o.CheckEquals(queue)) o.Cancel();
                });
            }
        }

        /// <summary>
        ///     Reset een taak
        /// </summary>
        /// <param name="queue">De taak die gereset moet worden</param>
        public void Reset(IQueue queue)
        {
            if (null == queue) throw new ArgumentNullException(nameof(queue));
            Cancel(queue);
            Add(queue);
        }

        /// <summary>
        ///     Sluit af alle taken
        /// </summary>
        public void ShutDown()
        {
            MaxRun = 0;
            lock (_queues)
            {
                _queues.Clear();
            }

            lock (_runnings)
            {
                _runnings.ForEach(o => o.Cancel());
            }
        }
    }
}