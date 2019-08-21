CREATE DATABASE M_Peoples;
USE M_Peoples;

CREATE TABLE Funcionarios (
IdFuncionario INT PRIMARY KEY IDENTITY
,Nome VARCHAR(128) NOT NULL
,Sobrenome VARCHAR(128) NOT NULL
,DataDeNascimento DATE NOT NULL
);

INSERT INTO Funcionarios (Nome, Sobrenome, DataDeNascimento) VALUES ('Catarina','Strada','11/04/1996'), ('Tadeu','Vitelli','09/01/1995');

SELECT * FROM Funcionarios;