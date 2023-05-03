using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCoder.Front
{
    public class Create_File : Form
    {
        public TextBox FileName;
        public Button CreateButton;

        public Create_File()
        {
            Title = "New file";

            FileName = new TextBox();
            

            CreateButton = new Button { Text = "Create" };

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
                
            };
        }
    }
}
