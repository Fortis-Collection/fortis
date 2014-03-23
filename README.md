# Fortis
##A strongly typed, interface based, model of the Sitecore API.
__Note:__ This version of Fortis is for Sitecore 6.X and has been tested on 6.5 & 6.6

Fortis is a library that forms the basis of a strongly typed model of the Sitecore API. This enables us to develop reliable, maintainable sites more efficiently than with traditional, losely typed Sitecore development.

Benefits:
* Encapsulating the Sitecore API and seperates out business logic, allowing for easier unit testing.
* Elimiates "magic strings"
* Build errors instead of run time errors
* Easily add custom functionality around the Sitecore Item class
* Simplfy regularly used Sitecore API calls
* It encapsulates the Sitecore API - so all the code is running standard Sitecore API calls, this means that all page editor functionality will still work.

## Contents

* [Get it on NuGet!](#getItOnNuget)
* [Pre-requisites](#requirements)
* [Why Use It?](#whyUseIt)
* [Getting Started](#gettingStarted)
    * [Setting up Fortis & Sitecore](#setup)
    * [Tutorial: Getting Started with Fortis](#tutorial1)
* [Code Examples](#examples)

## <a name="getItOnNuget"></a>Get it on NuGet!

    Install-Package Fortis6
    
For ASP.Net MVC appliactions use:

    Install-Package Fortis.Mvc

For ASP.Net WebForms appliactions use:

    Install-Package Fortis.WebForms

## <a name="requirements"></a>Pre-Requisites

* Sitecore 6.x (NB: Tested on 6.5 & 6.6)
* ASP.NET MVC 4.x (If using MVC)
 
## Documentation

### <a name="whyUseIt"></a>Why use it?

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

### <a name="gettingStarted"></a>Getting Started

#### <a name="setup"></a>Setting up Fortis
Fortis needs to be registered with your choice of [IoC](http://martinfowler.com/articles/injection.html) library. We have chosen [Simple Injector](https://simpleinjector.codeplex.com/) as this is a lightweight and fast container. ([IoC Benchmarks](http://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison))

The interfaces that need to be registered are:
* IItemFactory
* IContextProvider
* ISpawnProvider
* ITemplateMapProvider
* IModelAssembleyProvider

Once registered, Fortis requires a Global initialisation for use in Sitecore Pipelines.

##### Sitecore 7.x/Mvc Example Setup using _SimpleInjector_
```csharp
namespace MySitecoreWebsite
{
    public class Global : Sitecore.Web.Application
    {
        var container = new Container();
        
		// Register Fortis
		container.Register<IItemFactory, ItemFactory>();
		container.Register<IContextProvider, IContextProvider>();
		container.Register<ISpawnProvider, SpawnProvider>();
		container.Register<ITemplateMapProvider, TemplateMapProvider>();
		container.Register<IModelAssemblyProvider, ModelAssemblyProvider>();

		// Initialise fortis for pipelines and events
		Global.Initialise(
			container.GetInstance<ISpawnProvider>(),
			container.GetInstance<IItemFactory>()
			);
        
        // Register the container as MVC IDependencyResolver.
        DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
    }
}
```

#### Sitecore Configuration
To enable the Fortis presentation engine, the following configuration file should be added to the `/App_Config/Includes` folder in your project

```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"?>
    <sitecore>
        <pipelines>
            <renderField>
                <processor type="Fortis.Pipelines.RenderField.LinkRenderer, Fortis" patch:before="processor[@type='Sitecore.Pipelines.RenderField.AddBeforeAndAfterValues, Sitecore.Kernel']" />
            </renderField>
        </pipelines>

    </sitecore>
</configuration>

```

For Sitecore MVC projects add this configuration to enable View renderings to get strongly typed Fortis models.
```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"?>
    <sitecore>
    	<mvc.getModel>
			<processor patch:before="processor[@type='Sitecore.Mvc.Pipelines.Response.GetModel.GetFromItem, Sitecore.Mvc']" type="Fortis.Mvc.Pipelines.GetModel.GetFromView, Fortis.Mvc"/>
		</mvc.getModel>
    </sitecore>
</configuration>

```

#### Tutorial: Getting Started with Fortis

##### Overview
This tutorial introduces Fortis development by showing how to build a simple Sitecore website. You will install Sitecore 7.x, create some simple page templates and build a 2 page site using Fortis.

__Note:__ this tutorial assumes you have a good working knowledge of Sitecore and traditional Sitecore development. It also assumes you have installed a clean Sitecore 7.1 site and can setup the basic website project.

Sections:
* [Set up Sitecore Templates](#tutorial1_sitecore)
* [Set up the Project](#tutorial1_setup)
* 

##### <a name="tutorial1_sitecore"></a> Set up the Sitecore Templates
This section will show you how to create the Sitecore templates for use in this project.

1. Login to the Sitecore Desktop and open the Template Manager
2. Open the _User Defined_ folder and create a new Template. Inherit from the _Standard Template_. Call the template __Content Page__. Add the following fields:
    * Content Page Title: Single-Line Text
    * Content Page Body: Rich Text
    * Content Page Include in Menu: Checkbox

3. Create another new Template called __Home Page__. Inherit from the _Content Page_ template you have just created. Add the following fields:
    * Site Logo: Image
    * Site Title: Single-Line Text

4. Now open up the Content Editor and add a _Home Page_ to the default site. Under the _Home Page_ node you have just created, add 2 _Content Page_ items.

Your tree should now look like this:

* sitecore
    * Content
        * Default
            * Home Page
                * Content Page 1
                * Content Page 2


##### <a name="tutorial1_setup"></a> Create the Model
1. Add the Fortis.Mvc library to your Website project using NuGet.
2. In your Website project, add a __Model.cs__ class to the _Models_ Folder. In the Models.cs file we will add the Fortis strongly typed object models for the Sitecore Templates we have just created.

```csharp

using System;
using Sytem.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;
using Fortis.Model;
using Fortis.Model.Fields;
using Fortis.Providers;

/// <summary>
/// <para>Template interface</para>
/// <para>Template: ContentPage</para>
/// <para>ID: {4F53AD03-CAC8-48F3-BFDF-FEDDD692C4AD}</para>
/// <para>/sitecore/templates/User Defined/Content Page</para>
/// </summary>
[TemplateMapping("{4F53AD03-CAC8-48F3-BFDF-FEDDD692C4AD}", "InterfaceMap")]
public partial interface IContentPage : IItemWrapper
{
    [IndexField("Content Page Title")]
    ITextFieldWrapper ContentPageTitle { get; }
    
    [IndexField("Content Page Body")]
    IRichTextFieldWrapper ContentPageBody { get; }
    
    [IndexField("Content Page Include in Menu")]
    IBooleanFieldWrapper ContentPageIncludeInMenu { get; }
}

/// <summary>
/// <para>Template class</para><para>/sitecore/templates/User Defined/Page Types/Base Pages/ContentPage</para>
/// </summary>
[PredefinedQuery("TemplateId", ComparisonType.Equal, "{4F53AD03-CAC8-48F3-BFDF-FEDDD692C4AD}", typeof(Guid))]
[TemplateMapping("{4F53AD03-CAC8-48F3-BFDF-FEDDD692C4AD}", "")]
public partial class ContentPage : ItemWrapper, IContentPage
{
	private Item _item = null;

	public ContentPage(ISpawnProvider spawnProvider) : base(null, spawnProvider) { }

	public ContentPage(Guid id, ISpawnProvider spawnProvider) : base(id, spawnProvider) { }

	public ContentPage(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider) : base(id, lazyFields, spawnProvider) { }

	public ContentPage(Item item, ISpawnProvider spawnProvider) : base(item, spawnProvider)
	{
		_item = item;
	}
    
    [IndexField("Content Page Title")]
    public virtual ITextFieldWrapper ContentPageTitle
    { 
        get { return GetField<ListFieldWrapper>("Content Page Title", "content page title"); }
    }

    [IndexField("Content Page Body")]
    public virtual IRichTextFieldWrapper ContentPageBody
    { 
        get { return GetField<ListFieldWrapper>("Content Page Body", "content page body"); }
    }

    [IndexField("Content Page Include in Menu")]
    public virtual IBooleanFieldWrapper ContentPageIncludeInMenu
    { 
        get { return GetField<ListFieldWrapper>("Content Page Include in Menu", "content page include in menu"); }
    }

}
```

3. 


##### Breaking it Down








#### Introduction to Fortis

The base model for all your strongly typed models should be *IItemWrapper*. This base model contains all the useful base fields that would be used in a model. So this includes things like *ItemId*, *ItemName* etc...  

It also contains usefull methods to retrieve related items (Children, Parents etc...), generate the url etc...

*IItemFactory* is the main factory that encapsulates the Sitecore API to get the data from the database.

#### Example
Here is an example of getting an IGallery context item in a Controller rendering and then using that model in a view. This example assumes that all the IOC setup has already been done.

##### Controller Action
```csharp
public class GalleryController: Controller 
{
    private IItemFactory _itemFactory;
    
    public GalleryController(IItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
    }
    
    public ActionResult Index()
    {
        var contextItem = _itemFactory.GetContextItem<IGallery>();
        if (contextItem == null)
        {
            // This prevents the rendering from displaying
            return null;
        }
        return View(contextItem);
    }
}
```

##### The View 
```razor
@model MyProject.Models.UserDefined.IGallery

<section class="row">
    <div class="column c12">
        <div class="gutter">
            <h1>@Model.Title</h1>
            <h3>@Model.SubTitle</h1>
            @Model.Body
        </div>
    </div>
</section>
```

#### GetModelFromView

To be able to use a strongly typed object model in a __View Rendering__, Fortis.Mvc adds a new processor to the __mvc.getPageItem__ pipeline. This is the __GetModelFromView__ processor.

When creating your view, set the model to `Fortis.Model.RenderingModel<TPageItem, TRenderingItem>` or to `Fortis.Model.RenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem>`.

The __GetModelFromView__ processor inspects the model requested for the view and calls `IItemFactory.GetContextRenderingItems<TPageItem, TRenderingItem, TRenderingsParametersItem>()` for the view.  This will return a strongly typed object model with the following properties:

* PageItem: This is a model based on `IItemWrapper` that populates from the `Sitecore.Context.Item`.
* RenderingItem: Also a model based on `IItemWrapper`, this is populated from any DataSource set on the rendering.
* RenderingParametersItem: This is an optional model based on `IRenderingParametersWrapper`. This contains a strongly styped model of any custom rendering parameters applied to the rendering.


### Code Generation
Each model should have an interface and a corresponding concrete implementation. The interface and class should have template mappings that identify the ID of the template this object maps.

The class can also have an optional predefined query that will be read in the Sitecore 7 Search API.

Because of the number of templates in a usual Sitecore project, we can use code generation to build our Model classes. There are a number of ways to do this:
* Direct from the Sitecore database
* Sitecore Serialization
* [Team Development for Sitecore (TDS)](https://www.hhogdev.com/products/team-development-for-sitecore/overview.aspx) (our preferred choice)

We will outline the method using TDS. This product integrates with Visual Studio and serialized the Sitecore content tree down to disc. These files can then be added to your source control.

To generate the code we use the T4 code generation template files (Example .tt files are included in the package). This then uses the TransformT4.bat file to build our model. The example T4 templates use the TDS binaries to query the serialized items and build the models from those.

#### Example Model from Code Generation
##### Interface

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

##### Concrete Model
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

## <a name="examples"></a>Code Examples

### Examples Setup

In the included examples the following is assumed:
* Data Templates:
    * __Content__ inherits from _Standard Template_. This template contains main content fields (Content Title, Content Body)
    * __Navigation__ inherits from _Standard Template_. Contains navigation fields(Navigation Title, Navigation Include in Menu)
    * __Content Page__ inherits both _Content_ and _Navigation_
    * __Home Page__ inherits from _Content Page_, adds fields for Site Title, Site Logo
* Rendering Parameters
    * A set of custom rendering parameters have been created for the _Home Page_, these are named __Home Page Rendering Parameters__


### Rendering the current Context Item using a _View Rendering_
```csharp
@model Fortis.Model.RenderingModel<IContentPage, IContentPage>
<h1>
    @Model.ContentTitle
</h1>
<div>
    @Model.ContentBody
</div>

```

### Getting the rendering model in a _Controller Rendering_
Controller:
```csharp
public class ContentController
{
    private readonly IItemFactory _itemFactory;
    
    public ContentController(IItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
    }

    public ActionResult HomePage()
    {
        var renderingModel = _itemFactory.GetRenderingContextItems<
                                IHomePage, 
                                IItemWrapper, 
                                IHomePageRenderingParameters
                            >(_itemFactory);
                            
        return View(renderingModel);
    }
}
```

View:
```html
@model Fortis.Model.RenderingModel<IHomePage, IItemWrapper, IHomePageRenderingParameters>
<header>
    @Model.SiteTitle
</header>
<h1>
    @Model.ContentTitle
</h1>
<div>
    @Model.ContentBody
</div>
```

### Creating a Custom Rendering Model




* See README in _Lib\ folder
* See README in Fortis.Test\Fakes folder
