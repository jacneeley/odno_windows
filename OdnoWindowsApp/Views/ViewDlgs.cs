using OdnoWindowsApp.Core;
using OdnoWindowsApp.Model;
using OdnoWindowsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OdnoWindowsApp.Model.LastFmObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace OdnoWindowsApp.Views
{
    public class ViewDlgs
    {

        private FormSrv _srv;
        
        public ViewDlgs(FormSrv srv) {
            _srv = srv;
        }

        public Album ShowManualSearchInputDialog() {
            Form manSearchDlg = new Form()
            {
                Width = 500,
                Height = 500,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Manual Search",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label albumLabel = new Label()
            {
                Left = 50,
                Top = 25,
                Text = "Album:",
                Width = 50
            };
            TextBox albumTb = new TextBox() {
                Left = 50,
                Top = 50,
                Width = 250
            };
            Label artistLabel = new Label()
            {
                Left = 50,
                Top = 75,
                Text = "Artist:",
                Width = 50
            };
            TextBox artistTb = new TextBox()
            {
                Left = 50,
                Top = 100,
                Width = 250
            };

            Button search = new Button()
            {
                Text = "Search",
                Left = 250,
                Width = 50,
                Top = 125,
                DialogResult = DialogResult.None
            };

            Album? responseJson = null;
            var loading = ShowLoading("Fetching...");
            search.Click += async (sender, e) => {
                if (artistTb.Text.Length == 0 || albumTb.Text.Length == 0)
                {
                    MessageBox.Show("Album & Artist cannot be blank.");
                }
                else {
                    loading.Show();
                    ResponseBody response = await _srv.GetAlbumFromLastFM(artistTb.Text, albumTb.Text);
                    if (!response.IsSuccess) {
                        loading.Close();
                        MessageBox.Show(response.ResponseJson.error);
                        return;
                    }

                    loading.Close();

                    var json = response.ResponseJson;
                    var confirmation = MessageBox.Show("Is the following correct:\n" +
                        $"Album: {json.name}\n" +
                        $"Artist: {json.artist}\n" +
                        $"Release Date:{json.year}", "Confirm", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                    
                    if (confirmation == DialogResult.Yes)
                    {
                        responseJson = json;
                        search.DialogResult = DialogResult.OK;
                        manSearchDlg.Close();
                    }
                    else {
                        MessageBox.Show("Refine Search and try again...");
                    }
                }
            };

            manSearchDlg.Controls.Add(albumLabel);
            manSearchDlg.Controls.Add(albumTb);
            manSearchDlg.Controls.Add(artistLabel);
            manSearchDlg.Controls.Add(artistTb);
            manSearchDlg.Controls.Add(search);
            manSearchDlg.AcceptButton = search;

            manSearchDlg.ShowDialog();

            return responseJson;
        }
        public List<Song> showTracks(List<Song> songs, string album, string artist)
        {
            bool edited = false;

            ResponsiveDialog TrackDlg = new ResponsiveDialog(true, "SMALL");

            Label trackLabel = TrackDlg.ResponsiveLabel("Tracks:", new int[2] { 25, 25 });

            Button edit = TrackDlg.ResponsiveButton(new int[2] { 50, 25 }, "Edit");

            Button done = TrackDlg.ResponsiveButton(new int[2] { edit.Top, edit.Right + 25 }, "Done");

            Button reset = TrackDlg.ResponsiveButton(new int[2] { edit.Top, done.Right + 25 }, "Cancel");

            DataGridView tracksView = TrackDlg.ResponsiveDataGridView(new int[2] { reset.Bottom + 25, 25 });

            Button OK = TrackDlg.ResponsiveButton(new int[2] { tracksView.Bottom + 25, ((TrackDlg.Width / 2) - 50) }, "OK");

            var source = new BindingSource();
            
            List<TracksModel> tracks = new List<TracksModel>();
            if (songs.Any())
            {
                tracks = songs.Select(s => new TracksModel(s.TrackNum, s.Title)).ToList();
            }

            source.DataSource = tracks;
            tracksView.DataSource = source;
            var prev = source;
            tracksView.ReadOnly = true;

            edit.Click += (sender, e) => {
                prev = source;
                done.Visible = true;
                reset.Visible = true;
                tracksView.ReadOnly = false;
                edit.Enabled = false;
                OK.Enabled = false;
            };

            done.Click += (sender, e) => {
                tracksView.ReadOnly = true;
                edited = true;
                edit.Enabled = true;
                reset.Visible = false;
                done.Visible = false;
                OK.Enabled = true;
            };
            reset.Click += (sender, e) => {
                //TODO: figure this shit out...
                //tracksView.Rows.Clear();
                //tracksView.DataSource = prev;
                //tracksView.ReadOnly = true;
                //edit.Enabled = true;
                //reset.Visible = false;
                //done.Visible = false;
                //OK.Enabled = true;
                //tracksView.Refresh();
                TrackDlg.Close();
            };

            OK.Click += (sender, e) => {
                if (edited)
                {
                    foreach (DataGridViewRow row in tracksView.Rows)
                    {
                        string title = row.Cells["Title"].Value.ToString();
                        int trackNum = int.Parse(row.Cells["TrackNum"].Value.ToString());
                        Song curr = songs[trackNum - 1];
                        curr.TrackNum = trackNum;
                        curr.Title = title;
                    }
                    songs.OrderBy(s => s.TrackNum);

                    OK.DialogResult = DialogResult.OK;
                    TrackDlg.Close();
                }
                else {
                    TrackDlg.Close();
                }
            };

            TrackDlg.Controls.Add(trackLabel);
            TrackDlg.Controls.Add(tracksView);
            TrackDlg.Controls.Add(edit);
            TrackDlg.Controls.Add(done);
            TrackDlg.Controls.Add(reset);
            TrackDlg.Controls.Add(OK);
            TrackDlg.AcceptButton = OK;

            done.Visible = false;
            reset.Visible = false;

            TrackDlg.ShowDialog();

            return songs;
        }

        public static void showSettings() {
            bool change = false;
            bool pathChange = false;
            bool CDROMChange = false;

            ResponsiveDialog settingsDlg = new ResponsiveDialog(true, "SMALL");

            Label SaveLabel = settingsDlg.ResponsiveLabel("save location", new int[2] { 25, ((settingsDlg.Width / 2) - 150) });

            TextBox SaveTxtBox = settingsDlg.ResponsiveTextBox(new int[2] { SaveLabel.Bottom + 5, ((settingsDlg.Width / 2) - 150) }, SettingsMngr.settings["odno_tunes_path"]);

            Button BrowseBtn = settingsDlg.ResponsiveButton(new int[2] { SaveTxtBox.Top, SaveTxtBox.Right + 5 }, "Browse");

            Label DriveLabel = settingsDlg.ResponsiveLabel("Preferred CDROM", new int[2] { SaveTxtBox.Bottom + 30, ((settingsDlg.Width / 2) - 150) });

            ComboBox CDROMCombo = settingsDlg.ResponsiveComboBox(new int[2] { DriveLabel.Bottom + 5, ((settingsDlg.Width / 2) - 150) }, SettingsMngr.settings["default_cdrom"], CDROMDrive.CDROMS);

            Button save = settingsDlg.ResponsiveButton(new int[2] { CDROMCombo.Bottom + 50, ((settingsDlg.Width / 2) - 50) }, "OK");

            SaveTxtBox.KeyPress += (s, e) =>
            {
                pathChange = !SaveTxtBox.Text.Equals(SettingsMngr.settings["odno_tunes_path"]);
            };

            CDROMCombo.SelectedValueChanged += (s, e) =>
            {
                CDROMChange = !CDROMCombo.Text.Equals(SettingsMngr.settings["default_cdrom"]);
            };

            BrowseBtn.Click += (s, e) =>
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (Directory.Exists(SettingsMngr.settings["odno_tunes_path"]))
                    {
                        fbd.InitialDirectory = SettingsMngr.settings["odno_tunes_path"];
                    }
                    else
                    {
                        fbd.InitialDirectory = $"{GlobalConstants.UserPath}\\Music";
                    }

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        SaveTxtBox.Text = fbd.SelectedPath;
                    }
                }
            };

            save.Click += (s, e) =>
            {
                change = (pathChange || CDROMChange);
                if (change) {
                    SettingsMngr.settings["odno_tunes_path"] = SaveTxtBox.Text;
                    SettingsMngr.settings["default_cdrom"] = CDROMCombo.Text;
                    if (!SettingsMngr.Write())
                    {
                        MessageBox.Show("File could not be saved...", "ODNO ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                settingsDlg.Close();
            };

            if (!CDROMDrive.CDROMS.Any()) {
                CDROMCombo.Enabled = false;
            }

            settingsDlg.Controls.Add(SaveLabel);
            settingsDlg.Controls.Add(SaveTxtBox);
            settingsDlg.Controls.Add(DriveLabel);
            settingsDlg.Controls.Add(CDROMCombo);
            settingsDlg.Controls.Add(BrowseBtn);
            settingsDlg.Controls.Add(save);
            settingsDlg.AcceptButton = save;

            settingsDlg.ShowDialog();

        }

        public static bool OpenClose() {
            ResponsiveDialog diskDlg = new ResponsiveDialog(false, "TINY");

            bool done = false;

            diskDlg.Shown += (s, e) =>
            {
                done = CDROMDrive.OpenClose();
                diskDlg.Close();
            };

            diskDlg.Controls.Add(diskDlg.CenterLabel("Close CDROM to continue."));

            diskDlg.ShowDialog();

            return done;
        }

        public ResponsiveDialog ShowLoading(string label = "") {
            ResponsiveDialog loadingDlg = new ResponsiveDialog(false, "TINY");

            if ("".Equals(label)) {
                label = "loading...";
            }

            loadingDlg.Controls.Add(loadingDlg.CenterLabel(label));
            loadingDlg.Controls.Add(loadingDlg.MarqueeProgressBar(DockStyle.Bottom));

            return loadingDlg;
        }
    }
}
