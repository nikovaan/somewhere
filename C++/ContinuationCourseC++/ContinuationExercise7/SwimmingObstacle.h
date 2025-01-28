#pragma once
#include "BaseObstacle.h"

class SwimmingObstacle : Obstacle
{
	int SuccessCost = 2;
	int FailureCost = 5;
};