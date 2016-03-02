using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UITemplate.Controls
{
    /// <summary>
    /// Represents an observable collection of links.
    /// </summary>
    public class TabItemCollection
        : ObservableCollection<TabItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class.
        /// </summary>
        public TabItemCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="links">The links that are copied to this collection.</param>
        public TabItemCollection(IEnumerable<TabItem> links)
        {
            if (links == null)
            {
                throw new ArgumentNullException("links");
            }
            foreach (var link in links)
            {
                Add(link);
            }
        }

        public TabItem FindTabByName(string tabName)
        {
            foreach (TabItem item in Items)
            {
                if (item.DisplayName == tabName)
                    return item;
            }
            return null;
        }

    }
}
