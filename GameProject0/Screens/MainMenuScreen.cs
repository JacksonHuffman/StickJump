using Microsoft.Xna.Framework;
using GameProject0.StateManagement;
using System.Configuration;

namespace GameProject0.Screens
{
    // The main menu screen is the first thing displayed when the game starts up.
    public class MainMenuScreen : MenuScreen
    {
        Game _game;

        private int _lives;

        public MainMenuScreen(Game game, int lives) : base("StickJump!")
        {
            _game = game;

            var playGameMenuEntry = new MenuEntry("Start");
            //var optionsMenuEntry = new MenuEntry("Controls");
            var exitMenuEntry = new MenuEntry("Exit");

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            //optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            //MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
            _lives = lives;
        }


        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen(), new CutSceneScreen());
            //ScreenManager.AddScreen(new GamePlayScreen(_game), e.PlayerIndex);
            ExitScreen();
            ScreenManager.AddScreen(new ControlsAndObjectives(_game, _lives), e.PlayerIndex);
        }

        /*
        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }
        */

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            _game.Exit();
        }


    /*
    private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
    {
        ScreenManager.Game.Exit();
    }
    */
    }
}
