//******************************************************
// File: MainWindow.xaml.cs
//
// Purpose: Contains the c# code for TrainScheduleApplication
//
// Contains methods to load json files, query database, event handlers for events such as window load, listbox item selected
//
// Written By: Prabhdeep Singh
//
// Compiler: Visual Studio 2015
//
//******************************************************



using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;


using TrainSchedule;

namespace TrainScheduleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        // declaring BranchSchedule & TrainCollection variables to use in application
        private static BranchSchedule bc;
        private static TrainCollection trainCollection;


        public MainWindow()
        {
            InitializeComponent();

            // loads traincollection from pre-defined filename (traincollection.json)
            readTrainCollection();
        }

        // using observable collection of type Station for listbox
        private ObservableCollection<Station> stCollection = new ObservableCollection<Station>();


        //  creating a StationCollection variable to hold
        // the StationCollection in future (on file load)
        StationCollection stCol;


        // load window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // load the station listbox from database using loadFromDB method
            loadFromDB();
        }//Window_Loaded


        // load data from DB , connects to db & runs select query
        private void loadFromDB()
        {
            string connString = @"server = (localdb)\MSSQLLocalDB;" +
                              "integrated security = SSPI;" +
                              "database = TrainSchedule;" +
                              "MultipleActiveResultSets = True;";


            SqlConnection sqlConn;

            sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            // select query to query db
            string sql = "SELECT * FROM Stations";
            SqlCommand command = new SqlCommand(sql, sqlConn);

            SqlDataReader reader = command.ExecuteReader();

            //listBox_stations.ItemsSource = reader;
            listBox_stations.DisplayMemberPath = "Name";

            // while loop to loop through sql query results and processing the results
            while (reader.Read())
            {
                // prepares values for instantiating a station and adding it to observable station collection
                //Console.WriteLine(reader["Name"]);
                int id = Convert.ToInt32(reader["StationId"]);
                string name = reader["Name"].ToString();
                string location = reader["Location"].ToString();
                int fareZone = Convert.ToInt32(reader["FareZone"]);

                double mileageToPenn = Convert.ToDouble(reader["MileageToPenn"]);
                string picFileName = reader["PicFilename"].ToString();

                Station st = new Station(id, name, location, fareZone, mileageToPenn, picFileName);
                // add to station collection
                stCollection.Add(st);
            }

            // set the listbox item source - binding
            listBox_stations.ItemsSource = stCollection;

            sqlConn.Close();
        }



        // click on Import >> Import Stations from JSON File
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // load station data from json file into station table
            loadDBFromFile();
        } //MenuItem_Click


        // method to load json data into station table
        private void loadDBFromFile()
        {
            // connection string
            string connString = @"server = (localdb)\MSSQLLocalDB;" +
                              "integrated security = SSPI;" +
                              "database = TrainSchedule;" +
                              "MultipleActiveResultSets = True;";

            SqlConnection sqlConn;

            sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            #region filedialog
            // creating a open dialog and setting required properties
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            string path = Directory.GetCurrentDirectory();
            dlg.InitialDirectory = path;
            dlg.Filter = "JSON|*.json";
            dlg.Title = "Open StationCollection From JSON";
            #endregion

            if (dlg.ShowDialog() == true)
            {
                #region file input
                string fullPath = path + "\\" + dlg.SafeFileName;

                // instantiating stCol (StationCollectio) from the method which reads json in
                // readStationCollectionJSON takes json path and returns a StationCollection
                stCol = readStationCollectionJSON(fullPath);

                // need to truncate stations table before loading data
                string sqlTrim = "TRUNCATE TABLE Stations;";
                SqlCommand commandTrim = new SqlCommand(sqlTrim, sqlConn);
                int rowsAffectedTrim = commandTrim.ExecuteNonQuery();


                // function to add stations in StationCollection to listview
                foreach (Station s in stCol.Stations)
                {
                    //listBox_stations.Items.Add(s);
                    //Console.WriteLine(s);

                    // SQL statement to actually enter one row data in db
                    string sql = string.Format(
                        "Insert INTO Stations" +
                        "(StationId,Name,Location,FareZone,MileageToPenn,PicFilename) Values" +
                        "('{0}' ,'{1}' ,'{2}' ,'{3}','{4}','{5}')", s.Id, s.Name, s.Location, s.FareZone, s.MileageToPenn, s.PicFilename);

                    SqlCommand command = new SqlCommand(sql, sqlConn);

                    // execute the insert
                    int rowsAffected = command.ExecuteNonQuery();

                }
                #endregion

                // reloads data for listbox from station table, so user does not have to reload the window again
                loadFromDB();

            }

            sqlConn.Close();
        } //loadDBFromFile

        // this method is a reusable method that reads StationCollection from a give filename in parameter
        // @params filename - name of json file
        // returns the whole instance of StationCollection object

        private void listBox_branchtrains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string sid = listBox_branchtrains.SelectedItem.ToString();
                int id = Convert.ToInt32(sid);
                Train tr = trainCollection.FindTrain(id);
                listView_stationarrival.ItemsSource = tr.StationArrivals;

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }


            //Console.WriteLine(tr);

            //Console.WriteLine(listBox_branchtrains.SelectedItem);
            //Console.WriteLine(id);





        }
        private void listBox_stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Station s = (Station)listBox_stations.SelectedItem;
            //Console.WriteLine(s.Id);


            String stringPath = s.PicFilename;
            Uri imageUri = new Uri(stringPath, UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            //Image myImage = new Image();
            image_Station.Source = imageBitmap;


            //=  new Uri(s.PicFilename, UriKind.Relative);

            //image_Station.Source = new ImageSourceConverter().ConvertFromString(s.PicFilename) as ImageSource;


            //Console.WriteLine(listBox_stations.SelectedItem.ToString());
            //Console.WriteLine(e.AddedItems.ToString());

            //Console.WriteLine(listBox_stations.SelectedItem.ToString());
            ////tb_name.DataContext = e; 
            //Console.WriteLine(e);
            //Console.WriteLine(e.AddedItems);

            //Console.WriteLine(e.AddedItems[0]);


        }

   

        // click on File -> Exit   

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // click on File -> Open Branch Schedule , loads data into branch schedule tab
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            #region filedialog
            // creating a open dialog and setting required properties
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            string path = Directory.GetCurrentDirectory();
            dlg.InitialDirectory = path;
            dlg.Filter = "JSON|*.json";
            dlg.Title = "Open BranchCollection From JSON";
            #endregion

            if (dlg.ShowDialog() == true)
            {
                #region file input
                string fullPath = path + "\\" + dlg.SafeFileName;

                // instantiating stCol (StationCollectio) from the method which reads json in
                bc = readBranchScheduleJSON(fullPath);

                Console.WriteLine(bc);

                // set branch id number
                tb_BID.Text = bc.BranchId.ToString();
                // set ItemsSource for branch train listbox
                listBox_branchtrains.ItemsSource = bc.Id;
     
                #endregion

            }


        }

        // click on Help >> About , displays messagebox
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Train Schedule \nVersion 1.0.1 \nWritten by Prabhdeep Singh", "Train Schedule");
        }

        // method to read json and return a StationCollection instance
        // @params : filename
        public static StationCollection readStationCollectionJSON(string filename)
        {

            //string filename = filename;

            #region	StationCollection deserialization
            DataContractJsonSerializer SCJSer = new DataContractJsonSerializer(typeof(StationCollection));

            // open filestream reader for json file
            FileStream SCReader = new FileStream(filename, FileMode.Open, FileAccess.Read);
            // reading object in from json
            StationCollection inputSC = (StationCollection)SCJSer.ReadObject(SCReader);
            SCReader.Close();
            //Console.WriteLine();
            Console.WriteLine("StationCollection.JSON imported.\n");
            //Console.WriteLine();
            //Console.WriteLine("StationCollection Info : " + inputSC.ToString());
            // return the read StationCollection instance
            return inputSC;
            #endregion
        } //readStationCollectionJSON


        // method to read json and return a BranchSchedule instance
        // @params : filename
        public static BranchSchedule readBranchScheduleJSON(string filename)
        {
            //BranchSchedule
            //string filename = filename;

            #region	BranchSchedule deserialization
            DataContractJsonSerializer SCJSer = new DataContractJsonSerializer(typeof(BranchSchedule));

            // open filestream reader for json file
            FileStream SCReader = new FileStream(filename, FileMode.Open, FileAccess.Read);
            // reading object in from json
            BranchSchedule inputSC = (BranchSchedule)SCJSer.ReadObject(SCReader);
            SCReader.Close();
            //Console.WriteLine();
            Console.WriteLine("BranchCollection.JSON imported.\n");
            //Console.WriteLine();
            //Console.WriteLine("StationCollection Info : " + inputSC.ToString());
            // return the read StationCollection instance
            return inputSC;
            #endregion
        } //readBranchCollectionJSON


        // read train collection
        // loads train collection data on window load, filename set automatically (user not asked to select a file)
        // file name TrainCollection.json , resides in the project 
        private static void readTrainCollection()
        {
            //Console.WriteLine();
            //Console.Write("Please enter the TrainCollection JSON Filename to read : ");
            //string filename = Console.ReadLine();
            //// set filename
            string filename = "TrainCollection.json";

            #region	TrainCollection deserialization
            DataContractJsonSerializer TCSer = new DataContractJsonSerializer(typeof(TrainCollection));

            // open filestream reader for json file
            FileStream TCReader = new FileStream(filename, FileMode.Open, FileAccess.Read);
            // reading object in from json
            trainCollection = (TrainCollection)TCSer.ReadObject(TCReader);
            TCReader.Close();
            //Console.WriteLine();
            Console.WriteLine("TrainCollection.JSON imported.\n");
            //Console.WriteLine();
            Console.WriteLine("TrainCollection Info : " + trainCollection.ToString());
            #endregion
        }


       
    } // partial class

} // namespace

