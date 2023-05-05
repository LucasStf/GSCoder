using System;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Backend
{
    class File_Create : Form
    {
        public static void AddPageTabcontrol(MainForm form, string file_name, string fileContent)
        {
            var rightPanel = MainForm.rightPanel;


            var textArea = new CustomRichTextArea
            {
                ID = "CustomRichTextArea",
                CaretIndex = fileContent.Length,
                Text = fileContent,
                //move text to the right
            };

            //textArea.CaretIndex = textArea.Text.Length;

            var panel = new Panel { Padding = new Padding(5) };


            var label = new Label
            { 
                VerticalAlignment = VerticalAlignment.Center,
                //text color
                TextColor = Color.FromArgb(76, 86, 106, 1000),
            };

            panel.Content = new StackLayout
            {
                Items =
                {
                    label,
                    //new CustomVerticalLine(),
                    //textArea
                },
                Orientation = Orientation.Horizontal,
            };

            // Create a new TableLayout with two columns, one for line numbers and one for the TextArea
            var tableLayout = new TableLayout
            {
                Rows = {
                    new TableRow {
                        Cells = {
                            // Column for line numbers
                            new TableCell {
                                Control = new TableLayout {
                                    Rows = {
                                        // Create a TableRow with a label for each line number
                                        label
                                    }
                                }
                            },
                            // Column for TextArea
                            new TableCell {
                                Control = textArea
                            }
                        }
                    }
                },
                // Set the widths of the two columns
                //ColumnWidths = { 50, -1 }
                //Padding = new Padding(10),
                Spacing = new Size(10, 10),
            };

            var ScrollableWindow = new Scrollable
            {
                Border = BorderType.None,
                Padding = new Padding(0),
                Content = tableLayout
            };

            // Create a new TabPage with the TableLayout as content
            var tabPage = new TabPage()
            {
                Text = Path.GetFileName(file_name),
                Content = ScrollableWindow
            };

            textArea.TextChanged += (sender, e) =>
            {
                var lines = utils.GetLinesNumber(textArea.Text).ToString();
                // Split the fileContent into lines
                //get textarea text
                var lineNumbers = string.Join("\n", Enumerable.Range(1, Convert.ToInt32(lines)));

                //check if the text of the label is equal to the line numbers
                if (label.Text != lineNumbers)
                {
                    // Update the line numbers
                    label.Text = lineNumbers;
                }

                // Trouver la dernière occurrence d'un espace ou d'un retour à la ligne avant la position du curseur
                int startIndex = textArea.Text.LastIndexOfAny(new char[] { ' ', '\n', '\r' }, textArea.CaretIndex - 1) + 1;

                // Extraire le texte entre la position trouvée et la position actuelle du curseur
                string currentText = textArea.Text.Substring(startIndex, textArea.CaretIndex - startIndex);

                bool isTypeValid = lexer.IsTypeValid(currentText);

                if (isTypeValid)
                {
                    textArea.Selection = new Range<int>(startIndex, textArea.CaretIndex);
                    textArea.SelectionForeground = Color.FromArgb(255, 255, 0, 0);
                }
                else
                {
                    // set the text color to white
                    textArea.TextColor = Color.FromArgb(255, 255, 255, 255);
                }
            };

            //event when the user click enter
            textArea.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Space)
                {
                    
                }
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