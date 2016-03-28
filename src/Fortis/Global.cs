using Fortis.Model;
using Fortis.Providers;
using Fortis.Search;
using System;
using System.Text;

namespace Fortis
{
	public static class Global
	{
		public static ISpawnProvider SpawnProvider;
		public static IItemFactory ItemFactory;
		public static IItemSearchFactory ItemSearchFactory;

		/// <summary>
		/// Initialise a default Fortis setup with a context provider and verifies the initialisation.
		/// </summary>
		/// <param name="contextProvider"></param>
		public static void Initialise(IContextProvider contextProvider)
		{
			var templateMapProvider = new TemplateMapProvider(new ModelAssemblyProvider());
			var spawnProvider = new SpawnProvider(templateMapProvider);
			var itemFactory = new ItemFactory(contextProvider, SpawnProvider);
			var itemSearchFactory = new ItemSearchFactory(templateMapProvider, new SearchResultsAdapter());

			Initialise(spawnProvider, itemFactory, itemSearchFactory);
		}

		/// <summary>
		/// Initialise Fortis with a spawn provider and item factory and verifies the initialisation.
		/// </summary>
		/// <param name="spawnProvider"></param>
		/// <param name="itemFactory"></param>
		public static void Initialise(ISpawnProvider spawnProvider, IItemFactory itemFactory, IItemSearchFactory itemSearchFactory)
		{
			SpawnProvider = spawnProvider;
			ItemFactory = itemFactory;
			ItemSearchFactory = itemSearchFactory;

			Verify(ItemFactory, SpawnProvider, ItemSearchFactory);
		}

		/// <summary>
		/// Verifies that Fortis has been correctly set up.
		/// </summary>
		[Obsolete("Use method with parameters")]
		public static void Verify()
		{
			Verify(ItemFactory, SpawnProvider, ItemSearchFactory);
		}

		/// <summary>
		/// Verifies that Fortis has been correctly set up.
		/// </summary>
		/// <param name="itemFactory"></param>
		/// <param name="spawnProvider"></param>
		/// <param name="itemSearchFactory"></param>
		public static void Verify(IItemFactory itemFactory, ISpawnProvider spawnProvider, IItemSearchFactory itemSearchFactory)
		{
			var verificationFailed = false;
			var errorMessages = new StringBuilder();

			errorMessages.AppendLine("Fortis intialise verification failed:");
			errorMessages.AppendLine();

			if (!VerifySpawnProvider(spawnProvider))
			{
				errorMessages.AppendLine("SpawnProvider is not initialised");
				verificationFailed = true;
			}

			if (!VerifyItemFactory(itemFactory))
			{
				errorMessages.AppendLine("ItemFactory is not initialised");
				verificationFailed = true;
			}

			if (!VerifyItemSearchFactory(itemSearchFactory))
			{
				errorMessages.AppendLine("ItemSearchFactory is not initialised");
				verificationFailed = true;
			}

			if (verificationFailed)
			{
				throw new Exception(errorMessages.ToString());
			}
		}

		/// <summary>
		/// Verifies the item factory is correctly set up.
		/// </summary>
		/// <returns></returns>
		[Obsolete("Use method with parameter")]
		public static bool VerifyItemFactory()
		{
			return VerifyItemFactory(ItemFactory);
		}

		/// <summary>
		/// Verifies the item factory is correctly set up.
		/// </summary>
		/// <param name="itemFactory"></param>
		/// <returns></returns>
		public static bool VerifyItemFactory(IItemFactory itemFactory)
		{
			return itemFactory != null;
		}

		/// <summary>
		/// Verifies the spawn provider is correctly set up.
		/// </summary>
		/// <returns></returns>
		[Obsolete("Use method with parameter")]
		public static bool VerifySpawnProvider()
		{
			return VerifySpawnProvider(SpawnProvider);
		}

		/// <summary>
		/// Verifies the spawn provider is correctly set up.
		/// </summary>
		/// <param name="spawnProvider"></param>
		/// <returns></returns>
		public static bool VerifySpawnProvider(ISpawnProvider spawnProvider)
		{
			return spawnProvider != null && spawnProvider.TemplateMapProvider != null;
		}

		/// <summary>
		/// Verifies the item search factory is correctly set up
		/// </summary>
		/// <returns></returns>
		public static bool VerifyItemSearchFactory(IItemSearchFactory itemSearchFactory)
		{
			return itemSearchFactory != null;
		}
	}
}
