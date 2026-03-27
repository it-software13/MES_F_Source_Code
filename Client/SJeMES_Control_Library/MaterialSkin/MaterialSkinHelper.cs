using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library.MaterialSkin
{
    public class MaterialSkinHelper
    {
        public static MaterialSkinManager MaterialSkinManagerSetDefault(MaterialSkinManager.Themes themes, MaterialSkinManager MSM,MaterialForm MF)
        {
            MSM = MaterialSkinManager.Instance;
            MSM.AddFormToManage(MF);
            MSM.Theme = themes;
            MSM.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            return MSM;
        }
    }
}
