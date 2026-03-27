using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library.Controls.Btn
{
    public class ActionButtonDefaultConfig
    {
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <returns></returns>
        public static ActionButton GetUpdateBtnConfig()
        {
            ActionButton actionButton = new ActionButton();

            actionButton.Name = "UPDATE";
            actionButton.Text = "UPDATE";

            Image btnImage = Properties.Resources.ic_update_24;

            Image btnImageChecked = Properties.Resources.ic_update_24;
             
            Pen penBtn = new Pen(Color.Transparent);// 按钮边框颜色 
            Pen penBtnChecked = new Pen(Color.Transparent);// 按钮边框颜色--鼠标悬浮

            actionButton.BtnImage = btnImage;

            actionButton.BtnImageChecked = btnImageChecked;

            actionButton.BtnPen = penBtn;
            actionButton.BtnPenChecked = penBtnChecked;

            return actionButton;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <returns></returns>
        public static ActionButton GetDeleteBtnConfig()
        {
            ActionButton actionButton = new ActionButton();

            actionButton.Name = "DELETE";
            actionButton.Text = "DELETE";

            Image btnImage = Properties.Resources.ic_delete_24;

            Image btnImageChecked = Properties.Resources.ic_delete_24;

            Pen penBtn = new Pen(Color.Transparent); // 按钮边框颜色

            Pen penBtnChecked = new Pen(Color.Transparent);// 按钮边框颜色--鼠标悬浮

            actionButton.BtnImage = btnImage;

            actionButton.BtnImageChecked = btnImageChecked;

            actionButton.BtnPen = penBtn;

            actionButton.BtnPenChecked = penBtnChecked;

            return actionButton;
        }

        /// <summary>
        /// 详情按钮
        /// </summary>
        /// <returns></returns>
        public static ActionButton GetDetailBtnConfig()
        {
            ActionButton actionButton = new ActionButton();

            actionButton.Name = "DETAIL";
            actionButton.Text = "DETAIL";

            Image btnImage = Properties.Resources.ic_select_24;

            Image btnImageChecked = Properties.Resources.ic_select_24;

            Pen penBtn = new Pen(Color.Transparent);// 按钮边框颜色 

            Pen penBtnChecked = new Pen(Color.Transparent);// 按钮边框颜色--鼠标悬浮

            actionButton.BtnImage = btnImage;

            actionButton.BtnImageChecked = btnImageChecked;

            actionButton.BtnPen = penBtn;

            actionButton.BtnPenChecked = penBtnChecked;

            return actionButton;
        }


        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <returns></returns>
        public static ActionButton GetPrintBtnConfig()
        {
            ActionButton actionButton = new ActionButton();

            actionButton.Name = "PRINT";
            actionButton.Text = "PRINT";

            Image btnImage = Properties.Resources.ic_print_24;

            Image btnImageChecked = Properties.Resources.ic_print_24;

            Pen penBtn = new Pen(Color.Transparent);// 按钮边框颜色 

            Pen penBtnChecked = new Pen(Color.Transparent);// 按钮边框颜色--鼠标悬浮

            actionButton.BtnImage = btnImage;

            actionButton.BtnImageChecked = btnImageChecked;

            actionButton.BtnPen = penBtn;

            actionButton.BtnPenChecked = penBtnChecked;

            return actionButton;
        }


        /// <summary>
        /// 上传文件按钮
        /// </summary>
        /// <returns></returns>
        public static ActionButton GetUploadFileBtnConfig()
        {
            ActionButton actionButton = new ActionButton();

            actionButton.Name = "UploadFile";
            actionButton.Text = "UploadFile";

            Image btnImage = Properties.Resources.ic_upload_file_24;

            Image btnImageChecked = Properties.Resources.ic_upload_file_24;

            Pen penBtn = new Pen(Color.Transparent);// 按钮边框颜色 

            Pen penBtnChecked = new Pen(Color.Transparent);// 按钮边框颜色--鼠标悬浮

            actionButton.BtnImage = btnImage;

            actionButton.BtnImageChecked = btnImageChecked;

            actionButton.BtnPen = penBtn;

            actionButton.BtnPenChecked = penBtnChecked;

            return actionButton;
        }


        /// <summary>
        /// 上传图片按钮
        /// </summary>
        /// <returns></returns>
        public static ActionButton GetUploadIMGBtnConfig()
        {
            ActionButton actionButton = new ActionButton();

            actionButton.Name = "UploadIMG";
            actionButton.Text = "UploadIMG";

            Image btnImage = Properties.Resources.ic_upload_img_24;

            Image btnImageChecked = Properties.Resources.ic_upload_img_24;

            Pen penBtn = new Pen(Color.Transparent); // 按钮边框颜色 

            Pen penBtnChecked = new Pen(Color.Transparent); // 按钮边框颜色--鼠标悬浮

            actionButton.BtnImage = btnImage;

            actionButton.BtnImageChecked = btnImageChecked;

            actionButton.BtnPen = penBtn;

            actionButton.BtnPenChecked = penBtnChecked;

            return actionButton;
        }

    }
}
