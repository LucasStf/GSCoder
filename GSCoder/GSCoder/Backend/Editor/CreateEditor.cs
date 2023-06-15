using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Backend
{
    class CreateEditor
    {
        public static Scrollable Create(string fileContent)
       {
            var textArea = new RichTextArea
            {
                ID = "TextArea",
                Text = fileContent,
            };

            var label = new Label
            { 
                //TextColor = project_infos.foreground_color,
                //BackgroundColor = project_infos.editor_lines_color,
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
                //BackgroundColor = project_infos.editor_lines_color,
            };

            var ScrollableWindow = new Scrollable
            {
                Border = BorderType.None,
                Padding = new Padding(0),
                Content = tableLayout
            };

            var AutoCompleteWindow = new Form
            {
                Title = "AutoComplete",
                Width = 200,
                Height = 200,
                WindowStyle = WindowStyle.None,
            };

            //Initialisé les lignes avant l'évenement TextChanged
            string lineNumbers = Lines.GetLinesNumber(textArea.Text);
            label.Text = lineNumbers;

            //syntax color
            Syntax_Color.SetSyntaxColorOpenProject(textArea);

            textArea.TextChanged += (sender, e) =>
            {
                #region Line Numbers

                var lines = Lines.GetLinesNumber(textArea.Text).ToString();

                //check if the text of the label is equal to the line numbers
                if (label.Text != lines)
                {
                    // Update the line numbers
                    label.Text = lines;
                }

                #endregion

                #region Syntax Color && AutoComplete
            
                if (textArea.Text.Length > 0)
                {
                    int startIndex = textArea.Text.LastIndexOfAny(new char[] { ' ', '\n', '\r' }, textArea.CaretIndex - 1) + 1;

                    // Extraire le texte entre la position trouvée et la position actuelle du curseur
                    string currentText = textArea.Text.Substring(startIndex, textArea.CaretIndex - startIndex);

                    Syntax_Color.SetColorCurrentText(currentText, textArea, startIndex);
                    var AutoCompleteWords = lexer.GetStringAutoCompletion(currentText);

                    //show the list of words in an windows that dont have a focus
                    if (AutoCompleteWords.Count > 0)
                    {
                        
                        AutoCompleteWindow.Content = new ListBox
                        {
                            DataStore = AutoCompleteWords,
                            Width = 150,
                            Height = 100,
                        };

                        AutoCompleteWindow.Show();

                        //get the start position of the textarea
                        var windowPosition = textArea.PointToScreen(textArea.Location);

                        //set the position of the window
                        AutoCompleteWindow.Location = new Point((int)windowPosition.X, (int)windowPosition.Y + (textArea.CaretIndex + 50));

                        //lost the focus of the window
                        AutoCompleteWindow.CanFocus = false;
                    }
                    else
                    {
                        AutoCompleteWindow.Close();
                    }
                }

                #endregion
            };

            //event when the user click enter
            textArea.KeyDown += (sender, e) =>
            {
                //if shift + enter is pressed
                if (e.Key == Keys.Q && e.Modifiers == Keys.Control)
                {
                    bool syntaxErrors = parser.CheckSyntaxErrors(textArea.Text);
                    if (syntaxErrors != true)
                    {
                        utils.WriteToLogArea("No syntax errors found", false);
                    }
                }

                //if ctrl + s is pressed
                if (e.Key == Keys.S && e.Modifiers == Keys.Control)
                {
                    MessageBox.Show("Ctrl + S");
                }
            };
    
            return ScrollableWindow;
       }
    }
}