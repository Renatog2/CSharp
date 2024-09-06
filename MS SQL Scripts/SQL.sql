CREATE DATABASE CSharp;


/*Criando tabela Pessoas*/
CREATE TABLE Pessoas(
	ID int NOT NULL PRIMARY KEY,
	nome varchar (MAX) NOT NULL,
	CPF varchar (14) NOT NULL,
	situacao varchar (1) NOT NULL,
	dataDeAlteracao datetime
);


/* Este comando irá permitir o usuário inserir o número que quiser de ID, desligando o AutoIncrement.
 * Caso o banco não seja criado pela exportação, não é necessário executar este comando.
 */
SET IDENTITY_INSERT Pessoas ON;


