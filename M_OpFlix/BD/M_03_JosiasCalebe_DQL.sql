USE M_OpFlix;

GO

SELECT * FROM Lancamentos;
-- Seleção - trazer todas as categorias, inclusive as que não possuem lançamentos vinculados;
SELECT * FROM Categorias;
-- Seleção - trazer todas as plataformas, inclusive as que não possuem lançamentos vinculados.
SELECT * FROM Plataformas;
SELECT * FROM Usuarios;
SELECT * FROM ClassificacoesIndicativas;
SELECT * FROM Favoritos;


/*Criar uma view que traga todas as plataformas e que mostre quais os gêneros que passem na plataforma;
	(Netflix que passa o filme Sing, que é da categoria Animação. Logo, o resultado esperado da view, será da seguinte manteira:
	Plataforma Gênero/Categoria
	Netflix Animação)*/
CREATE VIEW vmCategoriasPlataformas AS
SELECT P.Plataforma, C.Categoria
FROM Lancamentos L
JOIN Plataformas P ON L.IdPlataforma = P.IdPlataforma
JOIN Categorias C ON L.IdCategoria = C.IdCategoria
;
SELECT * FROM vmCategoriasPlataformas ORDER BY Plataforma ASC;


-- Desafio: listar somente filmes únicos. Quando trouxer a seleção, deverá aparecer somente um Guardiões da Galáxia.
CREATE VIEW vmSelecionarDestintos AS
SELECT IdLancamento, Titulo, Sinopse, Categoria, ClassificacaoIndicativa, DataDeLancamento, TipoDeMidia, 
TempoDeDuracao, Episodios
FROM (
   SELECT IdLancamento, L.Titulo, L.Sinopse, C.Categoria, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento, 'dd/MM/yyyy') AS DataDeLancamento, L.TipoDeMidia, 
CONVERT(VARCHAR(5), L.TempoDeDuracao) AS TempoDeDuracao, L.Episodios ,
          row_number() over (partition by Titulo order by Titulo) as row_number
   FROM Lancamentos L
   JOIN Categorias C ON L.IdCategoria = C.IdCategoria 
   JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
   ) AS ROWS
WHERE row_number = 1;

SELECT * FROM vmSelecionarDestintos;

CREATE VIEW vmSelecionarDestintosTodasColunas AS
SELECT *
FROM (
   SELECT *,
          row_number() over (partition by Titulo order by Titulo) as row_number
   FROM Lancamentos
   ) AS ROWS
WHERE row_number = 1;

SELECT * FROM vmSelecionarDestintosTodasColunas;

CREATE PROCEDURE FavoritosPorIdUsuario @IdUsuario INT
AS
SELECT F.IdUsuario, U.NomeDeUsuario, DATEDIFF(year, U.DataDeNascimento, GETDATE()) AS Idade, L.IdLancamento, L.Titulo, L.Sinopse, C.Categoria, P.Plataforma, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento, 'dd/MM/yyyy') AS DataDeLancamento, L.TipoDeMidia, 
CONVERT(VARCHAR(5), L.TempoDeDuracao) AS TempoDeDuracao, L.Episodios
FROM Favoritos F
JOIN Lancamentos L ON L.IdLancamento = F.IdLancamento
JOIN Usuarios U ON U.IdUsuario = F.IdUsuario
JOIN Categorias C ON L.IdCategoria = C.IdCategoria
JOIN Plataformas P ON P.IdPlataforma = L.IdPlataforma
JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
WHERE U.IdUsuario = @IdUsuario AND DATEDIFF(year, U.DataDeNascimento, GETDATE()) >= CI.ClassificacaoIndicativa;

EXEC FavoritosPorIdUsuario @IdUsuario = 4

CREATE PROCEDURE SelecionarDestintosPorIdUsuario @IdUsuario INT
AS
SELECT *
FROM (
SELECT U.NomeDeUsuario, DATEDIFF(year, U.DataDeNascimento, GETDATE()) AS Idade, L.IdLancamento, L.Titulo, L.Sinopse, C.Categoria, P.Plataforma, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento, 'dd/MM/yyyy') AS DataDeLancamento, L.TipoDeMidia, 
CONVERT(VARCHAR(5), L.TempoDeDuracao) AS TempoDeDuracao, L.Episodios, row_number() over (partition by Titulo order by Titulo) as row_number
FROM Lancamentos L
JOIN Usuarios U ON U.IdUsuario = @IdUsuario
JOIN Categorias C ON L.IdCategoria = C.IdCategoria
JOIN Plataformas P ON P.IdPlataforma = L.IdPlataforma
JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
WHERE DATEDIFF(year, U.DataDeNascimento, GETDATE()) >= CI.ClassificacaoIndicativa
)AS ROWS
WHERE row_number = 1

EXEC SelecionarDestintosPorIdUsuario @IdUsuario = 7


CREATE PROCEDURE FavoritosPorNomeDeUsuario @NomeDeUsuario VARCHAR(255)
AS
SELECT DISTINCT U.Nome, DATEDIFF(year, U.DataDeNascimento, GETDATE()) AS Idade, L.Titulo, C.Categoria, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento,'dd/MM/yyyy') AS DataDeLancamento,L.TipoDeMidia, CONVERT(VARCHAR(5),L.TempoDeDuracao) AS TempoDeDuracao,L.Episodios 
FROM Favoritos F
JOIN Lancamentos L ON L.IdLancamento = F.IdLancamento
JOIN Usuarios U ON U.IdUsuario = F.IdUsuario
JOIN Categorias C ON L.IdCategoria = C.IdCategoria
JOIN Plataformas P ON P.IdPlataforma = L.IdPlataforma
JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
WHERE U.NomeDeUsuario = @NomeDeUsuario AND DATEDIFF(year, U.DataDeNascimento, GETDATE()) >= CI.ClassificacaoIndicativa;

EXEC FavoritosPorNomeDeUsuario @NomeDeUsuario = 'Erik'

-- Criar uma view, que dado uma entrada do usuário, por exemplo, 3, mostrar os próximos 3 lançamentos breves. Mostrando também a categoria e a plataforma.
CREATE VIEW vmProximosLancamentos AS
SELECT TOP 3 L.Titulo, C.Categoria, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento,'dd/MM/yyyy') AS DataDeLancamento,L.TipoDeMidia, CONVERT(VARCHAR(5),L.TempoDeDuracao) AS TempoDeDuracao 
FROM Lancamentos L
JOIN Categorias C ON L.IdCategoria = C.IdCategoria
JOIN Plataformas P ON P.IdPlataforma = L.IdPlataforma
JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
WHERE L.DataDeLancamento > GETDATE()
ORDER BY DATEDIFF(year, L.DataDeLancamento, GETDATE()); 

SELECT * FROM vmProximosLancamentos;


-- Criar um procedimento para listar os filmes dado uma categoria em String do usuário;(Listar os filmes da categoria Ação)
CREATE PROCEDURE LancamentosPorCategoria @Categoria VARCHAR(255)
AS
SELECT * FROM vmSelecionarDestintos
WHERE Categoria = @Categoria;

EXEC LancamentosPorCategoria @Categoria = 'Ação';

-- Criar um procedimento para listar a quantidades de filmes, dada uma categoria por Id;(Listar os filmes da categoria 1 ou o id correspondente da sua tabela).
CREATE PROCEDURE LancamentosPorIdCategoria @IdCategoria INT
AS
SELECT IdLancamento, Titulo, Sinopse, Categoria, ClassificacaoIndicativa, DataDeLancamento, TipoDeMidia, 
TempoDeDuracao, Episodios
FROM (
   SELECT IdLancamento, L.Titulo, L.Sinopse, C.Categoria, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento, 'dd/MM/yyyy') AS DataDeLancamento, L.TipoDeMidia, 
CONVERT(VARCHAR(5), L.TempoDeDuracao) AS TempoDeDuracao, L.Episodios ,
          row_number() over (partition by Titulo order by Titulo) as row_number
   FROM Lancamentos L
   JOIN Categorias C ON L.IdCategoria = C.IdCategoria 
   JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
   WHERE C.IdCategoria = @IdCategoria
   ) AS ROWS
WHERE row_number = 1;

EXEC LancamentosPorIdCategoria @IdCategoria = 1;

CREATE PROCEDURE LancamentosPorIdPlataforma @IdPlataforma INT
AS
   SELECT IdLancamento, L.Titulo, L.Sinopse, C.Categoria, P.Plataforma, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento, 'dd/MM/yyyy') AS DataDeLancamento, L.TipoDeMidia, 
CONVERT(VARCHAR(5), L.TempoDeDuracao) AS TempoDeDuracao, L.Episodios
   FROM Lancamentos L
   JOIN Categorias C ON L.IdCategoria = C.IdCategoria
   JOIN Plataformas P ON L.IdPlataforma = P.IdPlataforma
   JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
   WHERE P.IdPlataforma = @IdPlataforma

EXEC LancamentosPorIdPlataforma @IdPlataforma = 1;

CREATE PROCEDURE LancamentosPorDataDeLancamento @Data DATE
AS
SELECT IdLancamento, Titulo, Sinopse, Categoria, ClassificacaoIndicativa, DataDeLancamento, TipoDeMidia, 
TempoDeDuracao, Episodios
FROM (
   SELECT IdLancamento, L.Titulo, L.Sinopse, C.Categoria, CI.CI AS ClassificacaoIndicativa, FORMAT(DataDeLancamento, 'dd/MM/yyyy') AS DataDeLancamento, L.TipoDeMidia, 
CONVERT(VARCHAR(5), L.TempoDeDuracao) AS TempoDeDuracao, L.Episodios,
          row_number() over (partition by Titulo order by Titulo) as row_number
   FROM Lancamentos L
   JOIN Categorias C ON L.IdCategoria = C.IdCategoria 
   JOIN ClassificacoesIndicativas CI ON CI.IdClassificacaoIndicativa = L.IdClassificacaoIndicativa
   WHERE L.DataDeLancamento = @Data
      ) AS ROWS
WHERE row_number = 1;

EXEC LancamentosPorDataDeLancamento @Data = '31-07-2014';