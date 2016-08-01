using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Main.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditMessagePage : Page
    {
        public List<object> numbersList { get; set; }


        public EditMessagePage()
        {
            this.InitializeComponent();


            numbersList = new List<object>();


            numbersList.Add("1");
            numbersList.Add("2");
            numbersList.Add("3");
            numbersList.Add("4");
            numbersList.Add("5");
            numbersList.Add("6");
            numbersList.Add("7");
            numbersList.Add("8");
            numbersList.Add("9");
            numbersList.Add("10");
            numbersList.Add("11");
            numbersList.Add("12");
            numbersList.Add("13");
            numbersList.Add("14");
            numbersList.Add("15");
            numbersList.Add("16");


            loadNumbers();



            ListView1.ItemsSource = NewNumbersList;

            ListView1.SelectedIndex = 0;

        }


        private ObservableCollection<object> _newNumbersList;
        public ObservableCollection<object> NewNumbersList => _newNumbersList ?? (_newNumbersList = new ObservableCollection<object>());




        public IEnumerable<object> Get(int skip, int take)
        {
            return numbersList.Skip(skip).Take(take);
        }



        private void loadNumbers()
        {
            var numbers = Get(NewNumbersList.Count(), 5);
            foreach (var item in numbers)
            {
                NewNumbersList.Add(item);
            }
        }



        private void testBtn_Click(object sender, RoutedEventArgs e)
        {

            ListView1.SelectedIndex = ListView1.SelectedIndex + 1;
            ListView1.ScrollIntoView(ListView1.Items[ListView1.SelectedIndex]);

            loadNumbers();



            if (ListView1.SelectedIndex>=6)
            {
                NewNumbersList.Remove("1");
                NewNumbersList.Remove("2");
                NewNumbersList.Remove("3");
                //NewNumbersList.Remove("4");
                //NewNumbersList.Remove("5");



                NewNumbersList.Add("1");
                NewNumbersList.Add("2");
                NewNumbersList.Add("3");
                //NewNumbersList.Add("4");
                //NewNumbersList.Add("5");
            }


   
        //if (testLst.SelectedIndex > 0)
        //{
        //    testLst.ScrollIntoView(testLst.Items[testLst.SelectedIndex - 1]);
        //}

        //if (testLst.SelectedIndex < testLst.Items.Count-1)
        //{
        //    testLst.ScrollIntoView(testLst.Items[testLst.SelectedIndex - 1]);
        //}
    }


        private void _addNumbers(int numbers)
        {

        }

        private void _removeNumbers(int numbers)
        {

        }
        
    }
}
