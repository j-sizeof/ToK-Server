using System.Windows.Forms;

namespace ToK.GameServer.Utility
{
    /// <summary>
    /// Responsible for all kind of work related to logging events.
    /// TODO:   Think in a better approach to implement this class that don't depends on the WindowsForms components.
    ///         As a static wrapper, this class shouldn't have any dependencies on other flexible types.
    /// </summary>
    public static class CLog
    {
        private static RichTextBox m_RtbLog;

        /// <summary>
        /// Must be called before any call to any CLog's methods.
        /// </summary>
        /// <param name="rtbLog">The form's RichTextBox to show the logs in real time.</param>
        public static void Initialize(RichTextBox rtbLog)
        {
            m_RtbLog = rtbLog;
        }

        public static void Write(string txt)
        {
            m_RtbLog.Text += txt;
        }

        public static void WriteLine(string txt)
        {
            Write(txt + "\n");
        }
    }
}