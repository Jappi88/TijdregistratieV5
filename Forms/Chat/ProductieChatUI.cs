using BrightIdeasSoftware;
using ProductieManager.Forms.Chat;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ProductieManager.Properties;
using ContentAlignment = System.Drawing.ContentAlignment;
using TextAlign = NPOI.XSSF.UserModel.TextAlign;

namespace Forms
{
    public partial class ProductieChatUI : UserControl
    {
        //public ImageList LoadedUserImages { get; private set; } = new ImageList();

        public ProductieChatUI()
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

        public bool InitUI()
        {
            if (!ProductieChat.LoggedIn || ProductieChat.Chat == null)
            {
                XMessageBox.Show(this, $"Je bent niet ingelogd!\n\n Log in om te kunnen chatten met productie.",
                    "Niet Ingelogd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            Application.DoEvents();
            LoadProfile();
            LoadProfiles();
            ProductieChat.GebruikerUpdate += ProductieChat_GebruikerUpdate;
            ProductieChat.MessageRecieved += ProductieChat_MessageRecieved;
            return true;
        }

        public void CloseUI()
        {
            ProductieChat.GebruikerUpdate -= ProductieChat_GebruikerUpdate;
            ProductieChat.MessageRecieved -= ProductieChat_MessageRecieved;
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

        private ChatBubble CreateSendMessage(ProductieChatEntry entry, ChatBubble chat = null)
        {
            if (ProductieChat.Chat == null) return null;
            var xret = chat ?? new ChatBubble();
            string html = CreateHtmlString($"", entry.ID, Color.DarkGray, Color.Transparent, 0,
                entry.Afzender.UserName, new Size(48, 48), ImageAlign.Left, new Size(85, 0), TextAlign.LEFT,
                0, 0, Color.Transparent);

            html += CreateHtmlString($"<b>{entry.Afzender.UserName} zegt:</b><br><br>{entry.Bericht}", entry.ID,
                Color.White,
                Color.CornflowerBlue, 10,
                null, new Size(32, 32), ImageAlign.Left, new Size(125, 0), TextAlign.LEFT, 10, 15, Color.Transparent);

            html += CreateHtmlString(entry.Tijd.ToString("F"), entry.ID, Color.LightSlateGray, Color.Transparent, 5,
                entry.IsGelezen ? "gelezen" : "verzonden",
                new Size(24, 24), ImageAlign.Left, new Size(150, 0), TextAlign.LEFT, 10, 10, Color.Transparent);
            xret.SetMessage(entry, html, xuserimages.Images[entry.Afzender.UserName]);
            return xret;
        }

        private ChatBubble CreateRecieveMessage(ProductieChatEntry entry, ChatBubble chat = null)
        {
            string html = CreateHtmlString($"",entry.ID, Color.DarkGray, Color.Transparent, 0,
                entry.Afzender.UserName, new Size(48, 48), ImageAlign.Left, new Size(110, 0), TextAlign.LEFT,
                0, 0, Color.Transparent);

            //html += CreateHtmlString($"{user} zegt:", Color.DarkGray, Color.Transparent, 5,
            //    null, new Size(48, 48), ImageAlign.Left, new Size(150, 0), TextAlign.LEFT, 0, 0, Color.Transparent);
            var xret = chat ?? new ChatBubble();
            html += CreateHtmlString($"<b>{entry.Afzender.UserName} zegt:</b><br><br>{entry.Bericht}", entry.ID, Color.Black,
                Color.PowderBlue, 10,
                null, new Size(32, 32), ImageAlign.Left, new Size(150, 0), TextAlign.LEFT, 10, 15, Color.Transparent);

            html += CreateHtmlString(entry.Tijd.ToString("F"), entry.ID, Color.LightSlateGray, Color.Transparent, 5,
                entry.IsGelezen ? "gelezen" : "verzonden",
                new Size(24, 24), ImageAlign.Left, new Size(175, 0), TextAlign.LEFT, 10, 10, Color.Transparent);
            xret.SetMessage(entry, html, xuserimages.Images[entry.Afzender.UserName]);
            return xret;
        }

        public string CreateHtmlString(string value, string id, Color textcolor, Color backcolor, int padding, string imagename,Size imagesize, ImageAlign imagealign, Size margin, TextAlign align, int borderwidth, int borderradius,
            Color bordercolor)
        {
            string txt = $"{value.Replace("\n", "<br>")}";
            string img = GetImageHtml(imagename, imagesize);
            string cmb = "";
            if (imagealign == ImageAlign.Right)
                cmb = txt + img;
            else cmb = img + txt;
            string field = $"<p id='{id}' style= 'padding: {padding}px; " +
                           $"margin: {margin.Height}px {margin.Width}px; " +
                           $"text-align: {Enum.GetName(typeof(TextAlign), align)?.ToLower()}; " +
                           $"background-color: {backcolor.Name}; " +
                           $"border: {borderwidth}px {bordercolor.Name}; " +
                           $"color: {textcolor.Name}; " +
                           $"corner-radius: {borderradius}px; " +
                           "'>" +
                           $"{cmb}" +
                           $"</p>";


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
                    var prod = Manager.Database.GetProductie(xvalue, true);
                    if (prod == null || xreplaces.Contains(prod.ProductieNr)) continue;
                    xreturn = xreturn.Replace(xvalue, $"\n<a color='purple' href='{prod.ProductieNr}'>[{prod.Omschrijving}]</a>");
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
                    var prod = Manager.Database.GetProductie(xvalue, true);
                    if (prod == null || xreplaces.Contains(prod.ProductieNr)) continue;
                    xreturn = xreturn.Replace(xvalue, $"\n<a color='purple' href='{prod.ProductieNr}'>{prod.Omschrijving}</a>");
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

        internal string _Selected = null;

        private void ProductieChat_MessageRecieved(ProductieChatEntry message)
        {
            try
            {
                if (string.IsNullOrEmpty(message?.Bericht)) return;
                //if (message.Afzender.Equals(_selecteduser))
                //{
                    
                    
                //    Invoke(new MethodInvoker(() =>
                //    {
                //        var xmsg = CreateRecieveMessage(message);
                //        xchatpanel.Controls.Add(xmsg);
                //        xchatpanel.Controls.SetChildIndex(xmsg, xchatpanel.Controls.Count - 1);
                //        xchatpanel.ScrollControlIntoView(xmsg);
                //    }));
                //}
                //else
                    BeginInvoke(new MethodInvoker(LoadProfiles));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ProductieChat_GebruikerUpdate(UserChat user)
        {
            if (Disposing || IsDisposed) return;
            BeginInvoke(new MethodInvoker(() =>
            {
                LoadProfile();
                LoadProfiles();
            }));
        }

        private void LoadProfile()
        {
            if (ProductieChat.Chat == null) return;
                xprofileimage.Image = ProductieChat.Chat.GetProfielImage();
                xprofilenaam.Text = ProductieChat.Chat.UserName;
                xprofilestatus.Text = ProductieChat.Chat.IsOnline ? "Online" : "Offline";
                xprofilestatusimage.Image = ProductieChat.Chat.IsOnline
                    ? Resources.Online_32
                    : Resources.offline_32x32;
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
                    var img = ProductieChat.Chat.GetProfielImage() ?? Resources.avatardefault_92824;
                    int unread = ProductieChat.Chat.UnreadMessages(ProductieChat.Chat.UserName);
                    if (unread > 0)
                    {
                        var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White, unread.ToString(),
                            new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                        img = img.CombineImage(ximg, ContentAlignment.BottomLeft, 2.5);
                    }

                    if (ProductieChat.Chat.IsOnline)
                        img = img.CombineImage(Resources.Online_32, 3.5);
                    else
                        img = img.CombineImage(Resources.offline_32x32, 3.5);
                    xuserimages.Images.Add(ProductieChat.Chat.UserName, img);
                }

                for (int i = 0; i < ProductieChat.Gebruikers.Count; i++)
                {
                    var user = ProductieChat.Gebruikers[i];
                    if (user.UserName.ToLower().Trim() != "iedereen")
                    {
                        var acc = Manager.Database.AccountExist(user.UserName).Result;
                        if (!acc)
                        {
                            ProductieChat.Gebruikers[i].DeleteUser();
                            ProductieChat.Gebruikers.RemoveAt(i--);
                            continue;
                        }
                    }
                    if (_Selected != null &&
                        string.Equals(user.UserName, _Selected, StringComparison.CurrentCultureIgnoreCase))
                        selected = user;
                    var img = user.GetProfielImage() ?? Resources.avatardefault_92824;
                    //LoadedUserImages.Images.Add(user.UserName, img);
                    int unread = ProductieChat.Chat.UnreadMessages(user.UserName);
                    if (unread > 0)
                    {
                        var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White, unread.ToString(),
                            new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                        img = img.CombineImage(ximg, ContentAlignment.BottomLeft, 2.5);
                    }

                    if (user.IsOnline)
                        img = img.CombineImage(Resources.Online_32, 3.5);
                    else
                        img = img.CombineImage(Resources.offline_32x32, 3.5);
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
                LoadSelectedUser();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private UserChat _selecteduser;

        private async void LoadSelectedUser()
        {
            if (Manager.ProductieChat == null || Manager.LogedInGebruiker == null) return;
            var user = SelectedUser();
            if (user == null)
            {
                xchatpanel.SuspendLayout();
                xchatpanel.Controls.Clear();
                xchatpanel.ResumeLayout(false);
                xsendmessagepanel.Visible = false;
            }
            else
            {
                if (_selecteduser != null && _selecteduser.Equals(user))
                {
                    await LoadConversation(false);
                }
                else
                {
                    xsendmessagepanel.Visible = user != null;
                    xselecteduserimage.Image = user?.GetProfielImage();
                    xselectedusername.Text = user?.UserName;
                    xselecteduserstatus.Text = user == null ? "" : (user.IsOnline ? "Online" : "Offline");
                    xselecteduserstatusimage.Image = user == null
                        ? null
                        : user.IsOnline
                            ? Resources.Online_32
                            : Resources.offline_32x32;
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

                    await LoadConversation(true);

                }
            }
            _selecteduser = user;
        }

        private UserChat SelectedUser()
        {
            return (UserChat) xuserlist.SelectedObject;
        }

        private void UpdateMessages(List<ProductieChatEntry> entries, bool scroltoend, UserChat selected = null)
        {
            try
            {
                xchatpanel.SuspendLayout();
                if (selected != null && !selected.Equals(_selecteduser))
                {
                    xchatpanel.Controls.Clear();
                }
                else
                {
                    var toremove = xchatpanel.Controls.Cast<ChatBubble>().Where(x => entries.IndexOf(x.Message) == -1)
                        .ToList();
                    if (toremove.Count > 0)
                    {
                        toremove.ForEach(x => xchatpanel.Controls.Remove(x));
                    }
                }
                
                var xmessages = xchatpanel.Controls.Cast<ChatBubble>().ToList();
                bool updated = false;
                bool added = false;
                
                for (int i = 0; i < entries.Count; i++)
                {
                    var xent = entries[i];
                    bool isme = string.Equals(xent.Afzender.UserName, ProductieChat.Chat.UserName,
                        StringComparison.CurrentCultureIgnoreCase);

                    if (!isme && !xent.IsGelezen)
                    {
                        xent.IsGelezen = true;
                        updated = true;
                        xent.UpdateMessage();
                    }

                    var xmsg = isme ? CreateSendMessage(xent) : CreateRecieveMessage(xent);
                    var xold = xmessages.FirstOrDefault(x => x.Message.Equals(xent));
                    if (xold != null)
                    {
                        xold.SetMessage(xmsg);
                    }
                    else
                    {
                       // 
                        xchatpanel.Controls.Add(xmsg);
                        xchatpanel.Controls.SetChildIndex(xmsg, i);
                       // 
                        added = true;
                    }
                }
                xchatpanel.ResumeLayout(false);
                if (updated)
                {
                    selected?.Save();
                }

                if ((added || scroltoend) && xchatpanel.Controls.Count > 0)
                {
                    var xlast = xchatpanel.Controls[xchatpanel.Controls.Count -1];
                    xchatpanel.PerformLayout();
                    xchatpanel.ScrollControlIntoView(xlast);
                    xchatpanel.PerformLayout();
                    xchatpanel.ScrollControlIntoView(xlast);
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private Task LoadConversation(bool scrolltoend)
        {
            return Task.Run(() => xLoadConversation(scrolltoend));
        }

        private void xLoadConversation(bool scrolltoend)
        {
            //BeginInvoke(new MethodInvoker(() =>
            //{
                try
                {
                    if (InvokeRequired)
                        this.Invoke(new Action(() => xLoadConversation(scrolltoend)));
                    else
                    {

                        var selected = SelectedUser();
                        bool seltext = xchattextbox.Focused;
                        if (selected != null)
                        {
                            //load conversation
                            var messages =
                                ProductieChat.GetConversation(selected, !string.IsNullOrEmpty(selected.UserName));
                            if (this.Disposing || this.IsDisposed) return;
                            ProductieChat.RaiseNewMessageEvent = false;
                            UpdateMessages(messages, scrolltoend, selected);
                            _selecteduser = selected;
                            ProductieChat.RaiseNewMessageEvent = true;
                        }
                        else
                        {
                            xchatpanel.Controls.Clear();
                            xchatpanel.Invalidate();
                        }

                        if (seltext)
                            xchattextbox.Focus();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
           // }));
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
            string xmessage = CheckProductieLinks(xchattextbox.Text.Trim().WrapText(100, "\n"));
            var xent = ProductieChat.SendMessage(xmessage, ontvangers);
            if (xent != null)
            {
                var xmsg = CreateSendMessage(xent);
                xchatpanel.Controls.Add(xmsg);
                xchatpanel.Controls.SetChildIndex(xmsg, xchatpanel.Controls.Count - 1);
                xchatpanel.ScrollControlIntoView(xmsg);
            }
            xchattextbox.Text = "";
            xchattextbox.Select();
            xchattextbox.Focus();
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
                    XMessageBox.Show(this, $"'{Path.GetFileName(ofd.FileName)}' is geen geldige afbeelding!",
                        "Ongeldige Afbeelding", MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    //Image.FromStream(new MemoryStream(File.ReadAllBytes(ofd.FileName)));
                    if (!ProductieChat.ChangeProfielImage(ofd.FileName))
                    {
                        XMessageBox.Show(this, $"Het is niet gelukt om je profiel foto te wijzigen, probeer het later nog eens.",
                            "Ongeldige Afbeelding", MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(this, exception.Message,
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
    }
}
