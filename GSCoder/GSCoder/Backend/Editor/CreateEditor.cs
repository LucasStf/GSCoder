using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Backend
{
    class CreateEditor
    {
        public static Scrollable Create(string fileContent)
       {
              var textArea = new CustomRichTextArea
            {
                ID = "CustomRichTextArea",
                CaretIndex = fileContent.Length,
                Text = fileContent,
                //move text to the right
            };

            var label = new Label
            { 
                VerticalAlignment = VerticalAlignment.Center,
                //text color
                TextColor = Color.FromArgb(76, 86, 106, 255),
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

                #region Syntax Color
            
                // Trouver la dernière occurrence d'un espace ou d'un retour à la ligne avant la position du curseur
                int startIndex = textArea.Text.LastIndexOfAny(new char[] { ' ', '\n', '\r' }, textArea.CaretIndex - 1) + 1;

                // Extraire le texte entre la position trouvée et la position actuelle du curseur
                string currentText = textArea.Text.Substring(startIndex, textArea.CaretIndex - startIndex);

                var token = lexer.GetTokenType(currentText);

                var color = Syntax_Color.GetSyntaxColor(token);


                // set the text color to white
                textArea.TextColor = Color.FromArgb(255, 255, 255, 255);

                if(token != lexer.TokenTypes.Unknown)
                {
                    //set the selection on the current text
                    textArea.Selection = new Range<int>(startIndex, startIndex + currentText.Length -1);

                    textArea.SelectionForeground = color;

                    // set the cursor position to the end of the currenttext
                    textArea.CaretIndex = currentText.Length + startIndex;
                }
                
                #endregion

            };

            //event when the user click enter
            textArea.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Space)
                {
                    
                }
            };
    
              return ScrollableWindow;
       }
    }
}