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
            this.WindowStyle = WindowStyle.Default;

            BackgroundColor = project_infos.main_color;
            Backend.Project.project.checkup();

            Title = "GSCoder";
            MinimumSize = new Size(1300, 800);

            leftPanel = Layout_left.CreateLeftPanel(this);
            rightPanel = Layout_right.CreateRightPanel();

            leftPanel.BackgroundColor = project_infos.main_color;

            var splitter = new Splitter
            {
                Position = 50,
                Panel1 = leftPanel,
                Panel2 = rightPanel,
                FixedPanel = SplitterFixedPanel.Panel1,
                SplitterWidth = 5,
            };

            var logPanel = CreateLogPanel.CreateLogArea();
            //logArea.BackgroundColor = project_infos.log_background_color;
            //logArea.TextColor = project_infos.foreground_color;

            var mainSplitter = new Splitter
            {
                Orientation = Orientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1,
                Panel1 = splitter,
                Panel2 = logPanel,
                SplitterWidth = 5,
                Position = 200,
            };

            Content = mainSplitter;
            
            // create menu
            Menu = Layout_MenuBar.create_menu_bar(this);
        }
    }
}