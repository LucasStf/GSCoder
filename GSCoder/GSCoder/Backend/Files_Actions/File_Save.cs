using System;
using System.IO;
using System.Text;
using Eto.Forms;

namespace GSCoder.Backend
{
    class File_Save
    {
        public static void SaveFile(MainForm form, string projectPath, string fileName)
        {
            try
            {
                var file_name = utils.GetSelectedItemTreeGridView(form);

                var tabControl = MainForm.rightPanel.FindChild<Eto.Forms.TabControl>("tabControl");
                //get the tab page by his name
                var tabPage = tabControl.Pages;

                string text = "";

                foreach (var tab in tabPage)
                {
                    if (tab.Text == file_name)
                    {
                        //get the text in the tab page
                        var textArea = tab.FindChild<RichTextArea>("TextArea");
                        if (textArea != null)
                        {
                            text = textArea.Text;
                        }
                        else
                        {
                            throw new Exception("TextArea not found in tab page");
                        }
                    }
                }

                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(projectPath + "/" + fileName + ".gsc"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add some information to the file.
                    fs.WriteAsync(info, 0, info.Length);
                    MessageBox.Show("File saved !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving the file: " + ex.Message);
            }
        }
    }
}