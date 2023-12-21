using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoClicker.Runtime;
using AutoClicker.UI.Script;
using AutoClicker.UI.Script.Sample;

namespace AutoClicker.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BaseScipt> _scripts = [];
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            listBoxScriptList.ItemsSource = new List<BaseScipt>() { new TestScript(), new MabinogiScript() };
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxScriptList.SelectedItem != null && listBoxScriptList.SelectedItem is BaseScipt script)
            {
                try
                {
                    MarcoRuntime runtime = new();
                    runtime.RunMarco(script.MarcoEvents);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Execution failed", $"{ex}");
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
        }

        private void listBoxScriptList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxScriptContect.ItemsSource = ((BaseScipt)listBoxScriptList.SelectedItem).MarcoEvents;
        }
    }
}