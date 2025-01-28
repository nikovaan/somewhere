#include "sfml_view.h"

int main()
{
	SFMLView view("SFML Window", 800, 600);

	#if defined(TEXTURE_LOAD_WAY_1)
	// loading either like this)
	auto texture = view.loadTextures({
		"assets/textures/sand.png", "assets/textures/grass.png", "assets/textures/wall.png", "assets/textures/player.png"
		});
	#else
	// or like this
	std::vector<std::string> filenames = {
		"assets/textures/sand.png", "assets/textures/grass.png", "assets/textures/wall.png", "assets/textures/player.png"
	};
	auto textures = view.loadTextures(filenames);
	#endif
	// TODO look into this some time
	float rotation = 0.0f; // radians

	while (view.isOpen())
	{
		// TODO update here
		view.beginFrame();
		view.clearScreen(0xff, 0x00, 0xff);
		sf::Vector2f position{ 2, 5 };
		sf::Vector2f size = { 2, 2 };
		const sf::Texture& tex = *textures[3]; // player texture
		view.renderSprite(position, size, rotation, tex);
		view.renderSprite({4, 4}, {1, 1}, 0.0f, *textures[2]);
		view.endFrame();
		rotation = rotation + 0.001f;
	}
}