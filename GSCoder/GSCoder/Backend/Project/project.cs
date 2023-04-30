using Directories.Net;
using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;
using System.IO;
using System.Linq;
using System.Net;

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

        public static void OpenProject(MainForm form)
        {
            // Show the select folder dialog to let the user choose the project folder
            using (var dialog = new SelectFolderDialog())
            {
                DialogResult result = dialog.ShowDialog(form);
                if (result == DialogResult.Ok)
                {
                    string projectPath = dialog.Directory.ToString();

                    // Create a new TabControl
                    var tabControl = new TabControl();

                    // Get all the files in the project folder
                    string[] files = Directory.GetFiles(projectPath);

                    // Loop through the files and create a new tab page with a TextArea for each file
                    foreach (string file in files)
                    {
                        // Read the content of the file
                        string fileContent = File.ReadAllText(file);

                        // Create a new TextArea with the file content
                        var textArea = new TextArea()
                        {
                            Text = fileContent,
                            AcceptsTab = true,
                            Size = new Size(200, 100)
                        };

                        // Create a new TabPage with the TextArea as content
                        var tabPage = new TabPage()
                        {
                            Text = Path.GetFileName(file),
                            Content = new StackLayout
                            {
                                Orientation = Orientation.Vertical,
                                Items =
                                {
                                    textArea
                                }
                            }
                        };

                        // Add the TabPage to the TabControl
                        tabControl.Pages.Add(tabPage);
                    }

                    // Add the TabControl to the MainForm
                    form.FindChild<StackLayout>("layout_right").Items.Add(tabControl);
                }
            }
        }

        public static void CreateProject(Create_project form)
        {
            UserDirectories userDirectories = new UserDirectories();
            var project_path = userDirectories.DocumentDir + "\\GSCoder\\Projects\\" + form.jeuComboBox.SelectedValue + "\\" + form.nomTextBox.Text;
            //check if a project already exist
            if(!Directory.Exists(project_path))
            {
                try
                {
                    //create the directory
                    Directory.CreateDirectory(project_path);

                    //create the main.gsc file
                    File.Create(project_path + "\\main.gsc");
                    
                    form.Close();
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
    }
}
