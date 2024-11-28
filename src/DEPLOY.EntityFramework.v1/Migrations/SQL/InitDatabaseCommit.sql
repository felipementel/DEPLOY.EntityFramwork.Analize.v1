IF OBJECT_ID(N'[dbo].[_ControleMigracoes]') IS NULL
BEGIN
    CREATE TABLE [dbo].[_ControleMigracoes] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK__ControleMigracoes] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Contratos] (
    [ContratoId] uniqueidentifier NOT NULL,
    [Numero] bigint NOT NULL,
    [DataInicio] date NOT NULL,
    [DataFim] date NOT NULL,
    [Ativo] bit NOT NULL,
    [CreatedAt] datetime NOT NULL,
    [UpdatedAt] datetime NOT NULL,
    CONSTRAINT [PK_Contratos] PRIMARY KEY ([ContratoId])
);
GO

CREATE TABLE [Pessoas] (
    [PessoaId] uniqueidentifier NOT NULL,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Telefone] nvarchar(max) NOT NULL,
    [Documento] nvarchar(450) NOT NULL,
    [Endereco] nvarchar(450) NOT NULL,
    [DataNascimento] date NOT NULL,
    [CreatedAt] datetime NOT NULL,
    [UpdatedAt] datetime NOT NULL,
    CONSTRAINT [PK_Pessoas] PRIMARY KEY ([PessoaId])
);
GO

CREATE TABLE [ContratoPessoa] (
    [ContratosId] uniqueidentifier NOT NULL,
    [InteressadosId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ContratoPessoa] PRIMARY KEY ([ContratosId], [InteressadosId]),
    CONSTRAINT [FK_ContratoPessoa_Contratos_ContratosId] FOREIGN KEY ([ContratosId]) REFERENCES [Contratos] ([ContratoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContratoPessoa_Pessoas_InteressadosId] FOREIGN KEY ([InteressadosId]) REFERENCES [Pessoas] ([PessoaId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ContratoPessoa_InteressadosId] ON [ContratoPessoa] ([InteressadosId]);
GO

CREATE INDEX [IX_Names_Descending] ON [Pessoas] ([Endereco] DESC, [Documento] DESC);
GO

INSERT INTO [dbo].[_ControleMigracoes] ([MigrationId], [ProductVersion])
VALUES (N'20240224200948_InitDatabaseAPI', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [dbo].[_ControleMigracoes] ([MigrationId], [ProductVersion])
VALUES (N'20240224201414_InitDatabaseCommit', N'8.0.2');
GO

COMMIT;
GO

