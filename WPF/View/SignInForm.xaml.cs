using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.HCI.View;
using BookingApp.WPF.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private SignInViewModel _viewModel;


        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _viewModel = new SignInViewModel();
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    //CommentsOverview commentsOverview = new CommentsOverview(user);
                    //commentsOverview.Show();
                    //Close();
                    switch (user.Role)
                    {
                        case "Owner":
                            OwnerForm ownerForm = new OwnerForm(user);
                            ownerForm.Show();
                            Close();
                            break;
                        case "Superowner":
                            OwnerForm SuperownerForm = new OwnerForm(user);
                            SuperownerForm.Show();
                            Close();
                            break;
                        case "Guest":
                            GuestForm guestForm = new GuestForm(user);
                            guestForm.Show();
                            Close();
                            break;
                        case "SuperGuest":
                            if (!_viewModel.CheckSuperGuest(user.Id))
                            {
                                user.Role = "Guest";
                            }
                            GuestForm guestForm1 = new GuestForm(user);
                            guestForm1.Show();
                            Close();
                            break;
                        case "Guide":
                            GuideForm guideForm = new GuideForm(user);
                            guideForm.Show();
                            Close();
                            break;
                        case "Tourist":
                            Home tourist = new Home(user);
                            tourist.Show();
                            Close();
                            break;
                    }
                }  
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}