using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Controls;
using Rpm.Mailing;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Misc
{
    /// <summary>
    /// Een notificatie helper
    /// </summary>
    public static class Notifications
    {
        // private static readonly List<Form_Alert> _messages = new List<Form_Alert>();
        /// <summary>
        /// Toon notificatie
        /// </summary>
        /// <param name="message">De bericht die getoond moet worden</param>
        /// <param name="title">De titel van de notificatie</param>
        public static void Notify(this RemoteMessage message,IWin32Window owner, string title)
        {
            message?.Message?.Alert(owner, title, message.MessageType);
        }

        /// <summary>
        /// Toon  notificatie
        /// </summary>
        /// <param name="message">De bericht die getoond moet worden</param>
        public static void Notify(this RemoteMessage message, IWin32Window owner)
        {
            message.Message.Alert(owner, message.Title, message.MessageType);
        }

        /// <summary>
        /// Toon notificatie
        /// </summary>
        /// <param name="msg">De bericht die getoond moet worden</param>
        /// <param name="title">De titel van de notificatie</param>
        /// <param name="type">De soort notificatie</param>
        /// <param name="closed">Een event die je kan koppelen als de notificatie is afgesloten</param>
        public static void Alert(this string msg, IWin32Window owner, string title, MsgType type, FormClosedEventHandler closed = null)
        {

            if (Application.OpenForms.Count == 0) return;

            try
            {
                var visibleform = owner??Application.OpenForms["Mainform"];
                try
                {
                    var frm = new Form_Alert();
                    if (closed != null)
                        frm.FormClosed += closed;
                    frm.OwnerForm = Manager.ActiveForm;
                    frm.FormClosed += MessageClosed;
                    //_messages.Add(frm);
                    frm.showAlert(msg, title, type);
                    // frm.MdiParent = parent;
                    frm.Show();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                //visibleform?.Select();
                //visibleform?.Focus();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void MessageClosed(object sender, FormClosedEventArgs args)
        {
            //if (sender is Form_Alert msg)
            //    _messages.Remove(msg);
            //var parent = Application.OpenForms[Application.OpenForms.Count - 1];
            //parent?.BringToFront();
            //parent?.Focus();
            //parent?.Select();
        }
    }
}