using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCoder.Front
{
    internal class Create_project : Form
    {
        public TextBox nomTextBox;
        public ComboBox jeuComboBox;
        public Button creerButton;

        public Create_project(MainForm mainForm)
        {
            Title = "New project";

            nomTextBox = new TextBox();
            jeuComboBox = new ComboBox()
            {
                Items =
                {
                    "T6",
                },
                SelectedIndex = 0,
            };

            creerButton = new Button { Text = "Create" };

            Content = new TableLayout
            {
                Spacing = new Size(5, 5),
                Padding = new Padding(10),
                Rows =
                {
                    new TableRow(new Label { Text = "Project name : " }, nomTextBox),
                    new TableRow(new Label { Text = "Game : " }, jeuComboBox),
                    new TableRow(null, creerButton),
                }
            };

            creerButton.Click += (sender, e) =>
            {
                Backend.Project.project.CreateProject(mainForm, this);
            };
        }
    }
}
