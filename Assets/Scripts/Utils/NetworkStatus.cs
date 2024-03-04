using UnityEngine;

namespace UntitledCube.Utils
{
    public static class NetworkStatus
    {
        /// <summary>
        /// Whether the device has any form of internet connection.
        /// </summary>
        public static bool IsConnected => Application.internetReachability != NetworkReachability.NotReachable;

        /// <summary>
        /// Whether the device is connected to the internet via a WiFi network.
        /// </summary>
        public static bool IsConnectedToWiFi => Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;

        /// <summary>
        /// Whether the device is connected to the internet via a cellular data network.
        /// </summary>
        public static bool IsConnectedToCellular => Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
    }
}
