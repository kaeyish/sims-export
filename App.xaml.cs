using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TourService TourService { get; set; }
        public TouristService TouristService { get; set; }
        public NotificationService NotificationService { get; set; }
        public TourRatingService TourRatingService { get; set; }
        public TourReservationService TourReservationService { get; set; }
        public VoucherService VoucherService { get; set; }

        public SimpleTourRequestService SimpleTourRequestService { get; set; }
        public ComplexRequestService ComplexRequestService { get; set; }

        public AccommodationRenovationRecommendationService AccommodationRenovationRecommendationService { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public RenovationService RenovationService { get; set; }
        public OwnerRatingService OwnerRatingService { get; set; }
        public RatingService RatingService { get; set; }
        public SuperGuestService SuperGuestService { get; set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            
            // Register the repositories
            services.AddSingleton<ITourRepository, TourRepository>(); 
            services.AddSingleton<ITouristRepository, TouristRepository>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();
            services.AddSingleton<ITourRatingRepository, TourRatingRepository>();
            services.AddSingleton<IVoucherRepository, VoucherRepository>();
            services.AddSingleton<ITourReservationRepository, TourReservationRepository>();
            services.AddSingleton<ISimpleTourRequestRepository, SimpleTourRequestRepository>();
            services.AddSingleton<IComplexRequestRepository, ComplexRequestRepository>();
            services.AddSingleton<IRenovationRecommendationRepository, RenovationRecommendationRepository>();
            services.AddSingleton<IAccommodationReservationRepository, AccommodationReservationRepository>();
            services.AddSingleton<IAccommodationRepository, AccommodationRepository>();
            services.AddSingleton<IRenovationRepository, RenovationRepository>();
            services.AddSingleton<IOwnerRatingRepository, OwnerRatingRepository>();
            services.AddSingleton<IRatingRepository, RatingRepository>();
            services.AddSingleton<ISuperGuestRepository, SuperGuestRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();






            // Register the services
            services.AddTransient<TourService>(); 
            services.AddTransient<TouristService>();
            services.AddTransient<NotificationService>();
            services.AddTransient<TourRatingService>();
            services.AddTransient<VoucherService>();
            services.AddTransient<TourReservationService>();
            services.AddTransient<SimpleTourRequestService>();
            services.AddTransient<ComplexRequestService>();
            services.AddTransient<AccommodationRenovationRecommendationService>();
            services.AddTransient<AccommodationReservationService>();
            services.AddTransient<AccommodationService>();
            services.AddTransient<RenovationService>();
            services.AddTransient<OwnerRatingService>();
            services.AddTransient<RatingService>();
            services.AddTransient<SuperGuestService>();


            // Other service registrations go here

            
            var serviceProvider = services.BuildServiceProvider();

            // Resolve the services
            TourService = serviceProvider.GetRequiredService<TourService>();
            TouristService = serviceProvider.GetRequiredService<TouristService>();
            VoucherService = serviceProvider.GetRequiredService<VoucherService>();
            NotificationService = serviceProvider.GetRequiredService<NotificationService>();
            TourReservationService = serviceProvider.GetRequiredService<TourReservationService>();
            TourRatingService = serviceProvider.GetRequiredService<TourRatingService>();
            VoucherService = serviceProvider.GetRequiredService<VoucherService>();
            SimpleTourRequestService = serviceProvider.GetRequiredService<SimpleTourRequestService>();
            ComplexRequestService = serviceProvider.GetRequiredService<ComplexRequestService>();
            AccommodationRenovationRecommendationService = serviceProvider.GetRequiredService<AccommodationRenovationRecommendationService>();
            AccommodationReservationService = serviceProvider.GetRequiredService<AccommodationReservationService>();
            AccommodationService = serviceProvider.GetRequiredService<AccommodationService>();
            RenovationService = serviceProvider.GetRequiredService<RenovationService>();
            OwnerRatingService = serviceProvider.GetRequiredService<OwnerRatingService>();
            RatingService = serviceProvider.GetRequiredService<RatingService>();
            SuperGuestService = serviceProvider.GetRequiredService<SuperGuestService>();



        }
    }
}
