using System;
using System.IO;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Backend
{
    class File_Create : Form
    {
        public static void AddPageTabcontrol(MainForm form, string file_name, string fileContent)
        {
            var rightPanel = MainForm.rightPanel;

            // Create a new TabPage with the TableLayout as content
            var tabPage = new TabPage()
            {
                Content = CreateEditor.Create(fileContent),
                Text = file_name,
                //Padding = new Padding(10),
                //BackgroundColor = project_infos.main_color,
            };

            ((TabControl)rightPanel.Content).Pages.Add(tabPage);
        }




        public static void AddItemToTreeGrid(MainForm form, string file_name, string file_extension, TreeGridItemCollection treeGridItemCollection)
        {
            var leftPanel = MainForm.leftPanel;

            //get the file name without the extension
            treeGridItemCollection.Add(new TreeGridItem { Values = new object[] { file_name, file_extension } });

            ((TreeGridView)leftPanel.Content).DataStore = treeGridItemCollection;
        }

        public static void CreateFile(string project_path, string file_name)
        {
            string file_path = project_path + "/" + file_name + ".gsc";

            File.Create(file_path);
        }
    }
}