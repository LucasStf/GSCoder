﻿using Directories.Net;
using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;
using System;
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
            var TokenDesign = parser.GetParsedCode(initialisingMenu);

            //change the drawable content
            //draw the menu in the drawable
            MainForm.drawable.Paint += (sender, e) =>
            {
                //clear the drawable
                e.Graphics.Clear(Colors.DarkGray);

                var graphics = e.Graphics;
                bool material = false;

                for(int i = 0; i < TokenDesign.Count; i++)
                {
                    //Console.WriteLine(TokenDesign[i]);

                    if(TokenDesign[i] == "Background" || TokenDesign[i] == "Scrollbar")
                    {
                        material = true;
                    }
                    else if(TokenDesign[i] == "selfSetMaterial")
                    {
                        //Background && Scrollbar
                        var x = TokenDesign[i + 10];
                        var y = TokenDesign[i + 12];
                        var width = TokenDesign[i + 14];
                        var height = TokenDesign[i + 16];
                        var r = TokenDesign[i + 19];
                        var g = TokenDesign[i + 21];
                        var b = TokenDesign[i + 23];

                        Console.WriteLine("x: " + x + " y: " + y + " width: " + width + " height: " + height + " color: " + r + ", " + g + ", " + b);

                        Color color = Color.FromArgb(int.Parse(r), int.Parse(g), int.Parse(b), 255);

                        var startX = int.Parse(x);
                        var startY = int.Parse(y);
                        var endX = startX + int.Parse(width);
                        var endY = startY + int.Parse(height);

                        if(material)
                        {
                            graphics.FillRectangle(new SolidBrush(color), startX, startY, endX, endY);
                            material = false;
                        }
                        else
                        {
                            graphics.DrawLine(Pens.Green, startX, startY, endX, endY);
                            material = false;
                        }
                    }
                }
                //show the changes
                MainForm.drawable.Invalidate();
                //x y width height color : 120, 0, 240, 1000, (1,1,1)

                /*graphics.FillRectangle(Brushes.White, 120, 0, 240, 1000); // Background
                graphics.FillRectangle(Brushes.Green, 120, 60, 240, 15); // Scrollbar
                graphics.DrawLine(Pens.Green, 120, 50, 360, 50); // BorderMiddle
                graphics.DrawLine(Pens.Green, 119, 0, 119, 1000); // BorderLeft
                graphics.DrawLine(Pens.Green, 360, 0, 360, 1000); // BorderRight*/
            };
        }
    }
}