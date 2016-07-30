﻿using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Main.Views;
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
            var navigationService = this.createNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditSetMessageViewModel>();
            SimpleIoc.Default.Register<EdditMessageViewModel>();

        }
        
        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
        public EditSetMessageViewModel EditSetMessageViewModel => SimpleIoc.Default.GetInstance<EditSetMessageViewModel>();
        public EdditMessageViewModel EdditMessageViewModel => SimpleIoc.Default.GetInstance<EdditMessageViewModel>();


        private INavigationService createNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("EditSet", typeof(EditSetMessagePage));
            navigationService.Configure("EditMessage", typeof(EditMessagePage));
            return navigationService;
        }
    }
}
