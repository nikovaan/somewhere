///
/// \file sfml_view.h Contains window and simple 2d drawing functionality.
///

#pragma once
#include <iostream>
#include <stdexcept>
#include <string>
#include <SFML/Window.hpp>
#include <SFML/Graphics/RenderWindow.hpp>
#include <SFML/System/Vector2.hpp>
#include <SFML/Graphics/Texture.hpp>
#include <SFML/Graphics/Rect.hpp>
#include <SFML/Graphics/Sprite.hpp>

// TODO
class SFMLView
{
public:
	SFMLView(const std::string& title, int width, int height) : m_window()
	{
		m_window.create(sf::VideoMode(sf::Vector2u(width, height)), title);
	}
	~SFMLView() {}

	///
	/// \brief isOpen
	/// \return
	///
	inline bool isOpen() const
	{
		return m_window.isOpen();
	}

	///
	/// \brief beginFrame
	///
	inline void beginFrame()
	{
		while (auto ev = m_window.pollEvent())
		{
			if (ev->is<sf::Event::Closed>())
			{
				m_window.close();
			}
		}
	}

	///
	/// \brief endFrame
	///
	inline void endFrame()
	{
		m_window.display();
	}
	
	///
	/// \brief clearScreen
	/// \param r
	/// \param g
	/// \param b
	///
	inline void clearScreen(uint8_t red, uint8_t green, uint8_t blue)
	{
		m_window.clear(sf::Color(red, green, blue, 0xff));
	}
	
	///
	/// \brief loadTexture
	/// \param fileName
	/// \return
	///
	inline std::shared_ptr< sf::Texture > loadTexture(const std::string& fileName)
	{
		std::shared_ptr< sf::Texture > result = std::make_shared<sf::Texture>();
		if (result->loadFromFile(fileName))
		{
			return result;
		}
		throw std::runtime_error("Failed to load texture: " + fileName);
	}

	///
	/// \brief loadTextures
	/// \param textureFileNames
	/// \return
	///
	inline std::vector< std::shared_ptr< sf::Texture > > loadTextures(const std::vector< std::string >& textureFileNames)
	{
		std::vector< std::shared_ptr< sf::Texture > > _result;
		for (const auto& filename : textureFileNames)
		{
			_result.push_back(loadTexture(filename));
		}
		
		return _result;
	}


	///
	/// \brief renderSprite
	/// \param position
	/// \param size
	/// \param rotation
	/// \param texture
	/// \param clipStart
	/// \param clipSize
	///
	inline void renderSprite(const sf::Vector2f& position, const sf::Vector2f& size, float rotation, const sf::Texture& texture,
					  const sf::Vector2<int>& clipStart, const sf::Vector2<int>& clipSize)
	{
		sf::Sprite sprite(texture, sf::IntRect(clipStart, clipSize));
		sprite.setOrigin(sf::Vector2f(0.5f*m_tileSizeX, 0.5f*m_tileSizeY));
		sprite.setPosition(sf::Vector2f(m_tileSizeX * position.x, m_tileSizeY*position.y));
		sprite.setRotation(sf::radians(rotation));
		sprite.setScale(size);
		m_window.draw(sprite);
	}
	
	
	///
	/// \brief renderSprite
	/// \param position
	/// \param size
	/// \param rotation
	/// \param texture
	///
	inline void renderSprite(const sf::Vector2f& _position, const sf::Vector2f& _size, float _rotation, const sf::Texture& _texture)
	{
		sf::Sprite sprite(_texture);
		sprite.setOrigin(sf::Vector2f(0.5f * m_tileSizeX, 0.5f * m_tileSizeY));
		sprite.setPosition(sf::Vector2f(m_tileSizeX * _position.x, m_tileSizeY * _position.y));
		sprite.setRotation(sf::radians(_rotation));
		sprite.setScale(_size);
		
		m_window.draw(sprite);
	}

	///
	/// \brief renderMap
	/// \param map
	/// \param textures
	///
	void renderMap(const std::vector<std::vector<int>>& map, std::vector<std::shared_ptr<sf::Texture>>& textures)
	{
		/*		for (size_t y = 0; y < map.size(); ++y) // teacher version
			{ 
			for (size_t x = 0; x < map[y].size(); ++x)
				{
				int tileId = map[y][x];
				if (tileId < 0) {
					continue; // continue for loop without rendering this tile, since it is negative.
				}
				renderSprite({ float(x), float(y) }, { 1.0f,1.0f }, 0.0f, *textures[tileId]);
			}
		}*/
		
		float x = 0.0f;
		float y = 0.0f;
		for (const auto& mapindex : map)
		{
			for (const auto& mapindex2 : mapindex)
			{
				if (mapindex2 < 0)
				{
					x = x + 1.0f;
					continue;
				}
				renderSprite(sf::Vector2f(x, y), sf::Vector2f({ 1.0f, 1.0f }), 0.0f, *textures[mapindex2]);
				x = x + 1.0f;
			}
			y = y + 1.0f;
			x = 0.0f;
		}
	}

	///
	/// \brief setViewPosition
	/// \param position
	///
	void setViewPosition(const sf::Vector2f& _position)
	{
		auto windSize = m_window.getSize();
		auto centerPoint = sf::Vector2f(windSize.x * 0.5f, windSize.y * 0.5f);
		centerPoint.x = centerPoint.x + _position.x;
		centerPoint.y = centerPoint.y + _position.y;
		m_window.setView(sf::View(centerPoint, sf::Vector2f(windSize.x, windSize.y)));
	}

	///
	/// \brief setFramerateLimit
	/// \param limit
	///
	inline void setFramerateLimit(unsigned int limit)
	{
		m_window.setFramerateLimit(limit);
	}

	///
	/// \brief setTileSize
	/// \param tileSizeX
	/// \param tileSizeY
	///
	inline void setTileSize(int tileSizeX, int tileSizeY)
	{
		assert(tileSizeX >= 1);
		assert(tileSizeY >= 1);
		m_tileSizeX = tileSizeX;
		m_tileSizeY = tileSizeY;
	}

	// TODO
	std::shared_ptr<sf::Texture> loadTexture(const std::string& filename)
	{
		std::shared_ptr<sf::Texture> result = std::make_shared<sf::Texture>();
		if (result->loadFromFile(filename))
		{
			return result;
		}
		throw std::runtime_error("Failed to load texture: " + filename);
	}
	
private:
	int m_tileSizeX = 64;
	int m_tileSizeY = 64;
	sf::RenderWindow m_window;
};