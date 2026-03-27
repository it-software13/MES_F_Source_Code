using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace DataGrid.DataGridViewCustomColumn
{
    /// <summary>
    /// 表示 <see cref="DataGridView"/> 控件中的单个单元格。
    /// </summary>
    public class DataGridViewOperationCell: DataGridViewCell
    {
        /// <summary>
        /// 初始化<see cref="DataGridViewOperationCell"/>类的新实例
        /// </summary>
        public DataGridViewOperationCell()
        {
            this.Items = new DataGridViewOperationItems(this);
        }

        /// <summary>
        /// DataGridViewOperationItem 集合
        /// </summary>
        public DataGridViewOperationItems Items { get; protected set; }

        /// <summary>
        /// 与图像关联的用户定义文本
        /// </summary>
        [DefaultValue("")]
        public string Description { get; set; }

        /// <summary>
        /// 获取单元格的值。
        /// </summary>
        /// <param name="rowIndex">该单元格父行的索引。</param>
        /// <returns></returns>
        protected override object GetValue(int rowIndex)
        {
            //object obj = base.GetValue(rowIndex);
            //if (obj == null)
            //{
            //    DataGridViewOperationColumn owningColumn = base.OwningColumn as DataGridViewOperationColumn;
            //    if (owningColumn == null)
            //    {
            //        return obj;
            //    }
            //    DataGridViewOperationItems items = owningColumn.Items;
            //    if (items != null)
            //    {
            //        return items;
            //    }
            //    return obj;
            //}
            return this.Items;
        }

        /// <summary>
        /// 获取新记录所在行中单元格的默认值。
        /// </summary>
        public override object DefaultNewRowValue
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单元格的寄宿编辑控件的类型。
        /// </summary>
        public override Type EditType
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 获取与该单元格关联的格式化值的类型。
        /// </summary>
        public override Type FormattedValueType
        {
            get
            {
                return typeof(DataGridViewOperationItems);
            }
        }

        /// <summary>
        /// 获取或设置单元格中值的数据类型。
        /// </summary>
        public override Type ValueType
        {
            get
            {
                return typeof(DataGridViewOperationItems);
            }
        }

        internal void OnDataGridViewColumnCommonChange()
        {
            if (this.DataGridView != null && this.OwningColumn != null)
            {
                Type type = this.DataGridView.GetType();
                MethodInfo method = type.GetMethod("OnColumnCommonChange", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null) method.Invoke(this.DataGridView, new object[] { this.OwningColumn.Index });
            }
        }

        /// <summary>
        /// 克隆一个 <see cref="DataGridViewOperationCell"/>。
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DataGridViewOperationCell cell = base.Clone() as DataGridViewOperationCell;
            if (cell != null)
            {
                cell.Items = new DataGridViewOperationItems(cell);
                foreach (DataGridViewOperationItem srcItem in this.Items)
                {
                    cell.Items.Add(new DataGridViewOperationItem { Name = srcItem.Name, Text = srcItem.Text, Image = srcItem.Image, Enabled = srcItem.Enabled, Visible = srcItem.Visible });
                }
                cell.Description = this.Description;
            }

            return cell;
        }

        /// <summary>
        /// 为 <see cref="DataGridViewOperationCell"/> 创建一个新的辅助性对象。
        /// </summary>
        /// <returns></returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return null;
        }

        /// <summary>
        /// 获取为显示进行格式化的单元格的值。
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <param name="rowIndex">该单元格父行的索引。</param>
        /// <param name="cellStyle">对单元格有效的 <see cref="DataGridViewCellStyle"/>。</param>
        /// <param name="valueTypeConverter">与值类型关联的 <see cref="TypeConverter"/>，它提供到格式化值类型的自定义转换；如果不需要此类自定义转换，则为 null。</param>
        /// <param name="formattedValueTypeConverter">与格式化值类型相关联的 <see cref="TypeConverter"/>，它提供从该值类型进行的自定义转换；如果不需要这种自定义转换，则为 null。</param>
        /// <param name="context"><see cref="DataGridViewDataErrorContexts"/> 值的按位组合，用于描述需要格式化值的上下文。</param>
        /// <returns></returns>
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if ((context & DataGridViewDataErrorContexts.ClipboardContent) != 0)
            {
                return this.Description;
            }
            object obj2 = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            if ((obj2 == null) && (cellStyle.NullValue == null))
            {
                return null;
            }
            return obj2;
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="elementState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }

            //DataGridViewOperationItem.ActiveItem = "";

            if ((paintParts & DataGridViewPaintParts.Border) > DataGridViewPaintParts.None)
            {
                this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            this.PaintPrivate(graphics, cellBounds, rowIndex, elementState, formattedValue, cellStyle, advancedBorderStyle, paintParts);
            //Rectangle destRectangle = cellBounds;
            //Rectangle advancedBorderRectangle = this.BorderWidths(advancedBorderStyle);
            //destRectangle.Offset(advancedBorderRectangle.X, advancedBorderRectangle.Y);
            //destRectangle.Width -= advancedBorderRectangle.Right;
            //destRectangle.Height -= advancedBorderRectangle.Bottom;
            //if (cellStyle.Padding != Padding.Empty)
            //{
            //    destRectangle.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
            //    destRectangle.Width -= cellStyle.Padding.Horizontal;
            //    destRectangle.Height -= cellStyle.Padding.Vertical;
            //}
            //bool selectedFlag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
            //Color color = (((paintParts & DataGridViewPaintParts.SelectionBackground) > DataGridViewPaintParts.None) & selectedFlag) ? cellStyle.SelectionBackColor : cellStyle.BackColor;
            //using (SolidBrush cachedBrush = new SolidBrush(color))
            //{
            //    if (((paintParts & DataGridViewPaintParts.Background) > DataGridViewPaintParts.None) && (cachedBrush.Color.A == 0xff))
            //    {
            //        graphics.FillRectangle(cachedBrush, destRectangle);
            //    }
            //    if ((destRectangle.Width > 0) && (destRectangle.Height > 0) && !this.DataGridView.Rows[rowIndex].IsNewRow)
            //    {
            //        DataGridViewOperationItems items = formattedValue as DataGridViewOperationItems;
            //        if (items != null)
            //        {
            //            int itemsWidth = 0, itemsHeight = 0;
            //            DataGridViewOperationColumn column = base.OwningColumn as DataGridViewOperationColumn;
            //            if (column != null)
            //            {
            //                int visibleCount = items.VisibleCount;
            //                visibleCount += items.Count > visibleCount ? 1 : 0;
            //                itemsWidth = visibleCount * column.ItemSize.Width + (visibleCount - 1) * 2;
            //                itemsHeight = column.ItemSize.Height;
            //                Rectangle itemsRectangle = new Rectangle(0, 0, itemsWidth, itemsHeight);
            //                itemsRectangle.X = destRectangle.X + ((destRectangle.Width - itemsRectangle.Width) / 2);
            //                itemsRectangle.Y = destRectangle.Y + ((destRectangle.Height - itemsRectangle.Height) / 2);

            //                if ((paintParts & DataGridViewPaintParts.ContentForeground) > DataGridViewPaintParts.None)
            //                {
            //                    Region clip = graphics.Clip;
            //                    graphics.SetClip(Rectangle.Intersect(Rectangle.Intersect(itemsRectangle, destRectangle), Rectangle.Truncate(graphics.VisibleClipBounds)));
            //                    int index = 0;
            //                    Rectangle imageRectangle;
            //                    DataGridViewOperationItem.ActiveItem = "";
            //                    m_RectangleList.Clear();
            //                    foreach (DataGridViewOperationItem item in items)
            //                    {
            //                        if (item.Visible)
            //                        {
            //                            imageRectangle = new Rectangle(itemsRectangle.X + (column.ItemSize.Width + 2) * index, itemsRectangle.Y, column.ItemSize.Width, column.ItemSize.Height);
            //                            graphics.DrawImage(item.Image, imageRectangle, 0, 0, item.Image.Width, item.Image.Height, GraphicsUnit.Pixel);
            //                            index++;
            //                            m_RectangleList.Add(item.Name, imageRectangle);
            //                            //if (imageRectangle.Contains(this.DataGridView.PointToClient(Control.MousePosition)))
            //                            //{
            //                            //    DataGridViewOperationItem.ActiveItem = item.Name;
            //                            //}
            //                        }
            //                    }
            //                    if (items.Count > items.VisibleCount && column.OverflowImage != null)
            //                    {
            //                        imageRectangle = new Rectangle(itemsRectangle.X + (column.ItemSize.Width + 2) * index, itemsRectangle.Y, column.ItemSize.Width, column.ItemSize.Height);
            //                        graphics.DrawImage(column.OverflowImage, imageRectangle, 0, 0, column.OverflowImage.Width, column.OverflowImage.Height, GraphicsUnit.Pixel);
            //                        m_RectangleList.Add("__OperationOverflowItem", imageRectangle);
            //                        //if (imageRectangle.Contains(this.DataGridView.PointToClient(Control.MousePosition)))
            //                        //{
            //                        //    DataGridViewOperationItem.ActiveItem = "__OperationOverflowItem";
            //                        //}
            //                    }
            //                    graphics.Clip = clip;
            //                    foreach (string key in m_RectangleList.Keys)
            //                    {
            //                        if (m_RectangleList[key].Contains(this.DataGridView.PointToClient(Control.MousePosition)))
            //                        {
            //                            DataGridViewOperationItem.ActiveItem = key;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private SortedList<string, Rectangle> PaintPrivate(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            SortedList<string, Rectangle> result = new SortedList<string, Rectangle>();

            Rectangle destRectangle = cellBounds;
            Rectangle advancedBorderRectangle = this.BorderWidths(advancedBorderStyle);
            destRectangle.Offset(advancedBorderRectangle.X, advancedBorderRectangle.Y);
            destRectangle.Width -= advancedBorderRectangle.Right;
            destRectangle.Height -= advancedBorderRectangle.Bottom;
            if (cellStyle.Padding != Padding.Empty)
            {
                destRectangle.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                destRectangle.Width -= cellStyle.Padding.Horizontal;
                destRectangle.Height -= cellStyle.Padding.Vertical;
            }
            if (graphics != null)
            {
                bool selectedFlag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
                Color color = (((paintParts & DataGridViewPaintParts.SelectionBackground) > DataGridViewPaintParts.None) & selectedFlag) ? cellStyle.SelectionBackColor : cellStyle.BackColor;
                using (SolidBrush cachedBrush = new SolidBrush(color))
                {
                    if (((paintParts & DataGridViewPaintParts.Background) > DataGridViewPaintParts.None) && (cachedBrush.Color.A == 0xff))
                    {
                        graphics.FillRectangle(cachedBrush, destRectangle);
                    }
                }
            }
            if ((destRectangle.Width > 0) && (destRectangle.Height > 0) && !this.DataGridView.Rows[rowIndex].IsNewRow)
            {
                DataGridViewOperationItems items = formattedValue as DataGridViewOperationItems;
                if (items != null)
                {
                    int itemsWidth = 0, itemsHeight = 0;
                    DataGridViewOperationColumn column = base.OwningColumn as DataGridViewOperationColumn;
                    if (column != null)
                    {
                        int visibleCount = items.VisibleCount;
                        visibleCount += items.Count > visibleCount ? 1 : 0;
                        itemsWidth = visibleCount * column.ItemSize.Width + (visibleCount - 1) * 2;
                        itemsHeight = column.ItemSize.Height;
                        Rectangle itemsRectangle = new Rectangle(0, 0, itemsWidth, itemsHeight);
                        itemsRectangle.X = destRectangle.X + ((destRectangle.Width - itemsRectangle.Width) / 2);
                        itemsRectangle.Y = destRectangle.Y + ((destRectangle.Height - itemsRectangle.Height) / 2);

                        if ((paintParts & DataGridViewPaintParts.ContentForeground) > DataGridViewPaintParts.None)
                        {
                            Region clip = null;
                            if (graphics != null)
                            {
                                clip = graphics.Clip;
                                graphics.SetClip(Rectangle.Intersect(Rectangle.Intersect(itemsRectangle, destRectangle), Rectangle.Truncate(graphics.VisibleClipBounds)));
                            }
                            int index = 0;
                            Rectangle imageRectangle;
                            foreach (DataGridViewOperationItem item in items)
                            {
                                if (item.Visible)
                                {
                                    imageRectangle = new Rectangle(itemsRectangle.X + (column.ItemSize.Width + 2) * index, itemsRectangle.Y, column.ItemSize.Width, column.ItemSize.Height);
                                    try
                                    {
                                        if (graphics != null) graphics.DrawImage(item.Image, imageRectangle, 0, 0, item.Image.Width, item.Image.Height, GraphicsUnit.Pixel);
                                    }
                                    catch  
                                    { 
                                    }
                                    index++;
                                    result.Add(item.Name, imageRectangle);
                                }
                            }
                            if (items.Count > items.VisibleCount && column.OverflowImage != null)
                            {
                                imageRectangle = new Rectangle(itemsRectangle.X + (column.ItemSize.Width + 2) * index, itemsRectangle.Y, column.ItemSize.Width, column.ItemSize.Height);
                                if (graphics != null) graphics.DrawImage(column.OverflowImage, imageRectangle, 0, 0, column.OverflowImage.Width, column.OverflowImage.Height, GraphicsUnit.Pixel);
                                result.Add("__OperationOverflowItem", imageRectangle);
                            }
                            if (graphics != null) graphics.Clip = clip;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前项
        /// </summary>
        public string CurrentItem
        {
            get
            {
                string name = null;

                if (this.DataGridView != null && this.OwningColumn != null)
                {
                    DataGridViewAdvancedBorderStyle style = null;
                    DataGridViewElementStates states = DataGridViewElementStates.None;
                    Rectangle rectangle = Rectangle.Empty;

                    Type type = this.GetType();
                    MethodInfo method = type.GetMethod("ComputeBorderStyleCellStateAndCellBounds", BindingFlags.Instance | BindingFlags.NonPublic);
                    object[] args = new object[] { this.RowIndex, style, states, rectangle };
                    if (method != null) method.Invoke(this, args);
                    style = args[1] as DataGridViewAdvancedBorderStyle;
                    states = (DataGridViewElementStates)args[2];
                    rectangle = this.DataGridView.GetCellDisplayRectangle(this.ColumnIndex, this.RowIndex, false);
                    SortedList<string, Rectangle> rectangleList = this.PaintPrivate(null, rectangle, this.RowIndex, states, this.Value, this.OwningColumn.DefaultCellStyle, style, DataGridViewPaintParts.ContentForeground);

                    Point menuLocation = Point.Empty;
                    foreach (string key in rectangleList.Keys)
                    {
                        if (rectangleList[key].Contains(this.DataGridView.PointToClient(Control.MousePosition)))
                        {
                            name = key;
                            menuLocation = rectangleList[key].Location;
                            break;
                        }
                    }
                    if (name == "__OperationOverflowItem")
                    {
                        ContextMenuStrip menu = (this.DataGridView as SCDataGridView).OperationOverflowContextMenuStrip;
                        menu.Items.Clear();
                        foreach (DataGridViewOperationItem item in this.Items)
                        {
                            if (!item.Visible)
                            {
                                ToolStripMenuItem menuItem = new ToolStripMenuItem { Name = item.Name, Text = item.Text, Image = item.Image };
                                menuItem.Click += (sender, e) =>
                                {
                                    SCDataGridView dataGridView = this.DataGridView as SCDataGridView;
                                    if (dataGridView != null)
                                    {
                                        dataGridView.OnOperationOverflowItemClickInternal((sender as ToolStripMenuItem).Name, this.DataGridView.CurrentCell.ColumnIndex, this.DataGridView.CurrentCell.RowIndex);
                                    }
                                };
                                menu.Items.Add(menuItem);
                            }
                        }
                        Size itemSize = (this.OwningColumn as DataGridViewOperationColumn).ItemSize;
                        menuLocation.Offset(0, itemSize.Height);
                        menu.Show(this.DataGridView, menuLocation);
                    }
                }
                return name;
            }
        }

        /// <summary>
        /// 获取表示当前实例的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string[] textArray1 = new string[] { "DataGridViewOperationCell { ColumnIndex=", base.ColumnIndex.ToString(CultureInfo.CurrentCulture), ", RowIndex=", base.RowIndex.ToString(CultureInfo.CurrentCulture), " }" };
            return string.Concat(textArray1);
        }
    }
}
