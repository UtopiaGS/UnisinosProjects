#include "test.h"
#pragma once


class MyTests : public Test
{
public:
	MyTests() { } //do nothing, no scene yet

	void Step(Settings& settings) override
	{
		//run the default physics and rendering
		Test::Step(settings);

		//show some text in the main screen
		g_debugDraw.DrawString(5, m_textLine, "Now we have a foo test");
		m_textLine += 15;
	}

	static Test* Create()
	{
		return new MyTests;
	}

	void CreateBox(float posX, float posY, b2BodyDef def, b2FixtureDef fixDef) {
	
	}
};

static int testIndex = RegisterTest("NathyTests", "MyTest", MyTests::Create);