using Directories.Net;
using Eto.Drawing;
using Eto.Forms;
using System.IO;
using System.Linq;

namespace GSCoder.Backend.Project
{
    class project
    {
        public static void checkup()
        {
            UserDirectories userdirs = new UserDirectories();
            var GSCoderFolder = userdirs.DocumentDir + "\\GSCoder";
            var GSCoderFolderProjects = GSCoderFolder + "\\Projects";

            //check if the app directory exist
            try
            {
                if (!Directory.Exists(GSCoderFolder))
                {
                    Directory.CreateDirectory(GSCoderFolder);
                }
                if(!Directory.Exists(GSCoderFolderProjects))
                {
                    Directory.CreateDirectory(GSCoderFolderProjects);
                }
            }
            catch {
                MessageBox.Show("Error while creating the GSCoder folder in the user documents dir");
            }
        }

        public static StackLayout open_project(GSCoder.MainForm form)
        {
            //create the tabcontrol
            var tabcontrol = create_tabcontrol(form);

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

        public static TabControl create_tabcontrol(GSCoder.MainForm form)
        {
            string project_path = "";
            using (var dialog = new SelectFolderDialog())
            {
                DialogResult result = dialog.ShowDialog(form);
                if(result == DialogResult.Ok)
                {
                    project_path = dialog.Directory.ToString();
                }
            }

            var tabcontrol = new TabControl();
            if (project_path != "")
            {
                //get number of files in the project path
                string[] files = Directory.GetFiles(project_path);

                foreach (string file in files)
                {
                    var file_content = File.ReadAllText(file);
                    var text_area = new TextArea()
                    {
                        Text = file_content,
                        AcceptsTab = true,
                        Size = new Size(200, 100)
                    };

                    var tabpage = new TabPage()
                    {
                        Text = file,
                        Content = new StackLayout
                        {
                            Orientation = Orientation.Vertical,
                            Items =
                        {
                            text_area
                        }
                        }
                    };

                    tabcontrol.Pages.Add(tabpage);
                }
            }
            return tabcontrol;
        }
    }
}
