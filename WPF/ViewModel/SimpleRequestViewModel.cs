using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.WPF.HCI.View;
using MenuNavigation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{
    public class SimpleRequestViewModel : INotifyPropertyChanged
    {
        #region Komande
        public RelayCommand OpenSegments {  get; set; }
        public RelayCommand CreateSegments {  get; set; }
        public RelayCommand CreateRegular {  get; set; }
        public RelayCommand CreateComplex {  get; set; }
        public RelayCommand Stats {  get; set; }


        public void Executed_OpenSegment(object obj)
        {
                Segments segments = new Segments(_user, SelectedRequest.Segments, _frame, SelectedRequest.Name);
                _frame.Navigate(segments);
         }

        public bool CanExecute_OpenSegments(object obj)
        {
            if (SelectedRequest == null) { return false; }
            
            return true;
        }
        
        public void Executed_CreateRegular(object obj)
        {
            CreateRequest request = new CreateRequest(this);
            _frame.Navigate(request);
        }
        public void Executed_CreateSegments(object obj)
        {
            _type = ReqType.Complex;
            CreateRequest request = new CreateRequest(this);
            _frame.Navigate(request);
        }


        public void Executed_CreateComplex(object obj)
        {

            CreateComplexMain main = new CreateComplexMain(_user, _frame);
            _frame.Navigate(main);

        }
        public void Executed_Stats(object obj)
        {
            LanguageHistogramData = _service.GetLanguageHistogramData(_user, null);
            LocationHistogramData = _service.GetLocationHistogramData(_user, null);
            PieData = _service.GetApprovalRate(_user, null);
            AllYears = _service.GetAllYears(_user);
            AvgPeople = _service.GetAvgPeople(_user, null);

            RequestStats stats = new RequestStats(this);
            _frame.Navigate(stats);
        }

        public bool CanExecute_Others(object obj)
        { 
            return true;
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
        #endregion

        #region BindableFields
        private ObservableCollection<SimpleTourRequest> _requests;

        public ObservableCollection<SimpleTourRequest> RequestsVM { 
            get { return _requests; }
            set { _requests = value; OnPropertyChanged("RequestsVM"); }
        } 
        
        private ObservableCollection<ComplexTourRequest> _complexRequests;

        public ObservableCollection<ComplexTourRequest> ComplexRequests { 
            get { return _complexRequests; }
            set { _complexRequests = value; OnPropertyChanged("ComplexRequests"); }
        }
        
        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours{ 
            get { return _tours; }
            set { _tours = value; OnPropertyChanged("Tours"); }
        }

        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _description;

        public string Description {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }
        
        private string _location;

        public string Location {
            get { return _location; }
            set { _location = value; OnPropertyChanged("Location"); }
        }
        
        private string _language;

        public string Language {
            get { return _language; }
            set { _language = value; OnPropertyChanged("Language"); }
        }

        private string _partySize;

        public string PartySize {
            get { return _partySize; }
            set { _partySize = value; OnPropertyChanged("PartySize"); }
        }

        private DateTime _startDate;

        public DateTime StartDate {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }
        
        private DateTime _endDate;

        public DateTime EndDate {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged("EndDate"); }
        }
        
        private string _selectedYear;
        
        public string SelectedYear {
            get { return _selectedYear; }
            set { _selectedYear = value; OnPropertyChanged("SelectedYear"); }


        }   
        private ComplexTourRequest _request {  get; set; }

        public ComplexTourRequest SelectedRequest {
            get { return _request; }
            set { _request = value; OnPropertyChanged("SelectedRequest"); }
        }

        #endregion

        #region NonBindable fields
        private List<Person> people; 

        private Frame _frame;

        private User _user;

        private SimpleTourRequestService _service;
        private ComplexRequestService _complexService;

        private List<KeyValuePair<string, int>> _languageHistogramData;

        private ReqType _type;
        #endregion

        #region Charts
        
        
        public List<KeyValuePair<string, int>> LanguageHistogramData {
            get { return _languageHistogramData; }
            set { _languageHistogramData = value; OnPropertyChanged("LanguageHistogramData"); }
        }

        private List<KeyValuePair<string, int>> _locationHistogramData;

        public List<KeyValuePair<string, int>> LocationHistogramData {
            get { return _locationHistogramData; }
            set { _locationHistogramData = value; OnPropertyChanged("LocationHistogramData"); }
        }
        
        private List<KeyValuePair<string, double>> _pieData;

        public List<KeyValuePair<string, double>> PieData{
            get { return _pieData; }
            set { _pieData= value; OnPropertyChanged("PieData"); }
        }



        private ObservableCollection<string> _allYears;

        public ObservableCollection<string> AllYears {
            get { return _allYears; }
            set { _allYears = value; OnPropertyChanged("AllYears"); }
        }

         private string _avgPeople;
        
         public string AvgPeople {
            get { return _avgPeople; }
            set { _avgPeople= value; OnPropertyChanged("AvgPeople"); }
         }

        #endregion



        #region Konstruktor
        public SimpleRequestViewModel(User user, Frame frame) {
            _user = user;
            _frame = frame;
            var app = (App)Application.Current;
            _service = app.SimpleTourRequestService;
            _complexService = app.ComplexRequestService;

            Tours = new ObservableCollection<Tour>(app.TourService.FindAll());

            OpenSegments = new RelayCommand(Executed_OpenSegment, CanExecute_OpenSegments);
            CreateComplex = new RelayCommand(Executed_CreateComplex, CanExecute_Others);
            CreateRegular = new RelayCommand(Executed_CreateRegular , CanExecute_Others);
            Stats = new RelayCommand(Executed_Stats, CanExecute_Others);
            CreateSegments = new RelayCommand(Executed_CreateSegments, CanExecute_Others);


            RequestsVM = new ObservableCollection<SimpleTourRequest>(_service.FindAll(_user));
            ComplexRequests = new ObservableCollection<ComplexTourRequest>(_complexService.FindAll(_user));
            people = new List<Person>();
            LanguageHistogramData = new List<KeyValuePair<string, int>>();
            SelectedYear = "<All Years>";
            AvgPeople = "0";
            _type = ReqType.Simple;
        }

        #endregion


      

        #region Statistics Methods
        internal void RefreshCharts()
        {
            LanguageHistogramData = _service.GetLanguageHistogramData(_user, int.Parse(SelectedYear));
            LocationHistogramData = _service.GetLocationHistogramData(_user, int.Parse(SelectedYear));
            PieData = _service.GetApprovalRate(_user, int.Parse(SelectedYear));
            AvgPeople = _service.GetAvgPeople(_user, int.Parse(SelectedYear));
        }

        #endregion


        #region Person

        internal void AddPerson(string personName, string personLastName, string age)
        {
            Person person = new Person(personName, personLastName, int.Parse(age));
            people.Add(person);
        }

        internal void ClearPeople()
        {
            people.Clear();
        }

        #endregion


        internal void Save()
        {
            //if (people.Count() < int.Parse(PartySize))
            //    MessageBox.Show("Greska pri unosu ljudi");
           // else
            {
                _service.Save(_user,Name, Description, Location, Language, StartDate, EndDate, people, _type);
                Requests requests = new Requests(_user, _frame);
                _frame.Navigate(requests);
            }
        }

        #region Segments

        

            internal void CreateNewSegments()
            {
                CreateComplexMain main = new CreateComplexMain(_user, _frame);
                _frame.Navigate(main);
            }
    
        internal void Back()
        {
            Requests requests = new Requests(_user, _frame);
            _frame.Navigate(requests);
        }
        #endregion

        #region Refreshes

        internal void RefreshSimple(State? state) {

            if (state.HasValue)
                RequestsVM = new ObservableCollection<SimpleTourRequest>(_service.Refresh(state.Value, _user));
            else RequestsVM = new ObservableCollection<SimpleTourRequest>(_service.FindAll(_user));
        }

        internal void RefreshComplex(State? state)
        {
            if (state.HasValue)
                ComplexRequests = new ObservableCollection<ComplexTourRequest>(_complexService.Refresh(state.Value, _user));
            else ComplexRequests = new ObservableCollection<ComplexTourRequest>(_complexService.FindAll(_user));
        }


        #endregion

        internal void SendNotif()
        {
            Tour tour = new Tour();
            tour.Id = 123;
            tour.Location = new Location("Taipei", "Taiwan");
            tour.Language = "French";
            _service.SendNotifications(tour);
            MessageBox.Show("Poslato.");
        }

        

    }
}
