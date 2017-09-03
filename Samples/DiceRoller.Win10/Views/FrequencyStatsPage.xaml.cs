using DiceRoller.Win10.Services;
using OnePlat.DiceNotation.DieRoller;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DiceRoller.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FrequencyStatsPage : Page, INotifyPropertyChanged
    {
        IList<AggregateDieTrackingData> frequencyData;

        public FrequencyStatsPage()
        {
            this.InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// Gets the list of types of rollers in the frequency data.
        /// </summary>
        public List<string> RollerTypes
        {
            get { return this.frequencyData.Select(d => d.RollerType).Distinct().ToList(); }
        }

        /// <summary>
        /// Gets the list of various dice rolled int he frequency data.
        /// </summary>
        public List<string> DiceSides
        {
            get { return this.frequencyData.Select(d => d.DieSides).Distinct().ToList(); }
        }

        /// <summary>
        /// Gets the list of display items for this page.
        /// </summary>
        public List<AggregateDieTrackingData> Items { get; private set; }

        /// <summary>
        /// Gets the maximum frequency value for the selected dataset.
        /// </summary>
        public float FrequencyMax
        { get; private set; }

        private double listViewWidth = 0;
        /// <summary>
        /// Gets or sets the ListViewWidth as it changes.
        /// </summary>
        public double ListViewWidth
        {
            get { return this.listViewWidth; }
            set
            {
                this.listViewWidth = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Helper methods

        /// <summary>
        /// Called when navigated to this page.
        /// </summary>
        /// <param name="e">event args</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.UpdateBusyProgress(true);
            this.frequencyData = await AppServices.Instance.DiceFrequencyTracker.GetFrequencyDataViewAsync();

            this.DataContext = this;

            if (this.RollerTypes.Count == 1)
            {
                this.RollerTypesCombobox.SelectedIndex = 0;
            }

            this.UpdateBusyProgress(false);
        }

        /// <summary>
        /// Updates the display state of the busy progress indicator.
        /// </summary>
        /// <param name="busy">Is the app busy</param>
        private void UpdateBusyProgress(bool busy)
        {
            this.BusyProgressBar.Visibility = busy ? Visibility.Visible : Visibility.Collapsed;
            this.ShowStatsButton.IsEnabled = !busy;
        }

        /// <summary>
        /// Updates the frequency data based on the filters selected
        /// on this page.
        /// </summary>
        private void UpdateFrequencyData()
        {
            string selectedRollerType = this.RollerTypesCombobox.SelectedValue as string;
            string selectedDiceSides = this.DiceSidesCombobox.SelectedValue as string;

            var list = from d in this.frequencyData
                       where d.RollerType == selectedRollerType && d.DieSides == selectedDiceSides
                       select d;

            if (list.Count() > 0)
            {
                this.FrequencyMax = list.Max(d => d.Percentage) + 1;
            }

            this.Items = list.ToList();
            this.StatsListView.ItemsSource = this.Items;
        }
        #endregion

        #region Event handlers

        /// <summary>
        /// Click handler for Settings page navigation.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        /// <summary>
        /// Click handler for returning to Home page.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        /// <summary>
        /// Click handler to show stats based on selected filters.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ShowStatsButton_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateFrequencyData();
        }

        /// <summary>
        /// Event handler for Stats listview size changing. Updates the property
        /// used to set the bar graph width.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void StatsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width != e.PreviousSize.Width)
            {
                this.ListViewWidth = e.NewSize.Width;
            }
        }
        #endregion

        #region INotifyPropertyChanged methods

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var changed = this.PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
