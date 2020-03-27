#include "GameObject.h"



GameObject::GameObject()
{
	model = glm::mat4();
}


GameObject::~GameObject()
{
}

void GameObject::setShader(Shader * shader)
{
	this->shader = shader;
	shader->Use();
}

void GameObject::setVAO(GLuint VAO)
{
	this->VAO = VAO;
}

void GameObject::setTransform(glm::mat4 transform)
{
	this->model = transform;
	// Get their uniform location
	GLint modelLoc = glGetUniformLocation(shader->Program, "model");
	// Pass them to the shaders
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
}

void GameObject::setRotation(float angle, glm::vec3 axis, bool reset)
{
	if (reset) model = glm::mat4();
	model = glm::rotate(model, angle, axis);
}

void GameObject::setTranslation(glm::vec3 displacements, bool reset)
{
	if (reset) model = glm::mat4();
	model = glm::translate(model, displacements);
}

void GameObject::setScale(glm::vec3 scaleFactors, bool reset)
{
	if (reset) model = glm::mat4();
	model = glm::scale(model, scaleFactors);
	dimensions = scaleFactors;
}

void GameObject::draw()
{
	//considerando que possui EBO
	glBindVertexArray(VAO);
	glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
	glBindVertexArray(0);
}

void GameObject::update()
{
	// E então enviamos para o shader como uma variável mat4 uniforme
	// Get their uniform location
	if (isRigidBody)
	{
		//Bem rudimentar ainda, sem buscar fixtures nem nada, só pra sprites mesmo
		b2Vec2 pos = body->getBody()->GetPosition();
		float ori = body->getBody()->GetAngle();

		position = glm::vec3(pos.x, pos.y, 0.0);
		angle = ori;

		setTranslation(position);
		setRotation(angle, glm::vec3(0.0f, 0.0f, 1.0f), false);
		setScale(glm::vec3(dimensions.x, dimensions.y, 1.0), false); //pra acumular as transforms, não pode resetar!
	}

	GLint modelLoc = glGetUniformLocation(shader->Program, "model");
	// Pass them to the shaders
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
}

void GameObject::setPhysics(bool _isRigidBody)
{
	isRigidBody = _isRigidBody;
	
}

