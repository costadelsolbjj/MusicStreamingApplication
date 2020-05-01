using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStreamingApplication.Models;
using MusicStreamingApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicStreamingApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchSongsPage : ContentPage
    {
        public SearchSongsPage()
        {
            InitializeComponent();
        }

        private async void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue==null)
            return;
            var songsService = new SongsService();
           var songs =  await songsService.SearchSongs(e.NewTextValue);
            songsListView.ItemsSource = songs;
        }

        private void SongsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Songs songs = e.SelectedItem as Songs;

            Navigation.PushAsync(new AudioPlayerPage(songs));

        }
    }
}