using Directories.Net;
using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;
using System;
using System.IO;
using System.Linq;
using System.Net;
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

        public static async Task OpenProject(MainForm form)
        {
            // Show the select folder dialog to let the user choose the project folder
            using (var dialog = new SelectFolderDialog())
            {
                //open the dialog in the documents dir
                UserDirectories userdirs = new UserDirectories();
                dialog.Directory = userdirs.DocumentDir + "/GSCoder/Projects";

                DialogResult result = dialog.ShowDialog(form);
                if (result == DialogResult.Ok)
                {
                    string projectPath = dialog.Directory.ToString();

                    var treeGridItemCollection = new TreeGridItemCollection();

                    var leftPanel = MainForm.leftPanel;
                    var rightPanel = MainForm.rightPanel;

                    // Create a new TabControl
                    var tabControl = new TabControl();

                    ((TabControl)rightPanel.Content).Pages.Clear();

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

                            // Create a new TabPage with the TextArea as content
                            var tabPage = new TabPage()
                            {
                                Text = Path.GetFileName(file),
                                Content = new RichTextArea
                                {
                                    Text = fileContent
                                }
                            };

                            //get the file name without the extension
                            treeGridItemCollection.Add(new TreeGridItem { Values = new object[] { Path.GetFileNameWithoutExtension(file), Path.GetExtension(file) } });
                            

                            // Add the TabPage to the TabControl
                            ((TabControl)rightPanel.Content).Pages.Add(tabPage);
                        }
                    }

                    ((TreeGridView)leftPanel.Content).DataStore = treeGridItemCollection;

                    // Add the TabControl to the MainForm
                    //form.FindChild<StackLayout>("layout_right").Items.Add(tabControl);
                }
            }
        }

        public static void CreateProject(Create_project form)
        {
            UserDirectories userDirectories = new UserDirectories();
            var project_path = userDirectories.DocumentDir + "/GSCoder/Projects/" + form.jeuComboBox.SelectedValue + "/" + form.nomTextBox.Text;
            //check if a project already exist
            if(!Directory.Exists(project_path))
            {
                try
                {
                    //create the directory
                    Directory.CreateDirectory(project_path);

                    //create the main.gsc file
                    File.Create(project_path + "/main.gsc");
                    
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
