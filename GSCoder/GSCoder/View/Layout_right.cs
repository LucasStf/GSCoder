using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCoder.View
{
    class Layout_right : Form    
    {
        public static StackLayout create_layout_right()
        {
            var tabcontrol = new TabControl();
            var toto = new TextArea()
            {
                AcceptsTab = true,
                Size = new Size(200, 100)
            };

            var tabpage1 = new TabPage()
            {
                Content = new StackLayout
                {
                    Orientation = Orientation.Vertical,
                    Items =
                    {
                        toto
                    }

                }
            };
            tabpage1.Text = "main.gsc";

            tabcontrol.Pages.Add(tabpage1);



            var layout_right = new StackLayout()
            {
                Padding = 10,
                Orientation = Orientation.Vertical,

                Items =
                {
                    tabcontrol
                }
            };

            return layout_right;
        }
    }
}
