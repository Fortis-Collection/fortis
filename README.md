# Fortis
##A strongly typed, interface based, model ofthe Sitecore API.

Fortis is a new library that forms the basis of a strongly typed model of the Sitecore API. This enables us to develop reliable, maintainable sites more efficiently than with traditional, losely typed Sitecore development.

Benefits:
* Encapsulating the Sitecore API and seperates out business logic, allowing for easier unit testing.
* Elimiates "magic strings"
* Build errors instead of run time errors
* Easily add custom functionality around the Sitecore Item class
* Simplfy regularly used Sitecore API calls
* It encapsulates the Sitecore API - so all the code is running standard Sitecore API calls, this means that all page editor functionality will still work.

## Contents

* [Get it on NuGet!](/#getItOnNuget)
* [Requirements](/#requirements)
* [Why Use It?](/#whyUseIt)
* [How To Use It?](/#howToUseIt)
* [Sitecore Configuration](/#sitecoreConfig)
* [Examples](/#examples)

## <a href="getItOnNuget"></a>Get it on NuGet!

    Install-Package Fortis
    
For ASP.Net MVC appliactions use:

    Install-Package Fortis.Mvc

## <a href="requirements"></a>Requirements

* Sitecore 7.x
* ASP.NET MVC 4.x
 
## Documentation

### <a href="whyUseIt"></a>Why use it?

Fortis works by having strongly typed object models of your Sitecore data templates. These models are all built using interfaces. The main benefit of using interfaces is that it allows us to get around the fact that .Net C# doesn't support multi-inheritance. This means that if your template inherits from multiple base templates in Sitecore, using interaces we can model that.

Example:
```csharp
public interface IPageTemplate: IItemWrapper, ISEOTemplate, INavigationTemplate, IContentTemplate
{
}
```

This then allows us to use the built in power of .Net to our advantage. 

1. We can check if a template is derived from a base template using the "is" keyword. E.g. 
```csharp
if (myTemplate is ISEOTemplate)
{
    // Do something
}
```
2. We can cast between interfaces easily. E.g.
```csharp
var contextItem = itemFactory.GetContextItem<IPageTemplate>();
var seoContent = (ISeoTemplate)contextItem;
```
3. We can pass objects between implementations without referencing Sitecore.
4. Using interfaces helps mitigate changes required in the presentation layer or other consumer of the model.

### <a href="howToUseIt"></a>How to Use it - *IItemWrapper*

The base model for all your strongly typed models should be *IItemWrapper*. This base model contains all the useful base fields that would be used in a model. So this includes things like *ItemId*, *ItemName* etc...  

It also contains usefull methods to retrieve related items (Children, Parents etc...)

### Code Generation


Each model should have an interface and a corresponding concrete implementation. The interface and class should have template mappings that identify the ID of the template this object maps.

The class can also have an optional predefined query that will be read in the Sitecore 7 Search API.

### Example Model


#### Interface

```csharp

/// <summary>
/// <para>Template interface</para>
/// <para>Template: My Template</para>
/// <para>ID: {0006AAA7-9379-444E-94B2-307ADC4D8835}</para>
/// <para>/sitecore/templates/UserDefined/My Template</para>
/// </summary>
[TemplateMapping("{0006AAA7-9379-444E-94B2-307ADC4D8835}", "InterfaceMap")]
public partial interface IMyTemplate: IItemWrapper
{
    ITextFieldWrapper MyTextField { get; set; }
    /*
     * Your properties here
     */
}
```

#### Concrete Model
```csharp

/// <summary>
/// <para>Template class</para>
/// <para>/sitecore/templates/UserDefined/My Template</para>
/// </summary>
[PredefinedQuery("TemplateId", ComparisonType.Equal, "{0006AAA7-9379-444E-94B2-307ADC4D8835}", typeof(Guid))]
[TemplateMapping("{0006AAA7-9379-444E-94B2-307ADC4D8835}", "")]
public partial class MyTemplate : ItemWrapper, IMyTemplate
{
	private Item _item = null;

	public MyTemplate(ISpawnProvider spawnProvider) : base(null, spawnProvider) { }

	public MyTemplate(Guid id, ISpawnProvider spawnProvider) : base(id, spawnProvider) { }

	public MyTemplate(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider) : base(id, lazyFields, spawnProvider) { }

	public MyTemplate(Item item, ISpawnProvider spawnProvider) : base(item, spawnProvider)
	{
		_item = item;
	}

    /*
     * Your properties here
     */
}

```

##### Field Types

Interface | Description
--------- | -----------
`IFieldWrapper` | The base interface for all fields
`IBooleanFieldWrapper` | 
`IDateTimeFieldWrapper` | 
`IFileFieldWrapper` | 
`IGeneralLinkFieldWrapper` | 
`IImageFieldWrapper` | for all Image fields
`IIntegerFieldWrappe` | 
`ILinkFieldWrapper` | for General Link, Link, Droptree, Droplink fields
`IListFieldWrapper` | for all Sitecore fields that have a list
`IRichTextFieldWrapper` | 
`ITextFieldWrapper` | for Single-Line Text, Multiline Text & Rich Text fields


## <a href="sitecoreConfig"></a>Sitecore Configuration
To enable the Fortis presentation engine, the following configuration file should be added to the `/App_Config/Includes` folder in your project

```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"?>
    <sitecore>
        <pipelines>
            <renderField>
                <processor type="Fortis.Pipelines.RenderField.LinkRenderer, Fortis" patch:before="processor[@type='Sitecore.Pipelines.RenderField.AddBeforeAndAfterValues, Sitecore.Kernel']" />
            </renderField>
        </pipelines>

        <controlSources>
    	    <source mode="on" namespace="LM.Lightcore.Tagging.FieldTypes" assembly="LM.Lightcore.Tagging" prefix="lightcore" />
        </controlSources>
    </sitecore>
</configuration>

```

For Sitecore MVC projects add this configuration to enable View renderings to get strongly typed Fortis models.
```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"?>
    <sitecore>
		<mvc.getModel>
			<processor patch:before="processor[@type='Sitecore.Mvc.Pipelines.Response.GetModel.GetFromItem, Sitecore.Mvc']" type="LM.Lightcore.Mvc.Pipelines.GetModel.GetFromView, LM.Lightcore.Mvc"/>
		</mvc.getModel>
    </sitecore>
</configuration>

```


## <a href="examples"></a>Code Examples







* See README in _Lib\ folder
* See README in Fortis.Test\Fakes folder
