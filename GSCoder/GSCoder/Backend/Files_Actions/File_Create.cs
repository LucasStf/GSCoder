using System.IO;
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
                Text = file_name,
                Content = CreateEditor.Create(fileContent),
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

            using (StreamWriter sw = new StreamWriter(File.Create(project_path + "/main.gsc")))
            {
                sw.Write("//" + file_name + " file");
            }
        }
    }
}