namespace OdnoWindowsApp
{
    partial class OdnoFormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            debugBtn = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            resetToolStripMenuItem = new ToolStripMenuItem();
            quitctrlQToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoctrlZToolStripMenuItem = new ToolStripMenuItem();
            redoctrlRToolStripMenuItem = new ToolStripMenuItem();
            editPreferencesToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            appearanceToolStripMenuItem = new ToolStripMenuItem();
            lightModeToolStripMenuItem = new ToolStripMenuItem();
            darkModeToolStripMenuItem = new ToolStripMenuItem();
            systemDefaultToolStripMenuItem = new ToolStripMenuItem();
            infoToolStripMenuItem = new ToolStripMenuItem();
            showHelpF1ToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            OdnoTabs = new TabControl();
            CDRipTab = new TabPage();
            driveInfoBox = new GroupBox();
            CDRomCombo = new ComboBox();
            cdBox = new GroupBox();
            cdArtistNameLbl = new Label();
            cdArtistNameBox = new TextBox();
            cdAlbumNameLbl = new Label();
            cdAlbumNameBox = new TextBox();
            ripBtn = new Button();
            ejectCloseBtn = new Button();
            locationLbl = new Label();
            dvdPB = new PictureBox();
            EditMetaDataTab = new TabPage();
            label7 = new Label();
            manEntryBtn = new Button();
            manSearchBtn = new Button();
            autoSearchBtn = new Button();
            albumBox = new GroupBox();
            coverComboBox = new ComboBox();
            genreComboBox = new ComboBox();
            convertBox = new GroupBox();
            groupBox1 = new GroupBox();
            cleanN = new RadioButton();
            cleanY = new RadioButton();
            convertLabel = new Label();
            choiceBox = new GroupBox();
            bitrateCB = new ComboBox();
            convertN = new RadioButton();
            convertY = new RadioButton();
            saveBtn = new Button();
            label4 = new Label();
            label6 = new Label();
            yearTextBox = new TextBox();
            showTracksBtn = new Button();
            label3 = new Label();
            label2 = new Label();
            artistTextBox = new TextBox();
            label1 = new Label();
            albumTextBox = new TextBox();
            albumPb = new PictureBox();
            browsebtn = new Button();
            albumFolderTb = new TextBox();
            menuStrip1.SuspendLayout();
            OdnoTabs.SuspendLayout();
            CDRipTab.SuspendLayout();
            driveInfoBox.SuspendLayout();
            cdBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dvdPB).BeginInit();
            EditMetaDataTab.SuspendLayout();
            albumBox.SuspendLayout();
            convertBox.SuspendLayout();
            groupBox1.SuspendLayout();
            choiceBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)albumPb).BeginInit();
            SuspendLayout();
            // 
            // debugBtn
            // 
            debugBtn.Location = new Point(871, 474);
            debugBtn.Name = "debugBtn";
            debugBtn.Size = new Size(75, 23);
            debugBtn.TabIndex = 0;
            debugBtn.Text = "Debugger";
            debugBtn.UseVisualStyleBackColor = true;
            debugBtn.Click += button1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, infoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(962, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resetToolStripMenuItem, quitctrlQToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(52, 20);
            fileToolStripMenuItem.Text = "Home";
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new Size(180, 22);
            resetToolStripMenuItem.Text = "Reset (ctrl + r)";
            resetToolStripMenuItem.Click += resetToolStripMenuItem_Click;
            // 
            // quitctrlQToolStripMenuItem
            // 
            quitctrlQToolStripMenuItem.Name = "quitctrlQToolStripMenuItem";
            quitctrlQToolStripMenuItem.Size = new Size(180, 22);
            quitctrlQToolStripMenuItem.Text = "Quit (ctrl + q)";
            quitctrlQToolStripMenuItem.Click += quitctrlQToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoctrlZToolStripMenuItem, redoctrlRToolStripMenuItem, editPreferencesToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoctrlZToolStripMenuItem
            // 
            undoctrlZToolStripMenuItem.Name = "undoctrlZToolStripMenuItem";
            undoctrlZToolStripMenuItem.Size = new Size(158, 22);
            undoctrlZToolStripMenuItem.Text = "Undo (ctrl + z)";
            undoctrlZToolStripMenuItem.Visible = false;
            // 
            // redoctrlRToolStripMenuItem
            // 
            redoctrlRToolStripMenuItem.Name = "redoctrlRToolStripMenuItem";
            redoctrlRToolStripMenuItem.Size = new Size(158, 22);
            redoctrlRToolStripMenuItem.Text = "Redo (ctrl + r)";
            redoctrlRToolStripMenuItem.Visible = false;
            // 
            // editPreferencesToolStripMenuItem
            // 
            editPreferencesToolStripMenuItem.Name = "editPreferencesToolStripMenuItem";
            editPreferencesToolStripMenuItem.Size = new Size(158, 22);
            editPreferencesToolStripMenuItem.Text = "Edit Preferences";
            editPreferencesToolStripMenuItem.Click += editPreferencesToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { appearanceToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // appearanceToolStripMenuItem
            // 
            appearanceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lightModeToolStripMenuItem, darkModeToolStripMenuItem, systemDefaultToolStripMenuItem });
            appearanceToolStripMenuItem.Name = "appearanceToolStripMenuItem";
            appearanceToolStripMenuItem.Size = new Size(137, 22);
            appearanceToolStripMenuItem.Text = "Appearance";
            // 
            // lightModeToolStripMenuItem
            // 
            lightModeToolStripMenuItem.Name = "lightModeToolStripMenuItem";
            lightModeToolStripMenuItem.Size = new Size(153, 22);
            lightModeToolStripMenuItem.Text = "Light Mode";
            // 
            // darkModeToolStripMenuItem
            // 
            darkModeToolStripMenuItem.Name = "darkModeToolStripMenuItem";
            darkModeToolStripMenuItem.Size = new Size(153, 22);
            darkModeToolStripMenuItem.Text = "Dark Mode";
            // 
            // systemDefaultToolStripMenuItem
            // 
            systemDefaultToolStripMenuItem.Name = "systemDefaultToolStripMenuItem";
            systemDefaultToolStripMenuItem.Size = new Size(153, 22);
            systemDefaultToolStripMenuItem.Text = "System Default";
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { showHelpF1ToolStripMenuItem, aboutToolStripMenuItem });
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(40, 20);
            infoToolStripMenuItem.Text = "Info";
            // 
            // showHelpF1ToolStripMenuItem
            // 
            showHelpF1ToolStripMenuItem.Name = "showHelpF1ToolStripMenuItem";
            showHelpF1ToolStripMenuItem.Size = new Size(154, 22);
            showHelpF1ToolStripMenuItem.Text = "Show Help (F1)";
            showHelpF1ToolStripMenuItem.Click += showHelpF1ToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(154, 22);
            aboutToolStripMenuItem.Text = "About";
            // 
            // OdnoTabs
            // 
            OdnoTabs.Controls.Add(CDRipTab);
            OdnoTabs.Controls.Add(EditMetaDataTab);
            OdnoTabs.Cursor = Cursors.Hand;
            OdnoTabs.Location = new Point(12, 27);
            OdnoTabs.Name = "OdnoTabs";
            OdnoTabs.SelectedIndex = 0;
            OdnoTabs.Size = new Size(938, 441);
            OdnoTabs.TabIndex = 2;
            OdnoTabs.Click += CdTab_Click;
            // 
            // CDRipTab
            // 
            CDRipTab.Controls.Add(driveInfoBox);
            CDRipTab.Controls.Add(dvdPB);
            CDRipTab.Location = new Point(4, 24);
            CDRipTab.Name = "CDRipTab";
            CDRipTab.Padding = new Padding(3);
            CDRipTab.Size = new Size(930, 413);
            CDRipTab.TabIndex = 0;
            CDRipTab.Text = "CD Rip";
            CDRipTab.UseVisualStyleBackColor = true;
            // 
            // driveInfoBox
            // 
            driveInfoBox.Controls.Add(CDRomCombo);
            driveInfoBox.Controls.Add(cdBox);
            driveInfoBox.Controls.Add(ripBtn);
            driveInfoBox.Controls.Add(ejectCloseBtn);
            driveInfoBox.Controls.Add(locationLbl);
            driveInfoBox.Location = new Point(382, 59);
            driveInfoBox.Name = "driveInfoBox";
            driveInfoBox.Size = new Size(527, 282);
            driveInfoBox.TabIndex = 1;
            driveInfoBox.TabStop = false;
            driveInfoBox.Text = "CD/DVD Drive";
            // 
            // CDRomCombo
            // 
            CDRomCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            CDRomCombo.FormattingEnabled = true;
            CDRomCombo.Location = new Point(29, 48);
            CDRomCombo.Name = "CDRomCombo";
            CDRomCombo.Size = new Size(268, 23);
            CDRomCombo.TabIndex = 5;
            CDRomCombo.SelectedIndexChanged += CDRomCombo_SelectedIndexChanged;
            // 
            // cdBox
            // 
            cdBox.Controls.Add(cdArtistNameLbl);
            cdBox.Controls.Add(cdArtistNameBox);
            cdBox.Controls.Add(cdAlbumNameLbl);
            cdBox.Controls.Add(cdAlbumNameBox);
            cdBox.Enabled = false;
            cdBox.Location = new Point(29, 91);
            cdBox.Name = "cdBox";
            cdBox.Size = new Size(374, 133);
            cdBox.TabIndex = 4;
            cdBox.TabStop = false;
            cdBox.Text = "CD Info";
            // 
            // cdArtistNameLbl
            // 
            cdArtistNameLbl.AutoSize = true;
            cdArtistNameLbl.Location = new Point(195, 42);
            cdArtistNameLbl.Name = "cdArtistNameLbl";
            cdArtistNameLbl.Size = new Size(73, 15);
            cdArtistNameLbl.TabIndex = 3;
            cdArtistNameLbl.Text = "Artist Name:";
            // 
            // cdArtistNameBox
            // 
            cdArtistNameBox.Location = new Point(192, 64);
            cdArtistNameBox.Name = "cdArtistNameBox";
            cdArtistNameBox.Size = new Size(138, 23);
            cdArtistNameBox.TabIndex = 2;
            cdArtistNameBox.TextChanged += cdArtistNameBox_TextChanged;
            // 
            // cdAlbumNameLbl
            // 
            cdAlbumNameLbl.AutoSize = true;
            cdAlbumNameLbl.Location = new Point(24, 42);
            cdAlbumNameLbl.Name = "cdAlbumNameLbl";
            cdAlbumNameLbl.Size = new Size(81, 15);
            cdAlbumNameLbl.TabIndex = 1;
            cdAlbumNameLbl.Text = "Album Name:";
            // 
            // cdAlbumNameBox
            // 
            cdAlbumNameBox.Location = new Point(21, 64);
            cdAlbumNameBox.Name = "cdAlbumNameBox";
            cdAlbumNameBox.Size = new Size(138, 23);
            cdAlbumNameBox.TabIndex = 0;
            cdAlbumNameBox.TextChanged += cdAlbumNameBox_TextChanged;
            // 
            // ripBtn
            // 
            ripBtn.Enabled = false;
            ripBtn.Location = new Point(437, 245);
            ripBtn.Name = "ripBtn";
            ripBtn.Size = new Size(75, 23);
            ripBtn.TabIndex = 3;
            ripBtn.Text = "Rip";
            ripBtn.UseVisualStyleBackColor = true;
            ripBtn.Click += ripBtn_Click;
            // 
            // ejectCloseBtn
            // 
            ejectCloseBtn.Location = new Point(328, 47);
            ejectCloseBtn.Name = "ejectCloseBtn";
            ejectCloseBtn.Size = new Size(75, 23);
            ejectCloseBtn.TabIndex = 2;
            ejectCloseBtn.Text = "Eject";
            ejectCloseBtn.UseVisualStyleBackColor = true;
            ejectCloseBtn.Click += ejectCloseBtn_Click;
            // 
            // locationLbl
            // 
            locationLbl.AutoSize = true;
            locationLbl.Location = new Point(29, 23);
            locationLbl.Name = "locationLbl";
            locationLbl.Size = new Size(53, 15);
            locationLbl.TabIndex = 1;
            locationLbl.Text = "Content:";
            // 
            // dvdPB
            // 
            dvdPB.ErrorImage = Properties.Resources.cd;
            dvdPB.Image = Properties.Resources.cd;
            dvdPB.InitialImage = Properties.Resources.cd;
            dvdPB.Location = new Point(31, 59);
            dvdPB.Name = "dvdPB";
            dvdPB.Size = new Size(334, 282);
            dvdPB.SizeMode = PictureBoxSizeMode.Zoom;
            dvdPB.TabIndex = 0;
            dvdPB.TabStop = false;
            // 
            // EditMetaDataTab
            // 
            EditMetaDataTab.Controls.Add(label7);
            EditMetaDataTab.Controls.Add(manEntryBtn);
            EditMetaDataTab.Controls.Add(manSearchBtn);
            EditMetaDataTab.Controls.Add(autoSearchBtn);
            EditMetaDataTab.Controls.Add(albumBox);
            EditMetaDataTab.Controls.Add(browsebtn);
            EditMetaDataTab.Controls.Add(albumFolderTb);
            EditMetaDataTab.Location = new Point(4, 24);
            EditMetaDataTab.Name = "EditMetaDataTab";
            EditMetaDataTab.Padding = new Padding(3);
            EditMetaDataTab.Size = new Size(930, 413);
            EditMetaDataTab.TabIndex = 1;
            EditMetaDataTab.Text = "Edit Metadata";
            EditMetaDataTab.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 12);
            label7.Name = "label7";
            label7.Size = new Size(82, 15);
            label7.TabIndex = 6;
            label7.Text = "Album Folder:";
            // 
            // manEntryBtn
            // 
            manEntryBtn.Location = new Point(528, 79);
            manEntryBtn.Name = "manEntryBtn";
            manEntryBtn.Size = new Size(95, 23);
            manEntryBtn.TabIndex = 5;
            manEntryBtn.Text = "Manual Entry";
            manEntryBtn.UseVisualStyleBackColor = true;
            manEntryBtn.Click += manEntryBtn_Click;
            // 
            // manSearchBtn
            // 
            manSearchBtn.Location = new Point(423, 79);
            manSearchBtn.Name = "manSearchBtn";
            manSearchBtn.Size = new Size(95, 23);
            manSearchBtn.TabIndex = 4;
            manSearchBtn.Text = "Manual Search";
            manSearchBtn.UseVisualStyleBackColor = true;
            manSearchBtn.Click += manSearchBtn_Click;
            // 
            // autoSearchBtn
            // 
            autoSearchBtn.Location = new Point(318, 79);
            autoSearchBtn.Name = "autoSearchBtn";
            autoSearchBtn.Size = new Size(95, 23);
            autoSearchBtn.TabIndex = 3;
            autoSearchBtn.Text = "Auto Search";
            autoSearchBtn.UseVisualStyleBackColor = true;
            autoSearchBtn.Click += autoSearchBtn_Click;
            // 
            // albumBox
            // 
            albumBox.BackColor = Color.Transparent;
            albumBox.Controls.Add(coverComboBox);
            albumBox.Controls.Add(genreComboBox);
            albumBox.Controls.Add(convertBox);
            albumBox.Controls.Add(saveBtn);
            albumBox.Controls.Add(label4);
            albumBox.Controls.Add(label6);
            albumBox.Controls.Add(yearTextBox);
            albumBox.Controls.Add(showTracksBtn);
            albumBox.Controls.Add(label3);
            albumBox.Controls.Add(label2);
            albumBox.Controls.Add(artistTextBox);
            albumBox.Controls.Add(label1);
            albumBox.Controls.Add(albumTextBox);
            albumBox.Controls.Add(albumPb);
            albumBox.Enabled = false;
            albumBox.Location = new Point(17, 116);
            albumBox.Name = "albumBox";
            albumBox.Size = new Size(907, 291);
            albumBox.TabIndex = 2;
            albumBox.TabStop = false;
            albumBox.Text = "Album Metadata";
            // 
            // coverComboBox
            // 
            coverComboBox.Cursor = Cursors.Hand;
            coverComboBox.FormattingEnabled = true;
            coverComboBox.Location = new Point(240, 111);
            coverComboBox.Name = "coverComboBox";
            coverComboBox.Size = new Size(170, 23);
            coverComboBox.TabIndex = 18;
            // 
            // genreComboBox
            // 
            genreComboBox.Cursor = Cursors.Hand;
            genreComboBox.FormattingEnabled = true;
            genreComboBox.Location = new Point(10, 164);
            genreComboBox.Name = "genreComboBox";
            genreComboBox.Size = new Size(170, 23);
            genreComboBox.TabIndex = 17;
            // 
            // convertBox
            // 
            convertBox.Controls.Add(groupBox1);
            convertBox.Controls.Add(convertLabel);
            convertBox.Controls.Add(choiceBox);
            convertBox.Controls.Add(convertN);
            convertBox.Controls.Add(convertY);
            convertBox.Location = new Point(429, 15);
            convertBox.Name = "convertBox";
            convertBox.Size = new Size(210, 215);
            convertBox.TabIndex = 16;
            convertBox.TabStop = false;
            convertBox.Text = "Convert";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cleanN);
            groupBox1.Controls.Add(cleanY);
            groupBox1.Location = new Point(6, 148);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 57);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Clean Folder?";
            // 
            // cleanN
            // 
            cleanN.AutoSize = true;
            cleanN.Checked = true;
            cleanN.Location = new Point(112, 23);
            cleanN.Name = "cleanN";
            cleanN.Size = new Size(39, 19);
            cleanN.TabIndex = 6;
            cleanN.TabStop = true;
            cleanN.Text = "no";
            cleanN.UseVisualStyleBackColor = true;
            // 
            // cleanY
            // 
            cleanY.AutoSize = true;
            cleanY.Location = new Point(33, 23);
            cleanY.Name = "cleanY";
            cleanY.Size = new Size(42, 19);
            cleanY.TabIndex = 5;
            cleanY.Text = "yes";
            cleanY.UseVisualStyleBackColor = true;
            cleanY.CheckedChanged += cleanY_CheckedChanged;
            // 
            // convertLabel
            // 
            convertLabel.AutoSize = true;
            convertLabel.Location = new Point(3, 25);
            convertLabel.Name = "convertLabel";
            convertLabel.Size = new Size(118, 15);
            convertLabel.TabIndex = 3;
            convertLabel.Text = "Convert WAV to MP3";
            // 
            // choiceBox
            // 
            choiceBox.Controls.Add(bitrateCB);
            choiceBox.Enabled = false;
            choiceBox.Location = new Point(6, 77);
            choiceBox.Name = "choiceBox";
            choiceBox.Size = new Size(200, 69);
            choiceBox.TabIndex = 2;
            choiceBox.TabStop = false;
            choiceBox.Text = "Select Bitrate";
            // 
            // bitrateCB
            // 
            bitrateCB.FormattingEnabled = true;
            bitrateCB.Location = new Point(6, 28);
            bitrateCB.Name = "bitrateCB";
            bitrateCB.Size = new Size(188, 23);
            bitrateCB.TabIndex = 0;
            // 
            // convertN
            // 
            convertN.AutoSize = true;
            convertN.Checked = true;
            convertN.Location = new Point(118, 47);
            convertN.Name = "convertN";
            convertN.Size = new Size(39, 19);
            convertN.TabIndex = 1;
            convertN.TabStop = true;
            convertN.Text = "no";
            convertN.UseVisualStyleBackColor = true;
            convertN.CheckedChanged += convertN_CheckedChanged;
            // 
            // convertY
            // 
            convertY.AutoSize = true;
            convertY.Location = new Point(39, 47);
            convertY.Name = "convertY";
            convertY.Size = new Size(42, 19);
            convertY.TabIndex = 0;
            convertY.Text = "yes";
            convertY.UseVisualStyleBackColor = true;
            convertY.CheckedChanged += convertY_CheckedChanged;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(818, 250);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(75, 23);
            saveBtn.TabIndex = 15;
            saveBtn.Text = "Start";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(240, 92);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 13;
            label4.Text = "Cover:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(240, 40);
            label6.Name = "label6";
            label6.Size = new Size(32, 15);
            label6.TabIndex = 9;
            label6.Text = "Year:";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // yearTextBox
            // 
            yearTextBox.Cursor = Cursors.IBeam;
            yearTextBox.Location = new Point(240, 59);
            yearTextBox.Name = "yearTextBox";
            yearTextBox.Size = new Size(170, 23);
            yearTextBox.TabIndex = 8;
            // 
            // showTracksBtn
            // 
            showTracksBtn.Location = new Point(240, 164);
            showTracksBtn.Name = "showTracksBtn";
            showTracksBtn.Size = new Size(170, 23);
            showTracksBtn.TabIndex = 7;
            showTracksBtn.Text = "Tracks";
            showTracksBtn.UseVisualStyleBackColor = true;
            showTracksBtn.Click += showTracksBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 146);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 6;
            label3.Text = "Genre:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 92);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 4;
            label2.Text = "Artist:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // artistTextBox
            // 
            artistTextBox.Cursor = Cursors.IBeam;
            artistTextBox.Location = new Point(10, 111);
            artistTextBox.Name = "artistTextBox";
            artistTextBox.Size = new Size(170, 23);
            artistTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 40);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 2;
            label1.Text = "Album:";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // albumTextBox
            // 
            albumTextBox.Cursor = Cursors.IBeam;
            albumTextBox.Location = new Point(10, 59);
            albumTextBox.Name = "albumTextBox";
            albumTextBox.Size = new Size(170, 23);
            albumTextBox.TabIndex = 1;
            // 
            // albumPb
            // 
            albumPb.ErrorImage = Properties.Resources.cd;
            albumPb.Image = Properties.Resources.cd;
            albumPb.InitialImage = Properties.Resources.cd;
            albumPb.Location = new Point(661, 22);
            albumPb.Name = "albumPb";
            albumPb.Size = new Size(232, 209);
            albumPb.SizeMode = PictureBoxSizeMode.StretchImage;
            albumPb.TabIndex = 0;
            albumPb.TabStop = false;
            albumPb.WaitOnLoad = true;
            // 
            // browsebtn
            // 
            browsebtn.Location = new Point(318, 30);
            browsebtn.Name = "browsebtn";
            browsebtn.Size = new Size(95, 23);
            browsebtn.TabIndex = 1;
            browsebtn.Text = "Browse";
            browsebtn.UseVisualStyleBackColor = true;
            browsebtn.Click += browsebtn_Click;
            // 
            // albumFolderTb
            // 
            albumFolderTb.Cursor = Cursors.IBeam;
            albumFolderTb.Location = new Point(17, 30);
            albumFolderTb.Name = "albumFolderTb";
            albumFolderTb.Size = new Size(286, 23);
            albumFolderTb.TabIndex = 0;
            // 
            // OdnoFormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 509);
            Controls.Add(OdnoTabs);
            Controls.Add(debugBtn);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "OdnoFormMain";
            Text = "Odno";
            Load += OdnoFormMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            OdnoTabs.ResumeLayout(false);
            CDRipTab.ResumeLayout(false);
            driveInfoBox.ResumeLayout(false);
            driveInfoBox.PerformLayout();
            cdBox.ResumeLayout(false);
            cdBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dvdPB).EndInit();
            EditMetaDataTab.ResumeLayout(false);
            EditMetaDataTab.PerformLayout();
            albumBox.ResumeLayout(false);
            albumBox.PerformLayout();
            convertBox.ResumeLayout(false);
            convertBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            choiceBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)albumPb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button debugBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitctrlQToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem appearanceToolStripMenuItem;
        private ToolStripMenuItem lightModeToolStripMenuItem;
        private ToolStripMenuItem darkModeToolStripMenuItem;
        private ToolStripMenuItem systemDefaultToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem showHelpF1ToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem resetToolStripMenuItem;
        private TabControl OdnoTabs;
        private TabPage CDRipTab;
        private TabPage EditMetaDataTab;
        private GroupBox albumBox;
        private Button browsebtn;
        private TextBox albumFolderTb;
        private PictureBox albumPb;
        private Label label1;
        private TextBox albumTextBox;
        private Label label2;
        private TextBox artistTextBox;
        private Label label3;
        private Button showTracksBtn;
        private Button manEntryBtn;
        private Button manSearchBtn;
        private Button autoSearchBtn;
        private Label label4;
        private Label label6;
        private TextBox yearTextBox;
        private Label label7;
        private Button saveBtn;
        private GroupBox convertBox;
        private ComboBox genreComboBox;
        private ComboBox coverComboBox;
        private RadioButton convertY;
        private GroupBox choiceBox;
        private RadioButton convertN;
        private Label convertLabel;
        private ComboBox bitrateCB;
        private PictureBox dvdPB;
        private GroupBox driveInfoBox;
        private Label locationLbl;
        private Button ejectCloseBtn;
        private Button ripBtn;
        private GroupBox cdBox;
        private Label cdArtistNameLbl;
        private TextBox cdArtistNameBox;
        private Label cdAlbumNameLbl;
        private TextBox cdAlbumNameBox;
        private GroupBox groupBox1;
        private RadioButton cleanN;
        private RadioButton cleanY;
        private ToolStripMenuItem editPreferencesToolStripMenuItem;
        private ToolStripMenuItem undoctrlZToolStripMenuItem;
        private ToolStripMenuItem redoctrlRToolStripMenuItem;
        private ComboBox CDRomCombo;
    }
}
