using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    enum EntryMode
    {
        Normal, //Enter the 'answer' for a cell
        Center, //Enter a 'center mark' in the cell
    }
    class SudokuCell : Button
    {
        public int Value { get; set; } //The 'correct' value for the cell
        public bool IsLocked { get; set; } //If the cell was one of the 'given digits', and is uneditable
        public int X { get; set; }
        public int Y { get; set; }
        public int FilledVal { get; set; } //The user's 'answer' for the cell
        public bool[] MarkedVals = new bool[9]; //The user's pencilmarks for the cell (indx 0-8 = 1-9)

        static Color UserColor = Color.FromArgb(30, 107, 229); //The color of user-entered numbers
        static Color LockedColor = Color.Black; //The color of 'given digits'

        public void Clear() //Clear the cell entirely
        {
            this.IsLocked = false;
            this.Text = string.Empty;
            this.FilledVal = 0;
            this.MarkedVals.Initialize();
        }

        // Clears the marks in a cell.
        // If there is a 'FilledVal', only that is cleared.
        public void ClearMarks()
        {
            if (this.IsLocked)
                return;
            if(this.FilledVal > 0)
                this.FilledVal = 0;
            else this.MarkedVals.Initialize();
            UpdateCell();
        }

        //Enter a number into a cell, either as an answer or centermark
        public void Mark(int num, EntryMode mode)
        {
            if (this.IsLocked)
                return;
            if (num < 1 || num > 9)
                return;
            bool shift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
            if (shift) //Toggle EntryMode.Center if shift is down
            {
                if (mode == EntryMode.Center)
                    mode = EntryMode.Normal;
                else mode = EntryMode.Center;
            }
            switch (mode)
            {
                case EntryMode.Normal:
                    if (this.FilledVal == num)
                        this.FilledVal = 0;
                    else this.FilledVal = num;
                    break;
                case EntryMode.Center:
                    if (this.FilledVal > 0)
                        break;
                    this.MarkedVals[num-1] = !this.MarkedVals[num-1];
                    break;
            }
        }

        //Update the visual of the cell to match it's current contents
        public void UpdateCell()
        {
            if (this.FilledVal > 0)
            {
                this.Text = this.FilledVal.ToString();
                this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            }
            else
            {
                this.Text = string.Empty;
                for(int q = 1; q <= 9; ++q)
                {
                    if (this.MarkedVals[q-1])
                        this.Text += q.ToString();
                }
                this.Font = new Font(SystemFonts.DefaultFont.FontFamily, this.Text.Length < 6 ? 14 : 8, FontStyle.Italic);
            }
            this.ForeColor = this.IsLocked ? LockedColor : UserColor;
        }

        //Prevents an ugly border from appearing after pressing 'Tab'
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
