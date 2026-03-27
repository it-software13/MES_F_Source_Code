using SJeMES_Control_Library.Controls.Btn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Controls.DataGridView
{
    public class DataGridViewActionButtonCell : DataGridViewButtonCell
    {

        private static List<ActionButton> buttonList;
        private static int nowColIndex = 0; // 当前列序号
        private static int nowRowIndex = 0; // 当前行序号

        private static int marginLeft = 10;//按钮之间的左边距

        private static int marginTop = 4;//按钮距离边框的顶部距离

       public  static List<ActionButton> ButtonList { get => buttonList; set => buttonList = value; }
        public static int MarginLeft { get => marginLeft; set => marginLeft = value; }


        /// <summary>
        /// 对单元格的重绘事件进行的方法重写。
        /// </summary>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
            object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            cellBounds = PrivatePaint(graphics, cellBounds, rowIndex, cellStyle, true);
            base.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            nowColIndex = this.DataGridView.Columns.Count - 1;
        }

        /// <summary>
        /// 私有的单元格重绘方法，根据鼠标是否移动到按钮上，对按钮的不同背景和边框进行绘制。
        /// </summary>
        private Rectangle PrivatePaint(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewCellStyle cellStyle, bool clearBackground)
        {

            if (clearBackground) // 是否需要重绘单元格的背景颜色
            {
                Brush brushCellBack = (rowIndex == this.DataGridView.CurrentRow.Index) ? new SolidBrush(cellStyle.SelectionBackColor) : new SolidBrush(cellStyle.BackColor);
                graphics.FillRectangle(brushCellBack, cellBounds.X +1, cellBounds.Y+1 , cellBounds.Width-2, cellBounds.Height-2);
            }

            if (ButtonList != null)
            {
                Rectangle rectangle;
                Rectangle preRectangle;
                int index = 0;
                foreach (var item in ButtonList)
                {
                    preRectangle = index == 0 ? Rectangle.Empty : buttonList[index - 1].Rectangle;
                    if (item.MouseOnButton) // 鼠标移动到修改按钮上，更换背景及边框颜色
                    {

                        rectangle = DrawRectangle(cellBounds, preRectangle, item.BtnImageChecked, index, graphics, item.BtnPenChecked);
                        item.Rectangle = rectangle;

                    }
                    else
                    {
                        //item.BtnPen.Color = cellStyle.BackColor;
                        rectangle = DrawRectangle(cellBounds, preRectangle, item.BtnImage, index, graphics, item.BtnPen);
                        item.Rectangle = rectangle;
                    }
                    index++;
                }
            }

            return cellBounds;
        }

        /// <summary>
        /// 绘画 按钮的样式和位置
        /// </summary>
        /// <param name="cellBounds"></param>
        /// <param name="preRectangle"></param>
        /// <param name="backImage"></param>
        /// <param name="first"></param>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <returns></returns>
        private Rectangle DrawRectangle(Rectangle cellBounds, Rectangle preRectangle, Image backImage, int first, Graphics graphics, Pen pen)
        {

            int offsetX = cellBounds.Location.X + marginLeft;

            if (preRectangle != Rectangle.Empty)
            {
                offsetX = preRectangle.Location.X + preRectangle.Width + marginLeft;
            }

            int x = offsetX;

            int y = cellBounds.Location.Y + marginTop;
            //cellBounds.Location.Y +(cellBounds.Height - backImage.Height) / 2;

            int width = backImage.Width;
            int height = backImage.Height;


            Rectangle rectangle = new Rectangle(x, y, width, height);

            graphics.DrawImage(backImage, rectangle);
            graphics.DrawRectangle(pen, rectangle.X, rectangle.Y -1, backImage.Width, backImage.Height);

            return rectangle;
        }

        /// <summary>
        /// 鼠标移动到单元格内时的事件处理，通过坐标判断鼠标是否移动到了修改或删除按钮上，并调用私有的重绘方法进行重绘。
        /// </summary>
        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            if (base.DataGridView == null) { return; }

            nowColIndex = e.ColumnIndex;
            nowRowIndex = e.RowIndex;

            Rectangle cellBounds = DataGridView[e.ColumnIndex, e.RowIndex].ContentBounds;
            Rectangle paintCellBounds = DataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            paintCellBounds.Width = DataGridView.Columns[nowColIndex].Width;
            paintCellBounds.Height = DataGridView.Rows[nowRowIndex].Height;

            if (ButtonList != null)
            {
                //Rectangle rectangle = Rectangle.Empty;
                Rectangle curRectangle;
                int index = 0;
                //Console.WriteLine("鼠标位置x:" + e.X);

                int offsetX = 0;
                foreach (var item in ButtonList)
                {

                    offsetX += cellBounds.Location.X + marginLeft;
                    if (index > 0)
                    {
                        offsetX += cellBounds.Location.X + ButtonList[index - 1].Rectangle.Width;
                    }

                    int x = offsetX;
                    int y = cellBounds.Location.Y + marginTop;
                    //int y = cellBounds.Location.Y + (cellBounds.Height - item.BtnImage.Height) / 2;
                    int width = item.BtnImage.Width;
                    int height = item.BtnImage.Height;

                    curRectangle = new Rectangle(x, y, width, height);
                    index++;

                    if (IsInRect(e, curRectangle)) // 鼠标移动到修改按钮上，更换背景及边框颜色
                    {
                        if (!item.MouseOnButton)
                        {
                            item.MouseOnButton = true;
                            PrivatePaint(this.DataGridView.CreateGraphics(), paintCellBounds, e.RowIndex, this.DataGridView.RowTemplate.DefaultCellStyle, true);
                            DataGridView.Cursor = Cursors.Hand;
                        }

                    }
                    else
                    {
                        if (item.MouseOnButton)
                        {
                            item.MouseOnButton = false;
                            PrivatePaint(this.DataGridView.CreateGraphics(), paintCellBounds, e.RowIndex, this.DataGridView.RowTemplate.DefaultCellStyle, false);
                            DataGridView.Cursor = Cursors.Default;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 鼠标从单元格内移出时的事件处理，调用私有的重绘方法进行重绘。
        /// </summary>
        protected override void OnMouseLeave(int rowIndex)
        {

            if (ButtonList != null)
            {
                bool flag = false;
                Rectangle rectangle = Rectangle.Empty;
                foreach (var item in ButtonList)
                {
                    if (item.MouseOnButton)
                    {
                        flag = true;
                    }
                    item.MouseOnButton = false;
                }

                if (flag)
                {
                    Rectangle paintCellBounds = DataGridView.GetCellDisplayRectangle(nowColIndex, nowRowIndex, true);

                    paintCellBounds.Width = DataGridView.Columns[nowColIndex].Width;
                    paintCellBounds.Height = DataGridView.Rows[nowRowIndex].Height;

                    PrivatePaint(this.DataGridView.CreateGraphics(), paintCellBounds, nowRowIndex, this.DataGridView.RowTemplate.DefaultCellStyle, false);
                    DataGridView.Cursor = Cursors.Default;
                }
            }

        }

        ///废弃内部
    /*    /// <summary>
        /// 获取被点击的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static ActionButton GetButtonClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            ActionButton actionButton = null;
            if (e.RowIndex < 0)
            {
                return actionButton;
            }
            if (sender is DataGridView)
            {
                DataGridView DgvGrid = (DataGridView)sender;
                if (DgvGrid.Columns[e.ColumnIndex] is DataGridViewActionButtonColumn)
                {
                    Rectangle cellBounds = DgvGrid[e.ColumnIndex, e.RowIndex].ContentBounds;

                    if (ButtonList != null)
                    {
                        //Rectangle rectangle = Rectangle.Empty;
                        Rectangle curRectangle;
                        int index = 0;
                        //Console.WriteLine("鼠标位置x:" + e.X);

                        int offsetX = 0;
                        foreach (var item in ButtonList)
                        {

                            offsetX += cellBounds.Location.X + marginLeft;
                            if (index > 0)
                            {
                                offsetX += cellBounds.Location.X + ButtonList[index - 1].Rectangle.Width + marginLeft;
                            }

                            int x = offsetX;
                            int y = cellBounds.Location.Y + marginTop;
                            //int y = cellBounds.Location.Y + (cellBounds.Height - item.BtnImage.Height) / 2;
                            int width = item.BtnImage.Width;
                            int height = item.BtnImage.Height;

                            curRectangle = new Rectangle(x, y, width, height);
                            index++;

                            item.IsClicked = false;
                            if (IsInRect(e, item.Rectangle))
                            {
                                item.IsClicked = true;
                                actionButton = item;
                                break;
                            }
                        }
                    }

                    return actionButton;
                }
            }
            return actionButton;
        }*/



        /// <summary>
        /// 判断鼠标坐标是否在指定的区域内。
        /// </summary>
        private static bool IsInRect(MouseEventArgs e, Rectangle area)
        {
            int x = e.X;
            int y = e.Y;
            // this.GetCursorPos();
            // return true;
            if (x > area.Left && x < area.Right && y > area.Top && y < area.Bottom)
            {
                return true;
            }

            return false;
        }


    }

}
