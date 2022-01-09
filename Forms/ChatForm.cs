using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms;
using MetroFramework.Drawing.Html;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using Various;
using Image = System.Drawing.Image;
using TextAlign = NPOI.XSSF.UserModel.TextAlign;

namespace ProductieManager.Forms
{
    public partial class ChatForm : Form
    {
        //public ImageList LoadedUserImages { get; private set; } = new ImageList();

        public ChatForm()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            ((OLVColumn) xuserlist.Columns[0]).ImageGetter = ProfileImageGet;
            ((OLVColumn) xuserlist.Columns[0]).AspectGetter = ProfileNameGet;
            ((OLVColumn) xuserlist.Columns[1]).AspectGetter = ProfileStatusGet;
            ((OLVColumn) xuserlist.Columns[2]).AspectGetter = ProfileLastSeenGet;
           // LoadedUserImages.ColorDepth = ColorDepth.Depth32Bit;
            //LoadedUserImages.ImageSize = new Size(64, 64);
        }

        private object ProfileNameGet(object user)
        {
            if (user is UserChat chat)
                return string.IsNullOrEmpty(chat.UserName) ? "Iedereen" : chat.UserName;
            return null;
        }

        private object ProfileImageGet(object user)
        {
            if (user is UserChat chat)
            {
                string id = string.IsNullOrEmpty(chat.UserName) ? "Iedereen" : chat.UserName;
                int index = xuserimages.Images.IndexOfKey(id);
                return index;
            }

            return null;
        }

        private object ProfileStatusGet(object user)
        {
            if (user is UserChat chat)
                return chat.IsOnline ? "Online" : "Offline";
            return "Offline";
        }

        private object ProfileLastSeenGet(object user)
        {
            if (user is UserChat chat)
            {
                if (string.Equals(chat.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase))
                {
                    var count = ProductieChat.Gebruikers.Count - 1;
                    string x1 = count == 1 ? "Gebruiker" : "Gebruikers";
                    return $"Totaal {count} {x1}";
                }

                return (chat.IsOnline ? "Online vanaf " : "Laatst online op ") +
                       (chat.LastOnline.Date == DateTime.Now.Date
                           ? chat.LastOnline.ToShortTimeString()
                           : chat.LastOnline.ToString("f"));
            }

            return "n.v.t.";
        }

        private string CreateSendMessage(ProductieChatEntry entry)
        {
            if (ProductieChat.Chat == null) return string.Empty;

            string html = CreateHtmlString($"", Color.DarkGray, Color.Transparent, 0,
                entry.Afzender.UserName, new Size(48, 48), ImageAlign.Right, new Size(85,0), TextAlign.RIGHT,
                0, 0, Color.Transparent);

            html += CreateHtmlString($"<b>{entry.Afzender.UserName} zegt:</b><br><br>{entry.Bericht}", Color.White,
                Color.CornflowerBlue, 10,
                null, new Size(32, 32), ImageAlign.Right, new Size(125, 0), TextAlign.RIGHT, 10, 15, Color.Transparent);

            html += CreateHtmlString(entry.Tijd.ToString("F"), Color.LightSlateGray, Color.Transparent, 5,
                entry.IsGelezen ? "gelezen" : "verzonden",
                new Size(24, 24), ImageAlign.Right, new Size(150, 0), TextAlign.RIGHT, 10, 10, Color.Transparent);
            return html;
        }

        private string CreateRecieveMessage(ProductieChatEntry entry)
        {

            string html = CreateHtmlString($"", Color.DarkGray, Color.Transparent, 0,
                entry.Afzender.UserName, new Size(48, 48), ImageAlign.Left, new Size(110, 0), TextAlign.LEFT,
                0, 0, Color.Transparent);

            //html += CreateHtmlString($"{user} zegt:", Color.DarkGray, Color.Transparent, 5,
            //    null, new Size(48, 48), ImageAlign.Left, new Size(150, 0), TextAlign.LEFT, 0, 0, Color.Transparent);

            html += CreateHtmlString($"<b>{entry.Afzender.UserName} zegt:</b><br><br>{entry.Bericht}", Color.Black,
                Color.PowderBlue, 10,
                null, new Size(32, 32), ImageAlign.Left, new Size(150, 0), TextAlign.LEFT, 10, 15, Color.Transparent);

            html += CreateHtmlString(entry.Tijd.ToString("F"), Color.LightSlateGray, Color.Transparent, 5,
                entry.IsGelezen ? "gelezen" : "verzonden",
                new Size(24, 24), ImageAlign.Left, new Size(175, 0), TextAlign.LEFT, 10, 10, Color.Transparent);
            return html;
        }

        public string CreateHtmlString(string value, Color textcolor, Color backcolor, int padding, string imagename,Size imagesize, ImageAlign imagealign, Size margin, TextAlign align, int borderwidth, int borderradius,
            Color bordercolor)
        {
            string txt = $"{value.Replace("\n", "<br>")}";
            string img = GetImageHtml(imagename, imagesize);
            string cmb = "";
            if (imagealign == ImageAlign.Right)
                cmb = txt + img;
            else cmb = img + txt;
            string field = $"<div style= 'padding: {padding}px; " +
                           $"margin: {margin.Height}px {margin.Width}px; " +
                           $"text-align: {Enum.GetName(typeof(TextAlign), align)?.ToLower()}; " +
                           $"background-color: {backcolor.Name}; " +
                           $"border: {borderwidth}px {bordercolor.Name}; " +
                           $"color: {textcolor.Name}; " +
                           $"corner-radius: {borderradius}px; " +
                           "'>" +
                           $"{cmb}" +
                           $"</div>";


            return field;
        }

        public string CheckProductieLinks(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            int index = 0;
            string xreturn = value;
            var xreplaces = new List<string> { };
            //eerst productie checken met een "X" letter.
            while((index = value.ToLower().IndexOf('x',index)) > -1)
            {
                int xstart = index;
                index++;
                try
                {
                    
                    int xend = value.IndexOf(' ', xstart);
                    if (xend < 0) xend = value.Length;
                    int length = xend - xstart;
                    if (length < 7)
                        continue; //productie nr bestaat uit 7 characters.

                    string xvalue = value.Substring(xstart, 7);
                    var prod = Manager.Database.GetProductie(xvalue).Result;
                    if (prod == null || xreplaces.Contains(prod.ProductieNr)) continue;
                    xreturn = xreturn.Replace(xvalue, $"<a color= white href='{prod.ProductieNr}'>[{prod.Omschrijving}]</a>");
                    xreplaces.Add(prod.ProductieNr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Nu productie checken met een "PM" letter.
            index = 0;
            xreplaces.Clear();
            while ((index = value.ToLower().IndexOf('p', index)) > -1)
            {
                int xstart = index;
                index++;
                if ((xstart + 3) < value.Length && !string.Equals(value.Substring(xstart, 2), "PM", StringComparison.CurrentCultureIgnoreCase))
                    continue;
                try
                {

                    int xend = value.IndexOf(' ', xstart);
                    if (xend < 0) xend = value.Length;
                    int length = xend - xstart;
                    if (length < 10)
                        continue; //productie nr bestaat uit 10 characters.

                    string xvalue = value.Substring(xstart, 10);
                    var prod = Manager.Database.GetProductie(xvalue).Result;
                    if (prod == null || xreplaces.Contains(prod.ProductieNr)) continue;
                    xreturn = xreturn.Replace(xvalue, $"<a color= white href='{prod.ProductieNr}'>{prod.Omschrijving}</a>");
                    xreplaces.Add(prod.ProductieNr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return xreturn;
        }

        public void SelectedUser(UserChat selecteduser)
        {
            if (xuserlist.SelectedObject is UserChat user)
            {
                if (user.Equals(selecteduser))
                    xuserlist.SelectedObject = null;
            }
            xuserlist.SelectedObject = selecteduser;
            xuserlist.SelectedItem?.EnsureVisible();
        }

        private string GetImageHtml(Bitmap image, Size size)
        {
            if (image == null) return "";
            return $"<img src='data:image/png;base64,{image.Base64Encoded()}'>";
        }

        private string GetImageHtml(string name, Size imagesize)
        {
            if (string.IsNullOrEmpty(name)) return "";
            return $"<img src='{name};width:{imagesize.Width};height:{imagesize.Height};'</>";
        }

        private string _Selected = null;
        public void Show(string selected = null)
        {
            _Selected = selected;
            base.Show();
        }

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            if (!ProductieChat.LoggedIn || ProductieChat.Chat == null)
            {
                XMessageBox.Show("Je bent niet ingelogd!\n\n Log in om te kunnen chatten met productie.",
                    "Niet Ingelogd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ProductieChat.GebruikerUpdate += ProductieChat_GebruikerUpdate;
            ProductieChat.MessageRecieved += ProductieChat_MessageRecieved;
            LoadProfile();
            LoadProfiles();
            LoadSelectedUser();
        }

        private void ProductieChat_MessageRecieved(ProductieChatEntry message)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {

                LoadProfiles();
                var selected = SelectedUser();
                if (selected != null && (string.Equals(selected.UserName, message.Afzender.UserName,
                        StringComparison.CurrentCultureIgnoreCase) || string.Equals(selected.UserName,
                        message.Ontvanger,
                        StringComparison.CurrentCultureIgnoreCase)))
                    LoadConversation(true);

            }));
        }

        private void ProductieChat_GebruikerUpdate(UserChat user)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                LoadProfile();
                LoadProfiles();
                LoadSelectedUser();
            }));
        }

        private void LoadProfile()
        {
            if (ProductieChat.Chat == null) return;
                xprofileimage.Image = ProductieChat.Chat.GetProfielImage();
                xprofilenaam.Text = ProductieChat.Chat.UserName;
                xprofilestatus.Text = ProductieChat.Chat.IsOnline ? "Online" : "Offline";
                xprofilestatusimage.Image = ProductieChat.Chat.IsOnline
                    ? Properties.Resources.Online_32
                    : Properties.Resources.offline_32x32;
        }

        private void LoadProfiles()
        {
            try
            {
                if (ProductieChat.Chat == null)
                {
                    xuserlist.SetObjects(new UserChat[] { });
                    return;
                }

                xuserlist.BeginUpdate();
                var selected = SelectedUser();
                var toremove = xuserlist.Objects?.Cast<UserChat>().ToList() ?? new List<UserChat>();
                xuserimages.Images.Clear();
               // xuserimages.Images.Add("Iedereen", Properties.Resources.users_clients_group_16774);
                //LoadedUserImages.Images.Clear();
               // LoadedUserImages.Images.Add(ProductieChat.Chat.UserName, ProductieChat.Chat.GetProfielImage());
                //LoadedUserImages.Images.Add("Iedereen", Properties.Resources.users_clients_group_16774);

                if (!ProductieChat.Gebruikers.Any(x => string.Equals(x.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase)))
                {
                    var iedereen = new UserChat()
                        { UserName = "Iedereen", IsOnline = true, ProfielImage = ProductieChat.GroupChatImagePath };
                    ProductieChat.Gebruikers.Add(iedereen);
                    ProductieChat.Gebruikers = ProductieChat.Gebruikers.OrderBy(x => x.UserName).ToList();
                }
                //load eerst de default gebruiker.
                if (ProductieChat.Chat != null)
                {
                    var img = ProductieChat.Chat.GetProfielImage() ?? Properties.Resources.avatardefault_92824;
                    int unread = ProductieChat.Chat.UnreadMessages(ProductieChat.Chat.UserName);
                    if (unread > 0)
                    {
                        var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White, unread.ToString(),
                            new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                        img = img.CombineImage(ximg, ContentAlignment.BottomLeft, 2.5);
                    }

                    if (ProductieChat.Chat.IsOnline)
                        img = img.CombineImage(Properties.Resources.Online_32, 3.5);
                    else
                        img = img.CombineImage(Properties.Resources.offline_32x32, 3.5);
                    xuserimages.Images.Add(ProductieChat.Chat.UserName, img);
                }

                for (int i = 0; i < ProductieChat.Gebruikers.Count; i++)
                {
                    var user = ProductieChat.Gebruikers[i];
                    if (_Selected != null &&
                        string.Equals(user.UserName, _Selected, StringComparison.CurrentCultureIgnoreCase))
                        selected = user;
                    var img = user.GetProfielImage() ?? Properties.Resources.avatardefault_92824;
                    //LoadedUserImages.Images.Add(user.UserName, img);
                    int unread = ProductieChat.Chat.UnreadMessages(user.UserName);
                    if (unread > 0)
                    {
                        var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White, unread.ToString(),
                            new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                        img = img.CombineImage(ximg, ContentAlignment.BottomLeft, 2.5);
                    }

                    if (user.IsOnline)
                        img = img.CombineImage(Properties.Resources.Online_32, 3.5);
                    else
                        img = img.CombineImage(Properties.Resources.offline_32x32, 3.5);
                    xuserimages.Images.Add(user.UserName, img);

                    var old = toremove.FirstOrDefault(x => x.Equals(user));
                    if (old == null)
                    {
                        xuserlist.AddObject(user);
                    }
                    else
                    {
                        xuserlist.RefreshObject(user);
                        toremove.Remove(old);
                    }
                }

                if (toremove.Count > 0)
                {
                    xuserlist.RemoveObjects(toremove);
                }
                xuserlist.EndUpdate();
                xuserlist.SelectedObject = selected;
                xuserlist.SelectedItem?.EnsureVisible();
                if (xuserlist.SelectedObject == null && xuserlist.Items.Count > 0)
                {
                    xuserlist.LowLevelScroll(0, -100);
                    //xuserlist.Items[0].EnsureVisible();
                }

                LoadSelectedUser();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void LoadSelectedUser()
        {
            if (Manager.ProductieChat == null || Manager.LogedInGebruiker == null) return;
            var user = SelectedUser();
            xselecteduserprofilebutton.Visible = user != null;
            xselecteduserimage.Image = user?.GetProfielImage();
            xselectedusername.Text = user?.UserName;
            xselecteduserstatus.Text = user == null ? "" : (user.IsOnline ? "Online" : "Offline");
            xselecteduserstatusimage.Image = user == null
                ? null
                : user.IsOnline
                    ? Properties.Resources.Online_32
                    : Properties.Resources.offline_32x32;
            if (string.Equals(user?.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase))
            {
                var users = ProductieChat.Gebruikers.Where(x => !string.Equals(Manager.LogedInGebruiker.Username,
                    x.UserName, StringComparison.CurrentCultureIgnoreCase) && !string.Equals("iedereen",
                    x.UserName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                var onlineusers = ProductieChat.Gebruikers.Where(x => x.IsOnline && !string.Equals(Manager.LogedInGebruiker.Username,
                    x.UserName, StringComparison.CurrentCultureIgnoreCase) && !string.Equals("iedereen",
                    x.UserName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                var x0 = users.Count == 1 ? $"gebruiker" : "gebruikers";
                var x1 = onlineusers.Count == 1 ? $"gebruiker" : "gebruikers";

                xselecteduserdate.Text = $"Stuur bericht naar {users.Count} {x0}, waarvan online: {onlineusers.Count} {x1}";
            }
            else
            {
                xselecteduserdate.Text = user == null
                    ? null
                    : (user.IsOnline ? "Online vanaf " : "Laatst online geweest op ") +
                      (user.LastOnline.Date == DateTime.Now.Date
                          ? user.LastOnline.ToShortTimeString()
                          : user.LastOnline.ToString("f"));
            }

            if (user != null)
                LoadConversation(true);
        }

        private UserChat SelectedUser()
        {
            return (UserChat) xuserlist.SelectedObject;
        }

        private async void LoadConversation(bool scrolltoend)
        {
            await Task.Run(() =>
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        var selected = SelectedUser();
                        if (selected != null)
                        {
                            int curpos = xchatpanel.VerticalScroll.Value;
                            //load conversation
                            var messages =
                                ProductieChat.GetConversation(selected, !string.IsNullOrEmpty(selected.UserName));
                            string msg = "";
                            bool notifyuser = false;
                            ProductieChat.RaiseNewMessageEvent = false;
                            for(int i = 0; i < messages.Count; i++)
                            {
                                var message = messages[i];
                                bool isme = string.Equals(message.Afzender.UserName, ProductieChat.Chat.UserName,
                                    StringComparison.CurrentCultureIgnoreCase);
                                if (!isme && !message.IsGelezen)
                                {
                                    message.IsGelezen = true;
                                    message.UpdateMessage();
                                    notifyuser = true;
                                }

                                msg += isme ? CreateSendMessage(message) : CreateRecieveMessage(message);
                            }

                            ProductieChat.RaiseNewMessageEvent = true;
                            if (notifyuser)
                                selected.Save();
                            xchatpanel.Text = msg;
                            var xpos = scrolltoend ? xchatpanel.VerticalScroll.Maximum : curpos;
                            for (int i = 0; i < 5; i++)
                            {
                                xchatpanel.VerticalScroll.Value = xpos;
                                xchatpanel.Update();
                                //Application.DoEvents();
                                if (xchatpanel.VerticalScroll.Value == xpos)
                                    break;
                            }
                        }
                        else xchatpanel.Text = "";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }));
            });
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProductieChat.GebruikerUpdate -= ProductieChat_GebruikerUpdate;
            ProductieChat.MessageRecieved -= ProductieChat_MessageRecieved;
            this.SetLastInfo();
        }

        private void xuserlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedUser();
        }

        private void SendMessage()
        {
            if (ProductieChat.Chat == null || !ProductieChat.LoggedIn ||
                string.IsNullOrEmpty(xchattextbox.Text.Trim()) ||
                xchattextbox.Text.Trim().ToLower() == "typ bericht...")
            {
                xchattextbox.Text = xchattextbox.Text.Trim();
                return;
            }
            var selected = SelectedUser();
            if (selected == null) return;
            string ontvangers = string.IsNullOrEmpty(selected.UserName)
                ? string.Join(";", ProductieChat.Gebruikers.Select(x => x.UserName))
                : selected.UserName;
            string xmessage = CheckProductieLinks(xchattextbox.Text.Trim());
            ProductieChat.SendMessage(xmessage, ontvangers);
            xchattextbox.Text = "";
            xchattextbox.Select();
            xchattextbox.Focus();
            LoadConversation(true);
        }

        private void xsendbutton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void wijzigProfielFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProductieChat.Chat == null || !ProductieChat.LoggedIn) return;
            var ofd = new OpenFileDialog();
            ofd.Title = "Kies een profiel foto";
            ofd.Filter = "Alles|*.*|PNG|*.png|JPG|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.FileName.IsImageFile())
                {
                    XMessageBox.Show($"'{Path.GetFileName(ofd.FileName)}' is geen geldige afbeelding!",
                        "Ongeldige Afbeelding", MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var img = Image.FromStream(new MemoryStream(File.ReadAllBytes(ofd.FileName)));
                    if (!ProductieChat.ChangeProfielImage(ofd.FileName))
                    {
                        XMessageBox.Show("Het is niet gelukt om je profiel foto te wijzigen, probeer het later nog eens.",
                            "Ongeldige Afbeelding", MessageBoxIcon.Exclamation);
                    }
                    img = null;
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(exception.Message,
                        "Ongeldige Afbeelding", MessageBoxIcon.Error);
                }

            }
        }

        private void xchattextbox_Enter(object sender, EventArgs e)
        {
            if (string.Equals(xchattextbox.Text.Trim(), "typ bericht...", StringComparison.CurrentCultureIgnoreCase))
            {
                xchattextbox.Text = "";
                xchattextbox.ForeColor = Color.Black;
            }
        }

        private void xchattextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xchattextbox.Text.Trim()))
            {
                xchattextbox.Text = "Typ bericht...";
                xchattextbox.ForeColor = Color.Gray;
            }
        }

        private void xchatview_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            string[] values = e.Src.Split(';');
            if (values.Length == 0) return;
            var xmg = xuserimages.Images[values[0]];
            if (xmg == null)
            {
                switch (values[0])
                {
                    case "gelezen":
                        xmg = Properties.Resources.MsgRead_32;
                        break;
                    case "verzonden":
                        xmg = Properties.Resources.message_send_32x32;
                        break;
                }
            }

            if (xmg != null)
            {
                string width = values.FirstOrDefault(x => x.ToLower().Contains("width"));
                string height = values.FirstOrDefault(x => x.ToLower().Contains("height"));
                int xwidth = -1;
                int xheight = -1;
                if (width != null)
                {
                    string[] xw = width.Split(':');
                    if (xw.Length > 1)
                        int.TryParse(xw[1], out xwidth);
                }
                if (height != null)
                {
                    string[] xw = height.Split(':');
                    if (xw.Length > 1)
                        int.TryParse(xw[1], out xheight);
                }

                if (xwidth > 0 && xheight > 0)
                    xmg = xmg.ResizeImage(xwidth, xheight);
                e.Callback(xmg);
            }
        }

        private async void xchatpanel_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            if (Manager.Database == null || Manager.Database.IsDisposed) return;
            try
            {
                var prod = await Manager.Database.GetProductie(e.Link);
                if (prod == null) return;
                var bew = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                Manager.FormulierActie(new object[] { prod, bew }, MainAktie.OpenProductie);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void xchatpanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("Producties") is ArrayList)
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void xchatpanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("Producties") is ArrayList collection)
            {
                var data = collection.Cast<IProductieBase>().ToList();
                string xvalue = string.Join(", ", data.Select(x => x.ProductieNr));
                if (xchattextbox.Text.Trim().ToLower() == "typ bericht..." || string.IsNullOrEmpty(xchattextbox.Text.Trim()))
                    xchattextbox.Text = xvalue;
                else xchattextbox.Text += ", " + xvalue;
                xchattextbox.ForeColor = Color.Black;
            }
        }

        private void xchattextbox_KeyDown(object sender, KeyEventArgs e)
        {
            var send = e.KeyData == Keys.Enter && e.KeyData != Keys.Shift;
            if (send)
            {
                SendMessage();
                e.SuppressKeyPress = true;
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            this.InitLastInfo();
        }
    }
}
