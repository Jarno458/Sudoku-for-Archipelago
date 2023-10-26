using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku
{
    class SudokuCell : Button
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }

        private int _x, _y;
        public int X {
            get => _x;
            set  {
                _x = value;
                this.darkCell = ((this._x / 3) + (this._y / 3)) % 2 == 1;
            } 
        }
        public int Y {
            get => _y;
            set  {
                _y = value;
                this.darkCell = ((this._x / 3) + (this._y / 3)) % 2 == 1;
            } 
        }


        private bool darkCell;

        private static Color deselectedLight = SystemColors.Control, deselectedDark = Color.LightGray;

        private static Color hoverLight = Color.FromArgb(255,192,192,192), hoverDark = Color.FromArgb(255,176,176,176);
        private static Color selectLight = Color.FromArgb(255,160,160,160), selectDark = Color.FromArgb(255,144,144,144);

        private static Color userText = Color.FromArgb(255,112,112,112), userMultiText = Color.FromArgb(255,0,128,128);
        private static Color highlightText = Color.FromArgb(255,0,0,192);
        
        public void Select(bool fullSelect){
            if(fullSelect)
                this.BackColor = darkCell ? selectDark : selectLight;
            else
                this.BackColor = darkCell ? hoverDark : hoverLight;
        }
        public void Deselect(){
            this.BackColor = darkCell ? deselectedDark : deselectedLight;
        }

        public void Clear()
        {
            if (this.IsLocked)
                return;

            this.Text = string.Empty;

            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.ForeColor = userText;
        }

        public void SetPreview(int number){
            if(this.Text == number.ToString())
                this.ForeColor = highlightText;
            else
                UpdateTextCol();
        }

        private void UpdateTextCol(){
            if (this.IsLocked){
                this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                this.ForeColor = Color.Black;
            }else if(this.Text.Length <= 1){
                this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                this.ForeColor = userText;
            } else if (this.Text.Length <= 6){
		        this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 14, FontStyle.Italic);
                this.ForeColor = userMultiText;
            } else{
		        this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 8, FontStyle.Italic);
                this.ForeColor = userMultiText;
            }
        }

        public void AddAnswerNum(int number){
            if (this.IsLocked)
                return;

            if (this.Text.Contains(number.ToString()))
                return;

            if(this.Text.Length >= 1){
                AddCenterNum(number);
                return;
            }
            
            this.Text += number.ToString();

            UpdateTextCol();
        }

        public void AddCenterNum(int number){
            if (this.Text.Contains(number.ToString()))
                return;

            this.Text += number.ToString();
            this.Text = string.Concat(this.Text.OrderBy(c => c));
            
            UpdateTextCol();
        }

    }
}
