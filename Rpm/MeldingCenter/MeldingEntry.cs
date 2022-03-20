using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using NPOI.SS.Formula.Functions;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.MeldingCenter
{
    public class MeldingEntry
    {
        public int ID { get; private set; } = Functions.GenerateRandomID();
        public byte[] ImageData { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recievers { get; set; } = new List<string>();
        public List<string> ReadUsers { get; set; } = new List<string>();
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public bool ShowMelding()
        {
            try
            {
                if (string.IsNullOrEmpty(Body)) return false;
                var ximage = ImageData.ImageFromBytes();
                if (Manager.OnRequestRespondDialog(Body, Subject, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        null,
                        null, ximage, MetroColorStyle.Red) == DialogResult.OK)
                {
                    UpdateRead(true);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool ForMe()
        {
            return Recievers.Count == 0 || Recievers.Any(x =>
                string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool HasRead()
        {
            return ReadUsers.Any(x =>
                string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ShouldShow()
        {
            return DateAdded.AddDays(7) > DateTime.Now && ForMe() && !HasRead();
        }

        public void UpdateRead(bool read)
        {
            ReadUsers.RemoveAll(x =>
                string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
            if (read && !string.IsNullOrEmpty(Manager.Opties?.Username))
                ReadUsers.Add(Manager.Opties.Username);
        }
    }
}
