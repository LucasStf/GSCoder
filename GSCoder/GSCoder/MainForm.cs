using Eto.Drawing;
using Eto.Forms;
using GSCoder.Front;

namespace GSCoder
{
    public partial class MainForm : Form
    {
        public static Panel rightPanel;
        public static Panel leftPanel;

        public static TextArea logArea = new TextArea();

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
                Position = 50,
                Panel1 = leftPanel,
                Panel2 = rightPanel,
                FixedPanel = SplitterFixedPanel.Panel1,
                SplitterWidth = 5,
            };

            var logPanel = CreateLogPanel.CreateLogArea();
            var mainSplitter = new Splitter
            {
                Orientation = Orientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1,
                Panel1 = splitter,
                Panel2 = logPanel,
                SplitterWidth = 5,
                Position = 200
            };


            Content = mainSplitter;
            
            // create menu
            Menu = Layout_MenuBar.create_menu_bar(this);
        }
    }
}