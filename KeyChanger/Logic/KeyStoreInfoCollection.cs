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
    public class KeyStoreInfoCollection
        : ObservableCollection<KeyStoreInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class.
        /// </summary>
        public KeyStoreInfoCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkCollection"/> class that contains specified links.
        /// </summary>
        /// <param name="links">The links that are copied to this collection.</param>
        public KeyStoreInfoCollection(IEnumerable<KeyStoreInfo> files)
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
