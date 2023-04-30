using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;

namespace GSCoder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Backend.Project.project.checkup();

            Title = "GSCoder";
            MinimumSize = new Size(570, 300);
            Content = new TableLayout
            {
                Spacing = new Size(5, 5),
                Rows =
                {
                    new TableRow
                    {
                        Cells =
                        {
                            new TableCell(Layout_left.create_layout_left(this), true),
                            new TableCell(Layout_right.create_layout_right(this), true)
                        }
                    }
                }
            };
            // create menu
            Menu = Layout_MenuBar.create_menu_bar(this);
        }
    }
}