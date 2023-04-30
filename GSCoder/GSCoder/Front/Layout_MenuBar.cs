﻿using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCoder.Front
{
    internal class Layout_MenuBar : Form
    {
        public static MenuBar create_menu_bar(GSCoder.MainForm form)
        {
            // create a few commands that can be used for the menu
            var open_project = new Command { MenuText = "Open" };
            open_project.Executed += (sender, e) =>
            {
                var layout_right = Backend.Project.project.open_project(form);
                
            };

            var create_project = new Command { MenuText = "Create" };
            var save_project = new Command { MenuText = "Save" };

            var save_file = new Command { MenuText = "Save" };
            var rename_file = new Command { MenuText = "Rename" };
            var create_file = new Command { MenuText = "Create" };
            var remove_file = new Command { MenuText = "Remove" };

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            //aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);



            // create menu
            var menu = new MenuBar
            {
                Items =
                {
					// File submenu
					new SubMenuItem { Text = "&Project", Items = { open_project, create_project, save_project } },
                    new SubMenuItem { Text = "&File", Items = { save_file, rename_file, create_file, remove_file } },
                    new SubMenuItem { Text = "&Settings", Items = {  } },
                    new SubMenuItem { Text = "&About", Items = { aboutCommand } }
                },
            };

            return menu;
        }
    }
}
