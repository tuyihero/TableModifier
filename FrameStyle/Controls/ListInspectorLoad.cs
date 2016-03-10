using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows;

namespace UITemplate.Controls
{
    /// <summary>
    /// Loads lorem ipsum content regardless the given uri.
    /// </summary>
    public class ListInspectorLoad
        : DefaultContentLoader
    {
        /// <summary>
        /// Loads the content from specified uri.
        /// </summary>
        /// <param name="uri">The content uri</param>
        /// <returns>The loaded content.</returns>
        protected override object LoadContent(Uri uri)
        {
            if (String.IsNullOrEmpty(uri.OriginalString))
                return null;

            PageManager.Instance.CurXmlFile = new XmlFileInfoPanel(uri);
            return PageManager.Instance.CurXmlFile;
        }
    }
}
