using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Dramatiker.Director
{
	public partial class CueControl : UserControl
	{
		public CueControl()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
