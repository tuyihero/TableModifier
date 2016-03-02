using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeyChanger
{
    /// <summary>
    /// Represents an observable collection of links.
    /// </summary>
    public class KeyChangeItemCollection
        : ObservableCollection<KeyChangeItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class.
        /// </summary>
        public KeyChangeItemCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="links">The links that are copied to this collection.</param>
        public KeyChangeItemCollection(IEnumerable<KeyChangeItem> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("links");
            }
            foreach (var link in files)
            {
                Add(link);
            }
        }

        #region



        #endregion
    }
}
