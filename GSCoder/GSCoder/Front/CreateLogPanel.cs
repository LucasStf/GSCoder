using Eto.Forms;
namespace GSCoder.Front
{
    class CreateLogPanel : Form
    {
        public static Splitter CreateLogArea()
        {
            var panel = new Panel
            {
                ID = "panel_log",
                Content = MainForm.logArea,
            };

            var splitter = new Splitter
            {
                Orientation = Orientation.Vertical,
                FixedPanel = SplitterFixedPanel.Panel2,
                Panel1 = MainForm.rightPanel,
                Panel2 = panel,
                SplitterWidth = 5,
                Position = 650
            };

            return splitter;
        }
    }
}