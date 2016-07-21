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
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            SetupNavigation();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditViewModel>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();

            navigationService.Configure("MainPage", new Uri("../Views/MainPage.xaml", UriKind.Relative));
            navigationService.Configure("EditPage", new Uri("../Views/EditPage.xaml", UriKind.Relative));

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
        

        public EditViewModel EditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditViewModel>(Guid.NewGuid().ToString());
            }
        }

    }
}
