using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace LaborationVFX.Components.Input
{
    public interface IInputHandler
    {
        List<ActionType> getUnhandledActions();
    }

    class InputHandler : GameComponent, IInputHandler
    {

        Dictionary<Keys, UserAction> _keyboardActionMappings = new Dictionary<Keys, UserAction>();
        Dictionary<Keys, UserAction> _keyboardActionMappingsNewKeyPress = new Dictionary<Keys, UserAction>();
        KeyboardState _keyboardLastState;

        List<ActionType> unhandledActions = new List<ActionType>();


        public InputHandler(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {

            UserAction accelerate = new UserAction(ActionType.IncreaseSpeed);
            accelerate.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction decelerate = new UserAction(ActionType.DecreaseSpeed);
            decelerate.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction rollLeft = new UserAction(ActionType.RollLeft);
            rollLeft.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction rollRight = new UserAction(ActionType.RollRight);
            rollRight.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction pitchUp = new UserAction(ActionType.PitchUp);
            pitchUp.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction pitchDown = new UserAction(ActionType.PitchDown);
            pitchDown.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction yawLeft = new UserAction(ActionType.YawLeft);
            yawLeft.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction yawRight = new UserAction(ActionType.YawRight);
            yawRight.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction quit = new UserAction(ActionType.Quit);
            quit.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction incSea = new UserAction(ActionType.IncreaseSealevel);
            incSea.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction decSea = new UserAction(ActionType.DecreaseSealevel);
            decSea.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction left = new UserAction(ActionType.Left);
            left.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction right = new UserAction(ActionType.Right);
            right.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction up = new UserAction(ActionType.Up);
            up.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction down = new UserAction(ActionType.Down);
            down.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);

            UserAction switchmode = new UserAction(ActionType.SwitchMode);
            switchmode.ActionPerformed += new UserAction.ActionEvent(action_ActionPerformed);


            _keyboardActionMappings.Add(Keys.R, accelerate);
            _keyboardActionMappings.Add(Keys.F, decelerate);
            _keyboardActionMappings.Add(Keys.Left, rollLeft);
            _keyboardActionMappings.Add(Keys.Right, rollRight);
            _keyboardActionMappings.Add(Keys.Down, pitchUp);
            _keyboardActionMappings.Add(Keys.Up, pitchDown);
            _keyboardActionMappings.Add(Keys.Q, yawLeft);
            _keyboardActionMappings.Add(Keys.E, yawRight);
            _keyboardActionMappings.Add(Keys.Escape, quit);
            _keyboardActionMappings.Add(Keys.PageUp, incSea);
            _keyboardActionMappings.Add(Keys.PageDown, decSea);

            _keyboardActionMappings.Add(Keys.A, left);
            _keyboardActionMappings.Add(Keys.D, right);
            _keyboardActionMappings.Add(Keys.W, up);
            _keyboardActionMappings.Add(Keys.S, down);

            _keyboardActionMappingsNewKeyPress.Add(Keys.Home, switchmode);



            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            foreach (Keys key in _keyboardActionMappings.Keys)
                if (keyboardState.IsKeyDown(key))
                    _keyboardActionMappings[key].FireEvent();

            foreach (Keys key in _keyboardActionMappingsNewKeyPress.Keys)
                if (keyboardState.IsKeyDown(key) && _keyboardLastState.IsKeyUp(key))
                    _keyboardActionMappingsNewKeyPress[key].FireEvent();


            _keyboardLastState = keyboardState;


            base.Update(gameTime);
        }

        public bool isNewKeyPress(Keys key)
        {
            KeyboardState kbs = Keyboard.GetState();

            return kbs.IsKeyDown(key) && _keyboardLastState.IsKeyUp(key);
        }

        void action_ActionPerformed(ActionType Type)
        {
            this.unhandledActions.Add(Type);

            //Trace.WriteLine("Perform the " + Type.ToString() + " action here");             
        }

        public List<ActionType> getUnhandledActions()
        {
            List<ActionType> result = this.unhandledActions;

            this.unhandledActions = new List<ActionType>();

            return result;
        }




    }
}
