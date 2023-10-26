using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Microsoft.VisualBasic.Devices;
using SudokuSpice.RuleBased;
using System.Windows.Input;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        const int CellSize = 75;

        static readonly Random Random = new();
        
        ArchipelagoSession session;
        DeathLinkService deathLinkService;

        SudokuCell[,] cells = new SudokuCell[9, 9];

        SudokuCell hover;

        bool isSelecting;
        List<SudokuCell> selected = new List<SudokuCell>();

        public Form1()
        {
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;

            InitializeComponent();
            
            createCells();

            startNewGame();

#if DEBUG
            ServerText.Text = "localhost";
#endif
            this.KeyPress += form_keyPressed;
            this.KeyDown += form_keyDowned;
        }

        void createCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var cell = new SudokuCell();

                    cell.Size = new Size(CellSize, CellSize);
                    cell.Location = new Point(i * CellSize, j * CellSize);
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.FlatAppearance.BorderColor = Color.Black;
                    cell.TabStop = false;
                    cell.X = i;
                    cell.Y = j;

                    cell.Clear();
                    cell.Deselect();

                    cell.MouseEnter += cell_MouseEnter;
                    cell.MouseDown += cell_MouseDown;
                    cell.MouseUp += cell_MouseUp;

                    cells[i, j] = cell;
                    panel1.Controls.Add(cell);
                }
            }
        }

        void cell_MouseEnter(object sender, EventArgs  e)
        {
            if(UserText.Focused || ServerText.Focused)
                return;

	        if (sender is not SudokuCell cell)
                return;

            if(isSelecting){
                selected.Add(cell);
                cell.Select(true);
                return;
            }
            
            if(!selected.Contains(cell))
		        cell.Select(false);
            if(!selected.Contains(hover))
                hover?.Deselect();
            hover = cell;
	    }

        void cell_MouseDown(object sender, EventArgs e)
        {
            SudokuCell cell = (SudokuCell)sender;
            this.ActiveControl = null;

            if(selected.Contains(cell)){
                foreach (SudokuCell c in selected)
                    c.Deselect();
                selected.Clear();

                return;
            }

            isSelecting = true;
            hover?.Deselect();
            hover = null;

            selected.Add(cell);
		    cell.Select(true);
        }

        void cell_MouseUp(object sender, EventArgs e)
        {
            isSelecting = false;
        }



        void form_keyPressed(object sender, KeyPressEventArgs e)
        {
            if(UserText.Focused || ServerText.Focused)
                return;
            
            if (e.KeyChar == '\b'){
                if(selected.Count == 0)
                    hover?.Clear();
                else foreach(SudokuCell cell in selected)
                    cell.Clear();
            }
            else if (e.KeyChar is >= '1' and <= '9'){
                int number = int.Parse(e.KeyChar.ToString());
                
                if(selected.Count == 0)
                    hover?.AddAnswerNum(number);
                else foreach(SudokuCell cell in selected)
                    cell.AddAnswerNum(number);
            }
        }

        void form_keyDowned(object sender, KeyEventArgs e)
        {
            if(UserText.Focused || ServerText.Focused)
                return;

	        if (e.KeyCode == Keys.Delete){
                if(selected.Count == 0)
                    hover?.Clear();
                else foreach(SudokuCell cell in selected)
                    cell.Clear();
            }


        }

        void startNewGame()
        {
	        var hintsCount = 0;

	        if (beginnerLevel.Checked)
		        hintsCount = 48;
	        else if (IntermediateLevel.Checked)
#if DEBUG
                hintsCount = 81;
#else
		        hintsCount = 35;
#endif
            else if (AdvancedLevel.Checked)
		        hintsCount = 24;

            var generator = new StandardPuzzleGenerator();
	        var puzzle = generator.Generate(9, hintsCount, TimeSpan.Zero);

            fillField(puzzle);

            checkButton.Enabled = true;

            LogWriteLine("New game started", Color.White);
        }

        void fillField(PuzzleWithPossibleValues puzzle)
        {
	        var solver = StandardPuzzles.CreateSolver();
	        var solved = solver.Solve(puzzle);
            
            for (int x = 0; x < 9; x++)
	        {
		        for (int y = 0; y < 9; y++)
		        {
			        var cell = cells[x, y];

			        cell.Value = solved[x, y].Value;
                    cell.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);

                    if (puzzle[x, y].HasValue)
			        {
				        cell.Text = cell.Value.ToString();
                        cell.ForeColor = Color.Black;
                        cell.IsLocked = true;
			        }
			        else
                    {
	                    cell.Text = "";
                        cell.ForeColor = SystemColors.ControlDarkDark;
				        cell.IsLocked = false;
                    }
                }
	        }
        }

        void checkButton_Click(object sender, EventArgs e)
        {
            bool hasError = false;
            bool isFilled = true;

            foreach (var cell in cells)
            {
                if (string.IsNullOrEmpty(cell.Text))
                {
                    isFilled = false;
                    break;
                }

                if (!string.Equals(cell.Value.ToString(), cell.Text))
                {
                    hasError = true;
                }
            }

            if (!isFilled)
            {
                ShowMessageBox("Result", "Not all fields are filled yet", Color.Blue);
            }
			else if (hasError)
            {
                if (deathLinkService != null && DeathLinkCheckBox.Checked)
                {
                    var deathLink = new DeathLink(session.Players.GetPlayerAlias(session.ConnectionInfo.Slot), "Failed to solve a Sudoku");
                    deathLinkService.SendDeathLink(deathLink);
                }

                ShowMessageBox("Result", "Wrong inputs", Color.Blue);
            }
            else
            {
                if (session != null && session.Socket.Connected)
                {
                    checkButton.Enabled = false;

                    var missing = session.Locations.AllMissingLocations;
                    var alreadyHinted = session.DataStorage.GetHints()
                        .Where(h => h.FindingPlayer == session.ConnectionInfo.Slot)
                        .Select(h => h.LocationId);

                    var availableForHinting = missing.Except(alreadyHinted).ToArray();

                    if (availableForHinting.Any())
                    {
                        var locationId = availableForHinting[Random.Next(0, availableForHinting.Length)];

                        session.Locations.ScoutLocationsAsync(true, locationId);
                        
                        ShowMessageBox("Result", "Correct, unlocked 1 hint", Color.Blue);
                    }
                    else
                    {
                        ShowMessageBox("Result", "Correct, no remaining locations left to hint for", Color.DarkBlue);
                    }
                }
                else
                {
                    ShowMessageBox("Result", "Correct, no hints are unlocked as you are not connected", Color.Blue);
                }
            }
        }

        void clearButton_Click(object sender, EventArgs e)
        {
            foreach (var cell in cells)
                if (cell.IsLocked == false)
                    cell.Clear();
        }

        void newGameButton_Click(object sender, EventArgs e)
        {
            startNewGame();
        }

        void ConnectButton_Click(object sender, EventArgs e)
        {
            if (session != null)
            {
                session = null;
                deathLinkService = null;
                ConnectButton.Text = "Connect";
                UserText.Enabled = true;
                ServerText.Enabled = true;
                PasswordText.Enabled = true;

                LogWriteLine("Disconnected", Color.Red);

                return;
            }

            var serverUri = ServerText.Text;

            try
            {
                session = ArchipelagoSessionFactory.CreateSession(serverUri);
                session.Socket.ErrorReceived += Socket_ErrorReceived;
                session.MessageLog.OnMessageReceived += MessageLog_OnMessageReceived;

                var result = session.TryConnectAndLogin("", UserText.Text, ItemsHandlingFlags.NoItems, password: PasswordText.Text,
                    tags: new[] { "BK_Sudoku", "TextOnly" }, requestSlotData: false);

                if (!result.Successful)
                {
                    ShowMessageBox("Login Failed", string.Join(',', ((LoginFailure)result).Errors), Color.Red);
                }
                else
                {
                    if (session.RoomState.Version < new Version(0, 3, 7))
                    {
                        session.Socket.DisconnectAsync();

                        ShowMessageBox("Version mismatch", "Server out of date, this version of BK Sudoku can only connect to servers of 0.3.7 or higher", Color.Red);

                        return;
                    }
                    
	                LogWriteLine("Connected", Color.Green);

                    ConnectButton.Text = "Disconnect";
                    UserText.Enabled = false;
                    ServerText.Enabled = false;
                    PasswordText.Enabled = false;

                    deathLinkService = session.CreateDeathLinkService();
                    deathLinkService.OnDeathLinkReceived += (deathLink) =>
                    {
                        startNewGame();
                        ShowMessageBox("DeathLink", $"DeathLink received from: {deathLink.Source}, reason: {deathLink.Cause}", Color.DarkRed);
                    };
                    
                    DeathLinkCheckBox_CheckedChanged(sender, e);
                }
            }
            catch (Exception exception)
            {
	            ShowMessageBox("ERROR", exception.Message, Color.Red);
            }
        }

        void Socket_ErrorReceived(Exception e, string message)
        {
            LogWriteLine($"Socket ERROR {e.Message}", Color.Red);
            LogWriteLine(e.StackTrace, Color.Red);
        }

        void MessageLog_OnMessageReceived(LogMessage message)
        {
            switch (message)
            {
                case HintItemSendLogMessage hintMessage when hintMessage.IsRelatedToActivePlayer:
                    Invoke(() =>
                    {
                        foreach (var part in hintMessage.Parts)
                            LogWrite(part.Text, ToSystemColor(part.Color));

                        APLog.AppendText(Environment.NewLine);
                        APLog.ScrollToCaret();
                    });
                    break;

                case ItemSendLogMessage itemMessage when itemMessage.Item.Flags == ItemFlags.Advancement 
                                                         && itemMessage.IsReceiverTheActivePlayer:
                    Invoke(() =>
                    {
                        foreach (var part in itemMessage.Parts)
                            LogWrite(part.Text, ToSystemColor(part.Color));

                        APLog.AppendText(Environment.NewLine);
                        APLog.ScrollToCaret();
                    });
                    break;
            }
        }

        static Color ToSystemColor(Archipelago.MultiClient.Net.Models.Color c) =>
            Color.FromArgb(255, c.R, c.G, c.B);

        void LogWrite(string text, Color color)
        {
            APLog.SelectionStart = APLog.TextLength;
            APLog.SelectionLength = 0;

            APLog.SelectionColor = color;
            APLog.AppendText(text);
            APLog.SelectionColor = APLog.ForeColor;

            APLog.ScrollToCaret();
        }

        void LogWriteLine(string text, Color color)
        {
	        LogWrite(text, color);
	        APLog.AppendText(Environment.NewLine);

	        APLog.ScrollToCaret();
        }

        void ShowMessageBox(string title, string message, Color color)
        {
	        LogWriteLine(message, color);
	        MessageBox.Show(message, title);
	    }

        void DeathLinkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (session == null || deathLinkService == null)
                return;

            if (DeathLinkCheckBox.Checked)
                deathLinkService.EnableDeathLink();
            else
                deathLinkService.DisableDeathLink();
        }
    }
}
