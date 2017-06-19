# HouaiOptions.UI

An extension package for 
[HouraiOptions](https://github.com/HouraiTeahouse/HouraiOptions) that provides
automatic UI construction for options created by HouraiOptions. 

Currently the only supported UI framework is uGUI, Unity3D's built in UI framework.

# Installation
1. Install the following dependencies:
   * [HouraiLib](https://github.com/HouraiTeahouse/HouraiLib)
   * [HouraiOptions](https://github.com/HouraiTeahouse/HouraiOptions)
2. Install the latest compatible *.unitypackage from the 
   [releases page](https://github.com/HouraiTeahouse/HouraiOptions.UI/releases)
   for the target Unity3D version.
3. (Optional): Alternatively simply load the files from the git repository
   instead of unitypackage.

# Usage

### Selecting Controls
UI controls are selected via property annotations. A default control will be used 
```csharp
// See HouraiOptioons for information about other attributes
[OptionCategory("Audio Options")]
public class SomeOptions {

    // Non-annotated options will be exposed using the default 
    [Option]
    public float OptionFloat1 { get; set; }

    // Change the drawer by providing an alternative drawer attribute
    // These alternative versions 
    [Option, Slider(0f, 1f)]
    public float OptionFloat2 { get; set; }

}
```

### Generating UI Elements
The `OptionUIBuilder` component reads the OptionSystem's singleton instance's metadata and builds a UI based on it. It does this by mapping drawer attributes to prefabs. You will need to supply it with prefabs for each control type you expect to be using.

### Creating Custom Controls
```csharp
// To create a custom control create a class that derives from AbstractOptionViewAttribute
public class CustomControl : AbstractOptionViewAttribute {

  // Implement the abstract method BuildUI. This constructs an individual 
  // Called only once to build the UI.
  // Params:
  //  optionInfo: The metadata of the option
  //  element: The instantiated 
  public override void BuildUI(OptionInfo optionInfo, GameObject element) {
    // Do something...
  }
  
}

using System.Linq;

// Alternatively derive from an existing control
//
// Note: controls derived from other controls do not always need a prefab defined,
//       and will instead fallback to prefabs for their base classes.
//       For example: ResolutionDropdown is derived from Dropdown. If no prefab for 
//       ResolutionDropdown is specified, the prefab for Dropdown will be used instead.
public class EvenOnlyDropdown : Dropdown {

  public EvenOnlyDropdown(params int[] values) {
    Options = values.Where(x => x % 2 == 0)
                    .Select(x => x.ToString())
                    .ToList();
  }

}
```

## Supported Controls

### General Controls
 * **Toggle** - A simple on/off click. Default for `bool`.
 * **IntField** - A standard text entry box for integers. Default for `int`.
 * **StringField** - A standard text entry box for strings. Default for `string`.
 * **FloatField** - A standard text entry box for floating point values. Default for `float`.
 * **Slider** - A draggable slider between two extremes. Works with `int` and `float`.
 * **Dropdown** - A dropdown to select between different values. Works with `string`.

### Specialized Controls
 * **ResolutionDropdown** - A dropdown for selecting from available screen resolutions. Works with `string`.
 * **QualityLevelDropdown** - A dropdown for selecting from available graphics quality levels. Works with `string`.
