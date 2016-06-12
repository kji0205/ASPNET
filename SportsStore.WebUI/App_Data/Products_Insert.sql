USE [SportsStore]
GO

INSERT INTO [dbo].[Products]
           ([Name]
           ,[Description]
           ,[Price]
           ,[Category])
     VALUES
           (<Name, nvarchar(max),>
           ,<Description, nvarchar(max),>
           ,<Price, decimal(18,2),>
           ,<Category, nvarchar(max),>)
GO

