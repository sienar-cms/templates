using Sienar.Layouts;

namespace SienarWeb;

public class SienarWebDefaultLayout : TopNavLayout
{
	public SienarWebDefaultLayout()
	{
		MenuNames = [SienarWebMenuNames.MainMenu];
	}
}