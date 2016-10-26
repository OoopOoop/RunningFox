using Main.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
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
    public sealed partial class EditSetMessagePage : Page
    {
        ObservableCollection<string> _reference;
      //  ObservableCollection<string> _selection;
        string _deletedItem;


        public EditSetMessagePage()
        {
            this.InitializeComponent();
            
         //   _reference = GetSampleData();
            //_selection = new ObservableCollection<string>();
           // SourceListView.ItemsSource = _reference;
           // messagesListView.ItemsSource = _selection;
        }



        //private ObservableCollection<string> GetSampleData()
        //{
        //    return new ObservableCollection<string>
        //    {
        //        "My Research Paper",
        //        "Electricity Bill",
        //        "My To-do list",
        //        "TV sales receipt",
        //        "Water Bill",
        //        "Grocery List",
        //        "Superbowl schedule",
        //        "World Cup E-ticket"
        //    };
        //}

        private void SourceListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            // Prepare a string with one dragged item per line
            var items = new StringBuilder();
            foreach (var item in e.Items)
            {
                if (items.Length > 0) items.AppendLine();
                items.Append(item as string);
            }
            // Set the content of the DataPackage
            e.Data.SetText(items.ToString());
            // As we want our Reference list to say intact, we only allow Copy
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }


        //private void TargetListView_DragOver(object sender, DragEventArgs e)
        //{
        //    // Our list only accepts text
        //    e.AcceptedOperation = (e.DataView.Contains(StandardDataFormats.Text)) ? DataPackageOperation.Copy : DataPackageOperation.None;
        //}


        //private async void TargetListView_Drop(object sender, DragEventArgs e)
        //{
        //    // This test is in theory not needed as we returned DataPackageOperation.None if
        //    // the DataPackage did not contained text. However, it is always better if each
        //    // method is robust by itself
        //    if (e.DataView.Contains(StandardDataFormats.Text))
        //    {
        //        // We need to take a Deferral as we won't be able to confirm the end
        //        // of the operation synchronously
        //        var def = e.GetDeferral();
        //        var s = await e.DataView.GetTextAsync();
        //        var items = s.Split('\n');
 
        //        foreach (var item in items)
        //        {
        //             _reference.Add(item);
                  
        //        }
        //        e.AcceptedOperation = DataPackageOperation.Copy;
        //        def.Complete();
        //    }
        //}


        private void TargetListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
            // args.DropResult is always Move and therefore we have to rely on _deletedItem to distinguish
            // between reorder and move to trash
            // Another solution would be to listen for events in the ObservableCollection

        
            //TODO: bind to data context item selection changed event?
            if (_deletedItem != null)
            {
                _reference.Remove(_deletedItem);
                            
                _deletedItem = null;
            }
        }


        private async void TargetTextBlock_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                // We need to take the deferral as the source will read _deletedItem which
                // we cannot set synchronously
                var def = e.GetDeferral();
                _deletedItem = await e.DataView.GetTextAsync();
                e.AcceptedOperation = DataPackageOperation.Move;
                def.Complete();
            }
        }


        private void TargetTextBlock_DragEnter(object sender, DragEventArgs e)
        {
            // Trash only accepts text
            e.AcceptedOperation = (e.DataView.Contains(StandardDataFormats.Text) ? DataPackageOperation.Move : DataPackageOperation.None);
            // We don't want to show the Move icon
            e.DragUIOverride.IsGlyphVisible = false;
            e.DragUIOverride.Caption = "Drop item here to remove it from selection";
        }



        private void TargetListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            // The ListView is declared with selection mode set to Single.
            // But we want the code to be robust
            if (e.Items.Count == 1)
            {
                e.Data.SetText(e.Items[0] as string);
                // Reorder or move to trash are always a move
                e.Data.RequestedOperation = DataPackageOperation.Move;
                _deletedItem = null;
            }
        }
 
    }
}
