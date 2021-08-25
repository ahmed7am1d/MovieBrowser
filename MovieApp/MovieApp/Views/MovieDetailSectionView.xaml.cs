using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovieApp.ViewModels;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailSectionView : ContentView
    {
        int SelectionIndex = 0;
        List<Label> tabHeaders = new List<Label>();
        List<VisualElement> tabContents = new List<VisualElement>();
        public MovieDetailSectionView()
        {
            InitializeComponent();

            tabHeaders.Add(InfoTab);
            tabHeaders.Add(ActorsTab);
            tabHeaders.Add(WritersTab);
            tabHeaders.Add(AwardsTab);
            tabHeaders.Add(MediaTab);

            tabContents.Add(InfoContent);
            tabContents.Add(ActorsContent);
            tabContents.Add(WriterContent);
            tabContents.Add(AwardsContent);
            tabContents.Add(MediaContent);


        }

        #region Navigation Method (Translation)
        private async Task ShowSelection(int newTabIndex)
        {
            //don't navigate if we are in the same tab 
            if (newTabIndex == SelectionIndex) return;

            //navigation of the underline (on current tabbed header)
            var NewSelectedLabel = tabHeaders[newTabIndex];
            //navigate to bounds so it fit exactly under the label 
            await SelectionUnderline.TranslateTo(NewSelectedLabel.Bounds.X, 0, 150, easing: Easing.SinInOut);

            //update the style of the Selected Tab and make the old one back to it is orginal color 
            var SelectedStyle = (Style)Application.Current.Resources["SelectedTabLable"];
            var unSelectedStyle = (Style)Application.Current.Resources["TabPageLabels"];
            NewSelectedLabel.Style = SelectedStyle;
            tabHeaders[SelectionIndex].Style = unSelectedStyle;

            //---------- showing the content of tab ---------
            await tabContents[SelectionIndex].FadeTo(0);
            tabContents[SelectionIndex].IsVisible = false;
            //new tab content (new selected one ) content or grid will be visible 
            tabContents[newTabIndex].IsVisible = true;
            _= tabContents[newTabIndex].FadeTo(1);


            //updating the selectionIndex Value
            SelectionIndex = newTabIndex;
        }
        #endregion

        #region Gesture click even to obtain info about sended label(Cliked)
        //we are getting the sended or clicked label and then obtain index of it to navigate to it 
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var tabIndex = tabHeaders.IndexOf((Label)sender);
            await ShowSelection(tabIndex);

        }
        #endregion

    }
}