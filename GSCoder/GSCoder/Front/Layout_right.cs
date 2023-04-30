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
            var layout_right = Backend.Project.project.open_project(form);
            return layout_right;
        }
    }
}