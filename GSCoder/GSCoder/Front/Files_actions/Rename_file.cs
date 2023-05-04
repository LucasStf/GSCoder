using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GSCoder.Front
{
    public class Rename_file : Form
    {
        public TextBox FileName;
        public Button CreateButton;

        public Rename_file(MainForm mainForm)
        {
            Title = "Rename file";

            FileName = new TextBox();

            CreateButton = new Button { Text = "Rename" };

            Content = new TableLayout
            {
                Spacing = new Size(5, 5),
                Padding = new Padding(10),
                Rows =
                {
                    new TableRow(new Label { Text = "File name : " }, FileName),
                    new TableRow(null, CreateButton),
                }
            };

            CreateButton.Click += (sender, e) =>
            {
                try
                {
                    var selected_file = Backend.utils.GetSelectedItemTreeGridView(mainForm);
                    if(selected_file != null)
                    {
                        //rename the file
                        var file_path = Path.Combine(controllerProject.get_path(), Backend.utils.GetSelectedItemTreeGridView(mainForm) + ".gsc");
                        var new_file_path = Path.Combine(controllerProject.get_path(), FileName.Text + ".gsc");
                        File.Move(file_path, new_file_path);
                        Backend.utils.RenameItemTreeGridView(mainForm, FileName.Text);
                        Backend.utils.RenameTabPage(mainForm, FileName.Text, selected_file);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a file");
                    }
                }
                catch
                {
                    MessageBox.Show("Error while renaming the file");
                }
            };
        }
    }
}