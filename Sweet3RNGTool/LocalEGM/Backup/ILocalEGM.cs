using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGT.TestFramework.RemoteSwitch;

namespace IGT.TestFramework.EGM
{
    #region Enumerated Types
    public enum Platforms
    {
        AVP, /*AVP_Video, AVP_Stepper,*/ Video_960, Stepper_960, Unknown
    };

    public enum GameTypes
    {
        SlotLine, SlotMultiway, Poker, Keno, Blackjack, Other
    };

    public enum DomesticBillDenominations
    {
        One = 1, Two = 2, Five = 5, Ten = 10, Twenty = 20, Fifty = 50, OneHundred = 100
    };

    public enum Doors
    {
        MainDoor = 1, DropDoor = 2, CashboxDoor = 3, LogicDoor = 4, CardCageDoor = 5
    };
    #endregion Enumerated Types

    public interface ILocalEGM
    {
        #region Methods
        /// <summary>
        /// Adds the dollar bill of the specifed bill denomination to the EGM.
        /// </summary>
        /// <param name="billDenomination"></param>
        /// <param name="countryCode"></param>
        void BillIn(DomesticBillDenominations billDenomination, int countryCode);

        /// <summary>
        /// Adds the dollar bill of the specified bill denomination to the EGM.
        /// Bill denomination must correspond to an actual bill denomination (i.e. $1 is valid,
        /// but $9 is not). This method is mainly used for compatibility with COM, otherwise
        /// use method 'BillIn'.
        /// </summary>
        /// <param name="billDenomination"></param>
        /// <param name="countryCode"></param>
        void BillIn2(int billDenomination, int countryCode);

        /// <summary>
        /// Adds the specified number of coins to the EGM.
        /// </summary>
        /// <param name="numberOfCoins"></param>
        void CoinIn(int numberOfCoins);

        /// <summary>
        /// Simulates inserting a player card into the EGM.
        /// </summary>
        /// <param name="trackNumber">The track number (1-4).</param>
        /// <param name="trackData">The data corresponding to the specified track number.</param>
        void CardIn(int trackNumber, string trackData);

        /// <summary>
        /// Simulates removal of a player card from the EGM.
        /// </summary>
        void CardOut();

        /// <summary>
        /// Plays a game on the EGM at the current bet by pressing the "SPIN/DEAL/DRAW" button.
        /// </summary>
        void PlayGame();

        /// <summary>
        /// Opens the specified door on the EGM.
        /// </summary>
        /// <param name="door">The door type to open.</param>
        void OpenDoor(Doors door);

        /// <summary>
        /// Opens the door specified by the door integer.
        /// 1 = main door
        /// 2 = drop door
        /// 3 = cashbox door
        /// 4 = logic door
        /// 5 = card cage door
        /// </summary>
        /// <param name="door"></param>
        void OpenDoor2(int door);

        /// <summary>
        /// Closes the specified door on the EGM.
        /// </summary>
        /// <param name="door">The door type to close.</param>
        void CloseDoor(Doors door);

        /// <summary>
        /// Closes the specified door on the EGM.
        /// </summary>
        /// <param name="door">The door type to close.</param>
        void CloseDoor2(int door);

        /// <summary>
        /// Bets 1 credit on the EGM by pressing the BET 1 button.
        /// Only works on 7-button panel games. Ignored on 15-button panel games.
        /// </summary>
        void Bet1();

        /// <summary>
        /// Bets the maximum number of credits on the EGM by pressing the Max Bet button.
        /// </summary>
        void BetMax();

        /// <summary>
        /// Presses the PLAY 1 button on the EGM.
        /// </summary>
        void Play1();

        /// <summary>
        /// Presses the PLAY 2 button on the EGM.
        /// </summary>
        void Play2();

        /// <summary>
        /// Presses the PLAY 3 button on the EGM.
        /// </summary>
        void Play3();

        /// <summary>
        /// Presses the PLAY 4 button on the EGM.
        /// </summary>
        void Play4();

        /// <summary>
        /// Presses the PLAY 5 button on the EGM.
        /// </summary>
        void Play5();

        /// <summary>
        /// Presses the HOLD 1 button on the EGM.
        /// </summary>
        void Hold1();

        /// <summary>
        /// Presses the HOLD 2 button on the EGM.
        /// </summary>
        void Hold2();

        /// <summary>
        /// Presses the HOLD 3 button on the EGM.
        /// </summary>
        void Hold3();

        /// <summary>
        /// Presses the HOLD 4 button on the EGM.
        /// </summary>
        void Hold4();

        /// <summary>
        /// Presses the HOLD 5 button on the EGM.
        /// </summary>
        void Hold5();

        /// <summary>
        /// Cashes out on the EGM.
        /// </summary>
        void Cashout();

        /// <summary>
        /// Turns the jackpot reset key on the EGM.
        /// </summary>
        void TurnJackpotResetKey();

        /// <summary>
        /// Sets the EGM to the test random number generator. This means the user can supply
        /// random numbers to send to the game and trigger specific results.
        /// </summary>
        void SetTestRNG();

        /// <summary>
        /// Sets the EGM to the real random number generator. This means the EGM will generate its
        /// own random numbers when playing games.
        /// </summary>
        void SetRealRNG();

        /// <summary>
        /// Toggles the random number generator to test mode and sets it to the supplied array of values.
        /// </summary>
        /// <param name="rngVals">The array containing the RNG values.</param>
        void SendAllRNGValues(int[] rngVals);

        /// <summary>
        /// For reel-based slot games, toggles the random number generator and sets it to the supplied
        /// reel stop RNG values.
        /// </summary>
        /// <param name="reelStops">The array containing the reel stop RNG values.</param>
        void SendReelStopRNGValues(int[] reelStops);

        /// <summary>
        /// Sends one or more raw PAD commands to the EGM as text strings.
        /// </summary>
        /// <param name="commands">The array containing the PAD command strings</param>
        void SendPADCommands(string[] commands);

        /// <summary>
        /// Sends one or more raw PAD commands to the EGM as text strings and waits for a response from
        /// the EGM. The method will return when the EGM sends a response or after the specified timeout period
        /// passes.
        /// </summary>
        /// <param name="commands">The array containing the PAD command strings</param>
        /// <param name="timeout">
        /// The period of time to wait for a response from the EGM before returning (in milliseconds).
        /// </param>
        /// <returns></returns>
        string SendPADCommandsAndWait(string[] commands, int timeout);

        /// <summary>
        /// Gets the win amount of the last played game (returns 0 if the last
        /// game played was a losing game).
        /// </summary>
        /// <returns>
        /// The win amount of the last played game on the EGM. For losing games, returns 0.
        /// If win cannot be obtained, returns -1.
        /// </returns>
        int GetWinAmount();

        /// <summary>
        /// Gets the EGM's current state and returns a platform specific state name.
        /// </summary>
        /// <returns>
        /// The platform specific EGM state name. If state cannot be obtained, returns a blank string.
        /// </returns>
        string GetCurrentState();

        /// <summary>
        /// Gets the EGM's accounting meters. Returns a string array where each string
        /// is in the format: "Meter name = meter value".
        /// </summary>
        /// <returns>
        /// The EGM accounting meters. Returns a blank string if they cannot be obtained.
        /// </returns>
        string[] GetMeters();

        /// <summary>
        /// Gets the base program name running on the EGM. Currently only works on i960 EGMs.
        /// </summary>
        /// <returns>
        /// The base program name. Returns a blank string for other EGM platforms.
        /// </returns>
        string GetBaseProgramName();

        /// <summary>
        /// Gets the game program name running on the EGM. Currently only works on i960 EGMs.
        /// </summary>
        /// <returns>
        /// The game program name. Returns a blank string for other EGM platforms.
        /// </returns>
        string GetGameProgramName();

        /// <summary>
        /// Gets the current player denomination on the EGM. Currently only works on i960 EGMs.
        /// </summary>
        /// <returns>
        /// The current player denomination. -1 for other EGM platforms.
        /// </returns>
        int GetPlayerDenomination();

        /// <summary>
        /// Gets the current accounting denomination on the EGM. Currently only works on i960 EGMs.
        /// </summary>
        /// <returns>
        /// The current accounting denomination. -1 for other EGM platforms.
        /// </returns>
        int GetAccountingDenomination();

        /// <summary>
        /// Gets the current coin/hopper denomination on the EGM. Currently only works on i960 EGMs.
        /// </summary>
        /// <returns>The current coin/hopper denomination on i960 EGMs. -1 for other EGM platforms.</returns>
        int GetCoinDenomination();

        /// <summary>
        /// Gets the number of credits on the EGM. Currently only works on i960 EGMs.
        /// </summary>
        /// <returns>The number of credits on i960 EGMs. -1 for other EGM platforms.</returns>
        int GetCredits();

        /// <summary>
        /// Gets the asset number of the EGM. Currently only works on AVP EGMs.
        /// </summary>
        /// <returns>The asset number for AVP EGMs. -1 for other EGM platforms.</returns>
        long GetAssetNumber();

        /// <summary>
        /// Gets the EGM ID of the EGM in the format IGT_XXXXXXXXXXXX. For AVP EGMs only.
        /// </summary>
        /// <returns>The EGM ID in the format IGT_XXXXXXXXXXXX where the X's represent the MAC ID of
        /// the EGM (without the colons).
        /// </returns>
        string GetEGMID();

        /// <summary>
        /// Turns the EGM power on when EGM is powered off. Requires an iBoot network power switch from Dataprobe.
        /// </summary>
        /// <returns>The new power state of the EGM as an enumerated value.</returns>
        RemoteSwitchResponse TurnPowerOn(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword);

        /// <summary>
        /// Powers off the EGM. Requires an iBoot network power switch from Dataprobe.
        /// </summary>
        /// <returns>The new power state of the EGM as an enumerated value.</returns>
        RemoteSwitchResponse TurnPowerOff(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword);

        /// <summary>
        /// Cycles the power on the EGM. Requires an iBoot network power switch from Dataprobe.
        /// The interval between power off and power on is set up in the iBoot setup program.
        /// </summary>
        /// <returns>The new power state of the EGM as an enumerated value.</returns>
        RemoteSwitchResponse CyclePower(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword);

        /// <summary>
        /// Checks the current power state of the EGM (on, off, or cycling power).
        /// Requires an iBoot network power switch from Dataprobe.
        /// </summary>
        /// <returns>The current power state of the EGM as an enumerated value.</returns>
        RemoteSwitchResponse GetCurrentPowerState(string powerSwitchIP, int powerSwitchPort, string powerSwitchPassword);
        #endregion Methods
    }
}
