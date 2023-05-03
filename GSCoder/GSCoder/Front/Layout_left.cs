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

        public static Panel CreateLeftPanel()
        {
            var treeGridView = new TreeGridView()
            {
                ID = "treeGridView",
            };

            treeGridView.Columns.Add(new GridColumn
            {
                HeaderText = "File",
                DataCell = new TextBoxCell(0)
            });

            treeGridView.Columns.Add(new GridColumn
            {
                HeaderText = "Extension",
                DataCell = new TextBoxCell(1)
            });

            var panel = new Panel
            {
                ID = "panel_left",
                Content = treeGridView
            };

            return panel;
        } 
    }
}
