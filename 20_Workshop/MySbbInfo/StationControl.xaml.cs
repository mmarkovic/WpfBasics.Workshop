﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationControl.xaml.cs" company="bbv Software Services AG">
//   Copyright (c) 2012
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// <summary>
//   Interaction logic for StationControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MySbbInfo
{
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    using SbbApi;
    using SbbApi.ApiClasses;

    /// <summary>
    /// Interaction logic for StationControl.xaml
    /// </summary>
    public partial class StationControl : UserControl
    {
        private ITransportService transportService;

        public StationControl()
        {
            this.InitializeComponent();
        }

        public void Initialize(ITransportService transportService)
        {
            this.transportService = transportService;
        }

        private void LoadStationboardClick(object sender, RoutedEventArgs e)
        {
            var stationboardOutputBuilder = new StringBuilder();

            this.stationBoardResult.Items.Clear();

            IEnumerable<Stationboard> stationboard = this.transportService.GetStationBoard(this.txtStation.Text);
            foreach (var stop in stationboard)
            {
                var part = string.Format("Ziel: {0}, Abfahrt: {1}, mittels: {2}", stop.To, stop.Stop.Departure, stop.Name);
                stationboardOutputBuilder.Append(part);

                if (!string.IsNullOrEmpty(stop.Stop.Delay))
                {
                    part = string.Format(", Verspätung: {0}", stop.Stop.Delay);
                    stationboardOutputBuilder.Append(part);
                }

                this.stationBoardResult.Items.Add(stationboardOutputBuilder.ToString());
                stationboardOutputBuilder.Clear();
            }
        }
    }
}
