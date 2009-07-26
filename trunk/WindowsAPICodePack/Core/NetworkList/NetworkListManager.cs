//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Microsoft.WindowsAPICodePack
{
    /// <summary>
    /// Provides access to objects that represent networks and network connections.
    /// </summary>
    public static class NetworkListManager
    {
        #region Private Fields

        static NetworkListManagerClass manager = new NetworkListManagerClass();

        #endregion // Private Fields

        /// <summary>
        /// Retrieves a collection of <see cref="Microsoft.WindowsAPICodePack.Network"/> objects that represent the networks defined for this machine.
        /// </summary>
        /// <param name="level">
        /// The <see cref="Microsoft.WindowsAPICodePack.NetworkConnectivityLevels"/> that specify the connectivity level of the returned <see cref="Microsoft.WindowsAPICodePack.Network"/> objects.
        /// </param>
        /// <returns>
        /// A <see cref="Microsoft.WindowsAPICodePack.NetworkCollection"/> of <see cref="Microsoft.WindowsAPICodePack.Network"/> objects.
        /// </returns>
        public static NetworkCollection GetNetworks(NetworkConnectivityLevels level)
        {
            return new NetworkCollection(manager.GetNetworks(level));
        }

        /// <summary>
        /// Retrieves the <see cref="Microsoft.WindowsAPICodePack.Network"/> identified by the specified network identifier.
        /// </summary>
        /// <param name="networkId">
        /// A <see cref="System.Guid"/> that specifies the unique identifier for the network.
        /// </param>
        /// <returns>
        /// The <see cref="Microsoft.WindowsAPICodePack.Network"/> that represents the network identified by the identifier.
        /// </returns>
        public static Network GetNetwork(Guid networkId)
        {
            return new Network(manager.GetNetwork(networkId));
        }

        /// <summary>
        /// Retrieves a collection of <see cref="Microsoft.WindowsAPICodePack.NetworkConnection"/> objects that represent the connections for this machine.
        /// </summary>
        /// <returns>
        /// A <see cref="Microsoft.WindowsAPICodePack.NetworkConnectionCollection"/> containing the network connections.
        /// </returns>
        public static NetworkConnectionCollection GetNetworkConnections()
        {
            return new NetworkConnectionCollection(manager.GetNetworkConnections());
        }

        /// <summary>
        /// Retrieves the <see cref="Microsoft.WindowsAPICodePack.NetworkConnection"/> identified by the specified connection identifier.
        /// </summary>
        /// <param name="networkConnectionId">
        /// A <see cref="System.Guid"/> that specifies the unique identifier for the network connection.
        /// </param>
        /// <returns>
        /// The <see cref="Microsoft.WindowsAPICodePack.NetworkConnection"/> identified by the specified identifier.
        /// </returns>
        public static NetworkConnection GetNetworkConnection(Guid networkConnectionId)
        {
            return new NetworkConnection(manager.GetNetworkConnection(networkConnectionId));
        }

        /// <summary>
        /// Gets a value that indicates whether this machine 
        /// has Internet connectivity.
        /// </summary>
        /// <value>A <see cref="System.Boolean"/> value.</value>
        public static bool IsConnectedToInternet
        {
            get
            {
                return manager.IsConnectedToInternet;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether this machine 
        /// has network connectivity.
        /// </summary>
        /// <value>A <see cref="System.Boolean"/> value.</value>
        public static bool IsConnected
        {
            get
            {
                return manager.IsConnected;
            }
        }

        /// <summary>
        /// Gets the connectivity state of this machine.
        /// </summary>
        /// <value>A <see cref="Microsoft.WindowsAPICodePack.Connectivity"/> value.</value>
        public static Connectivity Connectivity
        {
            get
            {
                return manager.GetConnectivity();
            }
        }
    }

}