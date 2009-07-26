//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Microsoft.WindowsAPICodePack.Shell
{
    #region Property System Enumerations

    /// <summary>
    /// Indicates the format of a preoperty string.
    /// Typically use one, or a bitwise combination of 
    /// these flags to specify format. Some flags are mutually exclusive, 
    /// for example ShortTime | LongTime | HideTime, is not allowed.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "This is following the native API"), Flags]
    public enum PropertyDescriptionFormat
    {
        /// <summary>
        /// Use the format settings specified in the property's .propdesc file.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Precede the value with the property's display name. 
        /// If the hideLabelPrefix attribute of the labelInfo element 
        /// in the property's .propinfo file is set to true, then this flag is ignored.
        /// </summary>
        PrefixName = 0x1,

        /// <summary>
        /// Treat the string as a file name.
        /// </summary>
        FileName = 0x2,

        /// <summary>
        /// Byte sizes are always displayed in kilobytes (KB), regardless of size. 
        /// This allows for the clean alignment of the values in the column. 
        /// This flag only applies to properties of Integer types.
        /// </summary>
        AlwaysKB = 0x4,

        /// <summary>
        /// Reserved.
        /// </summary>
        RightToLeft = 0x8,

        /// <summary>
        /// Display time as 'hh:mm am/pm'.
        /// </summary>
        ShortTime = 0x10,

        /// <summary>
        /// Display time as 'hh:mm:ss am/pm'.
        /// </summary>
        LongTime = 0x20,

        /// <summary>
        /// Hide the time portion of datetime.
        /// </summary>
        HideTime = 64,

        /// <summary>
        /// Display date as 'MM/DD/YY'. For example, '3/21/04'.
        /// </summary>
        ShortDate = 0x80,

        /// <summary>
        /// Display date as 'DayOfWeek Month day, year'. 
        /// For example, 'Monday, March 21, 2004'.
        /// </summary>
        LongDate = 0x100,

        /// <summary>
        /// Hide the date portion of datetime.
        /// </summary>
        HideDate = 0x200,

        /// <summary>
        /// Use friendly date descriptions. For example, "Yesterday".
        /// </summary>
        RelativeDate = 0x400,

        /// <summary>
        /// Return the invitation text if formatting failed or the value was empty. 
        /// Invitation text is text displayed in a text box as a cue for the user, 
        /// such as 'Enter your name.' Formatting can fail if the data entered 
        /// is not of an expected type, such as putting alpha characters in 
        /// a phone number field.
        /// </summary>
        UseEditInvitation = 0x800,

        /// <summary>
        /// This flag requires UseEditInvitation to also be specified. When the 
        /// formatting flags are ReadOnly | UseEditInvitation and the algorithm 
        /// would have shown invitation text, a string is returned that indicates 
        /// the value is "Unknown" instead of the invitation text.
        /// </summary>
        ReadOnly = 0x1000,

        /// <summary>
        /// Do not detect reading order automatically. Useful when converting 
        /// to ANSI to omit the Unicode reading order characters.
        /// </summary>
        NoAutoReadingOrder = 0x2000,

        /// <summary>
        /// Smart display of DateTime values
        /// </summary>
        SmartDateTime = 0x4000
    }

    /// <summary>
    /// Display Types for a Property
    /// </summary>
    public enum PropertyDisplayType
    {
        /// <summary>
        /// String Display, this is the default if the property doesn't have one
        /// </summary>
        String = 0,

        /// <summary>
        /// Number Display
        /// </summary>
        Number = 1,

        /// <summary>
        /// Boolean Display
        /// </summary>
        Boolean = 2,

        /// <summary>
        /// DateTime Display
        /// </summary>
        DateTime = 3,

        /// <summary>
        /// Enumerated Display
        /// </summary>
        Enumerated = 4
    }

    /// <summary>
    /// Property Aggregation Type
    /// </summary>
    public enum PropertyAggregationType
    {
        /// <summary>
        /// Display the string "Multiple Values".
        /// </summary>
        Default = 0,

        /// <summary>
        /// Display the first value in the selection.
        /// </summary>
        First = 1,

        /// <summary>
        /// Display the sum of the selected values. This flag is never returned 
        /// for data types VT_LPWSTR, VT_BOOL, and VT_FILETIME.
        /// </summary>
        Sum = 2,

        /// <summary>
        /// Display the numerical average of the selected values. This flag 
        /// is never returned for data types VT_LPWSTR, VT_BOOL, and VT_FILETIME.
        /// </summary>
        Average = 3,

        /// <summary>
        /// Display the date range of the selected values. This flag is only 
        /// returned for values of the VT_FILETIME data type.
        /// </summary>
        DateRange = 4,

        /// <summary>
        /// Display a concatenated string of all the values. The order of 
        /// individual values in the string is undefined. The concatenated 
        /// string omits duplicate values; if a value occurs more than once, 
        /// it only appears a single time in the concatenated string.
        /// </summary>
        Union = 5,

        /// <summary>
        /// Display the highest of the selected values.
        /// </summary>
        Max = 6,

        /// <summary>
        /// Display the lowest of the selected values.
        /// </summary>
        Min = 7
    }

    /// <summary>
    /// Property Enumeration Types
    /// </summary>
    public enum PropEnumType
    {
        /// <summary>
        /// Use DisplayText and either RangeMinValue or RangeSetValue.
        /// </summary>
        DiscreteValue = 0,

        /// <summary>
        /// Use DisplayText and either RangeMinValue or RangeSetValue
        /// </summary>
        RangedValue = 1,

        /// <summary>
        /// Use DisplayText
        /// </summary>
        DefaultValue = 2,

        /// <summary>
        /// Use Value or RangeMinValue
        /// </summary>
        EndRange = 3
    };

    /// <summary>
    /// Describes how a property should be treated. 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "This is following the native API"), Flags]
    public enum PropertyColumnState : uint
    {
        /// <summary>
        /// Default value
        /// </summary>
        DefaultValue = 0x00000000,

        /// <summary>
        /// The value is displayed as a string.
        /// </summary>
        StringType = 0x00000001,

        /// <summary>
        /// The value is displayed as an integer.
        /// </summary>
        IntegerType = 0x00000002,

        /// <summary>
        /// The value is displayed as a date/time.
        /// </summary>
        DateType = 0x00000003,

        /// <summary>
        /// A mask for display type values StringType, IntegerType, and DateType.
        /// </summary>
        TypeMask = 0x0000000f,

        /// <summary>
        /// The column should be on by default in Details view.
        /// </summary>
        OnByDefault = 0x00000010,

        /// <summary>
        /// Will be slow to compute. Perform on a background thread.
        /// </summary>
        Slow = 0x00000020,

        /// <summary>
        /// Provided by a handler, not the folder.
        /// </summary>
        Extended = 0x00000040,

        /// <summary>
        /// Not displayed in the context menu, but is listed in the More... dialog.
        /// </summary>
        SecondaryUI = 0x00000080,

        /// <summary>
        /// Not displayed in the user interface (UI).
        /// </summary>
        Hidden = 0x00000100,

        /// <summary>
        /// VarCmp produces same result as IShellFolder::CompareIDs.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Perfer", Justification = "This is following the native API")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cmp", Justification = "This is following the native API")]
        PerferVarCmp = 0x00000200,

        /// <summary>
        /// PSFormatForDisplay produces same result as IShellFolder::CompareIDs.
        /// </summary>
        PreferFormatForDisplay = 0x00000400,

        /// <summary>
        /// Do not sort folders separately.
        /// </summary>
        NoSortByFolders = 0x00000800,

        /// <summary>
        /// Only displayed in the UI.
        /// </summary>
        ViewOnly = 0x00010000,

        /// <summary>
        /// Marks columns with values that should be read in a batch.
        /// </summary>
        BatchRead = 0x00020000,

        /// <summary>
        /// Grouping is disabled for this column.
        /// </summary>
        NoGroupBy = 0x00040000,

        /// <summary>
        /// Can't resize the column.
        /// </summary>
        FixedWidth = 0x00001000,

        /// <summary>
        /// The width is the same in all dots per inch (dpi)s.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DPI", Justification = "This is following the native API")]
        NoDPIScale = 0x00002000,

        /// <summary>
        /// Fixed width and height ratio.
        /// </summary>
        FixedRatio = 0x00004000,

        /// <summary>
        /// Filters out new display flags.
        /// </summary>
        DisplayMask = 0x0000F000,
    }

    /// <summary>
    /// Describes the condition type to use when displaying the property in the query builder user interface (UI).
    /// </summary>
    public enum PropertyConditionType
    {
        /// <summary>
        /// Default condition type
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates string.
        /// </summary>
        String = 1,

        /// <summary>
        /// Indicates size.
        /// </summary>
        Size = 2,

        /// <summary>
        /// Indicates date/time.
        /// </summary>
        DateTime = 3,

        /// <summary>
        /// Indicates Boolean.
        /// </summary>
        Boolean = 4,

        /// <summary>
        /// Indicates number.
        /// </summary>
        Number = 5,
    }

    /// <summary>
    /// Provides a set of flags to be used with IConditionFactory, 
    /// ICondition, and IConditionGenerator to indicate the operation
    /// </summary>
    public enum PropertyConditionOperation
    {
        /// <summary>
        /// An implicit comparison between the value of the property and the value of the constant.
        /// </summary>
        Implicit,

        /// <summary>
        /// The value of the property and the value of the constant must be equal.
        /// </summary>
        Equal,

        /// <summary>
        /// The value of the property and the value of the constant must not be equal.
        /// </summary>
        NotEqual,

        /// <summary>
        /// The value of the property must be less than the value of the constant.
        /// </summary>
        LessThan,

        /// <summary>
        /// The value of the property must be greater than the value of the constant.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// The value of the property must be less than or equal to the value of the constant.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// The value of the property must be greater than or equal to the value of the constant.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// The value of the property must begin with the value of the constant.
        /// </summary>
        ValueStartsWith,

        /// <summary>
        /// The value of the property must end with the value of the constant.
        /// </summary>
        ValueEndsWith,

        /// <summary>
        /// The value of the property must contain the value of the constant.
        /// </summary>
        ValueContains,

        /// <summary>
        /// The value of the property must not contain the value of the constant.
        /// </summary>
        ValueNotContains,

        /// <summary>
        /// The value of the property must match the value of the constant, where '?' matches any single character and '*' matches any sequence of characters.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DOS", Justification = "This is following the native API")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "WildCards", Justification = "This is following the native API")]
        DOSWildCards,

        /// <summary>
        /// The value of the property must contain a word that is the value of the constant.
        /// </summary>
        WordEqual,

        /// <summary>
        /// The value of the property must contain a word that begins with the value of the constant.
        /// </summary>
        WordStartsWith,

        /// <summary>
        /// The application is free to interpret this in any suitable way.
        /// </summary>
        ApplicationSpecific,
    }

    /// <summary>
    /// Property description grouping ranges
    /// </summary>
    public enum PropertyGroupingRange
    {
        /// <summary>
        /// Displays individual values.
        /// </summary>
        Discrete = 0,

        /// <summary>
        /// Display static alphanumeric ranges.
        /// </summary>
        Alphanumeric = 1,

        /// <summary>
        /// Display static size ranges.
        /// </summary>
        Size = 2,

        /// <summary>
        /// Display dynamically created ranges.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dymamic", Justification = "This is following the native API")]
        Dymamic = 3,

        /// <summary>
        /// Display month and year groups.
        /// </summary>
        Date = 4,

        /// <summary>
        /// Display percent groups.
        /// </summary>
        Percent = 5,

        /// <summary>
        /// Enumerated
        /// </summary>
        Enumerated = 6,
    }

    /// <summary>
    /// Indicates the particular wordings of sort offerings.
    /// <para>Note that the strings shown are English versions only. 
    /// Localized strings are used for other locales.</para>
    /// </summary>
    public enum PropertySortDescription
    {
        /// <summary>
        /// Default. "Sort going up", "Sort going down"
        /// </summary>
        General,

        /// <summary>
        /// "A on top", "Z on top"
        /// </summary>
        AToZ,

        /// <summary>
        /// "Lowest on top", "Highest on top"
        /// </summary>
        LowestToHighest,

        /// <summary>
        /// "Smallest on top", "Largest on top"
        /// </summary>
        SmallestToBiggest,

        /// <summary>
        /// "Oldest on top", "Newest on top"
        /// </summary>
        OldestToNewest,
    }

    /// <summary>
    /// These flags describe attributes of the typeInfo element in the property's .propdesc file.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "This is following the native API"), Flags]
    public enum PropertyTypeFlags : uint
    {
        /// <summary>
        /// The property uses the default values for all attributes.
        /// </summary>
        Default = 0x00000000,

        /// <summary>
        /// The property can have multiple values. These values are stored as a VT_VECTOR in the PROPVARIANT structure. This value is set by the multipleValues attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        MultipleValues = 0x00000001,

        /// <summary>
        /// This property cannot be written to. This value is set by the isInnate attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        IsInnate = 0x00000002,

        /// <summary>
        /// The property is a group heading. This value is set by the isGroup attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        IsGroup = 0x00000004,

        /// <summary>
        /// The user can group by this property. This value is set by the canGroupBy attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        CanGroupBy = 0x00000008,

        /// <summary>
        /// The user can stack by this property. This value is set by the canStackBy attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        CanStackBy = 0x00000010,

        /// <summary>
        /// This property contains a hierarchy. This value is set by the isTreeProperty attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        IsTreeProperty = 0x00000020,

        /// <summary>
        /// Include this property in any full text query that is performed. This value is set by the includeInFullTextQuery attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        IncludeInFullTextQuery = 0x00000040,

        /// <summary>
        /// This property is meant to be viewed by the user. This influences whether the property shows up in the "Choose Columns" dialog, for example. This value is set by the isViewable attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        IsViewable = 0x00000080,

        /// <summary>
        /// This property is included in the list of properties that can be queried. A queryable property must also be viewable. This influences whether the property shows up in the query builder UI. This value is set by the isQueryable attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        IsQueryable = 0x00000100,

        /// <summary>
        /// Windows Vista with Service Pack 1 (SP1) and later. Used with an innate property (that is, a value calculated from other property values) to indicate that it can be deleted. This value is used by the Remove Properties user interface (UI) to determine whether to display a check box next to an property that allows that property to be selected for removal. Note that a property that is not innate can always be purged regardless of the presence or absence of this flag.
        /// </summary>
        CanBePurged = 0x00000200,

        /// <summary>
        /// This property is owned by the system.
        /// </summary>
        IsSystemProperty = 0x80000000,

        /// <summary>
        /// A mask used to retrieve all flags.
        /// </summary>
        MaskAll = 0x800001FF,
    }

    /// <summary>
    /// These flags are associated with property names in property description list strings.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), Flags]
    public enum PropertyViewFlags : uint
    {
        /// <summary>
        /// Show by default.
        /// </summary>
        Default = 0x00000000,

        /// <summary>
        /// This property should be centered.
        /// </summary>
        CenterAlign = 0x00000001,

        /// <summary>
        /// This property should be right aligned.
        /// </summary>
        RightAlign = 0x00000002,

        /// <summary>
        /// Show this property as the beginning of the next collection of properties in the view.
        /// </summary>
        BeginNewGroup = 0x00000004,

        /// <summary>
        /// Fill the remainder of the view area with the content of this property.
        /// </summary>
        FillArea = 0x00000008,

        /// <summary>
        /// Applies to a property in a list of sorted properties, specifies "reverse sort" on that property.
        /// </summary>
        SortDescending = 0x00000010,

        /// <summary>
        /// Only show this property if it is present.
        /// </summary>
        ShowOnlyIfPresent = 0x00000020,

        /// <summary>
        /// The property should be shown by default in a view (where applicable).
        /// </summary>
        ShowByDefault = 0x00000040,

        /// <summary>
        /// The property should be shown by default in primary column selection user interface (UI).
        /// </summary>
        ShowInPrimaryList = 0x00000080,

        /// <summary>
        /// The property should be shown by default in secondary column selection UI.
        /// </summary>
        ShowInSecondaryList = 0x00000100,

        /// <summary>
        /// Hide the label if the view is normally inclined to show the label.
        /// </summary>
        HideLabel = 0x00000200,

        /// <summary>
        /// This property should not be displayed as a column in the UI.
        /// </summary>
        Hidden = 0x00000800,

        /// <summary>
        /// This property can be wrapped to the next row.
        /// </summary>
        CanWrap = 0x00001000,

        /// <summary>
        /// A mask used to retrieve all flags.
        /// </summary>
        MaskAll = 0x000003ff,
    }

    #endregion
}
