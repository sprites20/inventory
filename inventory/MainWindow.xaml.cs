using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;


namespace inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        internal static class ConsoleAllocator
        {
            [DllImport(@"kernel32.dll", SetLastError = true)]
            static extern bool AllocConsole();

            [DllImport(@"kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport(@"user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            const int SwHide = 0;
            const int SwShow = 5;


            public static void ShowConsoleWindow()
            {
                var handle = GetConsoleWindow();

                if (handle == IntPtr.Zero)
                {
                    AllocConsole();
                }
                else
                {
                    ShowWindow(handle, SwShow);
                }
            }

            public static void HideConsoleWindow()
            {
                var handle = GetConsoleWindow();

                ShowWindow(handle, SwHide);
            }
        }

        public Dictionary<string, Dictionary<string, string>> info =
    new Dictionary<string, Dictionary<string, string>> { };
        public class DataItem
        {
            public string Title { get; set; }
            public string Quantity { get; set; }
            public string Size { get; set; }
            public string Books { get; set; }
        }
       
    public MainWindow()
        {
                ConsoleAllocator.ShowConsoleWindow();
                InitializeComponent();

                // Your programmatically created DataGrid is attached to MainGrid here
                var dg = new DataGrid();
                this.MainGrid.Children.Add(dg);
                dg.BeginningEdit += (s, ss) => ss.Cancel = true;

            string[] ColumnHeaders = { "Title", "Quantity", "Size", "Books" };
                // create four columns here with same names as the DataItem's properties
                for (int i = 0; i < 4; ++i)
                {
                    var column = new DataGridTextColumn();
                    column.Header = ColumnHeaders[i];
                    column.Binding = new Binding(ColumnHeaders[i]);
                    dg.Columns.Add(column);
                }
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.UseColumnTextForButtonValue = True;
            col.Text = "ADD";
            col.Name = "MyButton";
            dataGridView1.Columns.Add(col);
                newBook("Harry Potter", "5", "10, 10, 10");
                newBook("Harry Potter 2", "12", "10, 10, 10");
                newBook("Harry Potter 3", "6", "10, 10, 10");
                for (int i = 4; i < 20; i++)
                {
                    newBook("Harry Potter" + i, "6", "10, 10, 10");
                }

                foreach (string key in info.Keys)
                {
                    int i = 0;
                    string[] Inserts = { "", "" };
                    foreach (string innerKey in info[key].Keys)
                    {
                        System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, info[key][innerKey]);
                        //dict[key][innerKey];
                        Inserts[i] = info[key][innerKey];
                        System.Console.WriteLine("{0}", i);
                        i++;
                    }
                    dg.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1] });
                }
                dg.Items.Clear();
                // create and add two lines of fake data to be displayed, here
                //dg.Items.Add(new DataItem { Title = key, Size = "b.2", Quantity = "b.3" });
                foreach (string key in info.Keys)
                {
                    int i = 0;
                    string[] Inserts = { "", "" };
                    foreach (string innerKey in info[key].Keys)
                    {
                        System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, info[key][innerKey]);
                        //dict[key][innerKey];
                        Inserts[i] = info[key][innerKey];
                        System.Console.WriteLine("{0}", i);
                        i++;
                    }
                    dg.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1] });
                }
        }

        
        private void newBook(string Title, string Quantity, string Size)
        {
            info.Add(Title, new Dictionary<string, string>());
            info[Title].Add("quantity", Quantity);
            info[Title].Add("size", Size);
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Hello World!");
        }
    }
}
