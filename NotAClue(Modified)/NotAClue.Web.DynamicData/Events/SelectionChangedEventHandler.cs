using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotAClue.Web.DynamicData
{
    /// <summary>
    /// Delegate for the Interface
    /// </summary>
    /// <param name="sender">
    /// A parent control also implementing the 
    /// ISelectionChangedEvent interface
    /// </param>
    /// <param name="e">
    /// An instance of the SelectionChangedEventArgs
    /// </param>
    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);
}
