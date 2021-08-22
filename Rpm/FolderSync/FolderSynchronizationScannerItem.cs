namespace FolderSync
{
    public class FolderSynchronizationScannerItem
    {
        protected string _Source;
        protected string _Destination;
        protected FolderSynchorizationOption _Option;
        protected bool _Monitor;
        protected int _Interval;
        public string Source
        {
            get => _Source;
            set => _Source = value;
        }

        public int Interval
        {
            get => _Interval;
            set => _Interval = value;
        }

        public string Destination
        {
            get => _Destination;
            set => _Destination = value;
        }

        public FolderSynchorizationOption Option
        {
            get => _Option;
            set => _Option = value;
        }

        public bool Monitor
        {
            get => _Monitor;
            set => _Monitor = value;
        }
    }
}
