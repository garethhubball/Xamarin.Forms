﻿namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries
{
	internal class TemplateCodeCollectionViewGridGallery : ContentPage
	{
		public TemplateCodeCollectionViewGridGallery(ItemsLayoutOrientation orientation = ItemsLayoutOrientation.Vertical)
		{
			var layout = new Grid
			{ 
				RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Star }
				}
			};

			var itemsLayout = new GridItemsLayout(2, orientation);

			var itemTemplate = new DataTemplate(() =>
			{
				var view = new Image
				{
					Source = "oasis.jpg",
					
					WidthRequest = 100,
					HeightRequest = 100
				};

				return view;
			});

			var collectionView = new CollectionView {ItemsLayout = itemsLayout, ItemTemplate = itemTemplate};

			var generator = new ItemsSourceGenerator(collectionView);
			var spanSetter = new SpanSetter(collectionView);

			layout.Children.Add(generator);
			layout.Children.Add(spanSetter);
			Grid.SetRow(spanSetter, 1);
			layout.Children.Add(collectionView);
			Grid.SetRow(collectionView, 2);

			Content = layout;

			spanSetter.UpdateSpan();
			generator.GenerateItems();
		}
	}
}