using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;

using CITray.Api;

namespace CITray.Hudson
{
    internal class Server : IServer
    {
        private List<Project> projectList = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server() : this(string.Empty, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="projects">The projects.</param>
        public Server(string uri, IEnumerable<Project> projects)
        {
            if (!string.IsNullOrEmpty(uri)) Uri = new Uri(uri);

            if (projects != null)
                projectList = new List<Project>(projects);
            else projectList = new List<Project>();
        }
        
        public Uri Uri { get; set; }

        public NetworkCredential Credentials { get; set; }

        #region IServer Members

        /// <summary>
        /// Gets the projects that are managed by this server.
        /// </summary>
        /// <value>The server's projects.</value>
        public IEnumerable<IProject> Projects
        {
            get { return projectList.Cast<IProject>(); }
        }

        /// <summary>
        /// Gets the server display name.
        /// </summary>
        /// <value>The server display name.</value>
        public string DisplayName
        {
            get
            {
                if (Uri == null) return SR.ServerIsNotSet;
                return Uri.ToString();
            }
        }

        #endregion
    }
}
