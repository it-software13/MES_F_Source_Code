using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Controls
{
    public partial class PageControl : UserControl
    {
        //委托及事件
        public delegate void BindPage(int pageSize, int pageIndex, out int totalCount);
        public event BindPage BindPageEvent;

        //属性
        public int PageSize { get; set; } = 15;  //每页显示记录数
        public int PageIndex { get; set; }      //页序号
        public int TotalCount { get; set; }     //总记录数 
        public int PageCount { get; set; }      //总页数

        public PageControl()
        { 
            InitializeComponent();
            //取消下划线
            linkFirst.LinkBehavior = LinkBehavior.NeverUnderline;
            linkPrev.LinkBehavior = LinkBehavior.NeverUnderline;
            linkNext.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLast.LinkBehavior = LinkBehavior.NeverUnderline;
            linkGo.LinkBehavior = LinkBehavior.NeverUnderline;

            cb_size.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置页
        /// </summary>
        public void SetPage()
        {
            PageSize = Convert.ToInt32(cb_size.Text);
            //总记录数
            int totalCount = 0;
            BindPageEvent(PageSize, PageIndex + 1, out totalCount);
            TotalCount = totalCount;

            //总页数
            if (TotalCount % PageSize == 0)
                PageCount = TotalCount / PageSize;
            else
                PageCount = TotalCount / PageSize + 1;

            //当前页及总页数
            txtCurrentPage.Text = (PageIndex + 1).ToString();
            lblTotalPage.Text = "common " + PageCount.ToString() + " Page";//共//Page
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkFirst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PageIndex = 0;
                SetPage();
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkPrev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PageIndex--;
                if (PageIndex < 0)
                {
                    PageIndex = 0;
                }
                SetPage();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PageIndex++;
                if (PageIndex > PageCount - 1)
                {
                    PageIndex = PageCount - 1;
                }
                SetPage();
            }
        }

        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PageIndex = PageCount - 1;
                SetPage();
            }
        }

        /// <summary>
        /// 只能按0-9、Delete、Enter、Backspace键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 127)
            {
                e.Handled = false;
                if (e.KeyChar == 13)
                {
                    Go();
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 跳转页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkGo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Go();
            }
        }

        private void Go()
        {
            if (string.IsNullOrEmpty(txtCurrentPage.Text))
            {
                MessageBox.Show("The specified page cannot be empty. ", "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCurrentPage.Focus();
                return;
            }

            if (int.Parse(txtCurrentPage.Text) > PageCount)
            {
                MessageBox.Show("The specified page has exceeded the total number of pages. ", "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCurrentPage.Focus();
                return;
            }

            PageIndex = int.Parse(txtCurrentPage.Text) - 1;
            SetPage();
        }

        /// <summary>
        /// linkFirst鼠标移过颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkFirst_MouseMove(object sender, MouseEventArgs e)
        {
            linkFirst.LinkColor = Color.Red; 
            linkFirst.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Bold);
        }

        /// <summary>
        /// linkFirst鼠标离开颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkFirst_MouseLeave(object sender, EventArgs e)
        {
            linkFirst.LinkColor = Color.Black;
            linkFirst.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Regular);
        }

        private void linkPrev_MouseMove(object sender, MouseEventArgs e)
        {
            linkPrev.LinkColor = Color.Red;
            linkPrev.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Bold);
        }

        private void linkPrev_MouseLeave(object sender, EventArgs e)
        {
            linkPrev.LinkColor = Color.Black;
            linkPrev.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Regular);
        }

        private void linkNext_MouseMove(object sender, MouseEventArgs e)
        {
            linkNext.LinkColor = Color.Red;
            linkNext.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Bold);
        }

        private void linkNext_MouseLeave(object sender, EventArgs e)
        {
            linkNext.LinkColor = Color.Black;
            linkNext.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Regular);
        }

        private void linkLast_MouseMove(object sender, MouseEventArgs e)
        {
            linkLast.LinkColor = Color.Red;
            linkLast.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Bold);
        }

        private void linkLast_MouseLeave(object sender, EventArgs e)
        {
            linkLast.LinkColor = Color.Black;
            linkLast.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Regular);
        }

        private void linkGo_MouseMove(object sender, MouseEventArgs e)
        {
            linkGo.LinkColor = Color.Red;
            linkGo.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Bold);
        }

        private void linkGo_MouseLeave(object sender, EventArgs e)
        {
            linkGo.LinkColor = Color.Black;
            linkGo.Font = new Font(linkFirst.Font.Name, 12, FontStyle.Regular);
        }

        private void cb_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(cb_size.Text);
        }
    }
}
