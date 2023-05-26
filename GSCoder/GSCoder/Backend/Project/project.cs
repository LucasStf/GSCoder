using Directories.Net;
using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GSCoder.Backend.Project
{
    class project
    {
        public static void checkup()
        {
            UserDirectories userdirs = new UserDirectories();
            var GSCoderFolder = userdirs.DocumentDir + "/GSCoder";
            var GSCoderFolderProjects = GSCoderFolder + "/Projects";

            //check if the app directory exist
            try
            {
                if (!Directory.Exists(GSCoderFolder))
                {
                    MessageBox.Show("GSCoder folder not found, creating it...");
                    Directory.CreateDirectory(GSCoderFolder);
                }
                if(!Directory.Exists(GSCoderFolderProjects))
                {
                    MessageBox.Show("GSCoder Projects folder not found, creating it...");
                    Directory.CreateDirectory(GSCoderFolderProjects);
                }
            }
            catch {
                MessageBox.Show("Error while creating the GSCoder folder in the user documents dir");
            }
        }

        public static async Task OpenProject(MainForm form, string projectPath)
        {
            var leftPanel = MainForm.leftPanel;
            var rightPanel = MainForm.rightPanel;

            // Create a new TabControl
            var tabControl = new TabControl();

            //get the tabcontrol with the id "tabControl"
            var tabControl_rightp = (TabControl)rightPanel.FindChild("tabControl");
            tabControl_rightp.Pages.Clear();

            // Get all the files in the project folder
            string[] files = Directory.GetFiles(projectPath);

            // Loop through the files and create a new tab page with a TextArea for each file
            foreach (string file in files)
            {
                //if the file is a .gsc file
                if (Path.GetExtension(file) == ".gsc")
                {
                    // Read the content of the file
                    string fileContent = await File.ReadAllTextAsync(file);

                    //Add the file to the treeview
                    File_Create.AddItemToTreeGrid(form, Path.GetFileNameWithoutExtension(file), Path.GetExtension(file), MainForm.treeGridItemCollection);

                    // Add the TabPage to the TabControl
                    File_Create.AddPageTabcontrol(form, Path.GetFileNameWithoutExtension(file), fileContent);
                }
            }

            //get the last directory of the project path
            string projectName = projectPath.Split('/').Last();
            //get the game name in the project path
            string gameName = projectPath.Split('/')[projectPath.Split('/').Length - 1];

            project_infos project_Infos = new project_infos(projectName, gameName, projectPath);
        }

        public static async void CreateProject(MainForm mainForm, Create_project create_Project_form)
        {
            UserDirectories userDirectories = new UserDirectories();
            var project_path = userDirectories.DocumentDir + "/GSCoder/Projects/" + create_Project_form.jeuComboBox.SelectedValue + "/" + create_Project_form.nomTextBox.Text;
            //check if a project already exist
            if(!Directory.Exists(project_path))
            {
                try
                {
                    //create the directory
                    Directory.CreateDirectory(project_path);

                    //create the main.gsc file
                    using (StreamWriter sw = new StreamWriter(File.Create(project_path + "/main.gsc")))
                    {
                        sw.Write(utils.code);
                    }

                    project_infos project_Infos = new project_infos(create_Project_form.nomTextBox.Text, create_Project_form.jeuComboBox.SelectedValue.ToString(), project_path);
                    
                    create_Project_form.Close();

                    //open the project
                    await OpenProject(mainForm, project_path);
                }
                catch 
                {
                    MessageBox.Show("Error while creating the project");
                }
            }
            else
            {
                MessageBox.Show("A project already exist with this name !");
            }
        }

        public static void ShowMenuPreview()
        {
            //get the tabcontrol with the id "tabControl"
            var tabControl = (TabControl)MainForm.rightPanel.FindChild("tabControl");

            //get the selected tab
            var selectedTab = tabControl.SelectedPage;

            //get the TextArea in the selected tab
            var textArea = (TextArea)selectedTab.FindChild("TextArea");

            var code = textArea.Text;

            //find the InitialisingMenu function
            var initialisingMenu = gsc_utils.GetModMenuDesign(code);

            MessageBox.Show(initialisingMenu);

            //change the drawable content
            //draw the menu in the drawable
            MainForm.drawable.Paint += (sender, e) =>
            {
                //clear the drawable
                e.Graphics.Clear(Colors.DarkGray);

                var graphics = e.Graphics;
                /*graphics.FillRectangle(Brushes.White, 120, 0, 240, 1000); // Background
                graphics.FillRectangle(Brushes.Green, 120, 60, 240, 15); // Scrollbar
                graphics.DrawLine(Pens.Green, 120, 50, 360, 50); // BorderMiddle
                graphics.DrawLine(Pens.Green, 119, 0, 119, 1000); // BorderLeft
                graphics.DrawLine(Pens.Green, 360, 0, 360, 1000); // BorderRight*/
            };
            
            //show the changes
            MainForm.drawable.Invalidate();
        }

        public static void DrawRectangleInDrawable(Drawable drawable, int x, int y, int width, int height, Color color)
        {
            drawable.Paint += (sender, e) =>
            {
                var graphics = e.Graphics;
                graphics.FillRectangle(new SolidBrush(color), x, y, width, height);
            };
        }
    }
}