using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStreamingApplication.Models;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicStreamingApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPlayerPage : ContentPage
    {
        public AudioPlayerPage(Songs songs)
        {
            InitializeComponent();
            LblSongName.Text = songs.SongName;
            LblSingerName.Text = songs.SingerName;
            ImgSongCover.Source = songs.SongFileCover;
            CrossMediaManager.Current.PlayingChanged += Current_PlayingChanged;
            CrossMediaManager.Current.Play(new MediaFile(songs.SongUrl, MediaFileType.Audio));
        }

        private void Current_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
        {
            LblStartTime.Text = e.Position.ToString(@"mm\:ss");
            LblEndTime.Text = e.Duration.ToString(@"mm\:ss");

            PbAudioPlayer.Progress = e.Progress / 100;


        }

        private void TapFastFwd_OnTapped(object sender, EventArgs e)
        {
            //CrossMediaManager.Current.StepForward();
        }

        private bool _isClicked = false;

        private void TapPausePlay_OnTapped(object sender, EventArgs e)
        {
            if (_isClicked)
            {
                CrossMediaManager.Current.Play();
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

        private void TapFastBwd_OnTapped(object sender, EventArgs e)
        {
            //CrossMediaManager.Current.StepBackward();
        }
    }
}