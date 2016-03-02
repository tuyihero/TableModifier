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
    public class PathItemCollection
        : ObservableCollection<PathItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class.
        /// </summary>
        public PathItemCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="links">The links that are copied to this collection.</param>
        public PathItemCollection(IEnumerable<PathItem> links)
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

        public void RemoveBehind(PathItem pathItem)
        {
            if (Items.Contains(pathItem))
            {
                int idx = Items.IndexOf(pathItem);
                while (Items.Count > idx + 1)
                {
                    Items.RemoveAt(idx + 1);
                }
            }
        }
    }
}
