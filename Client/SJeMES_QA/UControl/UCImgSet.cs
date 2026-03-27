using SJeMES_QA.FileSForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA.UControl
{
    public partial class UCImgSet : UserControl
    {
        FrmQaImgSetting pFrm;
        string img_guid;
        public UCImgSet(FrmQaImgSetting _pFrm, string _img_guid)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            pFrm = _pFrm;
            img_guid = _img_guid;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            List<string> newRes = new List<string>();
            bool main_del = false;
            foreach (var item in pFrm.image_guid_res[0].Split(','))
            {
                var item_info = item.Split(':');
                if (item_info[0].ToString() == img_guid)
                {
                    if (item_info[1] == "1")
                        main_del = true;
                    continue;
                }
                else
                {
                    newRes.Add(item);
                }
            }
            if (main_del && newRes.Count() > 0)
            {
                var item_info = newRes[0].Split(':');
                item_info[1] = "1";
                newRes[0] = string.Join(":", item_info);
            }
            pFrm.image_guid_res[0] = string.Join(",", newRes);
            pFrm.InitialImgControl();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            List<string> newRes = new List<string>();
            foreach (var item in pFrm.image_guid_res[0].Split(','))
            {
                var item_info = item.Split(':');
                if (item_info[0].ToString() == img_guid)
                {
                    item_info[1] = "1";
                }
                else
                {
                    item_info[1] = "0";
                }
                newRes.Add(string.Join(":", item_info));
            }
            pFrm.image_guid_res[0] = string.Join(",", newRes);
            pFrm.InitialImgControl();
        }
    }
}
