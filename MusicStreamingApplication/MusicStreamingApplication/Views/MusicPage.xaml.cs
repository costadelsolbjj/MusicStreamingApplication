using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStreamingApplication.Models;
using MusicStreamingApplication.Services;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicStreamingApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPage : ContentPage
    {
        private int pageNumber;
        private int pageSize = 10;
        public ObservableCollection<Songs> SongsData;
        public MusicPage()
        {
            pageNumber = 0;
            InitializeComponent();
            SongsData = new ObservableCollection<Songs>();
            GetSongs();
        }

        private async Task GetSongs()
        {

            try
            {
                SlBottomLoader.IsVisible = true;
                pageNumber++;
                var songsService = new SongsService();
                var songs = await songsService.GetAllSongs(pageNumber, pageSize);
                foreach (var song in songs)
                {
                    SongsData.Add(song);
                }
                lvAudioSongs.ItemsSource = SongsData;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                SlBottomLoader.IsVisible = false;

            }
        }

        private async void LvAudioSongs_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as Songs;
            var index = SongsData.IndexOf(item);

            if (SongsData.Count - 2 <= index)
            {
                await GetSongs();
            }

        }

        private void LvAudioSongs_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            SlAudioPlayer.IsVisible = true;
            var song = e.SelectedItem as Songs;
            CrossMediaManager.Current.PlayingChanged += Current_PlayingChanged;
            if (song != null)
            {
                CrossMediaManager.Current.Play(new MediaFile(song.SongUrl, MediaFileType.Audio));
                LblSongName.Text = song.SongName;
                ImgSongCover.Source = song.SongFileCover;
            }

        }

        private void Current_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
        {
            PbAudio.Progress = e.Progress / 100;
        }

        private bool _isClicked = false;
        private void TapPausePlay_OnTapped(object sender, EventArgs e)
        {
            if (_isClicked)
            {
                CrossMediaManager.Current.PlaybackController.Play();
                ImgPausePlay.Source = ImageSource.FromFile("pause.png");
                _isClicked = false;
            }
            else
            {
                CrossMediaManager.Current.Pause();
                ImgPausePlay.Source = ImageSource.FromFile("play.png");
                _isClicked = true;
            }
        }

        private void Searchtoolbar_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchSongsPage());
        }
    }
}