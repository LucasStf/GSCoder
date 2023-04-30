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
    }
}
