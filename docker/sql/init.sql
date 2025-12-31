USE [master];
GO

-- 1. Criação do Banco de Dados (fora de blocos lógicos para evitar erro de sintaxe)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'LEA_DB')
BEGIN
    CREATE DATABASE [LEA_DB];
END
GO

USE [LEA_DB];
GO

-- 2. Configurações de Otimização
ALTER DATABASE [LEA_DB] SET COMPATIBILITY_LEVEL = 160;
ALTER DATABASE [LEA_DB] SET RECOVERY SIMPLE; -- Melhor para bancos de estatísticas/desenvolvimento
ALTER DATABASE [LEA_DB] SET AUTO_CLOSE OFF;   -- Mudado de ON para OFF (Melhor performance)
ALTER DATABASE [LEA_DB] SET READ_COMMITTED_SNAPSHOT ON;
GO

---- 3. Criação do Usuário da Aplicação
--IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'IIS APPPOOL\leaApiPool')
--BEGIN
--    -- Nota: O LOGIN deve existir na instância para o USER funcionar
--    -- CREATE USER [IIS APPPOOL\leaApiPool] FOR LOGIN [IIS APPPOOL\leaApiPool];
--    -- ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\leaApiPool];
--    PRINT 'Nota: Certifique-se que o Login IIS APPPOOL\leaApiPool existe na instância.';
--END
--GO

-- 4. Tabelas Base (Sem chaves estrangeiras dependentes)

CREATE TABLE [dbo].[__EFMigrationsHistory](
    [MigrationId] [nvarchar](150) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
);

CREATE TABLE [dbo].[Leagues](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [Country] [int] NOT NULL, -- Corrigido de "Coutry"
    [Division] [smallint] NOT NULL,
    [Shield] [nvarchar](200) NULL,
    [Creation] [datetime2](7) NOT NULL DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [PK_Leagues] PRIMARY KEY CLUSTERED ([Id])
);

CREATE TABLE [dbo].[Referees](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [Creation] [datetime2](7) NOT NULL DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [PK_Referees] PRIMARY KEY CLUSTERED ([Id])
);

CREATE TABLE [dbo].[MatchesStatistics](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [ResultFullTime] [int] NOT NULL,
    [ResultHalfTime] [int] NOT NULL,
    [GoalsFullTime] [smallint] NOT NULL,
    [GoalsHalfTime] [smallint] NOT NULL,
    [Shots] [smallint] NOT NULL,
    [ShotsOnTarget] [smallint] NOT NULL,
    [Corners] [smallint] NOT NULL,
    [FoulsCommitted] [smallint] NOT NULL,
    [Yellow] [smallint] NOT NULL,
    [Red] [smallint] NOT NULL,
    [Creation] [datetime2](7) NOT NULL DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [PK_MatchesStatistics] PRIMARY KEY CLUSTERED ([Id])
);

-- 5. Tabelas Dependentes

CREATE TABLE [dbo].[Teams](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [Shield] [nvarchar](200) NULL,
    [LeagueId] [int] NOT NULL,
    [Creation] [datetime2](7) NOT NULL DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Teams_Leagues_LeagueId] FOREIGN KEY([LeagueId]) REFERENCES [dbo].[Leagues] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Matches](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Schedule] [datetime2](7) NOT NULL,
    [HomeTeamId] [int] NOT NULL,
    [AwayTeamId] [int] NOT NULL,
    [HomeStatisticsId] [int] NOT NULL,
    [AwayStatisticsId] [int] NOT NULL,
    [RefereeId] [int] NULL,
    [LeagueId] [int] NOT NULL,
    [Creation] [datetime2](7) NOT NULL DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Matches_Leagues_LeagueId] FOREIGN KEY([LeagueId]) REFERENCES [dbo].[Leagues] ([Id]),
    CONSTRAINT [FK_Matches_Referees_RefereeId] FOREIGN KEY([RefereeId]) REFERENCES [dbo].[Referees] ([Id]),
    CONSTRAINT [FK_Matches_Teams_Home] FOREIGN KEY([HomeTeamId]) REFERENCES [dbo].[Teams] ([Id]),
    CONSTRAINT [FK_Matches_Teams_Away] FOREIGN KEY([AwayTeamId]) REFERENCES [dbo].[Teams] ([Id]),
    CONSTRAINT [FK_Matches_Stats_Home] FOREIGN KEY([HomeStatisticsId]) REFERENCES [dbo].[MatchesStatistics] ([Id]),
    CONSTRAINT [FK_Matches_Stats_Away] FOREIGN KEY([AwayStatisticsId]) REFERENCES [dbo].[MatchesStatistics] ([Id])
);
GO

-- 6. Índices para Performance
CREATE UNIQUE NONCLUSTERED INDEX [IX_Teams_Name] ON [dbo].[Teams]([Name]) WHERE ([Name] IS NOT NULL);
CREATE NONCLUSTERED INDEX [IX_Matches_Schedule] ON [dbo].[Matches]([Schedule]);
CREATE NONCLUSTERED INDEX [IX_Matches_LeagueId] ON [dbo].[Matches]([LeagueId]);
GO