using Marois.Framework.Core.Controls.Shared.Entities;
using Marois.Framework.Core.Controls.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Marois.Framework.Core.WPF.Controls
{
    /// <summary>
    /// Models the data required for a Navigation Pane
    /// </summary>
    public class MaroisNavigationPaneModel : UserControlBase
    {
        /// <summary>
        /// The header of the section
        /// </summary>
        public string? Header { get; set; }

        /// <summary>
        /// An instance of a class that implements the INavigationLoadArguments inteface 
        /// which passes load arguments to the data source
        /// </summary>
        public INavigationLoadArguments NavigationLoadArguments { get; set; }

        /// <summary>
        /// Determines if the navigation pane is expanded
        /// </summary>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// A delegate that represents the source of the data
        /// </summary>
        public Func<List<MaroisNavigationEntity>>? DataSource { get; set; }
    }
}
