using DiceRoller.Win10.Services;
using OnePlat.DiceNotation.DieRoller;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DiceRoller.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FrequencyStatsPage : Page
    {
        IList<AggregateDieTrackingData> frequencyData;

        public FrequencyStatsPage()
        {
            this.InitializeComponent();

            this.frequencyData = AppServices.Instance.DiceFrequencyTracker.GetFrequencyDataView();
 
            this.DataContext = this;

            if (this.RollerTypes.Count == 1)
            {
                this.RollerTypesCombobox.SelectedIndex = 0;
            }
        }

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

            this.Items = list.ToList();
            this.StatsListView.ItemsSource = this.Items;
        }

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
        #endregion
    }
}
