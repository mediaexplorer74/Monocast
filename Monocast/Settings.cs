﻿using System;
using Windows.Storage;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Diagnostics;

namespace Monocast
{
    [DataContract]
    public class Settings : INotifyPropertyChanged
    {
        #region Private Variables
        private ApplicationDataContainer roamingSettings = null;

        // Defaults
        private const bool syncOnLaunch_DEFAULT = false;
        private const int episodesToKeep_DEFAULT = 20;
        private const int skipForwardTime_DEFAULT = 30;
        private const int skipBackTime_DEFAULT = 10;
        private const bool useEpisodeArtwork_DEFAULT = false;
        private const bool cachePodcastArtwork_DEFAULT = true;
        private const bool showArchived_DEFAULT = true;
        private const bool sortPodcastsByName_DEFAULT = false;

        private bool _SyncOnLaunch;
        private int _EpisodesToKeep;
        private int _SkipForwardTime;
        private int _SkipBackTime;
        private bool _UseEpisodeArtwork;
        private bool _CachePodcastArtwork;
        private bool _ShowArchived = true;
        private bool _SortPodcastsByName;
        private string _DownloadFolderToken;
        #endregion

        #region Public Properties
        public bool SyncOnLaunch
        {
            get => _SyncOnLaunch;
            set
            {
                if (_SyncOnLaunch != value)
                {
                    _SyncOnLaunch = value;
                    RaisePropertyChanged(nameof(SyncOnLaunch));
                    roamingSettings.Values[nameof(SyncOnLaunch)] = value;
                }
            }
        }

        public int EpisodesToKeep
        {
            get => _EpisodesToKeep;
            set
            {
                if (value < 1 || _EpisodesToKeep == value) return;
                _EpisodesToKeep = value;
                RaisePropertyChanged(nameof(EpisodesToKeep));
                roamingSettings.Values[nameof(EpisodesToKeep)] = value;
            }
        }

        public int SkipForwardTime
        {
            get => _SkipForwardTime;
            set
            {
                if (value < 1 || _SkipForwardTime == value) return;
                _SkipForwardTime = value;
                RaisePropertyChanged(nameof(SkipForwardTime));
                roamingSettings.Values[nameof(SkipForwardTime)] = value;
            }
        }

        public int SkipBackTime
        {
            get => _SkipBackTime;
            set
            {
                if (value < 1 || _SkipBackTime == value) return;
                _SkipBackTime = value;
                RaisePropertyChanged(nameof(SkipBackTime));
                roamingSettings.Values[nameof(SkipBackTime)] = value;
            }
        }

        public bool UseEpisodeArtwork
        {
            get => _UseEpisodeArtwork;
            set
            {
                if (_UseEpisodeArtwork != value)
                {
                    _UseEpisodeArtwork = value;
                    RaisePropertyChanged(nameof(UseEpisodeArtwork));
                    roamingSettings.Values[nameof(UseEpisodeArtwork)] = value;
                }
            }
        }

        public bool CachePodcastArtwork
        {
            get => _CachePodcastArtwork;
            set
            {
                if (_CachePodcastArtwork != value)
                {
                    _CachePodcastArtwork = value;
                    RaisePropertyChanged(nameof(CachePodcastArtwork));
                    roamingSettings.Values[nameof(CachePodcastArtwork)] = value;
                }
            }
        }

        public bool ShowArchived
        {
            get => _ShowArchived;
            set
            {
                if (_ShowArchived != value)
                {
                    _ShowArchived = value;
                    RaisePropertyChanged(nameof(ShowArchived));
                    roamingSettings.Values[nameof(Version)] = value;
                }
            }
        }

        public bool SortPodcastsByName
        {
            get => _SortPodcastsByName;
            set
            {
                if (_SortPodcastsByName != value)
                {
                    _SortPodcastsByName = value;
                    RaisePropertyChanged(nameof(SortPodcastsByName));
                    roamingSettings.Values[nameof(SortPodcastsByName)] = value;
                }
            }
        }

        public string DownloadFolderToken
        {
            get => _DownloadFolderToken;
            set
            {
                if (_DownloadFolderToken != value)
                {
                    _DownloadFolderToken = value;
                    RaisePropertyChanged(nameof(DownloadFolderToken));
                    roamingSettings.Values[nameof(DownloadFolderToken)] = value;
                }
            }
        }
        #endregion

        #region Constructors & Static Methods
        public event PropertyChangedEventHandler PropertyChanged;
        public Settings()
        {
            roamingSettings = ApplicationData.Current.RoamingSettings;

            if (roamingSettings == null)
                throw new Exception("Roaming settings cannot be null");

            SyncOnLaunch = getSetting(nameof(SyncOnLaunch), syncOnLaunch_DEFAULT);
            EpisodesToKeep = getSetting(nameof(EpisodesToKeep), episodesToKeep_DEFAULT);
            SkipForwardTime = getSetting(nameof(SkipForwardTime), skipForwardTime_DEFAULT);
            SkipBackTime = getSetting(nameof(SkipBackTime), skipBackTime_DEFAULT);
            UseEpisodeArtwork = getSetting(nameof(UseEpisodeArtwork), useEpisodeArtwork_DEFAULT);
            CachePodcastArtwork = getSetting(nameof(CachePodcastArtwork), cachePodcastArtwork_DEFAULT);
            SortPodcastsByName = getSetting(nameof(SortPodcastsByName), sortPodcastsByName_DEFAULT);
            DownloadFolderToken = getSetting(nameof(DownloadFolderToken), string.Empty);
        }
        #endregion

        #region Private Functions
        private void RaisePropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private T getSetting<T>(string settingName, T defaultValue)
        {
            try
            {
                Object obj = roamingSettings.Values[settingName];
                if (obj == null || obj.GetType() != typeof(T))
                {
                    return defaultValue;
                }
                return (T)obj;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return defaultValue;
            }
        }
        #endregion
    }
}
