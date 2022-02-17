using Data;
using Shop;

var project = new Project();
project.Initialize();
project.CheckConnection();
project.AuthorizeOrRegister();
project.Start();