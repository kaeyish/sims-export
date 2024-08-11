using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.WPF.HCI.View;
using MenuNavigation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{
    public class CreateComplexRequestViewModel : INotifyPropertyChanged
    {

        #region Komande

        public RelayCommand AddSegments { get; set; }
        public RelayCommand AddPerson { get; set; }
        public RelayCommand SaveSegment { get; set; }
        public RelayCommand AddPeople { get; set; }
        public RelayCommand FinishSegments { get; set; }

        public RelayCommand SaveComplex { get; set; }
        public RelayCommand Back{ get; set; }


        public void Executed_AddSegments(object obj)
        {
            CreateSegment segments = new CreateSegment(this);
            _frame.Navigate(segments);
        }
        public void Executed_Back(object obj)
        {
            Requests requests = new Requests(_user, _frame);
            _frame.Navigate(requests);
        }
        public void Executed_AddPerson(object obj)
        {
            Person person = new Person(FirstName, LastName, int.Parse(Age));
            people.Add(person);
        }

        public void Executed_FinishSegments(object obj)
        {
            _requests.Add(new SimpleTourRequest(_user.Id, NameSegment, DescriptionSegment, Location.Split(',')[0], Location.Split(',')[0], Language, people, StartDate, EndDate, State.Pending, ReqType.Complex));
            CreateComplexMain back = new CreateComplexMain(this);
            _frame.Navigate(back);
        }
        
        public void Executed_SaveSegments(object obj)
        {
            _requests.Add(new SimpleTourRequest(_user.Id, NameSegment, DescriptionSegment, Location.Split(',')[0], Location.Split(',')[0], Language, people, StartDate, EndDate, State.Pending, ReqType.Complex));
            CreateSegment segments = new CreateSegment(this);
            _frame.Navigate(segments);

        }
        public void Executed_AddPeople(object obj)
        {
            //pass
        }
        public void Executed_SaveComplex(object obj)
        {
            foreach (SimpleTourRequest segment in _requests)
            {
                _requestService.Save(segment);
            }

            _service.Save(_user, Name, Description, _requests);
            Requests requests = new Requests(_user, _frame);
            _frame.Navigate(requests);
        }

        public bool CanExecute_NoCond(object obj)
        {
            return true;
        }
        public bool CanExecute_SaveCom(object obj)
        {
            if (_requests.Any())
            return true;
            else return false;
        }
        public bool CanExecute_AddPeople(object obj)
        {
            if ( PartySize!=null && int.Parse(PartySize) > -1)
            return true;
            else return false;
        }
        public bool CanExecute_AddPerson(object obj)
        {
            if ( Age!=null && int.Parse(Age) > 0)
                return true;
            else return false;
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
        private string _name { get; set; }

        private string _description { get; set; }

        public string Name { get { return _name; } set {  _name = value; OnPropertyChanged("Name"); } }

        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }

        private List <SimpleTourRequest> _requests { get; set; }

        private string _nameSegment {  get; set; }
        public string NameSegment { 
            get { return _nameSegment; }
            set { _nameSegment = value; OnPropertyChanged("NameSegment"); }
        }


        private string _descriptionSegment { get; set; }

        public string DescriptionSegment {
            get { return _descriptionSegment; }
            set { _descriptionSegment = value; OnPropertyChanged("DescriptionSegment"); }
        }

        private string _location { get; set; }
        public string Location {
            get { return _location; }
            set { _location = value; OnPropertyChanged("Location"); }
        }

        private string _language{ get; set; }
        public string Language
        {
            get { return _language; }
            set { _language = value; OnPropertyChanged("Language"); }
        }

        private string _partySize{ get; set; }
        public string PartySize
        {
            get { return _partySize; }
            set { _partySize = value; OnPropertyChanged("PartySize"); }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged("EndDate"); }
        }

        private string _firstName { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged("FirstName"); }
        }
        private string _lastName { get; set; }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("LastName"); }
        }

        private string _age { get; set; }
        public string Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        #endregion


        #region PrivateFields
        private List<Person> people;

        private SimpleTourRequestService _requestService;



        private User _user;

        private Frame _frame;

        private ComplexRequestService _service;

        #endregion

        public CreateComplexRequestViewModel(User user, Frame frame) {
            
            _user = user;
            _frame = frame;

            var app = (App)Application.Current;
            _service = app.ComplexRequestService;
            _requestService = app.SimpleTourRequestService;
            people = new List<Person>();
            _requests = new List <SimpleTourRequest>();

            AddSegments = new RelayCommand(Executed_AddSegments, CanExecute_NoCond);
            SaveSegment = new RelayCommand(Executed_SaveSegments, CanExecute_NoCond);
            AddPeople = new RelayCommand(Executed_AddPeople, CanExecute_AddPeople);
            SaveComplex = new RelayCommand(Executed_SaveComplex, CanExecute_SaveCom);
            FinishSegments = new RelayCommand(Executed_FinishSegments, CanExecute_NoCond);
            AddPerson = new RelayCommand(Executed_AddPerson, CanExecute_AddPerson);
            Back = new RelayCommand(Executed_Back, CanExecute_NoCond);

        }

        internal void ClearPeople()
        {
            people.Clear();
        }



    }
}
