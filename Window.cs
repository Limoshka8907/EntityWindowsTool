// Добавляем отсутствующее пространство имен для Entity Framework (заглушка)
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntityWindowsTool
{
    // Исправляем наследование: Window -> Form
    public partial class AddOrUpdateWindow : Form
    {
        // Имитируем компоненты UI
        public TextBox InnTextBox = new TextBox();
        public TextBox NameTextBox = new TextBox();
        public TextBox RatingTextBox = new TextBox();
        public TextBox AddressTextBox = new TextBox();

        public ComboBox ComboBoxType = new ComboBox();

        public TextBox DirectorNameTextBox = new TextBox();
        public TextBox PhoneTextBox = new TextBox();
        public TextBox EmailTextBox = new TextBox();

        public Button SaveBtn = new Button();
        public Button ExitBtn = new Button();

        // Ваш контекст и поля
        private readonly AppDbContext _context;
        private readonly Partner _partner;

        public AddOrUpdateWindow()
        {
            InitializeComponent();

            // Инициализация контекста (заглушка для AppDbContextStorage)
            _context = AppDbContextStorage.Context;

            // Имитация PartnerStorage
            Partner partnerFromStorage = null; // или считать его так
            // например:
            // partnerFromStorage = PartnerStorage.Partner;

            if (partnerFromStorage != null)
            {
                _partner = partnerFromStorage;
                LoadData();
            }
            FillComboBox();

            // Подписываемся на события
            SaveBtn.Click += SaveBtn_Click;
            ExitBtn.Click += ExitBtn_Click;
        }

        private void FillComboBox()
        {
            try
            {
                ComboBoxType.Items.Clear();
                foreach (var item in _context.PartnerTypes.ToList())
                {
                    ComboBoxType.Items.Add(item.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загрузка данных в выпадающий список прошла неудачно, проверьте подключение к БД. {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                var partner = _partner;
                int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;
                string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;

                int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;
                var director = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId);

                string directorName = director?.Name ?? "";
                string directorPhone = director?.Phone ?? "";
                string directorEmail = director?.Email ?? "";

                InnTextBox.Text = partner.INN.ToString();
                NameTextBox.Text = partner.Name;
                RatingTextBox.Text = partner.Rating.ToString();
                AddressTextBox.Text = partner.Address.ToString();

                ComboBoxType.SelectedItem = partnerTypeName;

                DirectorNameTextBox.Text = directorName;
                PhoneTextBox.Text = directorPhone;
                EmailTextBox.Text = directorEmail;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загрузка данных прошла неудачно, проверьте подключение к БД. {ex.Message}");
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (
                !string.IsNullOrEmpty(NameTextBox.Text) &&
                ComboBoxType.SelectedItem != null &&
                !string.IsNullOrEmpty(DirectorNameTextBox.Text) &&
                !string.IsNullOrEmpty(PhoneTextBox.Text) &&
                !string.IsNullOrEmpty(RatingTextBox.Text) &&
                !string.IsNullOrEmpty(AddressTextBox.Text) &&
                !string.IsNullOrEmpty(EmailTextBox.Text)
                )
            {
                try
                {
                    if (_partner != null)
                    {
                        // Обновление существующего партнера
                        var partner = _partner;
                        int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;
                        string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;

                        int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;
                        var director = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId);

                        // Обновляем данные
                        partner.Name = NameTextBox.Text;
                        partner.Rating = int.Parse(RatingTextBox.Text);
                        partner.Address = AddressTextBox.Text;

                        //_context.Partners.AddOrUpdate(partner);
                        _context.SaveChanges();

                        // Обновляем тип
                        string newTypeName = ComboBoxType.SelectedItem.ToString();
                        var partnerTypePartnerOld = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID);
                        var partnerType = _context.PartnerTypes.FirstOrDefault(x => x.Name == newTypeName);

                        _context.PartnerTypePartners.Remove(partnerTypePartnerOld);
                        _context.SaveChanges();
                        var partnerTypePartnerNew = new PartnerTypePartner()
                        {
                            PartnerTypePartnersID = new Random().Next(100, 10000000),
                            PartnerID = partner.PartnerID,
                            PartnerTypeID = partnerType.PartnerTypeID
                        };
                        _context.PartnerTypePartners.Add(partnerTypePartnerNew);
                        _context.SaveChanges();

                        if (director != null)
                        {
                            director.Name = DirectorNameTextBox.Text;
                            director.Phone = PhoneTextBox.Text;
                            director.Email = EmailTextBox.Text;
                            //_context.Directors.AddOrUpdate(director);
                            _context.SaveChanges();
                        }

                        MessageBox.Show("Успешное сохранение данных!");
                        // Заглушка для открытия главного окна
                        // new MainWindow().Show();
                        // this.Close();
                    }
                    else
                    {
                        // Логика добавления нового
                        var partner = new Partner()
                        {
                            PartnerID = new Random().Next(100, 10000000),
                            Address = AddressTextBox.Text,
                            Name = NameTextBox.Text,
                            INN = int.Parse(InnTextBox.Text),
                            Rating = int.Parse(RatingTextBox.Text)
                        };
                        _context.Partners.Add(partner);
                        _context.SaveChanges();

                        var director = new Director()
                        {
                            DirectorID = new Random().Next(100, 10000000),
                            Name = DirectorNameTextBox.Text,
                            Email = EmailTextBox.Text,
                            Phone = PhoneTextBox.Text
                        };
                        _context.Directors.Add(director);
                        _context.SaveChanges();

                        var partnerTypePartner = new PartnerTypePartner()
                        {
                            PartnerTypePartnersID = new Random().Next(100, 10000000),
                            PartnerTypeID = ComboBoxType.SelectedIndex + 1, // Учитывая индекс
                            PartnerID = partner.PartnerID
                        };
                        _context.PartnerTypePartners.Add(partnerTypePartner);
                        _context.SaveChanges();

                        var partnerDirector = new PartnerDirector()
                        {
                            PartnerDirectorID = new Random().Next(100, 10000000),
                            DirectorID = director.DirectorID,
                            PartnerID = partner.PartnerID
                        };
                        _context.PartnerDirectors.Add(partnerDirector);
                        _context.SaveChanges();

                        MessageBox.Show("Успешное сохранение данных!");
                        // new MainWindow().Show();
                        // this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                }
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            // new MainWindow().Show();
            // this.Close();
        }

        // Дополнительные классы-заглушки
        public class TextBox
        {
            public string Text { get; set; }
        }
        public class ComboBox
        {
            public List<string> Items = new List<string>();
            public object SelectedItem { get; set; }
            public int SelectedIndex { get; set; }
        }
        public class Button
        {
            public event EventHandler Click;
        }

        private void InitializeComponent()
        {
            // Заглушка для метода
        }
    }

    // Заглушки для классов базы данных
    public class Partner
    {
        public int PartnerID { get; set; }
        public int INN { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Address { get; set; }
    }

    public class Director
    {
        public int DirectorID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class PartnerType
    {
        public int PartnerTypeID { get; set; }
        public string Name { get; set; }
    }

    public class PartnerTypePartner
    {
        public int PartnerTypePartnersID { get; set; }
        public int PartnerID { get; set; }
        public int PartnerTypeID { get; set; }
    }

    public class PartnerDirector
    {
        public int PartnerDirectorID { get; set; }
        public int DirectorID { get; set; }
        public int PartnerID { get; set; }
    }

    // Заглушка для контекста базы данных
    public class AppDbContext : DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<PartnerType> PartnerTypes { get; set; }
        public DbSet<PartnerTypePartner> PartnerTypePartners { get; set; }
        public DbSet<PartnerDirector> PartnerDirectors { get; set; }

        public void AddOrUpdate<T>(T entity) { }
    }

    // Заглушка для AppDbContextStorage
    public static class AppDbContextStorage
    {
        public static AppDbContext Context => new AppDbContext();
    }
}

// Заглушка для расширения Entity Framework
namespace Microsoft.EntityFrameworkCore
{
    public class DbContext
    {
        public DbSet<T> Set<T>() where T : class => null;
        public void Add<T>(T entity) where T : class { }
        public void Remove<T>(T entity) where T : class { }
        public int SaveChanges() => 0;
    }

    public class DbSet<T> where T : class
    {
        public T FirstOrDefault(Func<T, bool> predicate) => default;
        public List<T> ToList() => new List<T>();
        public void Add(T entity) { }
        public void Remove(T entity) { }
    }
}