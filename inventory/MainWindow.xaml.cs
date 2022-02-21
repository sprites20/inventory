using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        public MainWindow()
        {
            ConsoleAllocator.ShowConsoleWindow();
            InitializeComponent();

            //Initialize Stuff Here
            LoadCSV();

            LedgerBorder.Margin = new Thickness(6, 4, 0, 0);
            MakeLedgerBorder.Margin = new Thickness(816, 4, 10, 232);
            AddToLedgerBorder.Margin = new Thickness(816, 463, 0, 0);

            //LedgerBorder.Visibility = Visibility.Hidden;


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
            /*
            newBook("Harry Potter", "5", "10, 10, 10");
            newBook("Harry Potter 2", "12", "10, 10, 10");
            newBook("Harry Potter 3", "6", "10, 10, 10");
            for (int i = 4; i < 20; i++)
            {
                newBook("Harry Potter" + i, "6", "10, 10, 10");
            }
            */

            //Console.WriteLine(info.Any(dictionary => info.Value.Contains("Code")));
            
            
            // create four columns here with same names as the DataItem's properties
            updatedg1();

            //search("Harry Potter1");
        }

        private void search(string tobesearched)
        {
            dg.Items.Clear();
           
            //dg.ScrollIntoView(dg.Items[1], dg.Columns[1]);
            if ((bool)radiobutton_Books.IsChecked)
            {
                var result = info.Where(entry => entry.Key.Contains(tobesearched)).Select(item => item.Key);
                foreach (var key in result)
                {
                    Console.WriteLine(key);
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
                var result = ledgerinfo.Where(entry => entry.Key.Contains(tobesearched)).Select(item => item.Key);
                foreach (var key in result)
                {
                    Console.WriteLine(key);
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
                    dg.Items.Add(new Ledger { Title = key, DateCreated = Inserts[0], EntriesCount = Inserts[1] });
                    //dg2.Items.Add(new LedgerItems { Type = intQuantity > 0 ? "Inbound" : "Outbound", LedgerTitle = ledger.Title, Title = key, Quantity = ledgers[ledger.Title][key]["quantity"], Size = ledgers[ledger.Title][key]["size"], DateCreated = ledgers[ledger.Title][key]["datecreated"] });
                }
            }
        }
        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            search(searchbox.Text);
            // Omitted Code: Insert code that does something whenever
            // the text changes...
        } // end textChangedEventHandler
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

                    dg.Items.Add(new Ledger { Title = key, DateCreated = Inserts[0], EntriesCount = Inserts[1] });
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
                //string str = "";
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
                            dg2.Items.Add(new LedgerItems { Type = intQuantity > 0 ? "Inbound" : "Outbound", LedgerTitle = ledger.Title, Title = key, Quantity = ledgers[ledger.Title][key]["quantity"], Size = ledgers[ledger.Title][key]["size"], DateCreated = ledgers[ledger.Title][key]["datecreated"] });
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

        private void newLedger(string Title, string DateCreated, string EntriesCount)
        {
                ledgerinfo.Add(Title, new Dictionary<string, string>());
                ledgerinfo[Title].Add("datecreated", DateCreated);
                ledgerinfo[Title].Add("entriescount", EntriesCount);

                ledgers.Add(Title, new Dictionary<string, Dictionary<string, string>>());
            Console.WriteLine("Created new ledger");
            Console.WriteLine(ledgers[Title]);
            foreach (string key in currentledger.Keys)
                {
                
                ledgers[Title].Add(key, new Dictionary<string, string>());
                    
                    ledgers[Title][key].Add("quantity", currentledger[key]["quantity"]);
                    Console.WriteLine(ledgers[Title][key]["quantity"]);
                    ledgers[Title][key].Add("size", currentledger[key]["size"]);
                    Console.WriteLine(ledgers[Title][key]["size"]);

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
            bool pass = true;
            //Check if Quantity is int
            try
            {
                int.Parse(tbox_quantity.Text);
            }
            catch
            {
                pass = false;
                MessageBox.Show("Invalid Quantity. Please input an integer.");
            }
            //Check if Size if Valid
            try
            {
                char[] spearator = { ',' };
                // using the method
                string size = ReplaceWhitespace(tbox_size.Text, "");
                string[] strlist = size.Split(spearator);

                if(strlist.Length != 3)
                {
                    int.Parse("a");
                }

                foreach(string str in strlist)
                {
                    float.Parse(str);
                }
            }
            catch
            {
                pass = false;
                MessageBox.Show("Invalid Size");
            }

            if (pass)
            {
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
                newLedger( Title, datecreated, (currentledger.Count).ToString());

                foreach (string key in currentledger.Keys)
                {
                    int ledgerquantity = int.Parse(currentledger[key]["quantity"]);
                    if(ledgerquantity > 0)
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
                    else
                    {
                        if (info.ContainsKey(key))
                        {
                            int infoquantity = int.Parse(info[key]["quantity"]);
                            if (infoquantity >= Math.Abs(ledgerquantity))
                            {
                                info[key]["quantity"] = (infoquantity + ledgerquantity).ToString();
                                updatedg1();
                            }
                            else
                            {
                                MessageBox.Show("Not Enough Stock!");
                            }
                        }
                        else
                        {   
                            MessageBox.Show("Key not Found for Outbound: " + key);
                            ledgerinfo.Remove(Title);
                            ledgers.Remove(Title);
                            break;
                        }
                    }
                }
                
  
            }
            else
            {
                MessageBox.Show("Ledger Title Already Found!");
            }

            updatedg1();
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

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }

        private void Load_info()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files";
            fullPath = path + @"\Files\info.csv";
            if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
            {
                string readText = File.ReadAllText(fullPath);
                // Taking a string
                string str = readText;

                int n = 1;
                string[] lines = str
                    .Split(Environment.NewLine.ToCharArray())
                    .Skip(n)
                    .ToArray();

                str = string.Join(Environment.NewLine, lines);

                char[] spearator = {','};

                // using the method
                string[] strlist = str.Split(spearator);

                int count = 0;
                string[] inputs = new string[5];
                foreach (String s in strlist)
                {
                    if (count == 5)
                    {   
                        for(int i = 0; i < inputs.Length; i++)
                        {
                            Console.WriteLine(i.ToString() + " " + inputs[i]);
                        } 
                        string key = inputs[0];
                        newBook(key, inputs[1], inputs[2] + "," + ReplaceWhitespace(inputs[3], "") + "," + ReplaceWhitespace(inputs[4], ""));
                        count = 0;
                        Array.Clear(inputs, 0, inputs.Length);
                    }
                        string somestr = s.Replace(Environment.NewLine, "");
                        inputs[count] = somestr;
                        //Console.WriteLine(count.ToString() + " " + somestr);
                    
                    count++;
                }
                //Console.WriteLine(readText);
            }
        }

        private void Load_ledgerinfo()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files";
            fullPath = path + @"\Files\ledgerinfo.csv";
            if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
            {
                string readText = File.ReadAllText(fullPath);
                // Taking a string
                string str = readText;

                int n = 1;
                string[] lines = str
                    .Split(Environment.NewLine.ToCharArray())
                    .Skip(n)
                    .ToArray();

                str = string.Join(Environment.NewLine, lines);

                char[] spearator = { ',' };

                // using the method
                string[] strlist = str.Split(spearator);

                int count = 0;
                string[] inputs = new string[5];
                foreach (String s in strlist)
                {
                    if (count == 3)
                    {
                        for (int i = 0; i < inputs.Length; i++)
                        {
                            Console.WriteLine(i.ToString() + " " + inputs[i]);
                        }
                        string key = inputs[0];
                        newLedger(key, inputs[1], inputs[2]);
                        count = 0;
                        Array.Clear(inputs, 0, inputs.Length);
                    }
                    string somestr = s.Replace(Environment.NewLine, "");
                    inputs[count] = somestr;
                    //Console.WriteLine(count.ToString() + " " + somestr);

                    count++;
                }
                //Console.WriteLine(readText);
            }
        }
        private void saveInfo()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files";
            if (!Directory.Exists(fullPath))                            //Checks if File exists
            {
                Directory.CreateDirectory(fullPath);
            }
            fullPath = path + @"\Files\info.csv";
            string backupPath = path + @"\Files\info_backup.csv";
            if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
            {
                //fullPath = backupPath;
            }
            // Write file using StreamWriter  
            using (StreamWriter writer = new StreamWriter(backupPath))
            {
                writer.WriteLine(String.Format("{0},{1},{2},", "Title", "Quantity", "Size"));
                foreach (string key in info.Keys)
                {
                    writer.WriteLine(String.Format("{0},{1},{2},", key, info[key]["quantity"], info[key]["size"]));
                }
            }
            File.Delete(path + @"\Files\info.csv");
            File.Copy(backupPath, path + @"\Files\info.csv");
            File.Delete(backupPath);
            // Read a file  
            string readText = File.ReadAllText(fullPath);
            Console.WriteLine(readText);
        }
        private void saveLedgerInfo()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files";
            if (!Directory.Exists(fullPath))                            //Checks if File exists
            {
                Directory.CreateDirectory(fullPath);
            }
            fullPath = path + @"\Files\ledgerinfo.csv";
            string backupPath = path + @"\Files\ledgerinfo_backup.csv";
            if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
            {
                //fullPath = backupPath;
            }
            // Write file using StreamWriter  
            using (StreamWriter writer = new StreamWriter(backupPath))
            {
                writer.WriteLine(String.Format("{0},{1},{2},", "Title", "DateCreated", "EntriesCount"));
                foreach (string key in ledgerinfo.Keys)
                {
                    writer.WriteLine(String.Format("{0},{1},{2},", key, ledgerinfo[key]["datecreated"], ledgerinfo[key]["entriescount"]));
                }
            }
            File.Delete(path + @"\Files\ledgerinfo.csv");
            File.Copy(backupPath, path + @"\Files\ledgerinfo.csv");
            File.Delete(backupPath);
            // Read a file  
            string readText = File.ReadAllText(fullPath);
            Console.WriteLine(readText);
        }
        private void SaveAsCSV_Click(object sender, RoutedEventArgs e)
        {
            saveInfo();
            saveLedgerInfo();
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

        private void LoadCSV()
        {
            Load_info();
            Load_ledgerinfo();
        }
        static void Write1(Dictionary<string, Dictionary<string, string>> dictionary, string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Put count.
                writer.Write(dictionary.Count);
                //Console.WriteLine(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key);
                    writer.Write(dictionary[pair.Key].Count);
                    foreach (var pair2 in pair.Value)
                    {
                        writer.Write(pair2.Key);
                        writer.Write(pair2.Value);
                        //Console.WriteLine(pair2);
                    }
                }
            }
        }
        static void Write2(Dictionary<string, Dictionary<string, Dictionary<string, string>>> dictionary, string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Put count.
                writer.Write(dictionary.Count);
                //Console.WriteLine(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key);
                    writer.Write(dictionary[pair.Key].Count);
                    //Console.WriteLine(pair.Key);
                    //Console.WriteLine(dictionary[pair.Key].Count);
                    foreach (var pair2 in pair.Value)
                    {
                        writer.Write(pair2.Key);
                        writer.Write(dictionary[pair2.Key].Count);
                        //Console.WriteLine(pair2.Key);
                        //Console.WriteLine(dictionary[pair.Key].Count);
                        foreach (var pair3 in pair2.Value)
                        {
                            writer.Write(pair3.Key);
                            writer.Write(pair3.Value);
                            //Console.WriteLine(pair3.Key);
                            //Console.WriteLine(pair3.Value);
                        }
                    }
                }
            }
        }
        static Dictionary<string, Dictionary<string, Dictionary<string, string>>> Read2(string file)
        {
            var result = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            using (FileStream fs = File.OpenRead(file))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Get count.
                int count = reader.ReadInt32();
                //Console.WriteLine(count);
                // Read in all pairs.
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    result[key] = new Dictionary<string, Dictionary<string, string>>();
                    Console.WriteLine(key);
                    int count2 = reader.ReadInt32();
                    Console.WriteLine(count2);
                    for (int j = 0; j < count2; j++)
                    {
                        string key2 = reader.ReadString();
                        result[key][key2] = new Dictionary<string, string>();
                        Console.WriteLine(key2);
                        int count3 = reader.ReadInt32();
                        for (int k = 0; k < count3; k++)
                        {
                            string a = reader.ReadString();
                            string b = reader.ReadString();
                            result[key][key2][a] = b;
                        }
                    }
                }
            }
            return result;
        }

        static Dictionary<string, Dictionary<string, string>> Read1(string file)
        {
            var result = new Dictionary<string, Dictionary<string, string>>();
            using (FileStream fs = File.OpenRead(file))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Get count.
                int count = reader.ReadInt32();
                //Console.WriteLine(count);
                // Read in all pairs.
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    result[key] = new Dictionary<string, string>();
                    //Console.WriteLine(key);
                    int count2 = reader.ReadInt32();
                    //Console.WriteLine(count2);
                    for (int j = 0; j < count2; j++)
                    {

                        string a = reader.ReadString();
                        string b = reader.ReadString();
                        result[key][a] = b;
                        //Console.WriteLine(a);
                        //Console.WriteLine(b);
                    }
                }
            }
            return result;
        }

        private void InitializeDataBin()
        {
            try
            {
                info = Read1("C:\\Users\\NakaMura\\Desktop\\data\\info.bin");
                Console.WriteLine("Initialized info " + info.Count);
                ledgerinfo = Read1("C:\\Users\\NakaMura\\Desktop\\data\\ledgerinfo.bin");
                Console.WriteLine("Initialized ledgerinfo" + ledgerinfo.Count);
                books = Read2("C:\\Users\\NakaMura\\Desktop\\data\\books.bin");
                Console.WriteLine("Initialized books " + books.Count);
                ledgers = Read2("C:\\Users\\NakaMura\\Desktop\\data\\ledgers.bin");
                Console.WriteLine("Initialized ledgers " + ledgers.Count);
            }
            catch
            {

            }
            
        }
        private void UpdateDataBin()
        {
            try
            {
                Write1(info, "C:\\Users\\NakaMura\\Desktop\\data\\info.bin");
                info = Read1("C:\\Users\\NakaMura\\Desktop\\data\\info.bin");
                Write2(books, "C:\\Users\\NakaMura\\Desktop\\data\\books.bin");
                books = Read2("C:\\Users\\NakaMura\\Desktop\\data\\books.bin");

                Write1(ledgerinfo, "C:\\Users\\NakaMura\\Desktop\\data\\ledgerinfo.bin");
                ledgerinfo = Read1("C:\\Users\\NakaMura\\Desktop\\data\\ledgerinfo.bin");

                               
                Write2(ledgers, "C:\\Users\\NakaMura\\Desktop\\data\\ledgers.bin");
                ledgers = Read2("C:\\Users\\NakaMura\\Desktop\\data\\ledgers.bin");
            }
            catch
            {
                Console.WriteLine("Error!");
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
        }
        public class LedgerItems
        {
            public string Type { get; set; }
            public string LedgerTitle { get; set; }
            public string Title { get; set; }
            public string FormerQuantity { get; set; }
            public string Quantity { get; set; }
            public string LatterQuantity { get; set; }
            public string Size { get; set; }
            public string DateCreated { get; set; }
        }

        private string[] bookinfoColumnHeaders = { "Title", "Quantity", "Size" };
        private string[] ledgerinfoColumnHeaders = { "Title", "DateCreated", "EntriesCount" };
        private string[] dg2ColumnHeaders = { "Title", "Number", "Serial", "Location" };
        private string[] ledgeritemsColumnHeaders = { "Type", "LedgerTitle", "Title", "FormerQuantity", "Quantity", "LatterQuantity", "Size", "DateCreated" };
    }
}
