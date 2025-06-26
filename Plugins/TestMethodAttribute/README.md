## Change test method Attribute

This example demonstrates how the test method attribute can be replaced or manipulated.

What else can you learn from it:
 - how to inject the `ReqnrollGeneratorPlugins` variable into the consuming project
 - how to publish your plugin into a local NuGet source (consuming plugins as NuGet packages is the recommended way)


## Contents

### TestMethodGeneratorPlugin

#### GeneratorPlugin

Demonstrates how to register a class derived from `XUnit2TestGeneratorProvider` (or your preferred test generator provider), and additional dependencies.

#### CustomTestGeneratorProvider

Demonstrates how to override the methods of the test generator provider to apply your own test attributes.

## Pitfalls

- You shouldn't reference any other packages in your plugin project, as this code will be executed during build time (in a folder different from the `$(OutputPath)`).
- One possible solution if you don't like to use strings to define your attribute names (and prefer `nameof`) is that you declare those attributes in this project.

### TestMethodGeneratorPluginTest

Contains examples and validates the plugin using reflection.