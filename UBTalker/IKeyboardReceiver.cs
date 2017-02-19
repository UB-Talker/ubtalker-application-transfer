namespace UBTalker
{
    /// <summary>
    /// Should be implemented by Views that want the result of a keyboard input
    /// after switching back from a keyboard view
    /// </summary>
    public interface IKeyboardReceiver
    {
        /// <summary>
        /// A message was typed on the keyboard
        /// </summary>
        /// <param name="message"></param>
        void OnKeyboardInput(string message);

        /// <summary>
        /// Keyboard input was cancelled
        /// </summary>
        void OnKeyboardCancel();
    }
}
