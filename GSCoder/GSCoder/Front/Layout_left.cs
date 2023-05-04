using System.IO;
using Eto.Forms;

namespace GSCoder.Front
{
    class Layout_left : Form
    {
        public static StackLayout create_layout_left(GSCoder.MainForm form)
        {
            var layout = new StackLayout()
            {
                Padding = 10,
                Orientation = Orientation.Vertical,

                Items =
                {
                    "Hello World !",
                }
            };

            return layout;
        }

        public static Panel CreateLeftPanel(MainForm form)
        {
            var treeGridView = new TreeGridView()
            {
                ID = "treeGridView",
            };

            treeGridView.Columns.Add(new GridColumn
            {
                HeaderText = "File",
                DataCell = new TextBoxCell(0),
                MinWidth = 100
            });

            treeGridView.Columns.Add(new GridColumn
            {
                HeaderText = "Extension",
                DataCell = new TextBoxCell(1),
                MinWidth = 100
            });

            //create an event when the user click an item in the treegridview
            treeGridView.MouseUp += (sender, e) =>
            {
                var item = treeGridView.SelectedItem;
                if (item != null)
                {
                    //show a menu to let the user choose what to do with the file
                    var menu = new ContextMenu();
                    menu.Items.Add(new ButtonMenuItem
                    {
                        Text = "New File",
                        Command = new Command((sender2, e2) =>
                        {
                            var createFile = new Create_File(form);
                            createFile.Show();
                        })
                    });

                    menu.Items.Add(new ButtonMenuItem
                    {
                        Text = "Save File",
                        Command = new Command((sender2, e2) =>
                        {

                        })
                    });

                    menu.Items.Add(new ButtonMenuItem
                    {
                        Text = "Delete File",
                        Command = new Command((sender2, e2) =>
                        {

                        })
                    });

                    menu.Show(treeGridView, treeGridView.PointFromScreen(Mouse.Position));
                }
            };


            var panel = new Panel
            {
                ID = "panel_left",
                Content = treeGridView
            };

            return panel;
        } 
    }
}
