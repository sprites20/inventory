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
            List<string> locations = new List<string>();
            locations.Add("Shelf A");
            locations.Add("Shelf B");
            locations.Add("Shelf C");
            locations.Add("Shelf D");
            locations.Add("Shelf E");
            locations.Add("Shelf F");
            locations.Add("Shelf G");
            locations.Add("Shelf H");
            locations.Add("Shelf I");
            locations.Add("Shelf J");

            foreach (string item in locations)
            {
                LocationCB.Items.Add(item);
                MoveCB.Items.Add(item);
            }

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

            var col = new DataGridTextColumn();

            col.Header = "Location";
            col.Binding = new Binding("Location");

            ledger.Columns.Add(col);
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
            else if ((bool)radiobutton_Movements.IsChecked)
            {
                int len = moveinfoLedgersColumnHeaders.Length;
                for (int i = 0; i < len; ++i)
                {
                    var column = new DataGridTextColumn();

                    column.Header = ledgerinfoColumnHeaders[i];
                    column.Binding = new Binding(ledgerinfoColumnHeaders[i]);
                    dg.Columns.Add(column);
                }
                dg.Items.Clear();
                foreach (string key in moveledgersinfo.Keys)
                {
                    int i = 0;
                    string[] Inserts = { "", "", "" };
                    foreach (string innerKey in moveledgersinfo[key].Keys)
                    {
                        System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, moveledgersinfo[key][innerKey]);
                        //dict[key][innerKey];
                        Inserts[i] = moveledgersinfo[key][innerKey];
                        System.Console.WriteLine("{0}", i);
                        i++;
                    }

                    dg.Items.Add(new MoveLedgerInfo { Title = key, DateCreated = Inserts[0], EntriesCount = Inserts[1] });
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
        private Dictionary<string, Dictionary<string, bool>> dg2selecteditems = new Dictionary<string, Dictionary<string, bool>> { };
        private void dg2_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
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
                    Books book = new Books();
                    foreach (var obj in dg.SelectedItems)
                    {
                        dataitem = obj as DataItem;
                        
                        int intQuantity = int.Parse(dataitem.Quantity);
                        for (int i = 0; i < intQuantity; i++)
                        {
                            dg2.Items.Add(new Books { Title = dataitem.Title, Number = i.ToString(), Serial = dataitem.Title + "_" + i.ToString(), Location = books[dataitem.Title][i.ToString()]["location"], Defective = books[dataitem.Title][i.ToString()]["defective"] });
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
                            //string kye = key.Substring(0, key.Length - ledgers[ledger.Title][key]["location"].Length - 1);
                            dg2.Items.Add(new LedgerItems { Type = intQuantity > 0 ? "Inbound" : "Outbound",
                                LedgerTitle = ledger.Title,
                                Title = key.Substring(0, key.Length - ledgers[ledger.Title][key]["location"].Length - 1),
                                FormerQuantity = ledgers[ledger.Title][key]["formerquantity"],
                                Quantity = ledgers[ledger.Title][key]["quantity"],
                                LatterQuantity = ledgers[ledger.Title][key]["latterquantity"],
                                Size = ledgers[ledger.Title][key]["size"],
                                Location = ledgers[ledger.Title][key]["location"],
                                DateCreated = ledgers[ledger.Title][key]["datecreated"] });
                        }
                        //dg2.Items.Add(new LedgerItems { LedgerTitle = ledger.Title, Title = ledgers[ledger.Title][] });
                    }
                }
                else
                {

                }
            }
            else if ((bool)radiobutton_Movements.IsChecked)
            {
                //string str = "";
                dg2.Items.Clear();
                dg2.Columns.Clear();
                int len = moveledgersColumnHeaders.Length;
                for (int i = 0; i < len; ++i)
                {
                    var column = new DataGridTextColumn();

                    column.Header = moveledgersColumnHeaders[i];
                    column.Binding = new Binding(moveledgersColumnHeaders[i]);
                    dg2.Columns.Add(column);
                }
                if (dg.SelectedItems.Count > 0)
                {
                    MoveLedgerInfo moveledgerinfo = new MoveLedgerInfo();
                    MoveLedgers moveledger = new MoveLedgers();

                    foreach (var obj in dg.SelectedItems)
                    {
                        moveledgerinfo = obj as MoveLedgerInfo;
                        int intQuantity = int.Parse(moveledgerinfo.EntriesCount);
                        //MessageBox.Show(intQuantity.ToString());
                        foreach (var key in moveledgers[moveledgerinfo.Title].Keys)
                        {
                            
                            //string kye = key.Substring(0, key.Length - ledgers[ledger.Title][key]["location"].Length - 1);
                            dg2.Items.Add(new MoveLedgers
                            {
                                Title = moveledgers[moveledgerinfo.Title][key]["booktitle"],
                                Serial = key,
                                FormerLocation = moveledgers[moveledgerinfo.Title][key]["formerlocation"],
                                LatterLocation = moveledgers[moveledgerinfo.Title][key]["latterlocation"],
                            });
                        }
                        //dg2.Items.Add(new LedgerItems { LedgerTitle = ledger.Title, Title = ledgers[ledger.Title][] });
                    }
                }
                else
                {

                }
            }
        }

        public void ArrayPush<T>(ref T[] table, object value)
        {
            Array.Resize(ref table, table.Length + 1); // Resizing the array for the cloned length (+-) (+1)
            table.SetValue(value, table.Length - 1); // Setting the value for the new element
        }
        private void newBook(string Title, string Quantity, string Size, string Location)
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
                    books[Title][i.ToString()].Add("location", Location);
                    books[Title][i.ToString()].Add("defective", "False");
                }
            }
            else
            {
                int intQuantity = int.Parse(info[Title]["quantity"]);
                info[Title]["quantity"] = (int.Parse(info[Title]["quantity"]) + int.Parse(Quantity)).ToString();

                int[] nums = new int[] { };
                foreach(string key in books[Title].Keys)
                {
                    ArrayPush(ref nums, int.Parse(key));
                    Console.WriteLine("");
                }
                /*
                int intQuantity = int.Parse(Quantity);
                for (int i = intQuantity; i < intQuantity*2; i++)
                {
                    books[Title].Add(i.ToString(), new Dictionary<string, string>());
                    books[Title][i.ToString()].Add("serial", Title + "_" + i.ToString());
                    books[Title][i.ToString()].Add("location", "10");
                    books[Title][i.ToString()].Add("defective", "False");
                }
                */

                int qty = int.Parse(info[Title]["quantity"]);
                for (int i = intQuantity; i < qty; i++)
                {
                    books[Title].Add(i.ToString(), new Dictionary<string, string>());
                    books[Title][i.ToString()].Add("serial", Title + "_" + i.ToString());
                    books[Title][i.ToString()].Add("location", Location);
                    books[Title][i.ToString()].Add("defective", "False");
                }

            }
        }

        private void newLedgerInfo(string Title, string DateCreated, string EntriesCount)
        {
                ledgerinfo.Add(Title, new Dictionary<string, string>());
                ledgerinfo[Title].Add("datecreated", DateCreated);
                ledgerinfo[Title].Add("entriescount", EntriesCount);

                ledgers.Add(Title, new Dictionary<string, Dictionary<string, string>>());
            Console.WriteLine("Created new ledger");
            Console.WriteLine(ledgers[Title]);
            string lastkye = "";
            string lastkey = "";
            foreach (string key in currentledger.Keys)
                {
                string kye = key.Substring(0, key.Length - currentledger[key]["location"].Length - 1);
                
                ledgers[Title].Add(key, new Dictionary<string, string>());

                try
                {
                    if (lastkye == kye)
                    {
                        ledgers[Title][key].Add("formerquantity", ledgers[Title][lastkey]["latterquantity"]);
                    }
                    ledgers[Title][kye].Add("formerquantity", ledgers[Title][kye]["formerquantity"]);
                    ledgers[Title][key].Add("formerquantity", ledgers[Title][kye]["formerquantity"]);
                    //Console.WriteLine(ledgers[Title][kye]["formerquantity"]);
                }
                catch
                {
                    try
                    {
                        ledgers[Title][key].Add("formerquantity", info[kye]["quantity"]);
                    }
                    catch
                    {
                        try
                        {
                            ledgers[Title][key].Add("formerquantity", "0");
                        }
                        catch
                        {

                        }
                    }
                    
                    //ledgers[Title][kye].Add("formerquantity", "0");
                }

                ledgers[Title][key].Add("quantity", currentledger[key]["quantity"]);
                try
                {   
                        ledgers[Title][key].Add("latterquantity", (int.Parse(ledgers[Title][key]["formerquantity"]) + int.Parse(currentledger[key]["quantity"])).ToString());
                    

                    //Console.WriteLine("kye: " + info[kye]["quantity"]);
                }
                catch
                {
                    ledgers[Title][key].Add("latterquantity", currentledger[key]["quantity"]);

                }
                lastkye = kye;
                lastkey = key;
                /*
                try
                {
                    ledgers[Title][kye].Add("formerquantity", ledgers[Title][key]["formerquantity"]);
                }
                catch
                {
                    ledgers[Title][kye]["formerquantity"] = ledgers[Title][key]["formerquantity"];
                }*/
                Console.WriteLine(ledgers[Title][key]["latterquantity"]);
                ledgers[Title][key].Add("size", currentledger[key]["size"]);
                ledgers[Title][key].Add("location", currentledger[key]["location"]);
                ledgers[Title][key].Add("datecreated", DateCreated);
                }
        }

        private int somebooknum = 100;
        public void CreateNewBook_Click(object sender, RoutedEventArgs e)
        {
            //newBook("Harry Potter" + somebooknum.ToString(), "6", "10, 10, 10");
            somebooknum++;
            updatedg1();
        }
        
        private void updateledger ()
        {
            /*
            foreach (string key in currentledger.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "", "" };
                foreach (string innerKey in currentledger[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, currentledger[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = currentledger[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                ledger.Items.Add(new DataItem { Title = key, Quantity = Inserts[0], Size = Inserts[1], Location = Inserts[2] });
            }*/
            ledger.Items.Clear();
            foreach (string key in currentledger.Keys)
            {
                int i = 0;
                string[] Inserts = { "", "", "" };
                foreach (string innerKey in currentledger[key].Keys)
                {
                    System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, currentledger[key][innerKey]);
                    //dict[key][innerKey];
                    Inserts[i] = currentledger[key][innerKey];
                    System.Console.WriteLine("{0}", i);
                    i++;
                }
                ledger.Items.Add(new DataItem { Title = tbox_title.Text, Quantity = Inserts[0], Size = Inserts[1], Location = Inserts[2] });
            }
            // create and add two lines of fake data to be displayed, here
            //dg.Items.Add(new DataItem { Title = key, Size = "b.2", Quantity = "b.3" });
            /*
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
            }*/
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
                    string key = tbox_title.Text + " " + LocationCB.Text;
                    if (!currentledger.ContainsKey(key))
                    {   
                        if(int.Parse(tbox_quantity.Text) >= 0)
                        {
                            currentledger.Add(key, new Dictionary<string, string>());
                            currentledger[key].Add("quantity", tbox_quantity.Text);
                            currentledger[key].Add("size", tbox_size.Text);
                            currentledger[key].Add("location", LocationCB.Text);
                        }
                        else
                        {
                            MessageBox.Show("Please input a positive quantity!");
                        }
                    }
                    
                    else
                    {
                        currentledger[key]["quantity"] = (int.Parse(currentledger[key]["quantity"]) + int.Parse(tbox_quantity.Text)).ToString();
                    }

                    updateledger();
                }
            }
        }
        private void ConfirmLedger_Click(object sender, RoutedEventArgs e)
        {
            string datecreated = System.DateTime.Now.ToString("dd-MM-yyyy HHmmssff");
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

                newLedgerInfo( Title, datecreated, (currentledger.Count).ToString());

                foreach (string key in currentledger.Keys)
                {
                    string[] Inserts = new string[3];
                    int i = 0;
                    
                    foreach (string innerKey in currentledger[key].Keys)
                    {
                        System.Console.WriteLine("{0}\t{1}\t{2}", key, innerKey, currentledger[key][innerKey]);
                        //dict[key][innerKey];
                        Inserts[i] = currentledger[key][innerKey];
                        System.Console.WriteLine("{0}", i);
                        i++;
                    }
                    int ledgerquantity = int.Parse(currentledger[key]["quantity"]);
                    string kye = key.Substring(0, key.Length - Inserts[2].Length - 1);
                    if (ledgerquantity > 0)
                    {
                        newBook(kye, Inserts[0], Inserts[1], Inserts[2]);
                    }
                    else
                    {
                        /*
                        if (info.ContainsKey(kye))
                        {
                            int infoquantity = int.Parse(info[kye]["quantity"]);
                            if (infoquantity >= Math.Abs(ledgerquantity))
                            {
                                info[kye]["quantity"] = (infoquantity + ledgerquantity).ToString();
                                updatedg1();
                            }
                            else
                            {
                                MessageBox.Show("Not Enough Stock!");
                                ledgerinfo.Remove(Title);   
                                ledgers.Remove(Title);
                            }
                        }
                        else
                        {   
                            MessageBox.Show("Key not Found for Outbound: " + key);
                            ledgerinfo.Remove(Title);
                            ledgers.Remove(Title);
                            break;
                        }*/
                        MessageBox.Show("You can't remove here!");
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
                int infocount = bookinfoColumnHeaders.Length + 2;
                string[] inputs = new string[infocount];
                foreach (String s in strlist)
                {
                    if (count == infocount)
                    {   
                        for(int i = 0; i < inputs.Length; i++)
                        {
                            Console.WriteLine(i.ToString() + " " + inputs[i]);
                        } 
                        string key = inputs[0];
                        info.Add(key, new Dictionary<string, string>());
                        info[key].Add("quantity", inputs[1]);
                        info[key].Add("size", inputs[2] + "," + ReplaceWhitespace(inputs[3], "") + "," + ReplaceWhitespace(inputs[4], ""));
                        
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
        private void newLedger(string Type, string LedgerTitle, string Title, string FormerQuantity, string Quantity, string LatterQuantity, string Size, string Location, string DateCreated)
        {
            if (!ledgers.ContainsKey(LedgerTitle))
            {
                ledgers.Add(LedgerTitle, new Dictionary<string, Dictionary<string, string>>());
            }
            ledgers[LedgerTitle].Add(Title, new Dictionary<string, string>());
            ledgers[LedgerTitle][Title].Add("type", Type);
            ledgers[LedgerTitle][Title].Add("formerquantity", FormerQuantity);
            ledgers[LedgerTitle][Title].Add("quantity", Quantity);
            ledgers[LedgerTitle][Title].Add("latterquantity", LatterQuantity);
            ledgers[LedgerTitle][Title].Add("size", Size);
            ledgers[LedgerTitle][Title].Add("location", Location);
            ledgers[LedgerTitle][Title].Add("datecreated", DateCreated);
        }
        private void Load_books()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            foreach (string key in info.Keys)
            {
                string fullPath = path + @"\Files\Books\";
                fullPath = path + @"\Files\Books\" + key + ".csv";
                if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
                {
                    string readText = File.ReadAllText(fullPath);
                    // Taking a string
                    string str = readText;
                    Console.WriteLine(str);
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
                    string[] inputs = new string[4];
                    int maxcount = 4;
                    foreach (string s in strlist)
                    {
                        if (count == maxcount)
                        {
                            for (int i = 0; i < inputs.Length; i++)
                            {
                                Console.WriteLine(i.ToString() + " " + inputs[i]);
                            }
                            //newBook(key, inputs[1], inputs[2] + "," + ReplaceWhitespace(inputs[3], "") + "," + ReplaceWhitespace(inputs[4], ""));
                            if (!books.ContainsKey(key))
                            {
                                books.Add(key, new Dictionary<string, Dictionary<string, string>>());
                            }
                            
                            books[key].Add(inputs[0], new Dictionary<string, string>());
                            books[key][inputs[0]].Add("serial", inputs[1]);
                            books[key][inputs[0]].Add("location", inputs[2]);
                            books[key][inputs[0]].Add("defective", inputs[3]);
                            count = 0;
                            Array.Clear(inputs, 0, inputs.Length);
                        }
                        string somestr = s.Replace(Environment.NewLine, "");
                        inputs[count] = somestr;
                        Console.WriteLine(count.ToString() + " " + somestr);

                        count++;
                    }
                    //Console.WriteLine(readText);
                }
            }
        }
        private void Load_ledgers()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            foreach(string ledgerkey in ledgerinfo.Keys)
            {
                string fullPath = path + @"\Files\Ledgers\";
                fullPath = path + @"\Files\Ledgers\" + ledgerkey + ".csv";
                if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
                {
                    string readText = File.ReadAllText(fullPath);
                    // Taking a string
                    string str = readText;
                    Console.WriteLine(str);
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
                    string[] inputs = new string[11];
                    int maxcount = ledgeritemsColumnHeaders.Length + 2;
                    foreach (string s in strlist)
                    {
                        if (count == maxcount)
                        {
                            for (int i = 0; i < inputs.Length; i++)
                            {
                                Console.WriteLine(i.ToString() + " " + inputs[i]);
                            }
                            //newBook(key, inputs[1], inputs[2] + "," + ReplaceWhitespace(inputs[3], "") + "," + ReplaceWhitespace(inputs[4], ""));
                            newLedger(
                                inputs[0], 
                                inputs[1], 
                                inputs[2], 
                                inputs[3], 
                                inputs[4], 
                                inputs[5], 
                                inputs[6] + "," + inputs[7] + "," + inputs[8], 
                                inputs[9],
                                inputs[10]
                            );
                            
                            count = 0;
                            Array.Clear(inputs, 0, inputs.Length);
                        }
                        string somestr = s.Replace(Environment.NewLine, "");
                        inputs[count] = somestr;
                        Console.WriteLine(count.ToString() + " " + somestr);

                        count++;
                    }
                    //Console.WriteLine(readText);
                }
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

                int maxcount = ledgerinfoColumnHeaders.Length;
                foreach (String s in strlist)
                {
                    if (count == maxcount)
                    {
                        for (int i = 0; i < inputs.Length; i++)
                        {
                            Console.WriteLine(i.ToString() + " " + inputs[i]);
                        }
                        string key = inputs[0];
                        newLedgerInfo(key, inputs[1], inputs[2]);
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

        private void saveBooks()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files\Books";
            if (!Directory.Exists(fullPath))                            //Checks if File exists
            {
                Directory.CreateDirectory(fullPath);
            }
            foreach (string booktitle in books.Keys)
            {
                fullPath = path + @"\Files\Books\" + booktitle + ".csv";
                string backupPath = path + @"\Files\Books\" + booktitle + "_backup.csv";
                if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
                {
                    //fullPath = backupPath;
                }
                // Write file using StreamWriter  
                using (StreamWriter writer = new StreamWriter(backupPath))
                {
                    writer.WriteLine(String.Format("{0},{1},{2},{3},", "Number", "Serial", "Location", "Defective"));
                    foreach (string key in books[booktitle].Keys)
                    {
                        
                            writer.WriteLine(String.Format("{0},{1},{2},{3},",
                            key,
                            books[booktitle][key.ToString()]["serial"],
                            books[booktitle][key.ToString()]["location"],
                            books[booktitle][key.ToString()]["defective"]
                            ));
                    }
                }
                File.Delete(fullPath);
                File.Copy(backupPath, fullPath);
                File.Delete(backupPath);
                // Read a file  
                string readText = File.ReadAllText(fullPath);
                Console.WriteLine(readText);
            }
        }
        private void saveMoveLedgers()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files\MovementLedgers";
            if (!Directory.Exists(fullPath))                            //Checks if File exists
            {
                Directory.CreateDirectory(fullPath);
            }
            foreach (string movekey in moveledgers.Keys)
            {
                fullPath = path + @"\Files\MovementLedgers\" + movekey + ".csv";
                string backupPath = path + @"\Files\MovementLedgers\" + movekey + "_backup.csv";
                if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
                {
                    //fullPath = backupPath;
                }
                // Write file using StreamWriter  
                using (StreamWriter writer = new StreamWriter(backupPath))
                {
                    writer.WriteLine(String.Format("{0},{1},{2},{3},", "Title", "Serial", "FormerLocation", "LatterLocation"));
                    foreach (string key in moveledgers[movekey].Keys)
                    {

                        writer.WriteLine(String.Format("{0},{1},{2},{3},",
                        moveledgers[movekey][key]["title"],
                        key,
                        moveledgers[movekey][key]["formerlocation"],
                        moveledgers[movekey][key]["latterlocation"]
                        ));
                    }
                }
                File.Delete(fullPath);
                File.Copy(backupPath, fullPath);
                File.Delete(backupPath);
                // Read a file  
                string readText = File.ReadAllText(fullPath);
                Console.WriteLine(readText);
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
        private void saveLedgers()
        {
            string path = "";
            path = System.AppContext.BaseDirectory;
            Console.WriteLine(path);

            string fullPath = path + @"\Files\Ledgers";
            if (!Directory.Exists(fullPath))                            //Checks if File exists
            {
                Directory.CreateDirectory(fullPath);
            }
            foreach(string ledgertitle in ledgers.Keys)
            {
                fullPath = path + @"\Files\Ledgers\" + ledgertitle + ".csv";
                string backupPath = path + @"\Files\Ledgers\" + ledgertitle + "_backup.csv";
                if (File.Exists(fullPath))                                  //Print contents to backup called info_backup.csv
                {
                    //fullPath = backupPath;
                }
                // Write file using StreamWriter  
                using (StreamWriter writer = new StreamWriter(backupPath))
                {
                    writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},,,{7},{8}", "Type", "LedgerTitle", "Title", "FormerQuantity", "Quantity", "LatterQuantity", "Size", "Location", "DateCreated"));
                    foreach(string key in ledgers[ledgertitle].Keys)
                    {
                        //dg2.Items.Add(new LedgerItems { Type = intQuantity > 0 ? "Inbound" : "Outbound", LedgerTitle = ledger.Title, Title = key, Quantity = ledgers[ledger.Title][key]["quantity"], Size = ledgers[ledger.Title][key]["size"], DateCreated = ledgers[ledger.Title][key]["datecreated"] });
                        writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},", 
                            int.Parse(ledgers[ledgertitle][key]["quantity"]) > 0 ? "Inbound" : "Outbound",
                            ledgertitle,
                            //key.Substring(0, key.Length - ledgers[ledgertitle][key]["location"].Length - 1),
                            key,
                            ledgers[ledgertitle][key]["formerquantity"], 
                            ledgers[ledgertitle][key]["quantity"],
                            ledgers[ledgertitle][key]["latterquantity"],
                            ledgers[ledgertitle][key]["size"],
                            ledgers[ledgertitle][key]["location"],
                            ledgers[ledgertitle][key]["datecreated"]
                            ));
                    }
                }
                File.Delete(fullPath);
                File.Copy(backupPath, fullPath);
                File.Delete(backupPath);
                // Read a file  
                string readText = File.ReadAllText(fullPath);
                Console.WriteLine(readText);
            }
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
            saveBooks();
            saveLedgerInfo();
            saveLedgers();
        }

        private void MoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobutton_Books.IsChecked)
            {
                string datecreated = System.DateTime.Now.ToString("dd-MM-yyyy HHmmssff");
                string Title = tbox_movetitle.Text;
                int count = 0;
                dg2selecteditems = new Dictionary<string, Dictionary<string, bool>> { };

                if (Title == "")
                {
                    Title = datecreated;
                }

                if (dg2.SelectedItems.Count > 0)
                {
                    Books book = new Books();
                    foreach (var obj in dg2.SelectedItems)
                    {
                        book = obj as Books;
                        if (!dg2selecteditems.ContainsKey(book.Title))
                        {
                            dg2selecteditems.Add(book.Title, new Dictionary<string, bool>());
                        }
                        dg2selecteditems[book.Title].Add(book.Number, true);
                        Console.WriteLine(dg2selecteditems[book.Title][book.Number]);

                        
                        if (!moveledgers.ContainsKey(Title))
                        {
                            moveledgers.Add(Title, new Dictionary<string, Dictionary<string, string>>());
                        }
                        string serial = books[book.Title][book.Number]["serial"];
                        moveledgers[Title].Add(serial, new Dictionary<string, string>());
                        moveledgers[Title][serial].Add("booktitle", book.Title);
                        moveledgers[Title][serial].Add("formerlocation", books[book.Title][book.Number]["location"]);
                        books[book.Title][book.Number]["location"] = MoveCB.Text;
                        moveledgers[Title][serial].Add("latterlocation", books[book.Title][book.Number]["location"]);
                        count++;
                    }
                }

               
                if (moveledgers.Count == 0)
                {
                    MessageBox.Show("Ledger is Empty!");
                }

                if (!moveledgersinfo.ContainsKey(Title))
                {
                    moveledgersinfo.Add(Title, new Dictionary<string, string>());
                    moveledgersinfo[Title].Add("datecreated", datecreated);
                    moveledgersinfo[Title].Add("entriescount", count.ToString());
                }



                updatedg1();
                tbox_ledgertitle.Text = "";

                dg2.Items.Clear();
                dg2.Columns.Clear();
            }
        }
        private void UnmarkDefective_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobutton_Books.IsChecked)
            {
                dg2selecteditems = new Dictionary<string, Dictionary<string, bool>> { };

                if (dg2.SelectedItems.Count > 0)
                {
                    Books book = new Books();
                    foreach (var obj in dg2.SelectedItems)
                    {
                        book = obj as Books;
                        if (!dg2selecteditems.ContainsKey(book.Title))
                        {
                            dg2selecteditems.Add(book.Title, new Dictionary<string, bool>());
                        }
                        dg2selecteditems[book.Title].Add(book.Number, true);
                        Console.WriteLine(dg2selecteditems[book.Title][book.Number]);

                        books[book.Title][book.Number]["defective"] = "False";
                    }
                }
                dg2.Items.Clear();
                dg2.Columns.Clear();
            }
        }
        private void MarkDefective_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobutton_Books.IsChecked)
            {
                dg2selecteditems = new Dictionary<string, Dictionary<string, bool>> { };

                if (dg2.SelectedItems.Count > 0)
                {
                    Books book = new Books();
                    foreach (var obj in dg2.SelectedItems)
                    {
                        book = obj as Books;
                        if (!dg2selecteditems.ContainsKey(book.Title))
                        {
                            dg2selecteditems.Add(book.Title, new Dictionary<string, bool>());
                        }
                        dg2selecteditems[book.Title].Add(book.Number, true);
                        Console.WriteLine(dg2selecteditems[book.Title][book.Number]);

                        books[book.Title][book.Number]["defective"] = "True";
                    }
                }
                dg2.Items.Clear();
                dg2.Columns.Clear();
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

        private void radiobutton_Movements_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Ledgers Clicked!");
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
            Load_books();
            Load_ledgerinfo();
            Load_ledgers();
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

        public Dictionary<string, Dictionary<string, string>> moveledgersinfo =
        new Dictionary<string, Dictionary<string, string>> { };

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> moveledgers =
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
            public string Location { get; set; }
        }
        public class Books
        {
            public string Title { get; set; }
            public string Number { get; set; }
            public string Serial { get; set; }
            public string Location { get; set; }
            public string Defective { get; set; }
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
            public string Location { get; set; }
            public string DateCreated { get; set; }
        }

        public class MoveLedgerInfo
        {
            public string Title { get; set; }
            public string DateCreated { get; set; }
            public string EntriesCount { get; set; }
        }

        public class MoveLedgers
        {
            public string Title { get; set; }
            public string Serial { get; set; }
            public string FormerLocation { get; set; }
            public string LatterLocation { get; set; }
        }

        private string[] bookinfoColumnHeaders = { "Title", "Quantity", "Size" };
        private string[] ledgerinfoColumnHeaders = { "Title", "DateCreated", "EntriesCount" };
        private string[] dg2ColumnHeaders = { "Title", "Number", "Serial", "Location", "Defective" };
        private string[] ledgeritemsColumnHeaders = { "Type", "LedgerTitle", "Title", "FormerQuantity", "Quantity", "LatterQuantity", "Size", "Location", "DateCreated" };
        private string[] moveinfoLedgersColumnHeaders = { "Title", "DateCreated", "EntriesCount" };
        private string[] moveledgersColumnHeaders = { "Title", "Serial", "FormerLocation", "LatterLocation"  };
    }
}
