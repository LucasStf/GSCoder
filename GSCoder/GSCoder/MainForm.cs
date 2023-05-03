using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;

namespace GSCoder
{
    public partial class MainForm : Form
    {
        public static Panel rightPanel;
        public static Panel leftPanel;

        public static TreeGridItemCollection treeGridItemCollection = new TreeGridItemCollection();

        public MainForm()
        {
            Backend.Project.project.checkup();

            Title = "GSCoder";
            MinimumSize = new Size(1300, 800);


            leftPanel = Layout_left.CreateLeftPanel(this);
            rightPanel = Layout_right.CreateRightPanel();

            var splitter = new Splitter
            {
                Position = 200,
                Panel1 = leftPanel,
                Panel2 = rightPanel,
                FixedPanel = SplitterFixedPanel.Panel1,
                SplitterWidth = 5,
                //SplitterPosition = 200
            };

            Content = splitter;
            
            // create menu
            Menu = Layout_MenuBar.create_menu_bar(this);
        }
    }
}