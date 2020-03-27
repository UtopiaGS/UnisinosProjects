#include "test.h"
#pragma once


class CreateShapeClass : public Test
{
public:
	CreateShapeClass() {

		b2Vec2 gravity(0.0f, -9.8f);

		CreateBoudary(m_world);

		b2FixtureDef edgeFix;

		edgeFix.density = 1.0f;
		edgeFix.friction = 0.3f;

		CreateEdge(15.0f, -1.0f, edgeFix, -5.0f, 0.5f, 25.0, 8.0f, b2_dynamicBody, m_world);

		b2FixtureDef circleFix;

		circleFix.density = 1.0f;
		circleFix.friction = 0.3f;

		CreateCircle(-6.0f, 2.0f, circleFix, 2.0f, b2_dynamicBody, m_world);

		b2FixtureDef boxFix;

		boxFix.density = 1.0f;
		boxFix.friction = 0.01f;

		CreateBox(7.0f, 4.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 5.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 6.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 7.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 8.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 9.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 10.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 11.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 12.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);
		CreateBox(7.0f, 13.0f, boxFix, 0.5f, 0.5f, b2_staticBody, m_world);


		CreateBox(-10.0f, 4.0f, boxFix, 0.5f, 0.5f, b2_dynamicBody, m_world);
		CreateBox(-10.0f, 5.0f, boxFix, 0.5f, 0.5f, b2_dynamicBody, m_world);
		CreateBox(-10.0f, 6.0f, boxFix, 0.5f, 0.5f, b2_dynamicBody, m_world);
		CreateBox(-10.0f, 7.0f, boxFix, 0.5f, 0.5f, b2_dynamicBody, m_world);
		CreateBox(-10.0f, 8.0f, boxFix, 0.5f, 0.5f, b2_dynamicBody, m_world);
		CreateBox(-10.0f, 9.0f, boxFix, 0.5f, 0.5f, b2_dynamicBody, m_world);

		CreateCircle(-15.0f, 2.0f, circleFix, 0.5f, b2_dynamicBody, m_world);
		CreateCircle(-15.0f, 3.0f, circleFix, 0.5f, b2_dynamicBody, m_world);
		CreateCircle(-15.0f, 4.0f, circleFix, 0.5f, b2_dynamicBody, m_world);
		CreateCircle(-15.0f, 5.0f, circleFix, 0.5f, b2_dynamicBody, m_world);
		CreateCircle(-15.0f, 6.0f, circleFix, 0.5f, b2_dynamicBody, m_world);
		CreateCircle(-15.0f, 7.0f, circleFix, 0.5f, b2_dynamicBody, m_world);

	} 

	void Step(Settings& settings) override
	{
		//run the default physics and rendering
		Test::Step(settings);

		//show some text in the main screen
		g_debugDraw.DrawString(5, m_textLine, "Now we have a test with edge, circle and box creation");
		m_textLine += 15;
	}

	static Test* Create()
	{
		return new CreateShapeClass;
	}

	void CreateBox(float posX, float posY, b2FixtureDef &fixDef, float b, float a, b2BodyType type,  b2World * world) {
		b2Body * box;

		b2BodyDef def;
		def.position.Set(posX, posY);
		def.type = type;

		box = world->CreateBody(&def);

		b2PolygonShape shape;
		shape.SetAsBox(b, a);
		fixDef.shape = &shape;

		box->CreateFixture(&fixDef);
	}

	void CreateCircle(float posX, float posY, b2FixtureDef &fixDef, float radius, b2BodyType type, b2World * world) {
		b2Body * circle;

		b2BodyDef def;
		def.position.Set(posX, posY);
		def.type = type;

		circle = world->CreateBody(&def);

		b2CircleShape shape;
		shape.m_radius = radius;
		fixDef.shape = &shape;

		circle->CreateFixture(&fixDef);
	}

	void CreateEdge(float posX, float posY, b2FixtureDef &fixDef, float vec1X, float vec1y, float vec2x, float vec2y, b2BodyType type, b2World * world) {
		b2Body * edge;

		b2BodyDef def;
		def.position.Set(posX, posY);
		def.type = type;

		edge = world->CreateBody(&def);
		
		b2EdgeShape shape;
		shape.Set(b2Vec2(vec1X, vec1y), b2Vec2(vec2x, vec2y));
		fixDef.shape = &shape;

		edge->CreateFixture(&fixDef);
	}

	void CreateBoudary(b2World* world) {
		b2FixtureDef boxFix;

		boxFix.density = 1.0f;
		boxFix.friction = 0.3f;

		//CreateBox(0.0f, 20.0f, boxFix, 20.0f, 2.0f, b2_staticBody, m_world);
		//CreateBox(10.0f, 10.0f, boxFix, 2.0f, 2.0f, b2_staticBody, m_world);
		//CreateBox(5.0f, 20.0f, boxFix, 2.0f, 2.0f, b2_staticBody, m_world);
		CreateBox(-1.0f, -1.0f, boxFix, 40.0f, 0.5f, b2_staticBody, m_world);
		CreateBox(-1.0f, 40.0f, boxFix, 40.0f, 0.5f, b2_staticBody, m_world);
		CreateBox(39.5f, 19.5f, boxFix, 0.5f, 21.0f, b2_staticBody, m_world);
		CreateBox(-41.5f, 19.5f, boxFix, 0.5f, 21.0f, b2_staticBody, m_world);
	}
};

static int testIndex = RegisterTest("NathyTests", "CreateShapeClass", CreateShapeClass::Create);