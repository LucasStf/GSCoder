using System;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Directories.Net;

namespace GSCoder.Front
{
    class Open_project : Form
    {
        private ListBox projectListBox;
        public Open_project(MainForm form)
        {
            var userdirs = new UserDirectories();
            var projectPath = userdirs.DocumentDir + "/GSCoder/Projects";

            Title = "Choose a project";
            Padding = 10;

            // create a list with all projects
            var projects = Directory.GetDirectories(projectPath)
                .SelectMany(d => Directory.GetDirectories(d)
                    .Select(p => $"{Path.GetFileName(d)}/{Path.GetFileName(p)}"))
                .OrderBy(name => name)
                .ToList();

            projectListBox = new ListBox
            {
                Size = new Size(200, 200),
            };

            foreach (var project in projects)
            {
                projectListBox.Items.Add(project);
            }

            // buttons
            var openButton = new Button { Text = "Open the project" };
            var createButton = new Button { Text = "Create a new project" };

            // layout
            var layout = new TableLayout
            {
                Padding = new Padding(5),
                Spacing = new Size(5, 5),
                Rows =
                {
                    new TableRow(new Label { Text = "Select a project :"}, projectListBox),
                    new TableRow(createButton, openButton)
                }
            };

            // events
            openButton.Click += async (sender, e) =>
            {
                if (projectListBox.SelectedIndex != -1)
                {
                    var project = projectListBox.Items[projectListBox.SelectedIndex].Text;
                    var projectPath = userdirs.DocumentDir + "/GSCoder/Projects/" + project;
                    await Backend.Project.project.OpenProject(form, projectPath);
                    Close();
                }
            };

            createButton.Click += (sender, e) =>
            {
                var CreateProject = new Create_project(form);
                CreateProject.Show();
                Close();
            };

            // add layout to the form
            Content = layout;
        }
    }
}