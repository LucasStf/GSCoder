using Eto.Drawing;
using Eto.Forms;
using GSCoder.Backend;

namespace GSCoder.Front
{
    internal class Layout_MenuBar : Form
    {
        public static MenuBar create_menu_bar(GSCoder.MainForm form)
        {
            #region Project
            // create a few commands that can be used for the menu
            var open_project = new Command { MenuText = "Open" };
            
            open_project.Executed += (sender, e) =>
            {
                //await Backend.Project.project.OpenProject(form);
                var openProject = new Open_project(form);
                openProject.Show();
            };

            var create_project = new Command { MenuText = "Create" };
            create_project.Executed += (sender, e) =>
            {
                var CreateProject = new Create_project(form);
                CreateProject.Show();
            };

            var save_project = new Command { MenuText = "Save" };
            #endregion

            #region File
            var save_file = new Command { MenuText = "Save" };
            var rename_file = new Command { MenuText = "Rename" };
            var create_file = new Command { MenuText = "Create" };
            var remove_file = new Command { MenuText = "Remove" };
            var check_syntax = new Command { MenuText = "Check file Syntax" };
            var compile_file = new Command { MenuText = "Compile to GSC" };

            check_syntax.Executed += (sender, e) =>
            {
                var textArea = (RichTextArea)form.FindChild("TextArea");
                bool syntaxErrors = parser.CheckSyntaxErrors(textArea.Text);
                if (syntaxErrors != true)
                {
                    utils.WriteToLogArea("No syntax errors found", false);
                }
            };

            compile_file.Executed += (sender, e) =>
            {
                //var textArea = (RichTextArea)form.FindChild("TextArea");
                //bool syntaxErrors = parser.CheckSyntaxErrors(textArea.Text);
                //if (syntaxErrors != true)
                //{
                    //call the compiler
                    compiler.Compile();
                //}
            };

            #endregion

            var whiteTheme = new Command { MenuText = "White Theme" };
            //whiteTheme.Executed += (sender, e) => theme.SetWhiteTheme();

            var draculaTheme = new Command { MenuText = "Dracula Theme" };
            //draculaTheme.Executed += (sender, e) => theme.SetDraculaTheme();

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(form);

            #region MenuBar
            // create menu
            var menu = new MenuBar
            {
                Items =
                {
					// File submenu
					new SubMenuItem { Text = "&Project", Items = { open_project, create_project, save_project } },
                    new SubMenuItem { Text = "&File", Items = { save_file, rename_file, create_file, remove_file, check_syntax, compile_file } },
                    new SubMenuItem { Text = "&Settings", Items = { whiteTheme, draculaTheme } },
                    new SubMenuItem { Text = "&About", Items = { aboutCommand } }
                },
            };

            //menu.Style = "background-color: #ff0000;";

            #endregion
            return menu;
        }
    }
}