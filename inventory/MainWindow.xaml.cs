using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> books =
        new Dictionary<string, Dictionary<string, Dictionary<string, string>>> { };

        public Dictionary<string, Dictionary<string, string>> ledgerinfo =
        new Dictionary<string, Dictionary<string, string>> { };
        public class DataItem
        {
            public string Title { get; set; }
            public string Quantity { get; set; }
            public string Size { get; set; }
        }
        public class Books 
        {
            public string Title { get; set; }
            public string Number { get; set; }
            public string Serial { get; set; }
            public string Location { get; set; }
        }
        private string[] dgColumnHeaders = { "Title", "Quantity", "Size" };
        private string[] dg2ColumnHeaders = { "Title", "Number", "Serial", "Location" };
        public MainWindow()
        {
            ConsoleAllocator.ShowConsoleWindow();
            InitializeComponent();

            // Your programmatically created DataGrid is attached to MainGrid here

            dg.BeginningEdit += (s, ss) => ss.Cancel = true;
            dg2.BeginningEdit += (s, ss) => ss.Cancel = true;
            ledger.BeginningEdit += (s, ss) => ss.Cancel = true;
            // create four columns here with same names as the DataItem's properties
            for (int i = 0; i < 3; ++i)
            {
                var column = new DataGridTextColumn();

                column.Header = dgColumnHeaders[i];
                column.Binding = new Binding(dgColumnHeaders[i]);
                dg.Columns.Add(column);
            }

            for (int i = 0; i < 3; ++i)
            {
                var column = new DataGridTextColumn();

                column.Header = dgColumnHeaders[i];
                column.Binding = new Binding(dgColumnHeaders[i]);
                ledger.Columns.Add(column);
            }

            newBook("Harry Potter", "5", "10, 10, 10");
            newBook("Harry Potter 2", "12", "10, 10, 10");
            newBook("Harry Potter 3", "6", "10, 10, 10");
            for (int i = 4; i < 0; i++)
            {
                newBook("Harry Potter" + i, "6", "10, 10, 10");
            }


            // create four columns here with same names as the DataItem's properties
            updatedg1();
        }

        private void updatedg1()
        {
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

        private void clearLedger()
        {
            string[] ledgerColumnHeaders = { "Title", "Quantity", "Size" };
            for (int i = 0; i < 3; ++i)
            {
                var column = new DataGridTextColumn();
                column.Header = ledgerColumnHeaders[i];
                column.Binding = new Binding(ledgerColumnHeaders[i]);
                ledger.Columns.Add(column);
            }
            TextBox TextBox1 = new TextBox();
        }
        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string str = "";
            dg2.Items.Clear();
            dg2.Columns.Clear();
            for (int i = 0; i < dg2ColumnHeaders.Length; ++i)
            {
                var column = new DataGridTextColumn();

                column.Header = dg2ColumnHeaders[i];
                column.Binding = new Binding(dg2ColumnHeaders[i]);
                dg2.Columns.Add(column);
            }
            
            if (dg.SelectedItems.Count > 0)
            {
                DataItem dataitem = new DataItem();
                Books books = new Books();
                foreach (var obj in dg.SelectedItems)
                {
                    dataitem = obj as DataItem;
                    
                    int intQuantity = int.Parse(dataitem.Quantity);
                    for (int i = 0; i < intQuantity; i++)
                    {
                        dg2.Items.Add(new Books { Title = dataitem.Title, Number = i.ToString(),  Serial = dataitem.Title + "_" + i.ToString(), Location = "10"}); ;
                    }
                    
                    str += dataitem.Title + "\n";
                }
            }
            else
            {

            }
            
            System.Console.WriteLine(str);
        }

        private void newBook(string Title, string Quantity, string Size)
        {
            if (!info.ContainsKey(Title))
            {
                info.Add(Title, new Dictionary<string, string>());
                info[Title].Add("quantity", Quantity);
                info[Title].Add("size", Size);


                books.Add(Title, new Dictionary<string, Dictionary<string, string>>());
                int intQuantity = int.Parse(Quantity);
                for (int i = 0; i < intQuantity; i++)
                {
                    books[Title].Add(i.ToString(), new Dictionary<string, string>());
                    books[Title][i.ToString()].Add("serial", Title + "_" + i.ToString());
                    books[Title][i.ToString()].Add("location", "10");
                }
            }
            else
            {
                info[Title]["quantity"] = (int.Parse(info[Title]["quantity"]) + int.Parse(Quantity)).ToString();
                /*
                int intQuantity = int.Parse(Quantity);
                for (int i = 0; i < intQuantity; i++)
                {
                    books[Title].Add(i.ToString(), new Dictionary<string, string>());
                    books[Title][i.ToString()].Add("serial", Title + "_" + i.ToString());
                    books[Title][i.ToString()].Add("location", "10");
                }
                */
            }
        }

        private int somebooknum = 100;
        public void CreateNewBook_Click(object sender, RoutedEventArgs e)
        {
            newBook("Harry Potter" + somebooknum.ToString(), "6", "10, 10, 10");
            somebooknum++;
            updatedg1();
        }

        public void AddLedger_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ledger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ledger.BeginEdit();
        }
        private void ledger_Editing(object sender, SelectionChangedEventArgs e)
        {
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void updateledger ()
        {
            foreach (string key in ledgerinfo.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "" };
                foreach (string innerKey in ledgerinfo[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, ledgerinfo[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = ledgerinfo[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                ledger.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1] });
            }
            ledger.Items.Clear();
            // create and add two lines of fake data to be displayed, here
            //dg.Items.Add(new DataItem { Title = key, Size = "b.2", Quantity = "b.3" });
            foreach (string key in ledgerinfo.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "" };
                foreach (string innerKey in ledgerinfo[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, ledgerinfo[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = ledgerinfo[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                ledger.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1] });
            }
        }
        private void AddToLedger_Click(object sender, RoutedEventArgs e)
        {   
            if(!ledgerinfo.ContainsKey(tbox_title.Text))
            {
                ledgerinfo.Add(tbox_title.Text, new Dictionary<string, string>());
                ledgerinfo[tbox_title.Text].Add("quantity", tbox_quantity.Text);
                ledgerinfo[tbox_title.Text].Add("size", tbox_size.Text);
            }
            else
            {   
                ledgerinfo[tbox_title.Text]["quantity"] = (int.Parse(ledgerinfo[tbox_title.Text]["quantity"]) + int.Parse(tbox_quantity.Text)).ToString();
            }

            updateledger();
        }
        private void ConfirmLedger_Click(object sender, RoutedEventArgs e)
        {
            foreach (string key in ledgerinfo.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "" };
                foreach (string innerKey in ledgerinfo[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, ledgerinfo[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = ledgerinfo[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                newBook(key, Inserts[0], Inserts[1]);
            }
            updatedg1();
        }
    }
}
