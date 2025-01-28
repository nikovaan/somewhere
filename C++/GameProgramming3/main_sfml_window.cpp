#include "sfml_view.h"

int main()
{
	SFMLView view("SFML Window", 800, 600);

	while (view.isOpen())
	{
		// TODO update here
		view.beginFrame();
		view.clearScreen(0xff, 0x00, 0xff);
		// TODO render here
		// view.renderSprite(...);
		view.endFrame();
	}
}