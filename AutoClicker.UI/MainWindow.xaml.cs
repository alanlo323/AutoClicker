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
using AutoClicker.Runtime.Core;
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
            _scripts = [new TestScript(), new MabinogiScript()];
            listBoxScriptList.ItemsSource = _scripts;
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
            List<MarcoEvent> events = [];
            events.AddRange(((BaseScipt)listBoxScriptList.SelectedItem).GetAllMarcoEvents());
            listBoxScriptContect.ItemsSource = events;
            listBoxProperties.ItemsSource = null;
        }

        private void listBoxScriptContect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            if (listbox.SelectedItem != null && listbox.SelectedItem is MarcoEvent marcoEvent)
            {
                List<MarcoParam> dataSource = [
                    new()
                    {
                        RefObject = marcoEvent,
                        PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.Name))
                    },
                    new()
                    {
                        RefObject = marcoEvent,
                        PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.Repeat))
                    },
                    new()
                    {
                        RefObject = marcoEvent,
                        PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.DelayBefore))
                    },
                    new()
                    {
                        RefObject = marcoEvent,
                        PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.DelayAfter))
                    },
                ];

                switch (marcoEvent.EventType)
                {
                    case MarcoEvent.MarcoEventType.EmptyEvent:
                        break;
                    case MarcoEvent.MarcoEventType.MouseMoveEvent:
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.MouseMoveX))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.MouseMoveY))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.RefKey))
                        });
                        break;
                    case MarcoEvent.MarcoEventType.MouseKeyEvent:
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.MouseKey))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.KeyEvent))
                        });
                        break;
                    case MarcoEvent.MarcoEventType.KeyboardEvent:
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.KeyboardKey))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.KeyEvent))
                        });
                        break;
                    case MarcoEvent.MarcoEventType.FocusWindow:
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.WindowName))
                        });
                        break;
                    case MarcoEvent.MarcoEventType.FindImage:
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.ImageFilePath))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.ImageMinSimilarity))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent.ImageSearchingArea,
                            PropertyInfo = marcoEvent.ImageSearchingArea.GetType().GetProperty(nameof(System.Drawing.Rectangle.X))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent.ImageSearchingArea,
                            PropertyInfo = marcoEvent.ImageSearchingArea.GetType().GetProperty(nameof(System.Drawing.Rectangle.Y))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent.ImageSearchingArea,
                            PropertyInfo = marcoEvent.ImageSearchingArea.GetType().GetProperty(nameof(System.Drawing.Rectangle.Width))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent.ImageSearchingArea,
                            PropertyInfo = marcoEvent.ImageSearchingArea.GetType().GetProperty(nameof(System.Drawing.Rectangle.Height))
                        });
                        dataSource.Add(new()
                        {
                            RefObject = marcoEvent,
                            PropertyInfo = marcoEvent.GetType().GetProperty(nameof(MarcoEvent.ResultKey))
                        });
                        break;
                    default:
                        break;
                }

                listBoxProperties.ItemsSource = dataSource;
            }
        }
    }
}