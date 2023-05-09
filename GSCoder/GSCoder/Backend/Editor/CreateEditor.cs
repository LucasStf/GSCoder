using System;
using System.Collections.Generic;
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
                Text = fileContent,
            };

            var label = new Label
            { 
                TextColor = project_infos.foreground_color,
                BackgroundColor = project_infos.main_color,
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
                BackgroundColor = project_infos.main_color,
            };

            var ScrollableWindow = new Scrollable
            {
                Border = BorderType.None,
                Padding = new Padding(0),
                Content = tableLayout
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

                #region Syntax Color
            
                // Trouver la dernière occurrence d'un espace ou d'un retour à la ligne avant la position du curseur
                int startIndex = textArea.Text.LastIndexOfAny(new char[] { ' ', '\n', '\r' }, textArea.CaretIndex - 1) + 1;

                // Extraire le texte entre la position trouvée et la position actuelle du curseur
                string currentText = textArea.Text.Substring(startIndex, textArea.CaretIndex - startIndex);

                var token = lexer.GetTokenType(currentText);

                var color = Syntax_Color.GetSyntaxColor(token);


                // set the text color to white
                textArea.TextColor = project_infos.foreground_color;

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
                //if shift + enter is pressed
                if (e.Key == Keys.Q && e.Modifiers == Keys.Control)
                {
                    List<string> ParsedCode = parser.GetParsedCode(textArea.Text);
                    List<lexer.Tokens> TokensList = new List<lexer.Tokens>();

                    foreach (string token in ParsedCode)
                    {
                        TokensList.Add(lexer.GetToken(token));
                    }

                    /*foreach (lexer.Tokens token in TokensList)
                    {
                        MessageBox.Show(token.ToString());
                    }*/

                    if (TokensList.Count > 0)
                    {
                        bool syntaxErrors = parser.CheckSyntaxErrors(TokensList);
                        if (syntaxErrors == false)
                        {
                            utils.WriteToLogArea("No syntax errors found", false);
                        }
                        MessageBox.Show("Syntax checked !");
                    }
                    else
                    {
                        MessageBox.Show("No tokens...");
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