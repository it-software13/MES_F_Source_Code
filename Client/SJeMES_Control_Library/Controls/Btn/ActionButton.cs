using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library.Controls.Btn
{
    public class ActionButton
    {
        private string name;

        private string text;

        // 鼠标是否移动到该按钮上
        private bool mouseOnButton = false;

        // 按钮背景图片
        private Image btnImage;

        // 按钮边框颜色【线条颜色等样式】
        private Pen btnPen;

        // 按钮背景图片-被选中
        private Image btnImageChecked;

        // 按钮边框颜色【线条颜色等样式】-被选中
        private Pen btnPenChecked;

        //废弃
        private bool isClicked;

        // 按钮的位置信息 空间位置
        private Rectangle rectangle = Rectangle.Empty;

        public bool MouseOnButton { get => mouseOnButton; set => mouseOnButton = value; }
        public Image BtnImage { get => btnImage; set => btnImage = value; }

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public Image BtnImageChecked { get => btnImageChecked; set => btnImageChecked = value; }
        public Pen BtnPen { get => btnPen; set => btnPen = value; }
        public Pen BtnPenChecked { get => btnPenChecked; set => btnPenChecked = value; }
        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }

        /// <summary>
        /// 废弃
        /// </summary>
        public bool IsClicked { get => isClicked; set => isClicked = value; }
    }
}
