using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BookingApp.WPF.ViewModel
{
    public class MyToursViewModel : INotifyPropertyChanged
    {
        int counter = -1;
        private TourService _service;

        private TouristService _touristService;

        private User _loggedUser;

        private ObservableCollection<InstancePackDto> _instances { get; set; }

        public ObservableCollection<InstancePackDto> Instances
        {
            get { return _instances; }
            set { _instances = value; OnPropertyChanged("Instances"); }
        }

        private Frame _frame;

        private Tourist _tourist;


        private InstancePackDto _tourInstance;
        public InstancePackDto SelectedInstance
        {
            get { return _tourInstance; }
            set { _tourInstance = value; OnPropertyChanged("SelectedInstance"); }
        }

        public MyToursViewModel(User user, Status? status, Frame frame)
        {
            var app = (App)Application.Current;
            _touristService = app.TouristService;
            _service = app.TourService;
            _tourist = _touristService.FindOne(user.Id);
            Instances = new ObservableCollection<InstancePackDto>(_service.FindUsersTours(_tourist, status));
            _loggedUser = user;
            _frame = frame;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        internal void SwitchToRating(InstancePackDto data)
        {
            if (data.Status != "Finished") return;
            HCI.View.Rating rating = new HCI.View.Rating(_loggedUser, data, _frame);
            _frame.Navigate(rating);
        }


        internal void SwitchToTracking(InstancePackDto selectedItem)
        {
            if (selectedItem.Status != "OnGoing") return;
        }

        internal void Refresh(Status? status = null)
        {
            Instances.Clear();
            Instances = new ObservableCollection<InstancePackDto>(_service.FindUsersTours(_tourist, status));
        }



        public List<Tour> Tours { get; set; }

        public void CreatePdf()
        {
            counter++;
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reservation Report";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create fonts
            XFont headerFont = new XFont("Verdana", 20);
            XFont tableFont = new XFont("Verdana", 12);
            XFont HeadnoteFont = new XFont("Verdana", 10);

            // Draw the header
            gfx.DrawString("Generation time: " + DateTime.Now.ToString(), HeadnoteFont, XBrushes.Black, new XRect(0, 0, page.Width, 40), XStringFormats.Center);
            gfx.DrawString("Your Reservation Report", headerFont, XBrushes.Black, new XRect(0, 50, page.Width, 40), XStringFormats.Center);

            // Define table structure
            double yPoint = 90;
            double xPoint = 40;
            double rowHeight = 20;
            double columnWidth = (page.Width - 2 * xPoint) / 4; // Assuming 4 columns

            // Draw table headers
            gfx.DrawString("Name", tableFont, XBrushes.Black, new XRect(xPoint, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
            gfx.DrawString("Status", tableFont, XBrushes.Black, new XRect(xPoint +  3* columnWidth, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
            gfx.DrawString("Location", tableFont, XBrushes.Black, new XRect(xPoint +  columnWidth, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
            gfx.DrawString("Start Date", tableFont, XBrushes.Black, new XRect(xPoint +  2 * columnWidth, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
         
            // Draw a line below the header
            gfx.DrawLine(XPens.Black, xPoint, yPoint + rowHeight, page.Width - xPoint, yPoint + rowHeight);

            // Increment yPoint for data rows
            yPoint += rowHeight;

            // Draw the table data
            foreach (InstancePackDto tour in _instances)
            {
                gfx.DrawString(tour.Name, tableFont, XBrushes.Black, new XRect(xPoint, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
                gfx.DrawString(tour.Status, tableFont, XBrushes.Black, new XRect(xPoint + 3 * columnWidth, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
                gfx.DrawString(tour.Location.ToString(), tableFont, XBrushes.Black, new XRect(xPoint + columnWidth, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
                gfx.DrawString(tour.Date, tableFont, XBrushes.Black, new XRect(xPoint + 2 * columnWidth, yPoint, columnWidth, rowHeight), XStringFormats.TopLeft);
                
                // Draw a line below each row
                gfx.DrawLine(XPens.Gray, xPoint, yPoint + rowHeight, page.Width - xPoint, yPoint + rowHeight);

                // Increment yPoint for next row
                yPoint += rowHeight;
            }

            // Get the path to the user's Downloads folder
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";

                string name = "Reservation Report " + "- " + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                downloadsPath = System.IO.Path.Combine (downloadsPath, name);

            try {
                // Save the document
                document.Save(downloadsPath);
            } catch (Exception ex) { MessageBox.Show("Cannot save file. Check if it's already open!"); }
            // Optional: Open the PDF file automatically after saving
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(downloadsPath) { UseShellExecute = true });
        }


    }
}
