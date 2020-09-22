using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Financeiro.Scripts.Managers {

    public static class ErrorReporter {

        /// <summary>
        /// Method called before the error is reported/shown, and returns wheter the report should continue of not.
        /// </summary>
        public static Func<Exception, bool> OnBeforeReport;

        /// <summary>
        /// The method that implements the actual functionally to show or report the error.
        /// </summary>
        /// <param name="exception">The error.</param>
        /// <param name="message">The header of the error.</param>
        public static Action<Exception, string> ShowMessage;

        /// <summary>
        /// Method to run after the error is reported. This method runs on another thread.
        /// </summary>
        public static Action<Exception> OnAfterReport;

        /// <summary>
        /// Reports the errors that occurs on the application.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="title"></param>
        /// <param name="show_message"></param>
        public static void Report(Exception ex, string title, bool show_message = true) {
            if (!OnBeforeReport?.Invoke(ex) ?? false) return;
            if (show_message) ShowMessage?.Invoke(ex, title);
            var t = new Thread(() => {
                OnAfterReport?.Invoke(ex);
            });
            t.Start();
        }

    }
}
