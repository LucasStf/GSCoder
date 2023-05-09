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
                //Spacing = new Size(5, 5),
            };

            layout_right.VerticalContentAlignment = VerticalAlignment.Stretch;
            layout_right.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            //responsive layout

            return layout_right;
        }

        public static Panel CreateRightPanel()
        {
            // Créer un TabControl pour afficher les fichiers ouverts
            var tabControl = new TabControl()
            {
                ID = "tabControl",
                BackgroundColor = project_infos.main_color,
            };

            // Créer un panneau pour contenir le TabControl
            var panel = new Panel
            {
                ID = "panel_right",
                Content = tabControl
            };

            return panel;
        }
    }
}