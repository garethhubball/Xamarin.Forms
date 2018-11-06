﻿using System;
using System.Collections.ObjectModel;

namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries
{
	internal class ItemRemover : ContentView
	{
		readonly CollectionView _cv;
		readonly Entry _entry;

		public ItemRemover(CollectionView cv)
		{
			_cv = cv;

			var layout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Fill
			};

			var button = new Button { Text = "Remove" };
			var label = new Label { Text = "Index:", VerticalTextAlignment = TextAlignment.Center };

			_entry = new Entry { Keyboard = Keyboard.Numeric, Text = "0", WidthRequest = 200 };

			layout.Children.Add(label);
			layout.Children.Add(_entry);
			layout.Children.Add(button);

			button.Clicked += RemoveItem;

			Content = layout;
		}

		void RemoveItem(object sender, EventArgs e)
		{
			if (!int.TryParse(_entry.Text, out int index))
			{
				return;
			}

			if (!(_cv.ItemsSource is ObservableCollection<TestItem> observableCollection))
			{
				return;
			}

			if (index < observableCollection.Count)
			{
				observableCollection.Remove(observableCollection[index]);
			}
		}
	}
}