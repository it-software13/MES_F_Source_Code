using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Control_Library.VideoCapture
{
    public partial class FrmPhotograph : Form
    {
        private FilterInfoCollection videoDevices;//所有摄像设备
        private VideoCaptureDevice videoDevice;//摄像设备
        private VideoCapabilities[] videoCapabilities;//摄像头分辨率
        public FrmPhotographResult frmPhotographResult;//摄像头分辨率
        
        public FrmPhotograph(FrmPhotographResult _frmPhotographResult)
        {
            InitializeComponent();
            frmPhotographResult = _frmPhotographResult; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);//得到机器所有接入的摄像设备
            if (videoDevices.Count != 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    cboVideo.Items.Add(device.Name);//把摄像设备添加到摄像列表中
                }
            }
            else
            {
                cboVideo.Items.Add("没有找到摄像头");

                btnConnect.Enabled = false;
                btnCut.Enabled = false;
                btnPic.Enabled = false;

                frmPhotographResult.IsSuccess = false;
                frmPhotographResult.ErrorMsg = "No camera found";
                this.Close();
                return;
            }
            cboVideo.SelectedIndex = 0;//默认选择第一个

        }

        private void cboVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoDevices.Count != 0)
            {
                //获取摄像头
                videoDevice = new VideoCaptureDevice(videoDevices[cboVideo.SelectedIndex].MonikerString);
                GetDeviceResolution(videoDevice);//获得摄像头的分辨率
            }
        }
        //获得摄像头的分辨率
        private void GetDeviceResolution(VideoCaptureDevice videoCaptureDevice)
        {
            cboResolution.Items.Clear();//清空列表
            videoCapabilities = videoCaptureDevice.VideoCapabilities;//设备的摄像头分辨率数组
            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                //把这个设备的所有分辨率添加到列表
                cboResolution.Items.Add($"{capabilty.FrameSize.Width} x {capabilty.FrameSize.Height}");
            }
            cboResolution.SelectedIndex = 0;//默认选择第一个
            //cboResolution.SelectedIndex = -1;//默认选择第一个  chenged
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (videoDevice != null)//如果摄像头不为空
            {
                if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                {
                    videoDevice.VideoResolution = videoCapabilities[cboResolution.SelectedIndex];//摄像头分辨率
                    vispShoot.VideoSource = videoDevice;//把摄像头赋给控件
                    vispShoot.Start();//开启摄像头
                    EnableControlStatus(false);
                }
            }
        }
        //控件的显示切换
        private void EnableControlStatus(bool status)
        {
            cboVideo.Enabled = status;
            cboResolution.Enabled = status;
            btnConnect.Enabled = status;
            btnPic.Enabled = !status;
            btnCut.Enabled = !status;
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            DisConnect();//断开连接
            EnableControlStatus(true);
        }

        private void btnPic_Click(object sender, EventArgs e)
        {
            Bitmap img = vispShoot.GetCurrentVideoFrame();//拍照

            string saveImgName = $@"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.png";
            string savePathDic = Path.Combine(Application.StartupPath, "VideoDevicesPNG");

            if (!Directory.Exists(savePathDic))
                Directory.CreateDirectory(savePathDic);

            string savePath = Path.Combine(savePathDic, saveImgName);
            img.Save(savePath);

            picbPreview.Image = img;
            frmPhotographResult.IsSuccess = true;
            frmPhotographResult.SaveImgPath = savePath;
            frmPhotographResult.SaveImgName = saveImgName;
        }

        //关闭并释放
        private void DisConnect()
        {
            if (vispShoot.VideoSource != null)
            {
                vispShoot.SignalToStop();
                vispShoot.WaitForStop();
                vispShoot.VideoSource = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisConnect();//关闭并释放
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class FrmPhotographResult
    {
        public bool IsSuccess { get; set; }
        public string SaveImgPath { get; set; }
        public string SaveImgName { get; set; }
        public string ErrorMsg { get; set; }
    }

}
