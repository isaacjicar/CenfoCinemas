/****** Object:  Table [dbo].[TBL_Movie]    Script Date: 6/7/2025 11:01:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_Movie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Updated] [datetime] NULL,
	[Created] [datetime] NOT NULL,
	[Title] [nvarchar](75) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[Genre] [nvarchar](20) NOT NULL,
	[Director] [nchar](30) NOT NULL,
 CONSTRAINT [PK_TBL_Movie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


