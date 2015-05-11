using System;

namespace IGT.TestFramework.EGM
{
    public interface ICompactEGM
    {
        /// <summary>
        /// Connects to the device using the specified connection string.
        /// </summary>
        /// <param name="connectionStr">The connection string to use
        /// (i.e. "COM1" for serial ports, etc.).</param>
        /// <returns>True for a successful connection, false otherwise.</returns>
        bool Connect(string connectionStr);

        /// <summary>
        /// Connects to the device using the specified connection string and delay between device commands.
        /// </summary>
        /// <param name="connectionStr">The connection string to use
        /// (i.e. "COM1" for serial ports, etc.).</param>
        /// <param name="interCommandDelay">The time to wait (ms) between individual commands
        /// sent to the device.</param>
        /// <returns>True for a successful connection, false otherwise.</returns>
        bool Connect(string connectionStr, int interCommandDelay);

        /// <summary>
        /// Disconnects from the device.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Send data to the device. This data can be multiple tokens separated by:
        /// ';', ':', ',', '\n', '\r\n', '\t', or '|'
        /// </summary>
        /// <param name="data">The string data to send to the device.</param>
        void Send(string data);

        /// <summary>
        /// Send data to the device and wait for a response. This data can be multiple tokens separated by:
        /// ';', ':', ',', '\n', '\r\n', '\t', or '|'
        /// </summary>
        /// <param name="data">The string data to send to the device.</param>
        /// <param name="timeout">The length of time (ms) to wait for the device to respond.</param>
        /// <returns></returns>
        string Send(string data, int timeout);

        /// <summary>
        /// True if currently connected to a device, false otherwise.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Event that is raised when data comes through from the device.
        /// </summary>
        event EventHandler<EGMDataEventArgs> EGMDataReceived;
    }

    public class EGMDataEventArgs : EventArgs
    {
        public EGMDataEventArgs(string dataReceived)
        {
            Data = dataReceived;
        }

        public string Data
        { get; private set; }
    }
}
