using Microsoft.Extensions.Logging;
using OdnoWindowsApp.Core;
using OdnoWindowsApp.Model;
using OdnoWindowsApp.Services;
using OdnoWindowsApp.Util;
using OdnoWindowsApp.Views;
using System.Text.RegularExpressions;

namespace OdnoWindowsApp
{
    public partial class OdnoFormMain : Form
    {
        private static LastFmObjectModel.Album? responseJson;
        private static List<Song> songs = new List<Song>();
        private static Dictionary<string, string> imgMap = new Dictionary<string, string>();
        private static bool debug = false;

        private static bool albumProvided = false;
        private static bool artistProvided = false;

        private static bool ManEntryClicked = false;


        /* Dependencies */
        private readonly FormSrv _formSrv;
        private readonly ViewDlgs _viewDlgs;

        public OdnoFormMain()
        {
            if (_formSrv == null && _viewDlgs == null)
            {
                _formSrv = new FormSrv();
                _viewDlgs = new ViewDlgs(_formSrv);
            }

            InitializeComponent();
        }

        private void initControls()
        {
            this.KeyDown += QuitCmd;
            this.KeyDown += ResetCmd;

            if (CDROMDrive.isReady)
            {
                CDRomCombo.Text = CDROMDrive.dvdRoot;
                CDRomCombo.DataSource = CDROMDrive.CDROMS;
                cdBox.Enabled = true;
            }

            convertY.Checked = false;
            convertN.Checked = true;
            bitrateCB.DataSource = GlobalConstants.bitrates;
        }

        private void Reset(bool resetCmd = false)
        {
            foreach (var control in this.OdnoTabs.Controls)
            {
                if (control is TabPage)
                {
                    TabPage tb = (TabPage)control;
                    foreach (var tabControls in tb.Controls)
                    {
                        if (tabControls is GroupBox)
                        {
                            GroupBox gb = (GroupBox)tabControls;
                            foreach (var gControl in gb.Controls)
                            {
                                if (gControl is TextBox)
                                {
                                    TextBox textBox = (TextBox)gControl;
                                    textBox.Text = null;
                                }

                                if (gControl is ComboBox)
                                {
                                    ComboBox comboBox = (ComboBox)gControl;
                                    if (comboBox.Items.Count > 0)
                                        comboBox.SelectedIndex = 0;
                                }

                                if (gControl is PictureBox)
                                {
                                    PictureBox pb = (PictureBox)gControl;
                                    pb.Image = pb.InitialImage;
                                }
                            }
                        }

                        if (tabControls is TextBox)
                        {
                            TextBox textBox = (TextBox)tabControls;
                            textBox.Text = "";
                        }

                        if (tabControls is ComboBox)
                        {
                            ComboBox comboBox = (ComboBox)tabControls;
                            if (comboBox.Items.Count > 0)
                                comboBox.SelectedIndex = 0;
                        }

                        if (tabControls is PictureBox)
                        {
                            PictureBox pb = (PictureBox)tabControls;
                            pb.Image = pb.InitialImage;
                        }
                    }
                }
            }

            if (convertY.Checked)
            {
                convertY.Checked = false;
                convertN.Checked = true;
            }

            if (cleanY.Checked)
            {
                cleanY.Checked = false;
                cleanN.Checked = true;
            }

            songs.Clear();
            imgMap.Clear();

            responseJson = null;

            albumProvided = false;
            artistProvided = false;

            if (!resetCmd) { CDLoader(); }   
        }

        private static void InitOdnoDir()
        {
            if (!Directory.Exists(GlobalConstants.OdnoPath))
            {
                try
                {
                    Directory.CreateDirectory(GlobalConstants.OdnoPath);
                }
                catch (Exception e)
                {
                    //caller doesn't have access
                    string msg = "An Unexpected error occurred. odno folder could not be created. Try running odno with admin privileges.";
                    MessageBox.Show(msg, "odno ERROR");
                    OdnoLogger.LogError(e, $"{msg}\nLikely cause: caller doesn't have permission to user's files.");
                    return;
                }
            }
        }

        private void RenderResponse(LastFmObjectModel.Album response)
        {
            if (response == null)
            {
                MessageBox.Show("Album was not found. Try refining your search.", "odno INFO");
                return;
            }

            responseJson = response;

            if (!albumBox.Enabled) { albumBox.Enabled = true; }
            albumTextBox.Text = response.name;
            artistTextBox.Text = response.artist;
            genreComboBox.DataSource = response.tags.tag.Select(x => x.name).ToArray();

            var release_info = response.wiki == null ? "" : response.wiki.summary;

            if (imgMap.Any())
            {
                imgMap.Clear();
            }

            foreach (var img in response.image)
            {
                imgMap.Add(img.size, img.text);
            }

            yearTextBox.Text = response.year.ToString();

            coverComboBox.DataSource = response.image.Where(x => !x.size.Equals(""))
                .Select(x => x.size).ToArray();

            string albumImg = response.image[4].text;
            albumPb.Load(albumImg);

            if (!_formSrv.GetSongsToRender(response, albumImg).Any())
            {
                MessageBox.Show("Failed to render songs...", "ODNO ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            songs = _formSrv.GetSongsToRender(response, albumImg);
        }

        private bool CDLoader()
        {
            if (CDROMDrive.LoadDisc(GlobalConstants.OdnoPath))
            {
                CDRomCombo.DataSource = CDROMDrive.CDROMS;
                CDRomCombo.Text = CDROMDrive.CDROMS[0];

                cdBox.Enabled = true;

                cdAlbumNameBox.Enabled = true;
                cdArtistNameBox.Enabled = true;

                ripBtn.Enabled = true;

                return true;
            }

            return false;
        }

        ////////////
        /* events */
        ///////////
        private void OdnoFormMain_Load(object sender, EventArgs e)
        {
            /** TODO:
             *  Create pref file if it doesn't exist ; init file ; if exists read file contents into a hashmap.         [ DONE ]
             *  Check if FFMPEG is installed ; if not, install it with winget ; save "installed = true" to file         [ DONE ]
             *  find odno dir in user music folder ; if it doesn't exist, create it ; save location to file             [ DONE ]
             *  
             */
            this.KeyPreview = true;

            SettingsMngr.SettingsFile();

            InitOdnoDir();

            if (!CDLoader())
            {
                OdnoTabs.SelectedIndex = 1;
            }

            initControls();
        }

        private void manSearchBtn_Click(object sender, EventArgs e)
        {
            lockButtons(true, GlobalConstants.MSEARCH);

            if ("".Equals(albumFolderTb.Text))
            {
                MessageBox.Show("You must select a target album folder or create one.", "INFO");
                lockButtons(false, GlobalConstants.MSEARCH);
                return;
            }

            var response = _viewDlgs.ShowManualSearchInputDialog();

            Form loading = _viewDlgs.ShowLoading();
            loading.Show();

            RenderResponse(response);

            lockButtons(false, GlobalConstants.MSEARCH);

            loading.Close();
        }

        private async void autoSearchBtn_Click(object sender, EventArgs e)
        {
            lockButtons(true, GlobalConstants.AUTO);

            if (string.Empty.Equals(albumFolderTb.Text))
            {
                MessageBox.Show("You must select a target album folder.", "INFO");
                lockButtons(false, GlobalConstants.AUTO);
                return;
            }
            
            Form loading = _viewDlgs.ShowLoading();
            loading.Show();

            var response = await _formSrv.FetchAlbum(albumFolderTb.Text.Split("\\"));

            RenderResponse(response.ResponseJson);

            lockButtons(false, GlobalConstants.AUTO);

            loading.Close();
        }
        private void manEntryBtn_Click(object sender, EventArgs e)
        {
            ManEntryClicked = true;
            lockButtons(true, GlobalConstants.MENTRY);
            if (string.Empty.Equals(albumFolderTb.Text))
            {
                MessageBox.Show("You must select a target album folder or create one.", "INFO");
                lockButtons(false, GlobalConstants.MENTRY);
                return;
            }

            albumBox.Enabled = true;

            lockButtons(false, GlobalConstants.MENTRY);
        }

        private void showTracksBtn_Click(object sender, EventArgs e)
        {
            List<Song> tracks;
            if (responseJson != null)
            {
                tracks = _viewDlgs.showTracks(songs, albumTextBox.Text, artistTextBox.Text);
                if (debug)
                {
                    string concat = string.Join(" ", tracks.Select(s => s.ToString()).ToList());
                    MessageBox.Show(concat, "Debug INFO");
                }
            }
            else
            {
                //manual entry
                tracks = _viewDlgs.showTracks(songs, albumTextBox.Text, artistTextBox.Text);
            }
        }

        private void convertY_CheckedChanged(object sender, EventArgs e)
        {
            choiceBox.Enabled = true;
        }

        private void convertN_CheckedChanged(object sender, EventArgs e)
        {
            choiceBox.Enabled = false;
        }

        private void ejectCloseBtn_Click(object sender, EventArgs e)
        {
            cdBox.Enabled = false;
            CDRomCombo.Text = string.Empty;
            ejectCloseBtn.Enabled = false;

            if (!CDROMDrive.isReady)
            {
                return;
            }

            bool driveFound = ViewDlgs.OpenClose();

            if (!driveFound) {
                MessageBox.Show("Drive could not be found. Please make sure drive is closed. Refresh using Home > Reset", "ODNO INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ejectCloseBtn.Enabled = true;

            cdBox.Enabled = driveFound;
            ripBtn.Enabled = driveFound;
        }

        private async void ripBtn_Click(object sender, EventArgs e)
        {
            Form ripping = _viewDlgs.ShowLoading("Ripping\n This could take awhile...");
            ripping.Show();
            if (!(albumProvided & artistProvided & CDROMDrive.isReady))
            {
                if (!albumProvided || !artistProvided) {
                    MessageBox.Show("Enter Album Info to proceed.", "ODNO INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (!CDROMDrive.isReady) {
                    MessageBox.Show("Error preparing drive.", "ODNO ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            string albumName = cdAlbumNameBox.Text.Replace(" ", "_");
            string artistName = cdArtistNameBox.Text.Replace(" ", "_");

            string dir = $"{albumName}-{artistName}";
            string albumDir = string.IsNullOrEmpty(GlobalConstants.OdnoPath) ? string.Empty : $"{GlobalConstants.OdnoPath}\\{dir}";
            
            await _formSrv.Rip(albumDir);
            ripping.Close();

            albumFolderTb.Text = albumDir;

            if (OdnoTabs.SelectedIndex != 1)
            {
                OdnoTabs.SelectedIndex = 1;
            }
        }

        private void cdAlbumNameBox_TextChanged(object sender, EventArgs e)
        {
            albumProvided = !string.Empty.Equals(cdAlbumNameBox.Text);

            ripBtn.Enabled = (albumProvided && artistProvided & CDROMDrive.isReady);
        }

        private void cdArtistNameBox_TextChanged(object sender, EventArgs e)
        {
            artistProvided = !string.Empty.Equals(cdArtistNameBox.Text);

            ripBtn.Enabled = (artistProvided && albumProvided & CDROMDrive.isReady);
        }

        private void browsebtn_Click(object sender, EventArgs e)
        {
            //open file browser

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (Directory.Exists(GlobalConstants.OdnoPath))
                {
                    fbd.InitialDirectory = GlobalConstants.OdnoPath;
                }
                else
                {
                    fbd.InitialDirectory = $"{GlobalConstants.UserPath}\\Music";
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    albumFolderTb.Text = fbd.SelectedPath;
                }
            }
        }

        private async void saveBtn_Click(object sender, EventArgs e)
        {
            /**
             * logic
             * There must be files in the target dir
             * create tmp dir
             * save image
             * copy all to tmp
             * save metadata
             * do conversions (if any)
             */
            Form loading = _viewDlgs.ShowLoading();
            lockButtons(saveBtn.Enabled, "SAVE");

            if (Directory.Exists($"{albumFolderTb.Text}\\CD")) {
                _formSrv.Redo(albumFolderTb.Text);
            }

            var albumFiles = Directory.GetFiles(albumFolderTb.Text).ToList();

            albumFiles.Sort(new Helpers.TrackNameComparer());

            bool tracksReady = (Directory.Exists(GlobalConstants.OdnoPath) && albumFiles.Count() > 0);
            if (string.Empty.Equals(artistTextBox.Text) || (string.Empty.Equals(albumTextBox.Text)
                || !tracksReady))
            {
                MessageBox.Show("Nothing to save, folder was empty...", "odno INFO");
                lockButtons(saveBtn.Enabled, "SAVE");
                return;
            }

            string bitrate = string.IsNullOrEmpty(bitrateCB.Text) ? "" : bitrateCB.Text;

            var ffmpegCmds = new List<TrackFileMngr>();
            songs.ForEach(song =>
            {
                song.Genre = genreComboBox.Text;

                string orig = albumFiles[song.TrackNum - 1];
                string type = orig.Split('.').Last();
                TrackFileMngr ffmpeg = new TrackFileMngr(song, orig, convertY.Checked, bitrate, $".{type}");

                ffmpegCmds.Add(ffmpeg);
            });

            loading.Show();
            
            string img = imgMap.Count == 0 || !imgMap.ContainsKey(coverComboBox.Text) ? "" : imgMap[coverComboBox.Text];
            if (!await _formSrv.Save(albumFolderTb.Text, img, bitrate, ffmpegCmds)) {
                loading.Close();
                MessageBox.Show("Could not save album...", "ODNO ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lockButtons(saveBtn.Enabled, "SAVE");
                return;
            }


            if (cleanY.Checked)
            {
                if (!_formSrv.CleanAndMove(albumFolderTb.Text)) {
                    loading.Close();
                    MessageBox.Show($"Could not clean up {albumFolderTb.Text}...", "ODNO ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            loading.Close();
            MessageBox.Show("Process Complete!", "Success!", MessageBoxButtons.OK);

            lockButtons(saveBtn.Enabled, "SAVE");
        }

        private void lockButtons(bool isLock, string btnType)
        {
            switch (btnType)
            {

                case "AUTO":
                    manEntryBtn.Enabled = !isLock;
                    manSearchBtn.Enabled = !isLock; break;
                case "MSEARCH":
                    manEntryBtn.Enabled = !isLock;
                    autoSearchBtn.Enabled = !isLock; break;
                case "MENTRY":
                    manSearchBtn.Enabled = !isLock;
                    autoSearchBtn.Enabled = !isLock; break;
                case "SAVE":
                    saveBtn.Enabled = !isLock; break;
                default:
                    break;

            }
        }

        //Debugger for testing API calls - I made this because I was lazy.
        private async void button1_Click(object sender, EventArgs e)
        {
            debug = true;
            if (debug)
            {
                const string message = "Run test with Pet Sounds - The Beach Boys ?";
                const string caption = "Debugger";

                var result = MessageBox.Show(message, caption,
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    MessageBox.Show("Program will close.");
                    System.Windows.Forms.Application.Exit();
                }

                var response = await _formSrv.Debug("Souvlaki", "Slowdive");
                if (response.IsSuccess)
                {
                    RenderResponse(response.ResponseJson);
                    if (!songs.Any())
                    {
                        MessageBox.Show("Response could not be read.");
                        return;
                    }
                    responseJson = response.ResponseJson;
                }
                else
                {
                    MessageBox.Show("Response could not be read.");
                    System.Windows.Forms.Application.Exit();
                }
            }
        }

        private void cleanY_CheckedChanged(object sender, EventArgs e)
        {
            if (cleanY.Checked)
            {
                var result = MessageBox.Show("Cleaning the album folder will remove the original tracks and preserve the newly converted tracks automatically" +
                    " to organize your files and clear up space. Removed tracks will be place in recycling. Continue?",
                    "odno INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    cleanY.Checked = false;
                    cleanN.Checked = true;
                    return;
                }
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void quitctrlQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void QuitCmd(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Q)
            {
                var quit = MessageBox.Show("Would you like to quit?", "Quit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (quit == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void ResetCmd(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                Reset(true);
            }
        }

        private void CdTab_Click(object sender, EventArgs e)
        {
            if (OdnoTabs.SelectedIndex == 0 && !CDLoader())
            {
                OdnoTabs.SelectedIndex = 1;
                //MessageBox.Show("An Unknown Error Ocurred: Could not find CD/DVD Drive(s).", "ODNO ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void save_state(object sender, EventArgs e)
        {
            //save state on every edit. 
            //Use a linked list
            //Move the nodes to prev or next based on ctrl + z or ctrl + r
        }

        private void editPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDlgs.showSettings();
        }

        private void showHelpF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            //Open help page in browser.
        }

        private void CDRomCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalConstants.DefaultCdRom = CDRomCombo.SelectedItem.ToString();
        }
    }
}
