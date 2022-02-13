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

        public Dictionary<string, Dictionary<string, string>> currentledger =
        new Dictionary<string, Dictionary<string, string>> { };

        public Dictionary<string, Dictionary<string, string>> ledgerinfo =
        new Dictionary<string, Dictionary<string, string>> { };

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> ledgers =
        new Dictionary<string, Dictionary<string, Dictionary<string, string>>> { };
        /*
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> currentledger =
        new Dictionary<string, Dictionary<string, Dictionary<string, string>>> { };
        */
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
        public class Ledger
        {
            public string Title { get; set; }
            public string DateCreated { get; set; }
            public string EntriesCount { get; set; }
            public string Type { get; set; }
        }
        public class LedgerItems
        {   
            public string Type { get; set; }
            public string LedgerTitle { get; set; }
            public string Title { get; set; }
            public string Quantity { get; set; }
            public string Size { get; set; }
            public string DateCreated { get; set; }
        }

        private string[] bookinfoColumnHeaders = { "Title", "Quantity", "Size" };
        private string[] ledgerinfoColumnHeaders = { "Type", "Title", "DateCreated", "EntriesCount" };
        private string[] dg2ColumnHeaders = { "Title", "Number", "Serial", "Location" };
        private string[] ledgeritemsColumnHeaders = { "Type", "LedgerTitle", "Title", "Quantity", "Size", "DateCreated" };
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

                column.Header = bookinfoColumnHeaders[i];
                column.Binding = new Binding(bookinfoColumnHeaders[i]);
                dg.Columns.Add(column);
            }

            for (int i = 0; i < 3; ++i)
            {
                var column = new DataGridTextColumn();

                column.Header = bookinfoColumnHeaders[i];
                column.Binding = new Binding(bookinfoColumnHeaders[i]);
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
            dg.Columns.Clear();
            if ((bool)radiobutton_Books.IsChecked)
            {
                int len = bookinfoColumnHeaders.Length;
                for (int i = 0; i < len; ++i)
                {
                    var column = new DataGridTextColumn();

                    column.Header = bookinfoColumnHeaders[i];
                    column.Binding = new Binding(bookinfoColumnHeaders[i]);
                    dg.Columns.Add(column);
                }
                dg.Items.Clear();
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
            else if ((bool)radiobutton_Ledgers.IsChecked)
            {
                int len = ledgerinfoColumnHeaders.Length;
                for (int i = 0; i < len; ++i)
                {
                    var column = new DataGridTextColumn();

                    column.Header = ledgerinfoColumnHeaders[i];
                    column.Binding = new Binding(ledgerinfoColumnHeaders[i]);
                    dg.Columns.Add(column);
                }
                dg.Items.Clear();
                foreach (string key in ledgerinfo.Keys)
                {
                    int i = 0;
                    string[] Inserts = { "", "", "" };
                    foreach (string innerKey in ledgerinfo[key].Keys)
                    {
                        System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, ledgerinfo[key][innerKey]);
                        //dict[key][innerKey];
                        Inserts[i] = ledgerinfo[key][innerKey];
                        System.Console.WriteLine("{0}", i);
                        i++;
                    }

                    dg.Items.Add(new Ledger { Title = key, Type = Inserts[0], DateCreated = Inserts[1], EntriesCount = Inserts[2] });
                }
                
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
            if ((bool)radiobutton_Books.IsChecked)
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
                            dg2.Items.Add(new Books { Title = dataitem.Title, Number = i.ToString(), Serial = dataitem.Title + "_" + i.ToString(), Location = "10" });
                        }

                        str += dataitem.Title + "\n";
                    }
                }
                else
                {

                }
                System.Console.WriteLine(str);
            }
            else if ((bool)radiobutton_Ledgers.IsChecked)
            {
                string str = "";
                dg2.Items.Clear();
                dg2.Columns.Clear();
                int len = ledgeritemsColumnHeaders.Length;
                for (int i = 0; i < len; ++i)
                {
                    var column = new DataGridTextColumn();

                    column.Header = ledgeritemsColumnHeaders[i];
                    column.Binding = new Binding(ledgeritemsColumnHeaders[i]);
                    dg2.Columns.Add(column);
                }
                 if (dg.SelectedItems.Count > 0)
                {
                    Ledger ledger = new Ledger();
                    LedgerItems ledgeritems = new LedgerItems();

                    foreach (var obj in dg.SelectedItems)
                    {
                        ledger = obj as Ledger;
                        int intQuantity = int.Parse(ledger.EntriesCount);
                        //MessageBox.Show(intQuantity.ToString());
                        foreach(var key in ledgers[ledger.Title].Keys)
                        {
                            dg2.Items.Add(new LedgerItems { Type = ledgerinfo[ledger.Title]["type"], LedgerTitle = ledger.Title, Title = key, Quantity = ledgers[ledger.Title][key]["quantity"], Size = ledgers[ledger.Title][key]["size"], DateCreated = ledgers[ledger.Title][key]["datecreated"] });
                        }
                        //dg2.Items.Add(new LedgerItems { LedgerTitle = ledger.Title, Title = ledgers[ledger.Title][] });
                    }
                }
                else
                {

                }
            }
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

        private void newLedger(string Type, string Title, string DateCreated, string EntriesCount)
        {
                ledgerinfo.Add(Title, new Dictionary<string, string>());
                ledgerinfo[Title].Add("type", Type);
                ledgerinfo[Title].Add("datecreated", DateCreated);
                ledgerinfo[Title].Add("entriescount", EntriesCount);

                ledgers.Add(Title, new Dictionary<string, Dictionary<string, string>>());
                foreach (string key in currentledger.Keys)
                {
                    ledgers[Title].Add(key, new Dictionary<string, string>());
                    ledgers[Title][key].Add("quantity", currentledger[key]["quantity"]);
                    ledgers[Title][key].Add("size", currentledger[key]["size"]);
                    ledgers[Title][key].Add("datecreated", DateCreated);
                }
        }

        private int somebooknum = 100;
        public void CreateNewBook_Click(object sender, RoutedEventArgs e)
        {
            newBook("Harry Potter" + somebooknum.ToString(), "6", "10, 10, 10");
            somebooknum++;
            updatedg1();
        }
        
        private void updateledger ()
        {
            foreach (string key in currentledger.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "" };
                foreach (string innerKey in currentledger[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, currentledger[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = currentledger[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                ledger.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1] });
            }
            ledger.Items.Clear();
            // create and add two lines of fake data to be displayed, here
            //dg.Items.Add(new DataItem { Title = key, Size = "b.2", Quantity = "b.3" });
            foreach (string key in currentledger.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "" };
                foreach (string innerKey in currentledger[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, currentledger[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = currentledger[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                ledger.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1] });
            }
        }
        private void AddToLedger_Click(object sender, RoutedEventArgs e)
        {
            if (tbox_quantity.Text == "")
            {
                tbox_quantity.Text = "0";
            }
            if (tbox_title.Text == "")
            {
                MessageBox.Show("Please Input at least a Title!");
            }
            else
            {
                if (!currentledger.ContainsKey(tbox_title.Text))
                {
                    currentledger.Add(tbox_title.Text, new Dictionary<string, string>());
                    currentledger[tbox_title.Text].Add("quantity", tbox_quantity.Text);
                    currentledger[tbox_title.Text].Add("size", tbox_size.Text);
                }
                else
                {
                    currentledger[tbox_title.Text]["quantity"] = (int.Parse(currentledger[tbox_title.Text]["quantity"]) + int.Parse(tbox_quantity.Text)).ToString();
                }

                updateledger();
            }
        }
        private void ConfirmLedger_Click(object sender, RoutedEventArgs e)
        {
            string datecreated = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:ff");
            string Title = tbox_ledgertitle.Text;
            if (currentledger.Count == 0)
            {
                MessageBox.Show("Ledger is Empty!");
            }
            
            if (Title == "")
            {
                Title = datecreated;
            }
            if (!ledgerinfo.ContainsKey(Title))
            {
                newLedger((bool)InboundRadio.IsChecked ? "Inbound" : "Outbound", Title, datecreated, (currentledger.Count).ToString());
                if ((bool)InboundRadio.IsChecked)
                {
                    foreach (string key in currentledger.Keys)
                    {
                        int i = 0;
                        string[] Inserts = { "", "" };
                        foreach (string innerKey in currentledger[key].Keys)
                        {
                            System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, currentledger[key][innerKey]);
                            //dict[key][innerKey];
                            Inserts[i] = currentledger[key][innerKey];
                            System.Console.WriteLine("{0}", i);
                            i++;
                        }
                        newBook(key, Inserts[0], Inserts[1]);
                    }
                    updatedg1();
                }
                else if ((bool)OutboundRadio.IsChecked)
                {
                    foreach (string key in currentledger.Keys)
                    {
                        if (info.ContainsKey(key))
                        {
                            int infoquantity = int.Parse(info[key]["quantity"]);
                            int ledgerquantity = int.Parse(currentledger[key]["quantity"]);
                            if (infoquantity >= ledgerquantity)
                            {
                                info[key]["quantity"] = (infoquantity - ledgerquantity).ToString();
                                updatedg1();
                            }
                            else
                            {
                                MessageBox.Show("Not Enough Stock!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Key not Found!");
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Ledger Title Already Found!");
            }

            tbox_ledgertitle.Text = "";


        }
        private List<string> selectedinledger = new List<string>();
        private void ledger_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedinledger.Clear();
            foreach (string key in currentledger.Keys)
            {
                if (ledger.SelectedItems.Count > 0)
                {
                    DataItem dataitem = new DataItem();
                    Books books = new Books();
                    foreach (var obj in ledger.SelectedItems)
                    {
                        dataitem = obj as DataItem;
                        selectedinledger.Add(dataitem.Title);
                    }
                }
            }
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (string key in selectedinledger)
            {
                currentledger.Remove(key);
            }
            updateledger();
        }
        private void ClearLedger_Click(object sender, RoutedEventArgs e)
        {
            currentledger.Clear();
            updateledger();
        }

        private void radiobutton_Ledgers_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Ledgers Clicked!");
            updatedg1();
        }

        private void radiobutton_Books_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Books Clicked!");
            updatedg1();
        }
    }
}
