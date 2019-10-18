CREATE DATABASE M_OpFlix;

GO

USE M_OpFlix;

GO

CREATE TABLE Usuarios(
IdUsuario INT PRIMARY KEY IDENTITY
,Nome VARCHAR(255) NOT NULL
,Email VARCHAR(255) NOT NULL 
,Senha VARCHAR(255) NOT NULL
,NomeDeUsuario VARCHAR(255) NOT NULL UNIQUE
,DataDeNascimento DATE NOT NULL
,Tipo CHAR(1) NOT NULL DEFAULT('U')
);

CREATE TABLE Categorias(
IdCategoria INT PRIMARY KEY IDENTITY
,Categoria VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Plataformas(
IdPlataforma INT PRIMARY KEY IDENTITY
,Plataforma VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE ClassificacoesIndicativas(
IdClassificacaoIndicativa INT PRIMARY KEY IDENTITY
,ClassificacaoIndicativa TINYINT NOT NULL
);

CREATE TABLE Lancamentos(
IdLancamento INT PRIMARY KEY IDENTITY
,IdCategoria INT FOREIGN KEY REFERENCES Categorias(IdCategoria)
,IdPlataforma INT FOREIGN KEY REFERENCES Plataformas(IdPlataforma)
,IdClassificacaoIndicativa INT FOREIGN KEY REFERENCES ClassificacoesIndicativas(IdClassificacaoIndicativa)
,Titulo VARCHAR(255) NOT NULL
,Sinopse TEXT NOT NULL
,DataDeLancamento DATE NOT NULL DEFAULT(GETDATE())
,TipoDeMidia CHAR(1) NOT NULL
,TempoDeDuracao TIME(0) NOT NULL
,Episodios INT NOT NULL DEFAULT (1)
);

CREATE TABLE Favoritos(
IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario)
,IdLancamento INT FOREIGN KEY REFERENCES Lancamentos (IdLancamento)
);

CREATE TABLE Reviews(
IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario)
,IdLancamento INT FOREIGN KEY REFERENCES Lancamentos (IdLancamento)
,Review TEXT
,Nota INT NOT NULL DEFAULT(0)
);

EXEC sp_RENAME 'Favoritos.IdUsusario' , 'IdUsuario', 'COLUMN'

ALTER TABLE ClassificacoesIndicativas ADD CI VARCHAR(3); 
ALTER TABLE Lancamentos ADD NotaMedia INT DEFAULT(0) NOT NULL, Poster VARCHAR(500);
ALTER TABLE Reviews ADD IdReview INT PRIMARY KEY IDENTITY;
ALTER TABLE Usuarios ADD CONSTRAINT Email UNIQUE(Email);
-- Incluir uma imagem para cada usu�rio cadastrado;
ALTER TABLE Usuarios ADD ImagemUsuario VARCHAR(500) DEFAULT('https://image.flaticon.com/icons/svg/17/17004.svg');