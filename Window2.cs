using System;
using System.Windows.Forms;

namespace EntityWindowsTool
{
    public partial class MainWindow
    {
        private readonly AppDbContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            AppDbContextStorage.Context = _context;
            //var thing = new EntityWindowsTool.BufferEntity();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                foreach (var partner in _context.Partners.ToList())
                {
                    int partnerTypePartnerID = _context.PartnerTypePartners.FirstOrDefault(x => x.PartnerID == partner.PartnerID).PartnerTypeID;
                    string partnerTypeName = _context.PartnerTypes.FirstOrDefault(x => x.PartnerTypeID == partnerTypePartnerID).Name;

                    int directorId = _context.PartnerDirectors.FirstOrDefault(x => x.PartnerID == partner.PartnerID).DirectorID;
                    string directorName = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Name;
                    string directorPhone = _context.Directors.FirstOrDefault(x => x.DirectorID == directorId).Phone;

                    var dockPanel = new DockPanel();

                    dockPanel.Tag = partner;
                    dockPanel.MouseLeftButtonDown += DockPanelClick;

                    var textblock1 = new TextBlock() { Text = $"10%" };

                    DockPanel.SetDock(textblock1, Dock.Right);
                    dockPanel.Children.Add(textblock1);

                    var stackPanel = new StackPanel();

                    DockPanel.SetDock(stackPanel, Dock.Left);
                    dockPanel.Children.Add(stackPanel);

                    var textblock2 = new TextBlock() { Text = $"{partnerTypeName} | {partner.Name}" };
                    var textblock3 = new TextBlock() { Text = $"{directorName}" };
                    var textblock4 = new TextBlock() { Text = $"+7 {directorPhone}" };
                    var textblock5 = new TextBlock() { Text = $"{partner.Rating}" };

                    stackPanel.Children.Add(textblock2);
                    stackPanel.Children.Add(textblock3);
                    stackPanel.Children.Add(textblock4);
                    stackPanel.Children.Add(textblock5);

                    MainPanel.Children.Add(dockPanel);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Внимание, ошибка загрузки данных. {ex.Message}");
            }
        }

        private void DockPanelClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dockPanel = sender as DockPanel;
                if (dockPanel != null)
                {
                    var partner = dockPanel.Tag as Partner;

                    if (partner != null)
                    {
                        PartnerStorage.Partner = partner;
                        new AddOrUpdateWindow().Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Внимание, ошибка загрузки формы. {ex.Message}");

            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddOrUpdateWindow().Show();
            PartnerStorage.Partner = null;
            this.Close();
        }
    }

}