USE [TournoiDB]
GO
/****** Object:  Table [dbo].[Equipe]    Script Date: 29/12/2022 11:11:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomE] [varchar](50) NULL,
 CONSTRAINT [PK_Equipe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Joueur]    Script Date: 29/12/2022 11:11:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Joueur](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomJ] [varchar](50) NULL,
	[SexeJ] [varchar](50) NULL,
	[AgeJ] [int] NULL,
	[Id_E] [int] NULL,
 CONSTRAINT [PK_Joueur] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Joueur]  WITH CHECK ADD  CONSTRAINT [FK_Joueur_Equipe] FOREIGN KEY([Id_E])
REFERENCES [dbo].[Equipe] ([Id])
GO
ALTER TABLE [dbo].[Joueur] CHECK CONSTRAINT [FK_Joueur_Equipe]
GO
