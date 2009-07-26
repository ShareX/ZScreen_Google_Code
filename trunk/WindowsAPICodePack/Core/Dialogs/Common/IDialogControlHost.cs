//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Microsoft.WindowsAPICodePack
{
    /// <summary>
    /// Indicates that the implementing class is a dialog that can host
    /// customizable dialog controls (subclasses of DialogControl).
    /// </summary>
    public interface IDialogControlHost
    {
        /// <summary>
        /// Handle notifications of pseudo-controls being added 
        /// or removed from the collection.
        /// PreFilter should throw if a control cannot 
        /// be added/removed in the dialog's current state.
        /// PostProcess should pass on changes to native control, 
        /// if appropriate.
        /// </summary>
        /// <returns></returns>
        bool IsCollectionChangeAllowed();

        /// <summary>
        /// 
        /// </summary>
        void ApplyCollectionChanged();

        /// <summary>
        /// Handle notifications of individual child 
        /// pseudo-controls' properties changing..
        /// Prefilter should throw if the property 
        /// cannot be set in the dialog's current state.
        /// PostProcess should pass on changes to native control, 
        /// if appropriate.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        bool IsControlPropertyChangeAllowed(string propertyName, DialogControl control);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="control"></param>
        void ApplyControlPropertyChange(string propertyName, DialogControl control);
    }
}
