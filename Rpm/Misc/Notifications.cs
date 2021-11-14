using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Controls;
using Rpm.Mailing;
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
        /// <param name="parent">De scherm die de notificatie toont</param>
        public static void Notify(this RemoteMessage message, string title, Form parent)
        {
            message?.Message?.Alert( title, message.MessageType, parent);
        }

        /// <summary>
        /// Toon  notificatie
        /// </summary>
        /// <param name="message">De bericht die getoond moet worden</param>
        /// <param name="parent">De scherm die de notificatie toont</param>
        public static void Notify(this RemoteMessage message, Form parent)
        {
            message.Message.Alert(message.Title,message.MessageType,parent);
        }

       /// <summary>
       /// Toon notificatie
       /// </summary>
       /// <param name="msg">De bericht die getoond moet worden</param>
       /// <param name="title">De titel van de notificatie</param>
       /// <param name="type">De soort notificatie</param>
       /// <param name="parent">De scherm die de notificatie toont</param>
       /// <param name="closed">Een event die je kan koppelen als de notificatie is afgesloten</param>
        public static void Alert(this string msg, string title, MsgType type, Form parent, FormClosedEventHandler closed = null)
       {
           Task.Run(() =>
           {
               if (Application.OpenForms.Count == 0) return;
               
               try
               {
                   var visibleform = Application.OpenForms["Mainform"];
                   visibleform?.BeginInvoke(new MethodInvoker(() =>
                   {
                       try
                       {
                           var frm = new Form_Alert();
                           if (closed != null)
                               frm.FormClosed += closed;
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
                   }));
                   //visibleform?.Select();
                   //visibleform?.Focus();
               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
               }

           });
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