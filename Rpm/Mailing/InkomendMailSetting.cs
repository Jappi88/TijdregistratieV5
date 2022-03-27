using Rpm.Various;
using System.Collections.Generic;

namespace Rpm.Mailing
{
    public class InkomendMailSetting
    {
        public List<MessageAction> AllowedActions { get; set; } = new List<MessageAction>() { MessageAction.NieweProductie, MessageAction.ProductieWijziging, MessageAction.AlgemeneMelding, MessageAction.BijlageUpdate };
        public bool OnlyAllowedSenders { get; set; }
        public List<string> AllowedSenders { get; set; } = new List<string>();
    }
}
