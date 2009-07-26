//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Defines the methods and properties for a taskbar button's progress bar.
    /// </summary>
    public class ProgressBar
    {
        private static ProgressBarStateSettings progressBarSettings = ProgressBarStateSettings.Instance;

        internal ProgressBar()
        {
            // Add the defaults
            AddDefaults();

            // Refresh the UI
            progressBarSettings.RefreshState(progressBarSettings.DefaultHandle, State);
            progressBarSettings.RefreshValue(progressBarSettings.DefaultHandle, CurrentValue, MaxValue);
        }

        private void AddDefaults()
        {
            if (progressBarSettings.CurrentValues.ContainsKey(progressBarSettings.DefaultHandle))
            {
                progressBarSettings.CurrentValues[progressBarSettings.DefaultHandle] = progressBarSettings.DefaultMinValue;
                progressBarSettings.MaxValues[progressBarSettings.DefaultHandle] = progressBarSettings.DefaultMaxValue;
                progressBarSettings.States[progressBarSettings.DefaultHandle] = TaskbarButtonProgressState.NoProgress;
            }
            else
            {
                progressBarSettings.CurrentValues.Add(progressBarSettings.DefaultHandle, progressBarSettings.DefaultMinValue);
                progressBarSettings.MaxValues.Add(progressBarSettings.DefaultHandle, progressBarSettings.DefaultMaxValue);
                progressBarSettings.States.Add(progressBarSettings.DefaultHandle, TaskbarButtonProgressState.NoProgress);
            }
        }

        #region Properties
        /// <summary>
        /// Gets or sets the current value of the progress bar as a value between
        /// 0 and 100.
        /// </summary>
        public int CurrentValue
        {
            get
            {
                // We defaults aren't there, then add them
                if (!progressBarSettings.CurrentValues.ContainsKey(progressBarSettings.DefaultHandle))
                    AddDefaults();

                return progressBarSettings.CurrentValues[progressBarSettings.DefaultHandle];
            }
            set
            {
                if (progressBarSettings.DefaultHandle == IntPtr.Zero)
                    throw new InvalidOperationException("ProgressBar settings cannot be set without an open Form.");

                // We defaults aren't there, then add them
                if (!progressBarSettings.CurrentValues.ContainsKey(progressBarSettings.DefaultHandle))
                    AddDefaults();

                // Update this value only if different
                if (value == progressBarSettings.CurrentValues[progressBarSettings.DefaultHandle])
                    return;

                // Check for range
                if (value > MaxValue)
                    throw new System.ArgumentOutOfRangeException("value", "CurrentValue value provided must be less than or equal to the maximum value");

                // Check for negative numbers
                if (value < 0)
                    throw new System.ArgumentOutOfRangeException("value", "CurrentValue value provided must be a positive number");

                // Set the value
                progressBarSettings.CurrentValues[progressBarSettings.DefaultHandle] = value;

                //
                progressBarSettings.RefreshValue(progressBarSettings.DefaultHandle, CurrentValue, MaxValue);
            }
        }

        /// <summary>
        /// Gets or sets the maximum value of the taskbar. The default value is 100.
        /// </summary>
        public int MaxValue
        {
            get
            {
                // We defaults aren't there, then add them
                if (!progressBarSettings.MaxValues.ContainsKey(progressBarSettings.DefaultHandle))
                    AddDefaults();

                return progressBarSettings.MaxValues[progressBarSettings.DefaultHandle];
            }
            set
            {
                if (progressBarSettings.DefaultHandle == IntPtr.Zero)
                    throw new InvalidOperationException("ProgressBar settings cannot be set without an open Form.");

                // We defaults aren't there, then add them
                if (!progressBarSettings.MaxValues.ContainsKey(progressBarSettings.DefaultHandle))
                    AddDefaults();

                // Update this value only if different
                if (value == progressBarSettings.MaxValues[progressBarSettings.DefaultHandle])
                    return;

                // Set the value
                progressBarSettings.MaxValues[progressBarSettings.DefaultHandle] = value;

                //
                progressBarSettings.RefreshValue(progressBarSettings.DefaultHandle, CurrentValue, MaxValue);
            }
        }

        /// <summary>
        /// Gets or sets the current progress bar state.
        /// </summary>
        public TaskbarButtonProgressState State
        {
            get
            {
                // We defaults aren't there, then add them
                if (!progressBarSettings.States.ContainsKey(progressBarSettings.DefaultHandle))
                    AddDefaults();

                return progressBarSettings.States[progressBarSettings.DefaultHandle];
            }
            set
            {
                if (progressBarSettings.DefaultHandle == IntPtr.Zero)
                    throw new InvalidOperationException("ProgressBar settings cannot be set without an open Form.");

                // We defaults aren't there, then add them
                if (!progressBarSettings.States.ContainsKey(progressBarSettings.DefaultHandle))
                    AddDefaults();

                // Update this value only if different
                if (value == progressBarSettings.States[progressBarSettings.DefaultHandle])
                    return;

                // Set the value
                progressBarSettings.States[progressBarSettings.DefaultHandle] = value;

                //
                progressBarSettings.RefreshState(progressBarSettings.DefaultHandle, State);
            }
        }
        #endregion

    }
}
