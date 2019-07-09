using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Main.Views;
using System;

namespace Main.ViewModels
{
    public class ViewModelLocator
    {
        //well now look, another changes
        public ViewModelLocator()
        {
            var navigationService = this.createNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditSetMessageViewModel>();
            SimpleIoc.Default.Register<EditMessageViewModel>();
            SimpleIoc.Default.Register<PlayPageViewModel>();
        }

   

        //commit 4
        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
        
        //test commit 3
        public EditSetMessageViewModel EditSetMessageViewModel => SimpleIoc.Default.GetInstance<EditSetMessageViewModel>();
        //test commit 3
        public EditMessageViewModel EditMessageViewModel => SimpleIoc.Default.GetInstance<EditMessageViewModel>(Guid.NewGuid().ToString());
        
        //test comment 1
        public PlayPageViewModel PlayPageViewModel => SimpleIoc.Default.GetInstance<PlayPageViewModel>();


        //test comment
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
