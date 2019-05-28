using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NFSU2_Extra_Options_settings
{
    public partial class Main : Form
    {
        delegate void Delegate(string PathScript);

        static void ExoptCheck(Delegate del)
        {
            string[] custompath;
            custompath = Environment.GetCommandLineArgs();
            Array.Resize(ref custompath, 2);
            if (custompath[1] is null)
                del.Invoke(@"scripts\NFSU2ExtraOptionsSettings.ini");
            else
                del.Invoke(custompath[1]);
        }

        public Main()
        {
            InitializeComponent();
        }

        void LoadData(string PathScript)
        {
            try
            {
                string[] data = File.ReadAllLines(PathScript);

                #region Checks
                chOutrun.Checked = data[24].Contains("1") ? true : false;
                chRaceOptions.Checked = data[25].Contains("1") ? true : false;
                chOnline.Checked = data[26].Contains("1") ? true : false;
                chSubs.Checked = data[27].Contains("1") ? true : false;
                chPaintTypes.Checked = data[28].Contains("1") ? true : false;
                chVinyls.Checked = data[29].Contains("1") ? true : false;
                chDebugCustomize.Checked = data[30].Contains("1") ? true : false;
                chAnyTrackAnyMode.Checked = data[31].Contains("1") ? true : false;
                chFreeRunRace.Checked = data[32].Contains("1") ? true : false;
                chOutrunTab.Checked = data[33].Contains("1") ? true : false;
                chGarageCam.Checked = data[35].Contains("1") ? true : false;
                chTakeover.Checked = data[36].Contains("1") ? true : false;
                chCamModes.Checked = data[39].Contains("1") ? true : false;
                chDebugCam.Checked = data[40].Contains("1") ? true : false;
                chBarriers.Checked = data[46].Contains("1") ? true : false;
                chLockedArea.Checked = data[47].Contains("1") ? true : false;
                chLapPercentage.Checked = data[48].Contains("1") ? true : false;
                chRegionalCar.Checked = data[51].Contains("1") ? true : false;
                chUnlocker.Checked = data[52].Contains("1") ? true : false;
                chRainAlws.Checked = data[56].Contains("1") ? true : false;
                chUnfreezeKO.Checked = data[10].Contains("1") ? true : false;
                chPlusSign.Checked = data[20].Contains("1") ? true : false;
                chShMultiplying.Checked = data[21].Contains("1") ? true : false;
                chHotPos.Checked = data[4].Contains("1") ? true : false;
                chMusic.Checked = data[72].Contains("1") ? true : false;
                chSound.Checked = data[71].Contains("1") ? true : false;
                chWheelFix.Checked = data[66].Contains("1") ? true : false;
                chSplitScrFix.Checked = data[67].Contains("1") ? true : false;
                #endregion

                #region Txts
                txtScreenTimeLimit.Text = data[34].Replace("SplashScreenTimeLimit = ", "");
                txtSpeed.Text = data[41].Replace("GameSpeed = ", "");
                txtWrldAnimation.Text = data[42].Replace("WorldAnimationSpeed = ", "");
                txtLowBeam.Text = data[44].Replace("LowBeamBrightness = ", "");
                txtHighBeam.Text = data[45].Replace("HighBeamBrightness = ", "");
                txtNeon.Text = data[53].Replace("NeonBrightness = ", "");
                txtGeneralRain.Text = data[57].Replace("GeneralRainAmount = ", "");
                txtRoadRelection.Text = data[58].Replace("RoadReflectionAmount = ", "");
                txtRainSize.Text = data[59].Replace("RainSize = ", "");
                txtRainIntensity.Text = data[60].Replace("RainIntensity = ", "");
                txtRainCrossing.Text = data[61].Replace("RainCrossing = ", "");
                txtRainSpeed.Text = data[62].Replace("RainSpeed = ", "");
                txtRainGravity.Text = data[63].Replace("RainGravity = ", "");
                txtUnlocker.Text = data[1].Replace("UnlockAllThings = ", "");
                txtAnyMode.Text = data[2].Replace("AnyTrackInAnyMode = ", "");
                txtHeadLights.Text = data[3].Replace("HeadLights = ", "");
                #endregion

                #region Counts 
                couRegion.Value = Convert.ToInt32(data[49].Replace("GameRegion = ", ""));
                couMinLap.Value = Convert.ToInt32(data[7].Replace("Minimum = ", ""));
                couMaxLap.Value = Convert.ToInt32(data[8].Replace("Maximum = ", ""));
                couKOEnabled.Value = Convert.ToInt32(data[9].Replace("KOEnabled = ", ""));
                couMinOpponents.Value = Convert.ToInt32(data[13].Replace("Minimum = ", ""));
                couMaxOpponents.Value = Convert.ToInt32(data[14].Replace("Maximum = ", ""));
                couKOEnanbled1.Value = Convert.ToInt32(data[15].Replace("KOEnabled = ", ""));
                couMaxLAN.Value = Convert.ToInt32(data[16].Replace("MaximumLANPlayers = ", ""));
                couMaxMultiplier.Value = Convert.ToInt32(data[19].Replace("MaximumMultiplier = ", ""));
                #endregion

                #region Groups
                if (data[43].Contains("0"))
                {
                    radOff.Checked = true;
                }
                else if (data[43].Contains("1"))
                {
                    radLow.Checked = true;
                }
                else if (data[43].Contains("2"))
                {
                    radHigh.Checked = true;
                }
                else
                {
                    Warning();
                }

                int cash = Convert.ToInt32(data[50].Replace("StartingCash =", ""));
                chON.Checked = cash == 0 ? false : true;
                if (chON.Checked == false)
                {
                    couCash.ReadOnly = true;
                }
                else
                {
                    couCash.ReadOnly = false;
                    couCash.Value = Convert.ToInt32(data[50].Replace("StartingCash =", ""));
                }

                if (data[70].Contains("0"))
                {
                    radFullScr.Checked = true;
                }
                else if (data[70].Contains("1"))
                {
                    radWindowed.Checked = true;
                }
                else if (data[70].Contains("2"))
                {
                    radBorder.Checked = true;
                }
                else
                {
                    Warning();
                }
                #endregion

                #region Other
                if (couRegion.Value == 0)
                {
                    lblRegion.Text = "NTSC-U (NFSU2NA)";
                }
                else
                {
                    lblRegion.Text = "PAL/NTSC-J (NFSU2)";
                }
                #endregion

                panUnsaved.BackColor = Color.ForestGreen;
            }
            catch (FormatException)
            {
                panUnsaved.BackColor = Color.Gray;
            }
            catch (Exception ex)
            {
                panUnsaved.BackColor = Color.Gray;
                Error(ex);
            }
        }

        void ChangeData(string PathScript)
        {
            using (StreamWriter wrt = new StreamWriter(PathScript, false))
            {
                object binary;

                #region [Hotkeys]
                wrt.WriteLine("[Hotkeys]");
                wrt.WriteLine("UnlockAllThings = " + txtUnlocker.Text);
                wrt.WriteLine("AnyTrackInAnyMode = " + txtAnyMode.Text);
                wrt.WriteLine("HeadLights = " + txtHeadLights.Text);
                binary = chHotPos.Checked == true ? (byte)1 : (byte)0;
                wrt.WriteLine("EnableSaveLoadHotPos = " + binary);
                wrt.WriteLine("");
                #endregion

                #region [Lap controllers]
                wrt.WriteLine("[LapControllers]");
                wrt.WriteLine("Minimum = " + couMinLap.Value);
                wrt.WriteLine("Maximum = " + couMaxLap.Value);
                wrt.WriteLine("KOEnabled = " + couKOEnabled.Value);
                binary = chUnfreezeKO.Checked == true ? (byte)1 : (byte)0;
                wrt.WriteLine("UnfreezeKO = " + binary);
                wrt.WriteLine("");
                #endregion

                #region [Opponent controllers]
                wrt.WriteLine("[OpponentControllers]");
                wrt.WriteLine("Minimum = " + couMinOpponents.Value);
                wrt.WriteLine("Maximum = " + couMaxOpponents.Value);
                wrt.WriteLine("KOEnabled = " + couKOEnanbled1.Value);
                wrt.WriteLine("MaximumLANPlayers = " + couMaxLAN.Value);
                wrt.WriteLine("");
                #endregion

                #region [Drift toga]
                wrt.WriteLine("[Drift]");
                wrt.WriteLine("MaximumMultiplier = " + couMaxMultiplier.Value);
                binary = chPlusSign.Checked == true ? (byte)1 : (byte)0;
                wrt.WriteLine("PlusSign = " + binary);
                binary = chShMultiplying.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowWithoutMultiplying = " + binary);
                wrt.WriteLine("");
                #endregion

                #region [Menu]
                wrt.WriteLine("[Menu]");
                binary = chOutrun.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowOutrun = " + binary);
                binary = chRaceOptions.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowMoreRaceOptions = " + binary);
                binary = chOnline.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowOnline = " + binary);
                binary = chSubs.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowSubs = " + binary);
                binary = chPaintTypes.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowMorePaintTypes = " + binary);
                binary = chVinyls.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowSpecialVinyls = " + binary);
                binary = chDebugCustomize.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowDebugCarCustomize = " + binary);
                binary = chAnyTrackAnyMode.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("AnyTrackInAnyRaceMode = " + binary);
                binary = chFreeRunRace.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("FreeRunTrackSelect = " + binary);
                binary = chOutrunTab.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("OutrunTrackSelect = " + binary);
                wrt.WriteLine("SplashScreenTimeLimit = " + txtScreenTimeLimit.Text);
                binary = chGarageCam.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowcaseCamInfiniteYRotation = " + binary);
                binary = chTakeover.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("DisableTakeover = " + binary);
                wrt.WriteLine("");
                #endregion

                #region [Gameplay]
                wrt.WriteLine("[Gameplay]");
                binary = chCamModes.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("EnableHiddenCameraModes = " + binary);
                binary = chDebugCam.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("EnableDebugCamera = " + binary);
                wrt.WriteLine("GameSpeed = " + txtSpeed.Text);
                wrt.WriteLine("WorldAnimationSpeed = " + txtWrldAnimation.Text);
                if (radOff.Checked == true)
                {
                    binary = 0;
                }
                else if (radLow.Checked == true)
                {
                    binary = 1;
                }
                else
                {
                    binary = 2;
                }
                wrt.WriteLine("HeadLightsMode = " + binary);
                wrt.WriteLine("LowBeamBrightness = " + txtLowBeam.Text);
                wrt.WriteLine("HighBeamBrightness = " + txtHighBeam.Text);
                binary = chBarriers.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("RemoveRaceBarriers = " + binary);
                binary = chLockedArea.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("RemoveLockedAreaBarriers = " + binary);
                binary = chLapPercentage.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ShowPercentOn1LapRaces = " + binary);
                wrt.WriteLine("GameRegion = " + couRegion.Value);
                wrt.WriteLine("StartingCash = " + couCash.Value);
                binary = chRegionalCar.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("UnlockRegionalCars = " + binary);
                binary = chUnlocker.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("UnlockAllThings = " + binary);
                wrt.WriteLine("NeonBrightness = " + txtNeon.Text);
                wrt.WriteLine("");
                #endregion

                #region [Weather]
                wrt.WriteLine("[Weather]");
                binary = chRainAlws.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("AlwaysRain = " + binary);
                wrt.WriteLine("GeneralRainAmount = " + txtGeneralRain.Text);
                wrt.WriteLine("RoadReflectionAmount = " + txtRoadRelection.Text);
                wrt.WriteLine("RainSize = " + txtRainSize.Text);
                wrt.WriteLine("RainIntensity = " + txtRainIntensity.Text);
                wrt.WriteLine("RainCrossing = " + txtRainCrossing.Text);
                wrt.WriteLine("RainSpeed = " + txtRainSpeed.Text);
                wrt.WriteLine("RainGravity = " + txtRainGravity.Text);
                wrt.WriteLine("");
                #endregion

                #region [Fixes]
                wrt.WriteLine("[Fixes]");
                binary = chWheelFix.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("DisappearingWheelsFix = " + binary);
                binary = chSplitScrFix.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("ExperimentalSplitScreenFix = " + binary);
                wrt.WriteLine("");
                #endregion

                #region[Misc]
                wrt.WriteLine("[Misc]");
                if (radFullScr.Checked == true)
                {
                    binary = 0;
                }
                else if (radWindowed.Checked == true)
                {
                    binary = 1;
                }
                else
                {
                    binary = 2;
                }
                wrt.WriteLine("WindowedMode = " + binary);
                binary = chSound.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("EnableSound = " + binary);
                binary = chMusic.Checked == true ? (sbyte)1 : (sbyte)0;
                wrt.WriteLine("EnableMusic = " + binary);
                #endregion                

                panUnsaved.BackColor = Color.Green;
            }
        }

        void DefaultData(string PathScript)
        {
            using (StreamWriter wrt = new StreamWriter(PathScript, false))
            {
                #region [Hotkeys]
                wrt.WriteLine("[Hotkeys]");
                wrt.WriteLine("UnlockAllThings = 116");
                wrt.WriteLine("AnyTrackInAnyMode = 36");
                wrt.WriteLine("HeadLights = 72");
                wrt.WriteLine("EnableSaveLoadHotPos = 0");
                wrt.WriteLine("");
                #endregion

                #region [Lap controllers]
                wrt.WriteLine("[LapControllers]");
                wrt.WriteLine("Minimum = 0");
                wrt.WriteLine("Maximum = 127");
                wrt.WriteLine("KOEnabled = 5");
                wrt.WriteLine("UnfreezeKO = 0");
                wrt.WriteLine("");
                #endregion

                #region [Opponent controllers]
                wrt.WriteLine("[OpponentControllers]");
                wrt.WriteLine("Minimum = 0");
                wrt.WriteLine("Maximum = 5");
                wrt.WriteLine("KOEnabled = 5");
                wrt.WriteLine("MaximumLANPlayers = 6");
                wrt.WriteLine("");
                #endregion

                #region [Drift toga]
                wrt.WriteLine("[Drift]");
                wrt.WriteLine("MaximumMultiplier = 5");
                wrt.WriteLine("PlusSign = 0");
                wrt.WriteLine("ShowWithoutMultiplying = 0");
                wrt.WriteLine("");
                #endregion

                #region [Menu]
                wrt.WriteLine("[Menu]");
                wrt.WriteLine("ShowOutrun = 0");
                wrt.WriteLine("ShowMoreRaceOptions = 1");
                wrt.WriteLine("ShowOnline = 1");
                wrt.WriteLine("ShowSubs = 0");
                wrt.WriteLine("ShowMorePaintTypes = 1");
                wrt.WriteLine("ShowSpecialVinyls = 1");
                wrt.WriteLine("ShowDebugCarCustomize = 0");
                wrt.WriteLine("AnyTrackInAnyRaceMode = 0");
                wrt.WriteLine("FreeRunTrackSelect = 1");
                wrt.WriteLine("OutrunTrackSelect = 1");
                wrt.WriteLine("SplashScreenTimeLimit = 30");
                wrt.WriteLine("ShowcaseCamInfiniteYRotation = 0");
                wrt.WriteLine("DisableTakeover = 0");
                wrt.WriteLine("");
                #endregion

                #region [Gameplay]
                wrt.WriteLine("[Gameplay]");
                wrt.WriteLine("EnableHiddenCameraModes = 0");
                wrt.WriteLine("EnableDebugCamera = 0");
                wrt.WriteLine("GameSpeed = 1.0");
                wrt.WriteLine("WorldAnimationSpeed = 45.0");
                wrt.WriteLine("HeadLightsMode = 2");
                wrt.WriteLine("LowBeamBrightness = 0.75");
                wrt.WriteLine("HighBeamBrightness = 1.0");
                wrt.WriteLine("RemoveRaceBarriers = 0");
                wrt.WriteLine("RemoveLockedAreaBarriers = 0");
                wrt.WriteLine("ShowPercentOn1LapRaces = 1");
                wrt.WriteLine("GameRegion = 0");
                wrt.WriteLine("StartingCash = 0");
                wrt.WriteLine("UnlockRegionalCars = 1");
                wrt.WriteLine("UnlockAllThings = 0");
                wrt.WriteLine("NeonBrightness = 1");
                wrt.WriteLine("");
                #endregion

                #region [Weather]
                wrt.WriteLine("[Weather]");
                wrt.WriteLine("AlwaysRain = 0");
                wrt.WriteLine("GeneralRainAmount = 1");
                wrt.WriteLine("RoadReflectionAmount = 1");
                wrt.WriteLine("RainSize = 0.03");
                wrt.WriteLine("RainIntensity = 0.7");
                wrt.WriteLine("RainCrossing = 0.02");
                wrt.WriteLine("RainSpeed = 0.03");
                wrt.WriteLine("RainGravity = 0.35");
                wrt.WriteLine("");
                #endregion

                #region [Fixes]
                wrt.WriteLine("[Fixes]");
                wrt.WriteLine("DisappearingWheelsFix = 1");
                wrt.WriteLine("ExperimentalSplitScreenFix = 0");
                wrt.WriteLine("");
                #endregion

                #region[Misc]
                wrt.WriteLine("[Misc]");
                wrt.WriteLine("WindowedMode = 0");
                wrt.WriteLine("EnableSound = 1");
                wrt.WriteLine("EnableMusic = 1");
                #endregion                

                panUnsaved.BackColor = Color.Green;
            }
        }

        void Notfound()
        {
            string title = "Fatal error!";
            string message = "This program must be in the root folder of the game with all installed configuration files! Do the necessary actions and try again.";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Stop);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        void Notfound1()
        {
            string title = "Not found";
            string message = "This application must be in root folder of the game with speed2.exe or SPEED2.exe file for correct work! Please try again";
            MessageBoxButtons button = MessageBoxButtons.AbortRetryIgnore;
            DialogResult result = MessageBox.Show(message, title, button, MessageBoxIcon.Warning);
            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Retry)
            {
                buttStart_Click(null, null);
            }
        }

        void Notfound2()
        {
            string title = "Not found";
            string message = "This application must be in root folder of the game with SafeMode.bat file for correct work! Please try again.(This error not fatal, try start the game without safe mode)";
            MessageBoxButtons button = MessageBoxButtons.AbortRetryIgnore;
            DialogResult result = MessageBox.Show(message, title, button, MessageBoxIcon.Warning);
            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Retry)
            {
                buttSafeStart_Click(null, null);
            }
        }

        void Warning()
        {
            string title = "Warning";
            string message = "In configuration file have some mistakes. Reinstall the mod or ignore and mood all parametres now and apply your changes.";
            MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Retry)
            {
                DefaultData(@"scripts\NFSU2ExtraOptionsSettings.ini");
                LoadData(@"scripts\NFSU2ExtraOptionsSettings.ini");
            }
        }

        void AreYouSure()
        {
            string title = "Are you sure?";
            string message = "You will be canceled all changes. Are you sure?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                LoadData(@"scripts\NFSU2ExtraOptionsSettings.ini");
            }
        }

        void AreYouSure1()
        {
            string title = "Are you sure?";
            string message = "You will be changed setting of NFSU2. Are you sure?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                try
                {
                    ExoptCheck(ChangeData);
                }
                catch (DirectoryNotFoundException)
                {
                    Notfound();
                }
            }
        }

        void AreYouSure2()
        {
            string title = "Are you sure?";
            string message = "You will be changed setting to default of NFSU2. Are you sure?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                try
                {
                    ExoptCheck(DefaultData);
                }
                catch (DirectoryNotFoundException)
                {
                    Notfound();
                }
            }
        }

        void AreYouSure3()
        {
            string title = "Are you sure?";
            string message = "Are you sure you want to quit?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        void IOExcept()
        {
            string title = "Error";
            string message = "The game is being used by another process. Try running the game later.";
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
            if (result == DialogResult.Retry)
            {
                buttStart_Click(null, null);
            }
        }

        void Error(Exception ex)
        {
            string title = "Error";
            string message = "Unknown Error. Show more information?(experts only). All data will reset.";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, button, MessageBoxIcon.Error);
            if (result == DialogResult.No)
            {
                LoadData(@"scripts\NFSU2ExtraOptionsSettings.ini");
            }
            else
            {
                string title1 = "Error info";
                string message1 = ex.ToString();
                MessageBoxButtons button1 = MessageBoxButtons.OK;
                MessageBox.Show(message1, title1, button1, MessageBoxIcon.Information);
                LoadData(@"scripts\NFSU2ExtraOptionsSettings.ini");
            }
        }

        void LoadDataFromIni(object sender, EventArgs e)
        {
            try
            {
                ExoptCheck(LoadData);
            }
            catch (DirectoryNotFoundException)
            {
                Notfound();
            }
        }

        void buttCancel_Click(object sender, EventArgs e)
        {
            AreYouSure();
        }

        void buttConfirm_Click(object sender, EventArgs e)
        {
            AreYouSure1();
            try
            {
                ExoptCheck(LoadData);
            }
            catch (DirectoryNotFoundException)
            {
                Notfound();
            }
        }

        void buttKeys_Click(object sender, EventArgs e)
        {
            Process.Start("http://cherrytree.at/misc/vk.htm");
        }

        void OnOffCash(object sender, EventArgs e)
        {
            couCash.ReadOnly = chON.Checked == true ? false : true;
            if (chON.Checked == false)
            {
                couCash.Value = 0;
            }
            panUnsaved.BackColor = Color.Red;
        }

        void RegionName(object sender, EventArgs e)
        {
            if (couRegion.Value == 0)
            {
                lblRegion.Text = "NTSC-U (NFSU2NA)";
            }
            else
            {
                lblRegion.Text = "PAL/NTSC-J (NFSU2)";
            }
            panUnsaved.BackColor = Color.Red;
        }

        void buttDefault_Click(object sender, EventArgs e)
        {
            AreYouSure2();
            try
            {
                ExoptCheck(LoadData);
            }
            catch (DirectoryNotFoundException)
            {
                Notfound();
            }
        }

        void buttLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
            load.Filter = "Extra Options presets(*.exopt)|*.exopt";
            if (load.ShowDialog() == DialogResult.OK)
            {
                LoadData(load.FileName);
            }
        }

        void buttSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                InitialDirectory = @"scripts\Extra Options settings packs",
                DefaultExt = "exopt",
                Title = "Custom settings preset",
                FileName = "Custom settings preset",
                Filter = "Extra Options presets(*.exopt*)|*.exopt|All files(*.*)|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                ChangeData(save.FileName);
            }
        }

        void buttStart_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"SPEED2.exe");
            }
            catch (IOException)
            {
                IOExcept();
            }
            catch (Win32Exception)
            {
                try
                {
                    Process.Start(@"speed2.exe");
                }
                catch (Win32Exception)
                {
                    Notfound1();
                }
            }
        }

        void Quit(object sender, EventArgs e)
        {
            AreYouSure3();
        }

        void buttSafeStart_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"SafeMode.bat");
            }
            catch (IOException)
            {
                IOExcept();
            }
            catch (Win32Exception)
            {
                Notfound2();
            }
        }

        void Unsaved(object sender, EventArgs e)
        {
            panUnsaved.BackColor = Color.Red;
        }

        void KeyboardAction(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            switch (key)
            {
                case (char)Keys.Enter:
                    buttConfirm_Click(null, null);
                    break;
                case 'q':
                case 'Q':
                    Quit(null, null);
                    break;
                case (char)Keys.Escape:
                    buttCancel_Click(null, null);
                    break;
                case 'd':
                case 'D':
                    buttDefault_Click(null, null);
                    break;
                case 'g':
                case 'G':
                    buttStart_Click(null, null);
                    break;
                case 's':
                case 'S':
                    buttSafeStart_Click(null, null);
                    break;
                case 'i':
                case 'I':
                    ShowAppInfo(null, null);
                    break;
                case '+':
                    buttSave_Click(null, null);
                    break;
                case '-':
                    buttLoad_Click(null, null);
                    break;
            }
        }

        #region Infos
        void ShowAppInfo(object sender, EventArgs e)
        {
            Info_forms.About about = new Info_forms.About();
            about.Show();
        }

        void ShowOutrunInfo(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Info_forms.ShowOutrun showOutrun = new Info_forms.ShowOutrun();
                showOutrun.Show();
            }
        }
        #endregion
    }
}