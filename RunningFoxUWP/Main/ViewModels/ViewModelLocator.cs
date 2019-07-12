using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Main.Views;
using System;

namespace Main.ViewModels
{
    public class ViewModelLocator
    {
        // commit
        //public ViewModelLocator()
        //{
        //example commit change
        //    var navigationService = this.createNavigationService();
        //    SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        //    SimpleIoc.Default.Register<MainViewModel>();
        //    SimpleIoc.Default.Register<EditSetMessageViewModel>();
        //    SimpleIoc.Default.Register<EditMessageViewModel>();
        //    SimpleIoc.Default.Register<PlayPageViewModel>();
        //example commit change 2
        //}


        //Another commit is ready


        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
        
        //remote commit change 3
        public EditSetMessageViewModel EditSetMessageViewModel => SimpleIoc.Default.GetInstance<EditSetMessageViewModel>();
        //rempove commit change 5
        public EditMessageViewModel EditMessageViewModel => SimpleIoc.Default.GetInstance<EditMessageViewModel>(Guid.NewGuid().ToString());
        
        // remote commit change 4
        public PlayPageViewModel PlayPageViewModel => SimpleIoc.Default.GetInstance<PlayPageViewModel>();


      //test 2
        private INavigationService createNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("EditSet", typeof(EditSetMessagePage));
            navigationService.Configure("EditMessage", typeof(EditMessagePage));
            navigationService.Configure("PlayPage", typeof(PlayPage));
            return navigationService;
        }
    }
}
//test;
