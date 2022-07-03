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
            if (Manager.ProductieChat?.Chat == null || !Manager.ProductieChat.LoggedIn)
            {
                XMessageBox.Show(this, $"Je bent niet ingelogd!\n\n Log in om te kunnen chatten met productie.",
                    "Niet Ingelogd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            Application.DoEvents();
            LoadProfile();
            LoadProfiles();
            Manager.ProductieChat.GebruikerChanged += ProductieChat_GebruikerUpdate;
            Manager.ProductieChat.GebruikerDeleted += ProductieChat_GebruikerUpdate;
            Manager.ProductieChat.MessageChanged += ProductieChat_MessageUpdated;
            Manager.ProductieChat.MessageDeleted += ProductieChat_MessageUpdated;
            Manager.ProductieChat.PublicMessageChanged += ProductieChat_MessageUpdated;
            Manager.ProductieChat.PublicMessageDeleted += ProductieChat_MessageUpdated;
            return true;
        }

        public void CloseUI()
        {
            Manager.ProductieChat.GebruikerChanged -= ProductieChat_GebruikerUpdate;
            Manager.ProductieChat.GebruikerDeleted -= ProductieChat_GebruikerUpdate;
            Manager.ProductieChat.MessageChanged -= ProductieChat_MessageUpdated;
            Manager.ProductieChat.MessageDeleted -= ProductieChat_MessageUpdated;
            Manager.ProductieChat.PublicMessageChanged -= ProductieChat_MessageUpdated;
            Manager.ProductieChat.PublicMessageDeleted -= ProductieChat_MessageUpdated;
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
                    var count = Manager.ProductieChat.Gebruikers.Count - 1;
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

        public enum ImageAlign
        {
            Left,
            Top,
            Right,
            Bottom
        }

        private ChatBubble CreateSendMessage(ProductieChatEntry entry, ChatBubble chat = null)
        {
            if (Manager.ProductieChat.Chat == null) return null;
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
            xret.SetMessage(entry, html, GetProfileImage(entry.Afzender));
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
            xret.SetMessage(entry, html, GetProfileImage(entry.Afzender));
            return xret;
        }

        public Image GetProfileImage(UserChat user)
        {
            try
            {
                var img = Manager.ProductieChat?.GetProfielImage(user);
                var gbindex = Manager.ProductieChat.Gebruikers.IndexOf(user);
                if (gbindex >= 0)
                {
                    user = Manager.ProductieChat.Gebruikers[gbindex];
                }
                if (user.IsOnline)
                    img = img.CombineImage(Resources.Online_32, 3.5);
                else
                    img = img.CombineImage(Resources.offline_32x32, 3.5);
                img.Tag = user.IsOnline;
                return img;
            }
            catch { return null; }
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

        private void ProductieChat_MessageUpdated(object item, FileSystemEventArgs e)
        {
            try
            {
                if (item is ProductieChatEntry message)
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
                    if (message.Ontvangers != null && message.Ontvangers.Any(x=> string.Equals(x, _selecteduser?.UserName, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        var updated = false;
                        var added = false;
                        UpdateMessage(message, ref updated, ref added, true);
                    }
                    else
                        BeginInvoke(new MethodInvoker(LoadProfiles));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ProductieChat_GebruikerUpdate(object item, FileSystemEventArgs e)
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
            if (Manager.ProductieChat.Chat == null) return;
                xprofileimage.Image = Manager.ProductieChat.GetProfielImage();
                xprofilenaam.Text = Manager.ProductieChat.Chat.UserName;
                xprofilestatus.Text = Manager.ProductieChat.Chat.IsOnline ? "Online" : "Offline";
                xprofilestatusimage.Image = Manager.ProductieChat.Chat.IsOnline
                    ? Resources.Online_32
                    : Resources.offline_32x32;
        }

        private void ReloadImages()
        {
            try
            {
                xuserimages.Images.Clear();
                if (Manager.ProductieChat.Chat != null)
                {
                    var img = Manager.ProductieChat.GetProfielImage() ?? Resources.avatardefault_92824;
                    int unread = Manager.ProductieChat.UnreadMessages(Manager.ProductieChat.Chat.UserName, DateTime.Now.Subtract(TimeSpan.FromDays(30)));
                    if (unread > 0)
                    {
                        var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White, unread.ToString(),
                            new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                        img = img.CombineImage(ximg, ContentAlignment.BottomLeft, 2.5);
                    }

                    if (Manager.ProductieChat.Chat.IsOnline)
                        img = img.CombineImage(Resources.Online_32, 3.5);
                    else
                        img = img.CombineImage(Resources.offline_32x32, 3.5);
                    xuserimages.Images.Add(Manager.ProductieChat.Chat.UserName, img);
                }

                for (int i = 0; i < Manager.ProductieChat.Gebruikers.Count; i++)
                {
                    var user = Manager.ProductieChat.Gebruikers[i];
                    if (user.UserName.ToLower().Trim() != "iedereen")
                    {
                        var acc = Manager.Database.AccountExist(user.UserName);
                        if (!acc)
                        {
                            Manager.ProductieChat.DeleteUser(user);
                            Manager.ProductieChat.Gebruikers.RemoveAt(i--);
                            continue;
                        }
                    }
                    var img = Manager.ProductieChat.GetProfielImage(user) ?? Resources.avatardefault_92824;
                    //LoadedUserImages.Images.Add(user.UserName, img);
                    int unread = Manager.ProductieChat.UnreadMessages(user.UserName, DateTime.Now.Subtract(TimeSpan.FromDays(30)));
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
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void SetSelected(string user)
        {
            _Selected = user;
            var xuser = Manager.ProductieChat?.Gebruikers?.FirstOrDefault(x => string.Equals(x.UserName, _Selected, StringComparison.CurrentCultureIgnoreCase));
            if(xuser != null)
            {
                xuserlist.SelectedObject = xuser;
                xuserlist.SelectedItem?.EnsureVisible();
                if(xsendmessagepanel.Visible)
                {
                    xchattextbox.Focus();
                    xchattextbox.Select();
                }
            }
        }

        private void LoadProfiles()
        {
            try
            {
                if (Manager.ProductieChat.Chat == null)
                {
                    xuserlist.SetObjects(new UserChat[] { });
                    return;
                }
                var selected = SelectedUser();
                var toremove = xuserlist.Objects?.Cast<UserChat>().ToList() ?? new List<UserChat>();

                // xuserimages.Images.Add("Iedereen", Properties.Resources.users_clients_group_16774);
                //LoadedUserImages.Images.Clear();
                // LoadedUserImages.Images.Add(Manager.ProductieChat.Chat.UserName, Manager.ProductieChat.Chat.GetProfielImage());
                //LoadedUserImages.Images.Add("Iedereen", Properties.Resources.users_clients_group_16774);
                
                if (!Manager.ProductieChat.Gebruikers.Any(x => string.Equals(x.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase)))
                {
                    var iedereen = new UserChat()
                        { UserName = "Iedereen", IsOnline = true, ProfielImage = Manager.ProductieChat.GroupChatImagePath };
                    Manager.ProductieChat.Gebruikers.Add(iedereen);
                    Manager.ProductieChat.Gebruikers = Manager.ProductieChat.Gebruikers.OrderBy(x => x.UserName).ToList();
                }
                ReloadImages();
                //load eerst de default gebruiker.
                for (int i = 0; i < Manager.ProductieChat.Gebruikers.Count; i++)
                {
                    var user = Manager.ProductieChat.Gebruikers[i];
                    if (user.UserName.ToLower().Trim() != "iedereen")
                    {
                        var acc = Manager.Database.AccountExist(user.UserName);
                        if (!acc)
                        {
                            Manager.ProductieChat.DeleteUser(user);
                            Manager.ProductieChat.Gebruikers.RemoveAt(i--);
                            continue;
                        }
                    }
                    if (_Selected != null &&
                        string.Equals(user.UserName, _Selected, StringComparison.CurrentCultureIgnoreCase))
                        selected = user;

                    var old = toremove.FirstOrDefault(x => x.Equals(user));
                    if (old == null)
                    {
                        xuserlist.BeginUpdate();
                        xuserlist.AddObject(user);
                        xuserlist.EndUpdate();
                    }
                    else
                    {
                        xuserlist.RefreshObject(user);
                        toremove.Remove(old);
                    }
                }

                if (toremove.Count > 0)
                {
                    xuserlist.BeginUpdate();
                    xuserlist.RemoveObjects(toremove);
                    xuserlist.EndUpdate();
                }
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
            if (Manager.ProductieChat?.Chat == null || Manager.LogedInGebruiker == null) return;
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
                    xselecteduserimage.Image = Manager.ProductieChat?.GetProfielImage(user);
                    xselectedusername.Text = user?.UserName;
                    xselecteduserstatus.Text = user == null ? "" : (user.IsOnline ? "Online" : "Offline");
                    xselecteduserstatusimage.Image = user == null
                        ? null
                        : user.IsOnline
                            ? Resources.Online_32
                            : Resources.offline_32x32;
                    if (string.Equals(user?.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var users = Manager.ProductieChat.Gebruikers.Where(x => !string.Equals(Manager.LogedInGebruiker.Username,
                            x.UserName, StringComparison.CurrentCultureIgnoreCase) && !string.Equals("iedereen",
                            x.UserName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        var onlineusers = Manager.ProductieChat.Gebruikers.Where(x => x.IsOnline && !string.Equals(Manager.LogedInGebruiker.Username,
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

        private void UpdateMessage(ProductieChatEntry message, ref bool updated, ref bool added, bool scrolintoview, List<ChatBubble> chats = null)
        {
            if (this.InvokeRequired)
            {
                var xupdated = updated;
                var xadded = added;
                this.Invoke(new MethodInvoker(() => UpdateMessage(message, ref xupdated, ref xadded,scrolintoview, chats)));
                updated = xupdated;
                added = xadded;
                return;
            }
            try
            {
                var messages = chats ?? xchatpanel.Controls.Cast<ChatBubble>().ToList();
                var xent = message;
                bool isme = string.Equals(xent.Afzender.UserName, Manager.ProductieChat.Chat.UserName,
                    StringComparison.CurrentCultureIgnoreCase);
               
                if (!isme && !xent.IsGelezen)
                {
                    xent.IsGelezen = true;
                    Manager.ProductieChat?.UpdateMessage(xent);
                    updated = true;
                }
                var xmsg = isme ? CreateSendMessage(xent) : CreateRecieveMessage(xent);
                var xold = messages.FirstOrDefault(x => x.Message.Equals(xent));
                if (xold != null)
                {
                    xold.SetMessage(xmsg, updated);
                }
                else
                {
                    xchatpanel.SuspendLayout();
                    // 
                    xchatpanel.Controls.Add(xmsg);
                    xchatpanel.ResumeLayout(false);
                    //xchatpanel.Controls.SetChildIndex(xmsg, i);
                    // 
                    added = true;
                    if (scrolintoview)
                    {
                        xchatpanel.Invalidate();
                        xchatpanel.ScrollControlIntoView(xmsg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateMessages(List<ProductieChatEntry> entries, bool scroltoend, UserChat selected = null)
        {
            try
            {
               
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
                        xchatpanel.SuspendLayout();
                        toremove.ForEach(x => xchatpanel.Controls.Remove(x));
                        xchatpanel.ResumeLayout(false);
                    }
                }
                
                var xmessages = xchatpanel.Controls.Cast<ChatBubble>().ToList();
                bool updated = false;
                bool added = false;
                
                for (int i = 0; i < entries.Count; i++)
                {
                    var xent = entries[i];
                    UpdateMessage(xent, ref updated, ref added,false, xmessages);
                }
              
                if (updated)
                {
                    Manager.ProductieChat?.SaveGebruiker(selected);
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
                                Manager.ProductieChat.GetConversation(selected, !string.IsNullOrEmpty(selected.UserName), DateTime.Now.Subtract(TimeSpan.FromDays(30)));
                            if (this.Disposing || this.IsDisposed) return;
                            UpdateMessages(messages, scrolltoend, selected);
                            _selecteduser = selected;
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
            if (Manager.ProductieChat.Chat == null || !Manager.ProductieChat.LoggedIn ||
                string.IsNullOrEmpty(xchattextbox.Text.Trim()) ||
                xchattextbox.Text.Trim().ToLower() == "typ bericht...")
            {
                xchattextbox.Text = xchattextbox.Text.Trim();
                return;
            }
            var selected = SelectedUser();
            if (selected == null) return;
            string ontvangers = string.IsNullOrEmpty(selected.UserName)
                ? string.Join(";", Manager.ProductieChat.Gebruikers.Select(x => x.UserName))
                : selected.UserName;
            string xmessage = CheckProductieLinks(xchattextbox.Text.Trim().WrapText(100, "\n"));
            var xent = Manager.ProductieChat.SendMessage(xmessage, ontvangers);
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
            if (Manager.ProductieChat.Chat == null || !Manager.ProductieChat.LoggedIn) return;
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
                    if (!Manager.ProductieChat.ChangeProfielImage(ofd.FileName))
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
