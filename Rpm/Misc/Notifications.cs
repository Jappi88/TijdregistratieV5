using System.Threading;
using System.Windows.Forms;
using Rpm.Connection;
using Rpm.Controls;
using Rpm.Mailing;
using Rpm.Various;

namespace Rpm.Misc
{
    public static class Notifications
    {
       // private static readonly List<Form_Alert> _messages = new List<Form_Alert>();

        public static void Notify(this RespondMessage message, string title, Form parent)
        {
            message?.Message?.Alert( title, message.MessageType, parent);
        }

        public static void Notify(this RemoteMessage message, Form parent)
        {
            message.Message.Alert(message.Title,message.MessageType,parent);
        }

        public static void Alert(this string msg, string title, MsgType type, Form parent, FormClosedEventHandler closed = null)
        {
            new Thread(new ThreadStart(() =>
            {
                if (Application.OpenForms.Count == 0) return;
                var visibleform = Application.OpenForms[Application.OpenForms.Count - 1];

                parent?.BeginInvoke(new MethodInvoker(() =>
                {
                    var frm = new Form_Alert();
                    if (closed != null)
                        frm.FormClosed += closed;
                    frm.FormClosed += MessageClosed;
                    //_messages.Add(frm);
                    frm.showAlert(msg, title, type);
                   // frm.MdiParent = parent;
                    frm.Show();
                }));
            })).Start();
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