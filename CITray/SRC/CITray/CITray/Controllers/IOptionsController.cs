using System;
using System.Collections.Generic;

using CITray.UI.Options;

namespace CITray.Controllers
{
    internal interface IOptionsController
    {
        /// <summary>
        /// Gets an instance of the options dialog.
        /// </summary>
        /// <returns>A <see cref="CITray.UI.OptionsDialog"/> object.</returns>
        OptionsDialog GetOptionsDialog();

        IEnumerable<OptionsSection> TopLevelSections { get; }
    }
}
