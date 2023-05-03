﻿using Eto.Drawing;
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

        public Create_project()
        {
            Title = "Nouveau projet";

            nomTextBox = new TextBox();
            jeuComboBox = new ComboBox()
            {
                Items =
                {
                    "T6",
                },
                SelectedIndex = 0,
            };

            creerButton = new Button { Text = "Créer" };

            Content = new TableLayout
            {
                Spacing = new Size(5, 5),
                Padding = new Padding(10),
                Rows =
                {
                    new TableRow(new Label { Text = "Nom du projet : " }, nomTextBox),
                    new TableRow(new Label { Text = "Jeu : " }, jeuComboBox),
                    new TableRow(null, creerButton),
                }
            };

            creerButton.Click += (sender, e) =>
            {
                Backend.Project.project.CreateProject(this);
            };
        }
    }
}