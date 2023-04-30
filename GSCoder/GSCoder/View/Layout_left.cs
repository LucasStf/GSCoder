using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GSCoder.View
{
    class Layout_left : Form
    {
        public static StackLayout create_layout_left()
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
