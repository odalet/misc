using System.Collections.Generic;

namespace CITray.Api
{
    /// <summary>
    /// Represents a Continuous Integration server.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets the server display name.
        /// </summary>
        /// <value>The server display name.</value>
        string DisplayName { get; }

        /// <summary>
        /// Gets the projects that are managed by this server.
        /// </summary>
        /// <value>The server's projects.</value>
        IEnumerable<IProject> Projects { get; }
    }
}
