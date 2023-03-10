using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Mail;
using System.Net;

namespace bookStoreManagetment.ViewModel
{
    public class mailViewModel : BaseViewModel
    {
        #region "mail"
        private ObservableCollection<checkmail> _InventoryList;
        public ObservableCollection<checkmail> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }
        private ObservableCollection<checkmail> _DivInventoryList;
        public ObservableCollection<checkmail> DivInventoryList { get => _DivInventoryList; set { _DivInventoryList = value; OnPropertyChanged(); } }
        private ObservableCollection<Inventory> _InventoryCustomerList;
        public ObservableCollection<Inventory> InventoryCustomerList { get => _InventoryCustomerList; set { _InventoryCustomerList = value; OnPropertyChanged(); } }
        private ObservableCollection<Inventory> _InventoryEmployeeList;
        public ObservableCollection<Inventory> InventoryEmployeeList { get => _InventoryEmployeeList; set { _InventoryEmployeeList = value; OnPropertyChanged(); } }
        public checkmail SelectedItem { get; set; }
        public ICommand LoadMailCommand { get; set; }
        #endregion

        #region "sent mail"
        private ObservableCollection<Inventory> _InventoryListSentMail;
        public ObservableCollection<Inventory> InventoryListSentMail { get => _InventoryListSentMail; set { _InventoryListSentMail = value; OnPropertyChanged(); } }
        private ObservableCollection<Inventory> _DivInventoryListSentMail;
        public ObservableCollection<Inventory> DivInventoryListSentMail { get => _DivInventoryListSentMail; set { _DivInventoryListSentMail = value; OnPropertyChanged(); } }
        public Inventory SelectedItemSentMail { get; set; }
        public ICommand ShowSentMailCommand { get; set; }
        public ICommand btnChitietSentMailCommand { get; set; }

        #endregion

        #region "add, edit, delete mail"
        private ObservableCollection<string> _cbbmailtype;
        public ObservableCollection<String> cbbMailType { get { return _cbbmailtype; } set { _cbbmailtype = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _cbbsenttype;
        public ObservableCollection<String> cbbSentType { get { return _cbbsenttype; } set { _cbbsenttype = value; OnPropertyChanged(); } }
        private DateTime _datesent;
        public DateTime DateSent { get { return _datesent; } set { _datesent = value; OnPropertyChanged(); } }
        public ICommand ShowMailCommand { get; set; }
        public ICommand DetailMailCommand { get; set; }
        public ICommand btnCapnhatClickCommand { get; set; }
        public ICommand btnAddClickCommand { get; set; }
        public ICommand btnAddMailClick { get; set; }
        public ICommand btnDeleteCommand { get; set; }
        public ICommand cbbMailTypeSelectionChanged { get; set; }
        public string textBoxSearchValue { get; set; }
        public bool seen { get; set; }
        private bool _readOnly;
        public bool ReadOnly { get { return _readOnly; } set { _readOnly = value; OnPropertyChanged(); } }
        private bool _enable;
        public bool Enable { get { return _enable; } set { _enable = value; OnPropertyChanged(); } }
        private bool _cbbsenttypeenable;
        public bool ComboBoxSenttypeEnable { get { return _cbbsenttypeenable; } set { _cbbsenttypeenable = value; OnPropertyChanged(); } }
        private string subject;
        public string Subject { get { return subject; } set { subject = value; OnPropertyChanged(); } }
        private string content;
        public string Content { get { return content; } set { content = value; OnPropertyChanged(); } }
        private string sender;
        public string Sender { get { return sender; } set { sender = value; OnPropertyChanged(); Console.WriteLine(sender); } }
        private string mailtype;
        public string Mailtype { get { return mailtype; } set { mailtype = value; OnPropertyChanged(); } }
        private string senttype;
        public string Senttype { get { return senttype; } set { senttype = value; OnPropertyChanged(); } }
        #endregion

        #region "manipulation"
        private Visibility _gridDetailMailVisible;
        public Visibility GridDetailMailVisible { get { return _gridDetailMailVisible; } set { _gridDetailMailVisible = value; OnPropertyChanged(); } }
        private Visibility _gridDataGridVisible;
        public Visibility GridDataGridVisible { get { return _gridDataGridVisible; } set { _gridDataGridVisible = value; OnPropertyChanged(); } }
        private Visibility _gridSentMailVisible;
        public Visibility GridSentMailVisible { get { return _gridSentMailVisible; } set { _gridSentMailVisible = value; OnPropertyChanged(); } }
        private Visibility _gridEditMailVisible;
        public Visibility GridEditMailVisible { get { return _gridEditMailVisible; } set { _gridEditMailVisible = value; OnPropertyChanged(); } }
        public ICommand btnHuyClickCommand { get; set; }


        public ICommand OpenButton { get; set; }
        public ICommand CloseButton { get; set; }
        #endregion

        #region "filter"
        public ICommand toggleClickCommand { get; set; }
        public ICommand searchEngineer { get; set; }
        #endregion

        
        public mailViewModel()
        {
            #region "mail"
            LoadMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridDataGridVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Visible;
                GridSentMailVisible = Visibility.Collapsed;
                GridEditMailVisible = Visibility.Collapsed;
                textBoxSearchValue = "";

                  LoadData();
                
                SearchEngineer(textBoxSearchValue);
                seen = false;
                EnableChange(seen);
              
            });
            ShowMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridDataGridVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Visible;
                GridSentMailVisible = Visibility.Collapsed;
                GridEditMailVisible = Visibility.Collapsed;
            });
            DetailMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                seen = false;
                EnableChange(seen);
                SelectedItem = (p as checkmail);
                if (SelectedItem.Mail != null)
                {
                    Subject = SelectedItem.Mail.subjectMail;
                    Content = SelectedItem.Mail.content;
                    Sender = SelectedItem.Mail.sender;
                    Mailtype = SelectedItem.Mail.typeMail;
                    Console.WriteLine(Mailtype);
                    //MessageBox.Show(Mailtype);
                    Senttype = SelectedItem.Mail.typesent;
                    try
                    {
                        DateSent = (DateTime)SelectedItem.Mail.autosentDate;
                    }
                    catch
                    {
                        DateSent = DateTime.Now.Date;
                    }
                    GridDataGridVisible = Visibility.Collapsed;
                    GridDetailMailVisible = Visibility.Collapsed;
                    GridSentMailVisible = Visibility.Collapsed;
                    GridEditMailVisible = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Kh??ng c?? mail ????? ch???nh s???a");
                }    
            });
            #endregion

            #region "sent mail"
            ShowSentMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridDataGridVisible = Visibility.Collapsed;
                GridDetailMailVisible = Visibility.Visible;
                GridSentMailVisible = Visibility.Visible;
                GridEditMailVisible = Visibility.Collapsed;
            });
            btnChitietSentMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItemSentMail = new Inventory();
                SelectedItemSentMail = (p as Inventory);
                seen = true;
                EnableChange(seen);
                Subject = SelectedItemSentMail.Sentmail.subjectMail;
                Mailtype = SelectedItemSentMail.Sentmail.typeMail;
                //MessageBox.Show(Mailtype);
                Sender = SelectedItemSentMail.Sentmail.sender;
                int id = SelectedItemSentMail.Sentmail.idMail;
                DateSent = DataProvider.Ins.DB.Database.SqlQuery<DateTime>("select autosentDate from mail where idMail=" + id.ToString()).FirstOrDefault();
                Senttype = DataProvider.Ins.DB.Database.SqlQuery<string>("select typesent from mail where idMail=" + id.ToString()).FirstOrDefault();
                var _res = DataProvider.Ins.DB.mails.Where(i => i.idMail == id);
                Content = DataProvider.Ins.DB.Database.SqlQuery<String>("select content from mail where idMail=" + id.ToString()).FirstOrDefault();
               
                seen = true;
                EnableChange(seen);
                GridEditMailVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Collapsed;
                GridDataGridVisible = Visibility.Collapsed;
                GridSentMailVisible = Visibility.Collapsed;
            });
            #endregion

            #region "manipulation"
            btnHuyClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("B???n c?? mu???n tho??t?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GridDetailMailVisible = Visibility.Visible;
                    GridDataGridVisible = Visibility.Visible;
                    GridSentMailVisible = Visibility.Collapsed;
                    GridEditMailVisible = Visibility.Collapsed;
                    LoadData();
                    //settingButtonNextPrev();
                }
            });
            OpenButton = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (p as Button).Visibility = System.Windows.Visibility.Visible;
            });
            CloseButton = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (p as Button).Visibility = System.Windows.Visibility.Collapsed;
            });
            #endregion

            #region "filter"
            toggleClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("B???n c?? mu???n thay ?????i tr???ng th??i? ??i???u n??y c?? th??? s??? l???p l???i vi???c g???i mail?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SelectedItem = (p as checkmail);
                    var result = DataProvider.Ins.DB.mails.SingleOrDefault(i => i.idMail == SelectedItem.Mail.idMail);
                    if (result != null)
                    {
                        if (result.statusMail == "OFF")
                        {
                            result.statusMail = "ON";
                            //MessageBox.Show(SelectedItem.Mail.subjectMail + SelectedItem.Mail.typeMail + SelectedItem.Mail.typesent);
                            autosentmail(SelectedItem, SelectedItem.Mail.typeMail, SelectedItem.Mail.typesent);
                        }
                        else
                            result.statusMail = "OFF";
                        DataProvider.Ins.DB.SaveChanges();
                    }

                }

                LoadData();
            });
            searchEngineer = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SearchEngineer(textBoxSearchValue);
            });
            void SearchEngineer(string query)
            {
                InventoryList = new ObservableCollection<checkmail>();
                var lstMail = DataProvider.Ins.DB.mails.Where(i => (i.subjectMail.Contains(query) || i.typeMail.Contains(query)));
                foreach (var mail in lstMail)
                {
                    checkmail _Inventory = new checkmail();
                    _Inventory.Mail = mail;
                    if (mail.statusMail == "ON")
                        _Inventory.Check = true;
                    else
                        _Inventory.Check = false;
                    InventoryList.Add(_Inventory);
                }
                InventoryListSentMail = new ObservableCollection<Inventory>();
                var lstSentMail = DataProvider.Ins.DB.sentmails.Where(i => (i.subjectMail.Contains(query) || i.typeMail.Contains(query) || i.receiverMail.Contains(query)));
                foreach (var _smail in lstSentMail)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Sentmail = _smail;

                    InventoryListSentMail.Add(_Inventory);
                }
            }
            #endregion

            #region "add,update,delete"
            cbbMailTypeSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    if ((p as ComboBox).SelectedItem != null)
                    {
                        string value = (p as ComboBox).SelectedItem.ToString();
                        if (value == "Sinh nh???t")
                        {
                            ComboBoxSenttypeEnable = false;
                            Senttype = "EVERYYEAR";
                        }
                        else
                        {
                            ComboBoxSenttypeEnable = true;
                        }
                    }
                }
                catch
                {

                }
            });
            btnCapnhatClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem.Mail != null)
                {
                    if (MessageBox.Show("B???n c?? mu???n c???p nh???t?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DateTime now = DateTime.Now.Date;
                        //string query = "update mail set content=N'" + Content +"',typeMail=N'"+Mailtype +"',subjectMail=N'" + Subject + "',sender=N'" + Sender + "',updateDate=N'" + now.ToString() + "' where idMail=" + SelectedItem.Mail.idMail;
                        //DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                        if (check())
                        {
                            var res = DataProvider.Ins.DB.mails.SingleOrDefault(i => i.idMail == SelectedItem.Mail.idMail);
                            if (res != null)
                            {
                                res.content = Content;
                                res.typeMail = Mailtype;
                                res.subjectMail = Subject;
                                res.sender = Sender;
                                res.updateDate = now;
                                res.autosentDate = DateSent;
                                res.typesent = Senttype;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            MessageBox.Show("C???p nh???t th??nh c??ng");
                        }
                        else
                        {
                            MessageBox.Show("Vui l??ng ??i???n ????? th??ng tin");
                        }
                    }
                }
                
            });
            btnAddMailClick = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                clearData();
                seen = false;
                EnableChange(seen);
                GridEditMailVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Collapsed;
                GridDataGridVisible = Visibility.Collapsed;
                GridSentMailVisible = Visibility.Collapsed;
            });
            btnAddClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string query;
                DateTime now = DateTime.Now;
                if (check()) 
                {
                    if (Mailtype.ToLower() != "sinh nh???t")
                    {
                        query = String.Format("insert into mail values (N'{0}',N'{1}', N'{2}', N'{3}', N'S??? d???ng ????? g???i th??ng b??o', N'OFF', N'{4}',N'{5}',N'{6}')", Mailtype, now, Subject, Content, Sender, DateSent, Senttype);
                    }
                    else
                        query = String.Format("insert into mail values (N'{0}',N'{1}', N'{2}', N'{3}', N'S??? d???ng ????? g???i th??ng b??o', N'OFF', N'{4}', null,N'{5}')", Mailtype, now, Subject, Content, Sender, Senttype);

                    try
                    {
                        DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                        MessageBox.Show("Th??m th??nh c??ng");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    clearData();
                    LoadData();
                }
                else
                    MessageBox.Show("Vui l??ng nh???p ????? th??ng tin!!!");

            });
            btnDeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItem = (p as checkmail);
                if (SelectedItem != null && SelectedItem.Mail != null)
                {
                    int id = SelectedItem.Mail.idMail;

                    string sbj = SelectedItem.Mail.subjectMail;

                    if (MessageBox.Show(String.Format("B???n c?? mu???n x??a mail {0}?", sbj), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        string query = "delete from mail where idMail=" + id;
                        DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);

                        LoadData();
                        MessageBox.Show(String.Format("B???n ???? x??a th??nh c??ng mail {0}", sbj));

                    }
                }
                else
                {
                    MessageBox.Show("Kh??ng c?? mail ????? x??a");
                }    
            });
            void EnableChange(bool seen)
            {

                ReadOnly = seen;
                Enable = !seen;

            }
            #endregion
            bool check()
            {
                if (Mailtype != null && Mailtype != "" && Subject != "" && Subject != null && Content != "" && Content != null && Sender != null && Sender != "" && Senttype != null && Senttype != "")
                    return true;
                else
                    return false;
            }

            void LoadData()
            {
                DivInventoryList = new ObservableCollection<checkmail>();
                DivInventoryListSentMail = new ObservableCollection<Inventory>();
                InventoryCustomerList = new ObservableCollection<Inventory>();
                InventoryEmployeeList = new ObservableCollection<Inventory>();
                InventoryListSentMail = new ObservableCollection<Inventory>();
                var lstCus = DataProvider.Ins.DB.custommers;
                foreach (var cus in lstCus)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Custommer = cus;
                    InventoryCustomerList.Add(_Inventory);
                }
                var lstEmp = DataProvider.Ins.DB.employees;
                foreach (var emp in lstEmp)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Employee = emp;
                    InventoryEmployeeList.Add(_Inventory);
                }
                var lstsentMail = DataProvider.Ins.DB.sentmails;
                foreach (var item in lstsentMail)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Sentmail = item;
                    InventoryListSentMail.Add(_Inventory);
                }
                InventoryList = new ObservableCollection<checkmail>();


                var lstMail = DataProvider.Ins.DB.mails;
                foreach (var mail in lstMail)
                {
                    checkmail _Inventory = new checkmail();
                    _Inventory.Mail = mail;
                    if (mail.statusMail == "ON")
                        _Inventory.Check = true;
                    else
                        _Inventory.Check = false;
                    InventoryList.Add(_Inventory);
                }
                cbbMailType = new ObservableCollection<string>();
                cbbMailType.Add("H???i h???p");
                cbbMailType.Add("Khuy???n m??i");
                cbbMailType.Add("Sinh nh???t");
                cbbSentType = new ObservableCollection<string>();
                cbbSentType = new ObservableCollection<string>();
                cbbSentType.Add("ONEDAY");
                cbbSentType.Add("EVERYWEEK");
                cbbSentType.Add("EVERYMONTH");
                cbbSentType.Add("EVERYYEAR");
                DateSent = DateTime.Now.Date;

            }
      
            void clearData()
            {
                Subject = "";
                Content = "";
                Sender = "";
                Mailtype = "";
            }

            void sentmail(string from, string pass, string to, string subject, string body, int idmail, string _type)
            {
                //MessageBox.Show("??ang g???i ?????n " + to);
                try
                {
                    SmtpClient clientDetails = new SmtpClient();
                    clientDetails.Port = 587;
                    clientDetails.Host = "smtp.gmail.com";
                    clientDetails.EnableSsl = true;
                    clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;

                    clientDetails.Credentials = new NetworkCredential(from, pass);


                    MailMessage mailDetails = new MailMessage(from, to, subject, body);

                    clientDetails.Send(mailDetails);
                    //MessageBox.Show("???? g???i cho " + to);
                    sentmail _tmpsentmail = new sentmail();
                    _tmpsentmail.dateSent = DateTime.Now;
                    _tmpsentmail.idMail = idmail;
                    _tmpsentmail.mailStatus = "???? g???i";
                    _tmpsentmail.receiverMail = to;
                    _tmpsentmail.sender = from;
                    _tmpsentmail.subjectMail = subject;
                    _tmpsentmail.typeMail = _type;
                    DataProvider.Ins.DB.sentmails.Add(_tmpsentmail);
                    DataProvider.Ins.DB.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " to " + to);
                }
            }
            void autosentmail(checkmail selected, string typemail, string sentype)
            {
                DateTime now = DateTime.Now.Date;
                int nowday = now.Day;
                int nowmonth = now.Month;
                int nowyear = now.Year;
                string from = "dinhduyminh09082001@gmail.com";
                string pass = "cajurawrvjvlyein";

                DayOfWeek nowdayofweek = now.DayOfWeek;
                //MessageBox.Show(nowdayofweek.ToString());
                List<custommer> sentcus = new List<custommer>();
                List<employee> sentemp = new List<employee>();
                sentcus = DataProvider.Ins.DB.custommers.ToList();
                sentemp = DataProvider.Ins.DB.employees.ToList();

                if (typemail == "Sinh nh???t")
                {
                    foreach (var item in sentcus)
                    {
                        if (nowday == item.dateOfBirth.Day && nowmonth == item.dateOfBirth.Month)
                        {
                            sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                        }
                    }
                }
                else
                {
                    if (typemail == "Khuy???n m??i")
                    {

                        switch (sentype.Replace(" ", ""))
                        {
                            case "ONEDAY":
                                foreach (var item in sentcus)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month && nowyear == selected.Mail.autosentDate.Value.Year)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }

                                break;
                            case "EVERYWEEK":
                                foreach (var item in sentcus)
                                {
                                    if (nowdayofweek == selected.Mail.autosentDate.Value.DayOfWeek)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }

                                break;
                            case "EVERYMONTH":
                                foreach (var item in sentcus)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }

                                break;
                            case "EVERYYEAR":
                                foreach (var item in sentcus)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }

                                break;
                        }
                    }
                    else
                    {

                        switch (sentype.Replace(" ", ""))
                        {

                            case "ONEDAY":
                                foreach (var item in sentemp)
                                {

                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month && nowyear == selected.Mail.autosentDate.Value.Year)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }

                                break;
                            case "EVERYWEEK":
                                foreach (var item in sentemp)
                                {
                                    if (nowdayofweek == selected.Mail.autosentDate.Value.DayOfWeek)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }

                                break;
                            case "EVERYMONTH":
                                foreach (var item in sentemp)
                                {

                                    if (nowday == selected.Mail.autosentDate.Value.Day)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                        //MessageBox.Show("???? g???i");
                                    }
                                }

                                break;
                            case "EVERYYEAR":
                                foreach (var item in sentemp)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content, selected.Mail.idMail, selected.Mail.typeMail);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
