namespace FolderSync
{
    /// <summary>
    ///     Een synchronisatie object die gegevens bevat die gescanned moeten worden
    /// </summary>
    public class FolderSynchronizationScannerItem
    {
        /// <summary>
        ///     De brond folder die gescanned moet worden
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        ///     De interval waarmee het gescanned moet worden in miliseconden
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        ///     De bestemming van de folder waarmee gesynchroniseerd moet worden
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        ///     De scan opties
        /// </summary>
        public FolderSynchorizationOption Option { get; set; }

        /// <summary>
        ///     Of je de folder wilt monitoren
        /// </summary>
        public bool Monitor { get; set; }
    }
}