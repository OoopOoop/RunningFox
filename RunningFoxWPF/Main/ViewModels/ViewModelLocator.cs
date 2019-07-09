using GalaSoft.MvvmLight.Ioc;
using Main.Shared;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModels
{
    //TODO this doesnt compile on my unix
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SetupNavigation();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MessageSetViewModel>();
            SimpleIoc.Default.Register<MessageEditViewModel>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();

            navigationService.Configure("MainPage", new Uri("../Views/MainPage.xaml", UriKind.Relative));
            navigationService.Configure("EditMessageSet", new Uri("../Views/EditMessageSet.xaml", UriKind.Relative));
            navigationService.Configure("RunPage", new Uri("../Views/RunPage.xaml", UriKind.Relative));
            navigationService.Configure("NewMessageEdit", new Uri("../Views/NewMessageEdit.xaml", UriKind.Relative));


            SimpleIoc.Default.Unregister<IFrameNavigationService>();
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        

        public MessageSetViewModel MessageSetViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MessageSetViewModel>(Guid.NewGuid().ToString());
            }
        }



        public MessageEditViewModel MessageEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MessageEditViewModel>(Guid.NewGuid().ToString());
            }
        }
       
    }
}
