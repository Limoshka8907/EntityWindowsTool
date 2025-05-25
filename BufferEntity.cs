using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace EntityWindowsTool
{
    public class BufferEntity
    {
        public void Initialize(int id) { }
        public string GetStatus() { return string.Empty; }
        public bool SetConfiguration(string configName, string value) { return true; }
        public int CalculateSum(int a, int b) { return 0; }
        public void SaveData(object data) { }
        public object LoadData(string key) { return null; }
        public double GetTemperature() { return 0.0; }
        public void Reset() { }
        public List<string> GetItems() { return new List<string>(); }
        public DateTime GetCurrentTime() { return DateTime.Now; }

        public void SendMessage(string recipient, string message) { }
        public bool Authenticate(string username, string password) { return false; }
        public void Disconnect() { }
        public int GenerateId() { return 0; }
        public bool UpdateRecord(int recordId, object newData) { return true; }
        public string FetchDataById(int id) { return string.Empty; }
        public void LogEvent(string eventName, string details) { }
        public float CalculateAverage(float[] values) { return 0; }
        public void ExportReport(string filename) { }
        public int GetNumberOfUsers() { return 0; }

        public void AddItem(string itemName, int quantity) { }
        public bool RemoveItem(int itemId) { return false; }
        public string GetItemDescription(int itemId) { return string.Empty; }
        public void SendNotification(string userId, string message) { }
        public bool IsConnected() { return false; }
        public void Terminate() { }
        public double ConvertToDouble(object value) { return 0; }
        public void UpdateStatus(string status) { }
        public string[] GetSupportedLanguages() { return new string[0]; }
        public long GetTimestamp() { return 0; }

        private static string Text1 = "Словесное описание алгоритма\r\n1.\tПользователь заходит в систему\r\n2.\tАвторизация\r\n2.1.\tАвторизация в роли менеджера\r\n2.2.\tАвторизация в роли покупателя\r\n2.3.\tАвторизация в роли руководства\r\n2.4.\tАвторизация в роли физ. лица\r\n2.5.\tАвторизация в роли кладовщика\r\n3.\tОкно менеджера\r\n3.1.\tМенеджер просматривает информацию о менеджерах\r\n3.2.\tМенеджер просматривает информацию о пользователях\r\n3.3.\tПросматривать и изменять информацию своего профиля\r\n3.4.\tАдминистрировать заказы\r\n4.\tОкно покупателя\r\n4.1.\tПросмотр заказов\r\n4.2.\tПросматривать и изменять информацию своего профиля\r\n4.3.\tОставлять заказы на оптовую или розничную покупку\r\n5.\tВыход из системы\r\n\r\n\r\n";

        string multiLineStart = "public partial class MainWindow : Window\r\n {\r\n     private readonly AppDbContext _context;\r\n     public MainWindow()\r\n     {\r\n         InitializeComponent();\r\n         _context = new AppDbContext();\r\n         AppDbContextStorage.Context = _context;\r\n         LoadData();\r\n     }\r\n     //<DockPanel>\r\n     //        <TextBlock DockPanel.Dock=\"Right\" Text= \"10%\" ></ TextBlock >\r\n     //        < StackPanel DockPanel.Dock= \"Left\" >\r\n     //            < TextBlock Text= \"Данные\" ></ TextBlock >\r\n     //            < TextBlock Text= \"Данные\" ></ TextBlock >\r\n     //            < TextBlock Text= \"Данные\" ></ TextBlock >\r\n     //            < TextBlock Text= \"Данные\" ></ TextBlock >\r\n     //        </ StackPanel >\r\n     //    </ DockPanel >\r\n\r\n     private void LoadData()\r\n     {\r\n         try\r\n         {\r\n             foreach (var partner in _context.Partners.ToList())\r\n             {\r\n                 int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;\r\n                 string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;\r\n\r\n                 int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;\r\n                 string directorName = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Name;\r\n                 string directorPhone = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Phone;\r\n\r\n                 var dockPanel = new DockPanel();\r\n\r\n                 dockPanel.Tag = partner;\r\n                 dockPanel.MouseLeftButtonDown += DockPanelClick;\r\n\r\n                 var textblock1 = new TextBlock() { Text = $\"10%\" };\r\n\r\n                 DockPanel.SetDock(textblock1, Dock.Right);\r\n                 dockPanel.Children.Add(textblock1);\r\n\r\n                 var stackPanel = new StackPanel();\r\n\r\n                 DockPanel.SetDock(stackPanel, Dock.Left);\r\n                 dockPanel.Children.Add(stackPanel);\r\n\r\n                 var textblock2 = new TextBlock() { Text = $\"{partnerTypeName} | {partner.Name}\" };\r\n                 var textblock3 = new TextBlock() { Text = $\"{directorName}\" };\r\n                 var textblock4 = new TextBlock() { Text = $\"+7 {directorPhone}\" };\r\n                 var textblock5 = new TextBlock() { Text = $\"{partner.Rating}\" };\r\n\r\n                 stackPanel.Children.Add(textblock2);\r\n                 stackPanel.Children.Add(textblock3);\r\n                 stackPanel.Children.Add(textblock4);\r\n                 stackPanel.Children.Add(textblock5);\r\n\r\n                 MainPanel.Children.Add(dockPanel);\r\n             }\r\n\r\n\r\n         }\r\n         catch (Exception ex)\r\n         {\r\n             MessageBox.Show($\"Внимание, ошибка загрузки данных. {ex.Message}\");\r\n         }\r\n     }\r\n\r\n     private void DockPanelClick(object sender, MouseButtonEventArgs e)\r\n     {\r\n         try\r\n         {\r\n             var dockPanel = sender as DockPanel;\r\n             if (dockPanel != null)\r\n             {\r\n                 var partner = dockPanel.Tag as Partner;\r\n\r\n                 if (partner != null)\r\n                 {\r\n                     PartnerStorage.Partner = partner;\r\n                     new AddOrUpdateWindow().Show();\r\n                     this.Close();\r\n                 }\r\n             }\r\n         }\r\n         catch (Exception ex)\r\n         {\r\n             MessageBox.Show($\"Внимание, ошибка загрузки формы. {ex.Message}\");\r\n\r\n         }\r\n     }\r\n\r\n     private void BtnAdd_Click(object sender, RoutedEventArgs e)\r\n     {\r\n         new AddOrUpdateWindow().Show();\r\n         PartnerStorage.Partner = null;\r\n         this.Close();\r\n     }\r\n }";

        public void GetText1()
        {
            string textToCopy = Text1;

            if (!string.IsNullOrWhiteSpace(textToCopy))
            {
                try
                {
                    System.Windows.Forms.Clipboard.SetDataObject(textToCopy, true);
                    Console.WriteLine("Текст скопирован в буфер обмена!", "Успех");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось скопировать текст. Ошибка: {ex.Message}", "Ошибка");
                }
            }
            else
            {
                Console.WriteLine("Пустой текст нельзя скопировать.", "Ошибка");
            }
        }
    }

    //public partial class MainWindow : Window
    //{
    //    private readonly AppDbContext _context;
    //    public MainWindow()
    //    {
    //        InitializeComponent();
    //        _context = new AppDbContext();
    //        AppDbContextStorage.Context = _context;
    //        LoadData();
    //    }

    //    private void LoadData()
    //    {
    //        try
    //        {
    //            foreach (var partner in _context.Partners.ToList())
    //            {
    //                int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;
    //                string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;

    //                int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;
    //                string directorName = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Name;
    //                string directorPhone = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Phone;

    //                var dockPanel = new DockPanel();

    //                dockPanel.Tag = partner;
    //                dockPanel.MouseLeftButtonDown += DockPanelClick;

    //                var textblock1 = new TextBlock() { Text = $"10%" };

    //                DockPanel.SetDock(textblock1, Dock.Right);
    //                dockPanel.Children.Add(textblock1);

    //                var stackPanel = new StackPanel();

    //                DockPanel.SetDock(stackPanel, Dock.Left);
    //                dockPanel.Children.Add(stackPanel);

    //                var textblock2 = new TextBlock() { Text = $"{partnerTypeName} | {partner.Name}" };
    //                var textblock3 = new TextBlock() { Text = $"{directorName}" };
    //                var textblock4 = new TextBlock() { Text = $"+7 {directorPhone}" };
    //                var textblock5 = new TextBlock() { Text = $"{partner.Rating}" };

    //                stackPanel.Children.Add(textblock2);
    //                stackPanel.Children.Add(textblock3);
    //                stackPanel.Children.Add(textblock4);
    //                stackPanel.Children.Add(textblock5);

    //                MainPanel.Children.Add(dockPanel);
    //            }


    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Внимание, ошибка загрузки данных. {ex.Message}");
    //        }
    //    }

    //    private void DockPanelClick(object sender, MouseButtonEventArgs e)
    //    {
    //        try
    //        {
    //            var dockPanel = sender as DockPanel;
    //            if (dockPanel != null)
    //            {
    //                var partner = dockPanel.Tag as Partner;

    //                if (partner != null)
    //                {
    //                    PartnerStorage.Partner = partner;
    //                    new AddOrUpdateWindow().Show();
    //                    this.Close();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Внимание, ошибка загрузки формы. {ex.Message}");

    //        }
    //    }

    //    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    //    {
    //        new AddOrUpdateWindow().Show();
    //        PartnerStorage.Partner = null;
    //        this.Close();
    //    }
    //}

    //public partial class AddOrUpdateWindow : Window
    //{
    //    private readonly AppDbContext _context;
    //    private readonly Partner _partner;
    //    public AddOrUpdateWindow()
    //    {
    //        InitializeComponent();
    //        _context = AppDbContextStorage.Context;
    //        if (PartnerStorage.Partner != null)
    //        {
    //            _partner = PartnerStorage.Partner;
    //            LoadData();
    //        }
    //        FillComboBox();
    //    }

    //    private void FillComboBox()
    //    {
    //        try
    //        {
    //            ComboBoxType.Items.Clear();
    //            foreach (var item in _context.PartnerTypes.ToList())
    //            {
    //                ComboBoxType.Items.Add(item.Name);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Загрузка данных в выпадающий список прошла неудачно, проверьте подключение к БД. {ex.Message}");
    //        }
    //    }

    //    private void LoadData()
    //    {
    //        try
    //        {
    //            var partner = _partner;
    //            int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;
    //            string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;

    //            int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;
    //            string directorName = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Name;
    //            string directorPhone = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Phone;
    //            string directorEmail = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Email;

    //            InnTextBox.Text = partner.INN.ToString();
    //            NameTextBox.Text = partner.Name;
    //            RatingTextBox.Text = partner.Rating.ToString();
    //            AddressTextBox.Text = partner.Address.ToString();

    //            ComboBoxType.SelectedItem = partnerTypeName;

    //            DirectorNameTextBox.Text = directorName;
    //            PhoneTextBox.Text = directorPhone;
    //            EmailTextBox.Text = directorEmail;

    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Загрузка данных прошла неудачно, проверьте подключение к БД. {ex.Message}");
    //        }
    //    }

    //    private void SaveBtn_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (
    //            !string.IsNullOrEmpty(NameTextBox.Text)
    //            ||
    //            ComboBoxType.SelectedItem != null
    //            ||
    //            !string.IsNullOrEmpty(DirectorNameTextBox.Text)
    //            ||
    //            !string.IsNullOrEmpty(PhoneTextBox.Text)
    //            ||
    //            !string.IsNullOrEmpty(RatingTextBox.Text)
    //            ||
    //            !string.IsNullOrEmpty(AddressTextBox.Text)
    //            ||
    //            !string.IsNullOrEmpty(EmailTextBox.Text)
    //            )
    //        {
    //            try
    //            {
    //                if (_partner != null)
    //                {
    //                    var partner = _partner;
    //                    int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;
    //                    string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;

    //                    int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;
    //                    string directorName = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Name;
    //                    string directorPhone = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Phone;
    //                    string directorEmail = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Email;

    //                    partner.Name = NameTextBox.Text;
    //                    partner.Rating = int.Parse(RatingTextBox.Text);
    //                    partner.Address = AddressTextBox.Text;

    //                    _context.Partners.AddOrUpdate(partner);
    //                    _context.SaveChanges();


    //                    string newTypeName = ComboBoxType.SelectedItem.ToString();
    //                    var partnerTypePartnerOld = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID);
    //                    var partnerType = _context.PartnerTypes.FirstOrDefault(x => x.Name == newTypeName);

    //                    _context.PartnerTypePartners.Remove(partnerTypePartnerOld);
    //                    _context.SaveChanges();
    //                    var partnerTypePartnerNew = new PartnerTypePartner() { PartnerTypePartnersID = new Random().Next(100, 10000000), PartnerID = partner.PartnerID, PartnerTypeID = partnerType.PartnerTypeID };
    //                    _context.PartnerTypePartners.Add(partnerTypePartnerNew);
    //                    _context.SaveChanges();


    //                    var director = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId);
    //                    director.Name = DirectorNameTextBox.Text;
    //                    director.Phone = PhoneTextBox.Text;
    //                    director.Email = EmailTextBox.Text;
    //                    _context.Directors.AddOrUpdate(director);
    //                    _context.SaveChanges();

    //                    MessageBox.Show("Успешное сохранение данных!");

    //                    new MainWindow().Show();
    //                    this.Close();
    //                }
    //                else
    //                {
    //                    var partner = new Partner()
    //                    {
    //                        PartnerID = new Random().Next(100, 10000000),
    //                        Address = AddressTextBox.Text,
    //                        Name = NameTextBox.Text,
    //                        INN = Int32.Parse(InnTextBox.Text),
    //                        Rating = int.Parse(RatingTextBox.Text)
    //                    };

    //                    _context.Partners.Add(partner);
    //                    _context.SaveChanges();

    //                    var director = new Director()
    //                    {
    //                        DirectorID = new Random().Next(100, 10000000),
    //                        Name = DirectorNameTextBox.Text,
    //                        Email = EmailTextBox.Text,
    //                        Phone = PhoneTextBox.Text
    //                    };
    //                    _context.Directors.Add(director);
    //                    _context.SaveChanges();

    //                    var partnerTypePartner = new PartnerTypePartner()
    //                    {
    //                        PartnerTypePartnersID = new Random().Next(100, 10000000),
    //                        PartnerTypeID = ComboBoxType.SelectedIndex + 1,
    //                        PartnerID = partner.PartnerID

    //                    };
    //                    _context.PartnerTypePartners.Add(partnerTypePartner);
    //                    _context.SaveChanges();

    //                    var partnerDirector = new PartnerDirector()
    //                    {
    //                        PartnerDirectorID = new Random().Next(100, 10000000),
    //                        DirectorID = director.DirectorID,
    //                        PartnerID = partner.PartnerID

    //                    };
    //                    _context.PartnerDirectors.Add(partnerDirector);
    //                    _context.SaveChanges();


    //                    MessageBox.Show("Успешное сохранение данных!");

    //                    new MainWindow().Show();
    //                    this.Close();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show($"Загрузка данных прошла неудачно, проверьте подключение к БД. {ex.Message}");
    //            }
    //        }

    //    }

    //    private void ExitBtn_Click(object sender, RoutedEventArgs e)
    //    {
    //        new MainWindow().Show();
    //        this.Close();
    //    }
    //}

}
