using Fortis.Application;
using Fortis.Items;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Fortis.Test.Items
{
	public class TemplateModelAssembliesTests
	{
		[Fact]
		public void Assemblies_ValidAssembly_ContainsAssembly()
		{
			var expected = GetType().Assembly;
			var mockApplicationAssemblies = Substitute.For<IApplicationAssemblies>();

			mockApplicationAssemblies.Assemblies.Returns(new List<Assembly> { expected });

			var mockTemplateModelAssembly = Substitute.For<ITemplateModelAssembly>();

			mockTemplateModelAssembly.Assembly.Returns(expected.FullName);

			var mockConfiguration = Substitute.For<ITemplateModelAssembliesConfiguration>();

			mockConfiguration.Assemblies.Returns(new List<ITemplateModelAssembly>
			{
				mockTemplateModelAssembly
			});

			var templateModelAssemblies = new TemplateModelAssemblies(mockApplicationAssemblies, mockConfiguration);
			var actual = templateModelAssemblies.Assemblies;

			Assert.Contains(expected, actual);
		}

		[Fact]
		public void Constructor_InvalidAssembly_ThrowsException()
		{
			var currentAssembly = GetType().Assembly;
			var mockApplicationAssemblies = Substitute.For<IApplicationAssemblies>();

			mockApplicationAssemblies.Assemblies.Returns(new List<Assembly> { currentAssembly });

			var mockTemplateModelAssembly = Substitute.For<ITemplateModelAssembly>();

			mockTemplateModelAssembly.Assembly.Returns("Invalid Assembly Name");

			var mockConfiguration = Substitute.For<ITemplateModelAssembliesConfiguration>();

			mockConfiguration.Assemblies.Returns(new List<ITemplateModelAssembly>
			{
				mockTemplateModelAssembly
			});

			Assert.Throws<Exception>(() => new TemplateModelAssemblies(mockApplicationAssemblies, mockConfiguration));
		}
	}
}
