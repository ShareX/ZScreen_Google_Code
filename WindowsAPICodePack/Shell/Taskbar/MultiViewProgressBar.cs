//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents the progress bar feature on the Taskbar button for multiple windows 
    /// or window handles.
    /// </summary>
    public class MultipleViewProgressBar
    {
        private static ProgressBarStateSettings progressBarSettings = ProgressBarStateSettings.Instance;

        internal MultipleViewProgressBar()
        {

        }

        /// <summary>
        /// Sets the current value of the progress bar associated with 
        /// the given window handle (HWND).
        /// </summary>
        /// <param name="windowHandle">An <c>IntPtr</c> to the window handle for the progress bar.</param>
        /// <param name="value">The current progress bar value associated with the given window handle.</param>
        public void SetCurrentValue(IntPtr windowHandle, int value)
        {
            // Check for range
            if (progressBarSettings.MaxValues.ContainsKey(windowHandle) && value > progressBarSettings.MaxValues[windowHandle])
                throw new System.ArgumentOutOfRangeException("value", "CurrentValue value provided must be less than or equal to the maximum value");

            // Check for negative numbers
            if (value < 0)
                throw new System.ArgumentOutOfRangeException("value", "CurrentValue value provided must be a positive number");

            if (progressBarSettings.CurrentValues.ContainsKey(windowHandle))
            {
                if (progressBarSettings.CurrentValues[windowHandle] != value)
                    progressBarSettings.CurrentValues[windowHandle] = value;
            }
            else
                progressBarSettings.CurrentValues.Add(windowHandle, value);

            // Refresh value
            progressBarSettings.RefreshValue(windowHandle, GetCurrentValue(windowHandle), GetMaxValue(windowHandle));
        }

        /// <summary>
        /// Returns the current value of the progress bar associated 
        /// with the given window handle (HWND).
        /// </summary>
        /// <param name="windowHandle">An <c>IntPtr</c> to the window handle for the progress bar.</param>
        /// <returns>The current progress bar value associated with the given window handle.</returns>
        public int GetCurrentValue(IntPtr windowHandle)
        {
            return progressBarSettings.CurrentValues[windowHandle];
        }

        /// <summary>
        /// Sets the current value of the progress bar associated with 
        /// the given window.
        /// </summary>
        /// <param name="window">A reference to the <see cref="System.Windows.Window"/> for the progress bar.</param>
        /// <param name="value">The current progress bar value associated with the given window handle.</param>
        public void SetCurrentValue(Window window, int value)
        {
            // Check for null
            if (window == null)
                throw new ArgumentNullException("window");

            WindowInteropHelper helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle;

            SetCurrentValue(windowHandle, value);
        }

        /// <summary>
        /// Returns the current value of the progress bar associated 
        /// with the given window.
        /// </summary>
        /// <param name="window">A reference to the <see cref="System.Windows.Window"/> for the progress bar.</param>
        /// <returns>The current progress bar value associated with the given window.</returns>
        public int GetCurrentValue(Window window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            WindowInteropHelper helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle;

            return progressBarSettings.CurrentValues[windowHandle];
        }

        /// <summary>
        /// Sets the current value of the progress bar associated with 
        /// the given <see cref="System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="form">A reference to the <see cref="System.Windows.Forms.Form"/> for the progress bar.</param>
        /// <param name="value">The current progress bar value associated with the given form.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Need a top-level control")]
        public void SetCurrentValue(Form form, int value)
        {
            // Check for null
            if (form == null)
                throw new ArgumentNullException("form");

            IntPtr windowHandle = form.Handle;

            SetCurrentValue(windowHandle, value);
        }

        /// <summary>
        /// Gets the current value of the progress bar associated 
        /// with the given form.
        /// </summary>
        /// <param name="form">A reference to the <see cref="System.Windows.Forms.Form"/> for the progress bar.</param>
        /// <returns>The current progress bar value associated with the given form.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification="Need a top-level control")]
        public int GetCurrentValue(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            IntPtr windowHandle = form.Handle;

            return progressBarSettings.CurrentValues[windowHandle];
        }

        /// <summary>
        /// Sets the maximum value of the progress bar associated with 
        /// the given window handle (HWND).
        /// </summary>
        /// <param name="windowHandle">An <c>IntPtr</c> to the window handle for the progress bar.</param>
        /// <param name="value">The maximum progress bar value associated with the given window handle.</param>
        public void SetMaxValue(IntPtr windowHandle, int value)
        {
            if (progressBarSettings.MaxValues.ContainsKey(windowHandle))
            {
                if (progressBarSettings.MaxValues[windowHandle] != value)
                    progressBarSettings.MaxValues[windowHandle] = value;
            }
            else
                progressBarSettings.MaxValues.Add(windowHandle, value);

            // Refresh value
            progressBarSettings.RefreshValue(windowHandle, GetCurrentValue(windowHandle), GetMaxValue(windowHandle));
        }

        /// <summary>
        /// Gets the maximum value of the progress bar associated 
        /// with the given window handle (HWND).
        /// </summary>
        /// <param name="windowHandle">An <c>IntPtr</c> to the window handle for the progress bar.</param>
        /// <returns>The maximum progress bar value associated with the given window handle.</returns>
        public int GetMaxValue(IntPtr windowHandle)
        {
            if (!progressBarSettings.MaxValues.ContainsKey(windowHandle))
                progressBarSettings.MaxValues.Add(windowHandle, progressBarSettings.DefaultMaxValue);

            return progressBarSettings.MaxValues[windowHandle];
        }

        /// <summary>
        /// Sets the maximum value of the progress bar associated with 
        /// the given window.
        /// </summary>
        /// <param name="window">A reference to the <see cref="System.Windows.Window"/> for the progress bar.</param>
        /// <param name="value">The maximum progress bar value associated with the given window.</param>
        public void SetMaxValue(Window window, int value)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            WindowInteropHelper helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle;

            SetMaxValue(windowHandle, value);
        }

        /// <summary>
        /// Returns the maximum value of the progress bar associated 
        /// with the given window.
        /// </summary>
        /// <param name="window">A reference to the <see cref="System.Windows.Window"/> for the progress bar.</param>
        /// <returns>The maximum progress bar value associated with the given window.</returns>
        public int GetMaxValue(Window window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            WindowInteropHelper helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle;

            if (!progressBarSettings.MaxValues.ContainsKey(windowHandle))
                progressBarSettings.MaxValues.Add(windowHandle, progressBarSettings.DefaultMaxValue);

            return progressBarSettings.MaxValues[windowHandle];

        }

        /// <summary>
        /// Sets the maximum value of the progress bar associated with 
        /// the given <see cref="System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="form">A reference to the <see cref="System.Windows.Forms.Form"/> for the progress bar.</param>
        /// <param name="value">The maximum progress bar value associated with the given form.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Need a top-level control")]
        public void SetMaxValue(Form form, int value)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            IntPtr windowHandle = form.Handle;

            SetMaxValue(windowHandle, value);
        }

        /// <summary>
        /// Gets the maximum value of the progress bar associated 
        /// with the given form.
        /// </summary>
        /// <param name="form">A reference to the <see cref="System.Windows.Forms.Form"/> for the progress bar.</param>
        /// <returns>The maximum progress bar value associated with the given form.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Need a top-level control")]
        public int GetMaxValue(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            IntPtr windowHandle = form.Handle;

            if (!progressBarSettings.MaxValues.ContainsKey(windowHandle))
                progressBarSettings.MaxValues.Add(windowHandle, progressBarSettings.DefaultMaxValue);

            return progressBarSettings.MaxValues[windowHandle];
        }

        /// <summary>
        /// Sets the current state of the progress bar associated with 
        /// the given window handle (HWND).
        /// </summary>
        /// <param name="windowHandle">An <c>IntPtr</c> to the window handle for the progress bar.</param>
        /// <param name="state">The current progress bar state associated with the given window handle.</param>
        public void SetState(IntPtr windowHandle, TaskbarButtonProgressState state)
        {
            if (progressBarSettings.States.ContainsKey(windowHandle))
            {
                if (progressBarSettings.States[windowHandle] != state)
                    progressBarSettings.States[windowHandle] = state;
            }
            else
                progressBarSettings.States.Add(windowHandle, state);

            // Refresh state
            progressBarSettings.RefreshState(windowHandle, state);
        }

        /// <summary>
        /// Returns the current state of the progress bar associated 
        /// with the given window handle (HWND).
        /// </summary>
        /// <param name="windowHandle">An <c>IntPtr</c> to the window handle for the progress bar.</param>
        /// <returns>The current progress bar state associated with the given window handle.</returns>
        public TaskbarButtonProgressState GetState(IntPtr windowHandle)
        {
            return progressBarSettings.States[windowHandle];
        }

        /// <summary>
        /// Sets the current state of the progress bar associated with 
        /// the given window.
        /// </summary>
        /// <param name="window">A reference to the <see cref="System.Windows.Window"/> for the progress bar.</param>
        /// <param name="state">The current progress bar state associated with the given window.</param>
        public void SetState(Window window, TaskbarButtonProgressState state)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle;

            SetState(windowHandle, state);
        }

        /// <summary>
        /// Returns the current state of the progress bar associated 
        /// with the given window.
        /// </summary>
        /// <param name="window">A reference to <see cref="System.Windows.Window"/> for the progress bar.</param>
        /// <returns>The current progress bar state associated with the given window.</returns>
        public TaskbarButtonProgressState GetState(Window window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            WindowInteropHelper helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle;

            return progressBarSettings.States[windowHandle];
        }

        /// <summary>
        /// Sets the current state of the progress bar associated with 
        /// the given <see cref="System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="form">A reference to the <see cref="System.Windows.Forms.Form"/> for the progress bar.</param>
        /// <param name="state">The current progress bar state associated with the given form.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Need a top-level control")]
        public void SetState(Form form, TaskbarButtonProgressState state)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            IntPtr windowHandle = form.Handle;

            SetState(windowHandle, state);
        }

        /// <summary>
        /// Returns the current state of the progress bar associated 
        /// with the given form.
        /// </summary>
        /// <param name="form">A reference to the <see cref="System.Windows.Forms.Form"/> for the progress bar.</param>
        /// <returns>The current progress bar state associated with the given form.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Need a top-level control")]
        public TaskbarButtonProgressState GetState(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            IntPtr windowHandle = form.Handle;

            return progressBarSettings.States[windowHandle];
        }
    }
}
