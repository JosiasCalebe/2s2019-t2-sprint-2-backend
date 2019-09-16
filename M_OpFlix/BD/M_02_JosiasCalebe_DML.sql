USE M_OpFlix;

INSERT INTO Plataformas(Plataforma) VALUES ('Netflix'),('Prime Video'),('Disney+'),('Cinema');
INSERT INTO Categorias(Categoria) VALUES ('Ação'),('Romance'),('Documentário'),('Suspense'),('Terror'),('Animação'),('Aventura'),('Comédia');
INSERT INTO ClassificacoesIndicativas(ClassificacaoIndicativa) VALUES (0),(10),(12),(14),(16),(18);

INSERT INTO Usuarios(Nome, Email, Senha, NomeDeUsuario, DataDeNascimento,Tipo)VALUES
('Erik','erik@email.com','123456','Erik','01/01/0001','A')
,('Cassiana','cassiana@email.com','123456','Cassiana','01/01/0001','A')
,('Helena','helena@email.com','123456','Helena','01/01/0001','U')
,('Roberto','rob@email.com','3110','Roberto','01/01/0001','U');
INSERT INTO Usuarios (Nome,Email,NomeDeUsuario,Senha,DataDeNascimento)VALUES('Abdul','a@email.com','Abdul','123456','12-12-1990')

INSERT INTO Lancamentos(IdCategoria,IdPlataforma,IdClassificacaoIndicativa,Titulo,Sinopse,DataDeLancamento,TipoDeMidia,TempoDeDuracao,Episodios) VALUES
 (6,4,1,'Rei Leão(2019)'
 ,'Traído e exilado de seu reino, o leãozinho Simba precisa descobrir como crescer e retomar seu destino como herdeiro real nas planícies da savana africana.'
 ,'18/07/2019','F','01:58:00',1)
,(4,1,5,'Mindhunter - 2ª Temporada'
,'Mindhunter gira em torno dos agentes do FBI Holden Ford e Bill Tench, que entrevistaram assassinos em série presos, com o intuito de entender como esses
criminosos pensam e aplicam esse conhecimento para resolver os casos em curso.'
,'16/08/2019','S','00:50:00',9)
,(5,4,5,'It 2'
,'Vinte e sete anos depois dos eventos que chocaram os adolescentes que faziam parte do Clube dos Perdedores, os amigos realizam uma reunião. 
No entanto, o reencontro se torna uma verdadeira e sangrenta batalha quando Pennywise, o palhaço, retorna.'
,'05/09/2019','F','02:49:00',1)
,(2,4,2,'Yesterday'
,'Após um acidente um músico se torna a única pessoa que se lembra dos Beatles; ele então se torna famoso levando crédito por escrever e tocar suas músicas.'
,'04/05/2019','F','01:57:00',1)
,(3,1,6,'Wild Wild Country'
,'Wild Wild Country é um documentário sobre o controverso guru indiano Bhagwan Shree Rajneesh, sua antiga assistente pessoal Ma Anand Sheela e seus seguidores
da comunidade de Rajneeshpuram, próxima da pequena cidade de Antelope, ambas localizadas no Condado de Wasco, Oregon.'
,'16/03/2018','S','01:00:00',6)
,(1,2,6,'The Boys - 1ª Temporada'
,'Quando a fama sobe à cabeça, alguns super-heróis passam a se corromper e usar seu status para se promoverem ainda mais, o que pode colocar em risco a 
própria população. Pensando nisso, uma equipe da CIA foi preparada para cuidar desse caso. Conhecidos como "os meninos", esses agentes têm a missão de vigiar
o trabalho dessas personalidades, assim como controlar o surgimento de novos heróis'
,'26/07/2019','S','00:55:00',8)
,(8,2,3,'The Office - 6ª Temporada'
,'A série retrata o cotidiano divertido dos empregados de um escritório de uma empresa fornecedora de papel, localizada na Pensilvânia.'
,'01/09/2009','S','00:23:00',26)
,(1,1,4,'La Casa De Papel 3 temp'
,'Um grupo de nove ladrões, liderados por um Professor, prepara o roubo do século na Casa da Moeda da Espanha, com o objetivo de fabricar o próprio dinheiro
em quantidades incalculáveis e nunca antes vista'
,'03/03/2017','S','00:45:00',8)
,(6,4,1,'Toy Story 4'
,'Woody, Buzz Lightyear e o resto da turma embarcam em uma viagem com Bonnie e um novo brinquedo chamado Forky. A aventura logo se transforma em uma reunião
inesperada quando o ligeiro desvio que Woody faz o leva ao seu amigo há muito perdido, Bo Peep.'
,'20/06/2019','F','01:40:00',1)
,(7,4,1,'Turma Da Mônica: Laços'
,'O Floquinho desaparece. Para encontrar seu cachorro de estimação, Cebolinha conta com os amigos Cascão, Mônica e Magali e, claro, um plano infalível.'
,'27/06/2019','F','01:37:00',1)
,(7,4,1,'Deuses Americanos - 1ª Temporada'
,'O Floquinho desaparece. Para encontrar seu cachorro de estimação, Cebolinha conta com os amigos Cascão, Mônica e Magali e, claro, um plano infalível.'
,'27/06/2019','S','00:50:00',1);

-- Inserir dois lançamentos do Filme Guardiões da Galáxia. Inserir um lançamento com o título "Guardiões da Galáxia", mas um no cinema e outro no netflix.
INSERT INTO Lancamentos(IdCategoria,IdPlataforma,IdClassificacaoIndicativa,Titulo,Sinopse,DataDeLancamento,TipoDeMidia,TempoDeDuracao,Episodios) VALUES
 (1,4,3,'Guardiões Da Galáxia'
 ,'O aventureiro do espaço Peter Quill torna-se presa de caçadores de recompensas depois que rouba a esfera de um vilão traiçoeiro, Ronan. Para escapar do perigo, ele faz uma aliança com um grupo de quatro extraterrestres.'
 ,'31/07/2014','F','01:40:00',1)
 ,(1,1,3,'Guardiões Da Galáxia'
 ,'O aventureiro do espaço Peter Quill torna-se presa de caçadores de recompensas depois que rouba a esfera de um vilão traiçoeiro, Ronan. Para escapar do perigo, ele faz uma aliança com um grupo de quatro extraterrestres.'
 ,'31/07/2014','F','01:40:00',1);

-- Inserir as categorias: Terror, Ação, Comédia, Documentário, Drama e Ficção Científica.
INSERT INTO Categorias(Categoria) VALUES ('Drama'),('Ficção Cientifica');
INSERT INTO Plataformas(Plataforma) VALUES ('VHS');
INSERT INTO Favoritos(IdUsusario,IdLancamento) VALUES (1,8),(2,4),(3,3),(4,8),(4,10),(1,12);

-- Alterar La Casa De Papel 3 temp para La Casa De Papel - 3º Temporada;
UPDATE Lancamentos SET Titulo = 'La Casa De Papel - 3º Temporada' WHERE Titulo LIKE '%La Casa De Papel%';
-- Atualizar data de lançamento do filme O Rei Leão para a data de lançamento da animação original, 08/07/1994, e alterar veiculo para VHS;
UPDATE Lancamentos SET Titulo = 'Rei Leão', DataDeLancamento = '08/07/1994', IdPlataforma = 5 WHERE IdLancamento = 3;
-- Atualizar o usuário Helena para administrador;
UPDATE Usuarios SET Tipo = 'A' WHERE NomeDeUsuario = 'Helena';
UPDATE Lancamentos SET Episodios = 8 WHERE IdLancamento = 13;
UPDATE ClassificacoesIndicativas SET CI = 'L' WHERE ClassificacaoIndicativa = 0;
UPDATE Usuarios SET DataDeNascimento = '07/07/1990' WHERE IdUsuario = 4;

-- Deletar a série Deuses Americanos;
DELETE FROM Lancamentos WHERE Titulo LIKE '%Deuses Americanos%';