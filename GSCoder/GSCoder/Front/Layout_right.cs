using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCoder.Front
{
    class Layout_right : Form    
    {
        public static StackLayout create_layout_right(GSCoder.MainForm form)
        {
            // create layout_right
            var layout_right = new StackLayout
            {
                ID = "layout_right",
                Padding = new Padding(10),
                //Spacing = new Size(5, 5),
                Orientation = Orientation.Vertical
            };

            return layout_right;
        }
    }
}