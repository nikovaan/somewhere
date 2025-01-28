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

	std::vector<std::vector<int>> groundLayer = {
		{2, 2, 2, 2, 2},
		{2, 0, 0, 0, 0},
		{2, 0, 0, 0, 0},
		{2, 0, 0, 0, 0},
		{0, 0, 0, 0, 0},
		{2, 0, 0, 0, 0},
		{2, 0, 0, 0, 0},
		{2, 0, 0, 0, 0}
	};

	std::vector<std::vector<int>> objectLayer = {
		{-1, -1, -1, -1, -1},
		{-1, -1, -1, -1, -1},
		{-1, -1, -1, -1, -1},
		{-1, -1, -1, -1, -1},
		{-1, -1, 1, -1, -1},
		{-1, -1, -1, -1, -1},
		{-1, -1, -1, -1, -1},
		{-1, -1, -1, -1, -1}
	};

	while (view.isOpen())
	{
		// TODO update loop here
		view.beginFrame();
		view.clearScreen(0xff, 0x00, 0xff);

		view.renderMap(groundLayer, textures);
		view.renderMap(objectLayer, textures);

		sf::Vector2f position{ 2, 5 };
		sf::Vector2f size = { 2, 2 };
		const sf::Texture& tex = *textures[3];
		view.renderSprite(position, size, rotation, tex);
		view.endFrame();
		rotation = rotation + 0.001f;
	}
}