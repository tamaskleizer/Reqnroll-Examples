## Reqnroll Examples - Autofac

The test in this example fails on purpose (to show the output text).

This is a bit more complex example that demonstrates the following:

- how to import step implementations from one or more different assemblies
- how to use Reqnroll.Autofac plugin
- how to import drivers from different assemblies

### Step implementation from a separated assembly

You can extract your step implementations into a separate assembly. This can be useful if you want to reuse the same steps in different test projects.

1. Create your separate project for the steps.
2. Don't forget to decorate the steps class with the `[Binding]` attribute.
3. Reference your project / assembly in the project that has the feature files.
4. Create (and ensure it's copied to the output directory on build) the `reqnroll.json` configuration file.
5. Add the following section to the `reqnroll.json`
```json
{
    "bindingAssemblies": [
        {
            "assembly": "SeparatedFeatureSteps"
        }
    ]
}
```

### Reqnroll.Autofac

In this example we are using Reqnroll.Autofac plugin to deal with dependency injection. There is a project that implements a driver (see Reqnroll documentation for "driver pattern").

`SeparatedDriversDef` demonstrates that the test step implementation doesn't have to depend on the driver implemented in the `SeparatedDriver` project.