using System;
using System.Windows.Forms;

namespace CITray.Api.UI
{
    public class BaseOptionsPanel : UserControl
    {
        private IServiceProvider services = null;

        protected IServiceProvider Services
        {
            get { return services; }
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");
            services = serviceProvider;
        }      
    }
}
