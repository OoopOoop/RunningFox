using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Main.Views;
using System;
using Main.Models;

namespace Main.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditSetMessageViewModel>();
            SimpleIoc.Default.Register<EditMessageViewModel>();
            SimpleIoc.Default.Register<PlayPageViewModel>();
            SimpleIoc.Default.Register<IMessageSetTable, MessageSetTable>();
            SimpleIoc.Default.Register<IMessageTableCollection, MessageTableCollection>();
            SimpleIoc.Default.Register<MessageTable>();
        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public EditSetMessageViewModel EditSetMessageViewModel => SimpleIoc.Default.GetInstance<EditSetMessageViewModel>();

        public EditMessageViewModel EditMessageViewModel => SimpleIoc.Default.GetInstance<EditMessageViewModel>(Guid.NewGuid().ToString());
        
        public PlayPageViewModel PlayPageViewModel => SimpleIoc.Default.GetInstance<PlayPageViewModel>();

        private INavigationService CreateNavigationService()
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
