#pragma once
#include "BaseObstacle.h"

class FlyingObstacle : Obstacle
{
public:
	int SuccessCost = 3;
	int FailureCost = 8;
};