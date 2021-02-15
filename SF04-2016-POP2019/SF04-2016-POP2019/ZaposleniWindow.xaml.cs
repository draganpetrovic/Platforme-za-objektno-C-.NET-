using SF04_2016_POP2019.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SF04_2016_POP2019
{
    /// <summary>
    /// Interaction logic for ZaposleniWindow.xaml
    /// </summary>
    public partial class ZaposleniWindow : Window
    {
        ICollectionView view;

        public ZaposleniWindow()
        {
            InitializeComponent();
            InitalizeView();
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.Zaposleni);
            dgZaposleni.ItemsSource = view;
        }

        private void dgZaposleni_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active"))
                e.Column.Visibility = Visibility.Collapsed;
        }

    }
}
