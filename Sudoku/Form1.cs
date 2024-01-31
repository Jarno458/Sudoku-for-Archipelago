using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using SudokuSpice.RuleBased;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        const int CellSize = 75;

        static readonly Random Random = new();

        ArchipelagoSession session;
        DeathLinkService deathLinkService;

        SudokuCell[,] cells = new SudokuCell[9, 9];
        EntryMode mode = EntryMode.Normal;

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
        }

        void createCells()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    var cell = new SudokuCell();

                    cell.Size = new Size(CellSize, CellSize);
                    cell.Location = new Point(x * CellSize, y * CellSize);
                    cell.BackColor = ((x / 3) + (y / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.FlatAppearance.BorderColor = Color.Black;
                    cell.X = x;
                    cell.Y = y;

                    cell.KeyDown += Cell_KeyDowned;
                    cell.PreviewKeyDown += Cell_PreKeyDowned;
                    cell.MouseClick += Cell_MouseClick;
                    cell.UpdateCell();

                    cells[x, y] = cell;
                    panel1.Controls.Add(cell);
                }
            }
        }
        void ToggleMode() //Toggle between entry modes
        {
            if (mode == EntryMode.Normal)
                entryCenter.PerformClick();
            else if (mode == EntryMode.Center)
                entryNormal.PerformClick();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                //The default behavior for arrowkeys does not move properly between cells.
                //Thus, to fix this, we need to disable the default behavior here.
                case Keys.Left:
                case Keys.Right:
                case Keys.Down:
                case Keys.Up:
                    return true;
                //By default tab cycles through focus elements, but it does so in a fairly useless order.
                //Instead, we override it to toggle entry modes.
                case Keys.Tab:
                    ToggleMode();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        void Cell_MouseClick(object sender, EventArgs e)
        {
            var cell = (SudokuCell)sender;
            cell.Focus();
        }
        void Cell_PreKeyDowned(object sender, PreviewKeyDownEventArgs e)
        {
            var cell = (SudokuCell)sender;
            switch (e.KeyCode)
            {
                //Handle arrow keys moving between cells properly
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    {
                        int x = cell.X;
                        int y = cell.Y;
                        switch (e.KeyCode)
                        {
                            case Keys.Left:
                                --x;
                                break;
                            case Keys.Right:
                                ++x;
                                break;
                            case Keys.Up:
                                --y;
                                break;
                            case Keys.Down:
                                ++y;
                                break;
                        }
                        if (x >= 0 && x < 9 && y >= 0 && y < 9)
                            cells[x, y].Focus();
                        break;
                    }
            }
        }
        void Cell_KeyDowned(object sender, KeyEventArgs e)
        {
            var cell = (SudokuCell)sender;

            if (cell.IsLocked)
                return;

            switch (e.KeyCode)
            {
                case Keys.Delete:
                case Keys.Back:
                    cell.ClearMarks();
                    break;
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    {
                        int num = (int)(e.KeyCode - Keys.D0);
                        cell.Mark(num, mode);
                        break;
                    }
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    {
                        int num = (int)(e.KeyCode - Keys.NumPad0);
                        cell.Mark(num, mode);
                        break;
                    }
            }

            cell.UpdateCell();
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

                    cell.Clear();
                    cell.Value = solved[x, y].Value;
                    cell.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);

                    if (puzzle[x, y].HasValue)
                    {
                        cell.FilledVal = cell.Value;
                        cell.IsLocked = true;
                    }
                    else
                    {
                        cell.FilledVal = 0;
                        cell.IsLocked = false;
                    }
                    cell.UpdateCell();
                }
            }
        }

        void checkButton_Click(object sender, EventArgs e)
        {
            bool hasError = false;
            bool isFilled = true;

            foreach (var cell in cells)
            {
                if (cell.FilledVal == 0)
                {
                    isFilled = false;
                    break;
                }

                if (cell.Value != cell.FilledVal)
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
                if (!cell.IsLocked)
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

        private void SetEntryNormal(object sender, EventArgs e)
        {
            mode = EntryMode.Normal;
        }
        private void SetEntryCenter(object sender, EventArgs e)
        {
            mode = EntryMode.Center;
        }
    }
}
