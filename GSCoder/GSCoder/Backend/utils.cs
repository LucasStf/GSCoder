using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;

namespace GSCoder.Backend
{
    class utils
    {
        public static string GetSelectedItemTreeGridView(MainForm mainForm)
        {
            var treeGridView = MainForm.leftPanel.FindChild<Eto.Forms.TreeGridView>("treeGridView");
            var selectedItem = treeGridView.SelectedItem as Eto.Forms.TreeGridItem;

            if (selectedItem != null)
            {
                var file_name = selectedItem.Values[0].ToString();
                return file_name;
            }
            else
            {
                return null;
            }
        }

        public static void WriteToLogArea(string text, bool isError)
        {
            //clear the log area
            MainForm.logArea.Text = "";
            if (isError)
            {
                MainForm.logArea.TextColor = Colors.Red;
                MainForm.logArea.Text += text + "\n";
            }
            else
            {
                MainForm.logArea.TextColor = Colors.Green;
                MainForm.logArea.Text += text + "\n";
            }
        }


        public static string code = @"/*
                        Thanks to use the LTMT_Studio
                        LTMT_Studio :: The Best Open Source GSC IDE!
                        Project : 
                        Author : 
                        Game :
                        Description :
                        Date :
                    */

                    #include common_scripts\utility;

                    init()
                    {
                        level thread onPlayerConnect();
                    }

                    onPlayerConnect()
                    {
                        for(;;)
                        {
                            level waittill(""connected"", player);
                            player thread onPlayerSpawned();
                        }
                    }

                    onPlayerSpawned()
                    {
                        self endon(""disconnect"");
                        level endon(""game_ended"");
                        for(;;)
                        {
                            self waittill(""spawned_player"");
                            if(isDefined(self.playerSpawned))
                                continue;
                            self.playerSpawned = true;

                            self freezeControls(false);
                        }
                    }";
    }
}