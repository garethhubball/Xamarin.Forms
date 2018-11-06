﻿using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.UWP
{
	// TODO hartez 2018/06/28 09:08:55 This should really be named something like FormsItemControl/FormsItemsContentControl? Not sure yet.
	// ItemContentControl right now on Android
	public class FormsContentControl : ContentControl
	{
		public FormsContentControl()
		{
			DefaultStyleKey = typeof(FormsContentControl);
		}

		public static readonly DependencyProperty FormsDataTemplateProperty = DependencyProperty.Register(
			nameof(FormsDataTemplate), typeof(DataTemplate), typeof(FormsContentControl), 
			new PropertyMetadata(default(DataTemplate), FormsDataTemplateChanged));

		static void FormsDataTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue == null)
			{
				return;
			}

			var formsContentControl = (FormsContentControl)d;
			formsContentControl.RealizeFormsDataTemplate((DataTemplate)e.NewValue);
		}

		public DataTemplate FormsDataTemplate
		{
			get => (DataTemplate)GetValue(FormsDataTemplateProperty);
			set => SetValue(FormsDataTemplateProperty, value);
		}

		public static readonly DependencyProperty FormsDataContextProperty = DependencyProperty.Register(
			nameof(FormsDataContext), typeof(object), typeof(FormsContentControl), 
			new PropertyMetadata(default(object), FormsDataContextChanged));

		static void FormsDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var formsContentControl = (FormsContentControl)d;
			formsContentControl.SetFormsDataContext(e.NewValue);
		}

		public object FormsDataContext
		{
			get => GetValue(FormsDataContextProperty);
			set => SetValue(FormsDataContextProperty, value);
		}

		VisualElement _rootElement;

		internal void RealizeFormsDataTemplate(DataTemplate template)
		{
			var content = FormsDataTemplate.CreateContent();

			if (content is VisualElement visualElement)
			{
				if (_rootElement != null)
				{
					_rootElement.MeasureInvalidated -= RootElementOnMeasureInvalidated;
				}

				_rootElement = visualElement;
				_rootElement.MeasureInvalidated += RootElementOnMeasureInvalidated;

				// TODO hartez 2018/07/24 11:22:05 Using GetOrCreate might be a waste here, since we're creating the element in this method	
				// The "Get" part of GetOrCreate is checking a value that's certain to not be set. Change this to just create the renderer
				Content = visualElement.GetOrCreateRenderer().ContainerElement;
			}

			if (FormsDataContext != null)
			{
				SetFormsDataContext(FormsDataContext);
			}
		}

		void RootElementOnMeasureInvalidated(object sender, EventArgs e)
		{
			InvalidateMeasure();
		}

		internal void SetFormsDataContext(object context)
		{
			if (_rootElement == null)
			{
				return;
			}

			BindableObject.SetInheritedBindingContext(_rootElement, context);
		}

		protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize)
		{
			if (_rootElement == null)
			{
				return base.MeasureOverride(availableSize);
			}

			Size request = _rootElement.Measure(availableSize.Width, availableSize.Height, 
				MeasureFlags.IncludeMargins).Request;

			_rootElement.Layout(new Rectangle(Point.Zero, request));

			return new Windows.Foundation.Size(request.Width, request.Height); 
		}

		protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
		{
			if (!(Content is FrameworkElement frameworkElement))
			{
				return finalSize;
			}
		
			frameworkElement.Arrange(new Rect(_rootElement.X, _rootElement.Y, _rootElement.Width, _rootElement.Height));
			return base.ArrangeOverride(finalSize);
		}
	}
}