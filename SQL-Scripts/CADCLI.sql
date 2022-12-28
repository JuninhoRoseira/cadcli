IF DB_ID('DBCADCLI') IS NULL 
	CREATE DATABASE DBCADCLI
GO

USE DBCADCLI
GO

IF OBJECT_ID('DBO.CADCLI') IS NULL BEGIN
	CREATE TABLE CADCLI
	(
		Id bigint identity(1,1) NOT NULL,
		Documento varchar(14) NOT NULL,
		Nome varchar(80) NOT NULL,
		DataNascimento datetime NULL,
		Sexo char(1) NULL,
		Endereco varchar(100) NULL,
		Cidade varchar(100) NULL,
		Estado varchar(50) NULL,
		PRIMARY KEY CLUSTERED 
		(
			Id ASC
		) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IDX_CADCLI_01') BEGIN
	CREATE UNIQUE INDEX IDX_CADCLI_01 ON dbo.CADCLI
	(
		Documento ASC
	)
	INCLUDE
	(
		Nome,
		DataNascimento,
		Sexo,
		Endereco,
		Cidade,
		Estado
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END

IF NOT EXISTS(SELECT TOP 1 1 FROM CADCLI WHERE Documento = '18210905864') BEGIN
	INSERT INTO CADCLI(
		Documento,
		Nome,
		DataNascimento,
		Sexo,
		Endereco,
		Cidade,
		Estado
	)
	VALUES
	(
		'18210905864',
		'WILSON JOSÉ PINTO JÚNIOR',
		'1972-12-13',
		'M',
		'Rua Barão de Cascalho, 224, Jardim Eulina',
		'Campinas',
		'São Paulo'
	)
END
