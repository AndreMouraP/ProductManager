Welcome to the Product Manager application:

This application was made conseidering the following request sent via email by Rodrigo Guerreiro from k2:

" Project WebAPI - . Net Core 2.x
- Criação de 2 entidades: Categoria (id, nome) e Produto (id, nome, categoria)
- Deve ser utilizado SQL server e Entity framework core
- Pelo menos utilizar 2 "design patterns"
- Disponibilizar projecto via GIT
- Caso faça sentido, criar documentação com as principais opções tomadas"

As such, I did a simple product and category manager which I called "ProductManager".
I tried to do a clean and understandable application following the SOLID principles of object oriented applications.

As you run the Products.Api webapplication, you will come across a swagger page from which you can check the 2 available controllers and all endpoints available.

Now regarding the solution itself:
-As requested, I used 2 design patterns a mapper to map the dtos to view models and vice versa, and a factory to create contexts so that we close the connection when not in use.
- I created the exact entities requested with the required fields.

Testing:
-Even tho the tests might not cover the hole solution, I did a mock to emulate the database and created a base test which can be used to easily test diferent services. 
This means that you can easily add more tests to all the services you which to add or, if you which, you can add specific tests to specific services.