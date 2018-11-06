﻿namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries
{
	internal class TextCodeCollectionViewGallery : ContentPage
	{
		public TextCodeCollectionViewGallery(IItemsLayout itemsLayout)
		{
			var layout = new Grid
			{
				RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Star }
				}
			};

			var collectionView = new CollectionView {ItemsLayout = itemsLayout};

			var generator = new ItemsSourceGenerator(collectionView);

			layout.Children.Add(generator);
			layout.Children.Add(collectionView);
			Grid.SetRow(collectionView, 1);

			Content = layout;

			generator.GenerateItems();
		}
	}

	internal class TemplateCodeCollectionViewGallery : ContentPage
	{
		public TemplateCodeCollectionViewGallery(IItemsLayout itemsLayout)
		{
			var layout = new Grid
			{
				RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Star }
				}
			};

			var itemTemplate = new DataTemplate(() =>
			{
				var view = new Image
				{
					Source = "oasis.jpg",
					WidthRequest = 50,
					HeightRequest = 50
				};

				return view;
			});

			var collectionView = new CollectionView
			{
				ItemsLayout = itemsLayout,
				ItemTemplate = itemTemplate
			};

			var generator = new ItemsSourceGenerator(collectionView, initialItems: 2);

			layout.Children.Add(generator);
			layout.Children.Add(collectionView);
			Grid.SetRow(collectionView, 1);

			Content = layout;

			generator.GenerateItems();
		}
	}
}