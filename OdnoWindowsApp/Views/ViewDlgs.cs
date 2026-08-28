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
        
        private static Rectangle _bounds = Screen.PrimaryScreen.Bounds;
        
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
            search.Click += async (sender, e) => {
                if (artistTb.Text.Length == 0 || albumTb.Text.Length == 0)
                {
                    MessageBox.Show("Album & Artist cannot be blank.");
                }
                else { 
                    ResponseBody response = await _srv.GetAlbumFromLastFM(artistTb.Text, albumTb.Text);
                    if (!response.IsSuccess) {
                        MessageBox.Show(response.ResponseJson.error);
                        return;
                    }

                    var json = response.ResponseJson;
                    var confirmation = MessageBox.Show("Is the following correct:\n" +
                        $"{json.name}\n" +
                        $"{json.artist}\n" +
                        $"debug:{json}", "Confirm", MessageBoxButtons.YesNo,
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
            Form trackViewDlg = new Form()
            {
                Width = (int)(_bounds.Width * .33),
                Height = (int)(_bounds.Height * .66),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = $"{album} - {artist}",
                StartPosition = FormStartPosition.CenterScreen,
                AutoScroll = true
            };
            Label trackLabel = new Label()
            {
                Top = 25,
                Left = 50,
                Text = "Tracks",
                Width = 50
            };
            Button edit = new Button() {
                Top = 50,
                Left = 400,
                Text = "Edit",
                Width = 100
            };
            Button done = new Button()
            {
                Top = 50,
                Left = 150,
                Text = "Done",
                Width = 100,
                Visible = false
            };
            Button reset = new Button()
            {
                Top = 50,
                Left = 25,
                Text = "Reset",
                Width = 100,
                Visible = false
            };
            Button OK = new Button()
            {
                Top = 500,
                Text = "OK",
                Left = 250,
                Width = 100,
                DialogResult = DialogResult.None
            };
            DataGridView tracksView = new DataGridView()
            {
                Top = 100,
                Left = 50,
                Width = 400,
                Height = 400,
                BorderStyle = BorderStyle.FixedSingle
            };
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
                tracksView.DataSource = prev;
                tracksView.Refresh();
                tracksView.ReadOnly = true;
                edit.Enabled = true;
                reset.Visible = false;
                done.Visible = false;
                OK.Enabled = true;
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
                    trackViewDlg.Close();
                }
                else {
                    trackViewDlg.Close();
                }
            };

            trackViewDlg.Controls.Add(trackLabel);
            trackViewDlg.Controls.Add(tracksView);
            trackViewDlg.Controls.Add(edit);
            trackViewDlg.Controls.Add(done);
            trackViewDlg.Controls.Add(reset);
            trackViewDlg.Controls.Add(OK);
            trackViewDlg.AcceptButton = OK;

            trackViewDlg.ShowDialog();

            return songs;
        }

        public static void showSettings() {
            bool change = false;
            bool pathChange = false;
            bool CDROMChange = false;

            ResponsiveDialog settingsDlg = new ResponsiveDialog(new Size(800, 400), new Size(400, 200));

            Label SaveLabel = new Label()
            {
                Top = 25,
                Left = 50,
                Text = "Save Location",
                Width = (int)(settingsDlg.Width * .2),
            };

            TextBox SaveTxtBox = new TextBox() { 
            
                Top = 50,
                Left = 50,
                Text = SettingsMngr.settings["odno_tunes_path"],
                Width = (int)(settingsDlg.Width * .33),
            };

            Label DriveLabel = new Label()
            {
                Top = 80,
                Left = 50,
                Text = "Preferred CDROM",
                Width = (int)(settingsDlg.Width * .33),
            };

            ComboBox CDROMCombo = new ComboBox()
            {

                Top = 105,
                Left = 50,
                Text = SettingsMngr.settings["default_cdrom"],
                DataSource = CDROMDrive.CDROMS,
                Width = (int)(settingsDlg.Width * .33),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Button save = new Button()
            {
                Top = 150,
                Left = (int)(settingsDlg.Width * .5),
                Width = (int)(settingsDlg.Width * .15),
                Text = "OK",
            };

            SaveTxtBox.KeyPress += (s, e) =>
            {
                pathChange = !SaveTxtBox.Text.Equals(SettingsMngr.settings["odno_tunes_path"]);
            };

            CDROMCombo.SelectedValueChanged += (s, e) =>
            {
                CDROMChange = !CDROMCombo.Text.Equals(SettingsMngr.settings["default_cdrom"]);
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
            settingsDlg.Controls.Add(save);
            settingsDlg.AcceptButton = save;

            settingsDlg.ShowDialog();

        }

        public static bool OpenClose() {
            ResponsiveDialog diskDlg = new ResponsiveDialog(new Size(150, 80), new Size(75, 40))
            {
                Text = "Close Disk Drive To Continue."
            };

            bool done = false;

            diskDlg.Shown += (s, e) =>
            {
                done = CDROMDrive.OpenClose();
                diskDlg.Close();
            };

            diskDlg.ShowDialog();

            return done;
        }

        public ResponsiveDialog ShowLoading() {
            ResponsiveDialog loadingDlg = new ResponsiveDialog(new Size(150, 80), new Size(75, 40));

            Label loading = new Label() {
                Dock = DockStyle.Fill,
                Text = "Loading...",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = (int)(loadingDlg.Width * 0.2),
            };

            ProgressBar progressBar = new ProgressBar()
            {
                Dock = DockStyle.Bottom,
                Height = 20,
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 1
            };
            loadingDlg.Controls.Add(progressBar);

            loadingDlg.Controls.Add(loading);
            loadingDlg.Controls.Add(progressBar);

            return loadingDlg;
        }
    }
}
