﻿using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using PANDA.Command;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PANDA.ViewModel
{
    public class ProjectHelperSettingsViewModel : ViewModel
    {
        #region Members

        public List<VOBItem> DefaultVOBs { get; set; }

        #endregion


        #region Default Constructor

        public ProjectHelperSettingsViewModel()
        {
            SetupCommands();

            m_selectedVOB = null;
            m_selectedVOBs = new ObservableCollection<VOBItem>();
            m_VOBAutocompleteSource = new VOBAutocompleteSource();

            m_selectedView = null;
            m_ViewAutocompleteSource = new ViewAutocompleteSource();
        }

        #endregion


        #region Databinding


        private IAutocompleteSource m_ViewAutocompleteSource;
        public IAutocompleteSource ViewAutocompleteSource
        {
            get { return m_ViewAutocompleteSource; }
            set
            {
                ViewAutocompleteSource = value;
            }
        }

        private string m_currentView;
        public string CurrentView
        {
            get { return m_currentView; }
            set
            {
                m_currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private DirectoryInfo m_selectedView;
        public DirectoryInfo SelectedView
        {
            get { return m_selectedView; }
            set
            {
                m_selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        private IAutocompleteSource m_VOBAutocompleteSource;
        public IAutocompleteSource VOBAutocompleteSource
        {
            get { return m_VOBAutocompleteSource; }
            set
            {
                m_VOBAutocompleteSource = value;
            }
        }

        private VOBItem m_selectedVOB;
        public VOBItem SelectedVOB
        {
            get { return m_selectedVOB; }
            set
            {
                m_selectedVOB = value;
                OnPropertyChanged(nameof(SelectedVOB));
                AddToSelectedVOBs(value);
            }
        }

        private ObservableCollection<VOBItem> m_selectedVOBs;
        public ObservableCollection<VOBItem> SelectedVOBs
        {
            get { return m_selectedVOBs; }
            set
            {
                m_selectedVOBs = value;
                OnPropertyChanged(nameof(SelectedVOBs));
            }
        }

        #endregion


        #region Relay Commands

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        #endregion


        #region Events

        private void Cancel(object o)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
        }

        private void Save(object o)
        {
            DialogHost.CloseDialogCommand.Execute(true, null);
        }

        #endregion


        #region Methods

        private void SetupCommands()
        {
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        public void AddToSelectedVOBs(VOBItem item)
        {
            // Add to SelectedVOBs if the value is non-null and not already on the list
            if (item != null && !SelectedVOBs.Any(i => i.Name.Equals(item.Name)))
            {
                SelectedVOBs.Add(item);
                OnPropertyChanged(nameof(SelectedVOBs));
            }
        }

        public void RemoveFromSelectedVOBs(VOBItem item)
        {
            // Remove From SelectedVOBs if the value is on the list
            var itemToRemove = SelectedVOBs.SingleOrDefault(i => i.Name.Equals(item.Name));
            if (itemToRemove != null)
            { 
                SelectedVOBs.Remove(itemToRemove);
                OnPropertyChanged(nameof(SelectedVOBs));
            }
        }

        #endregion
    }
}