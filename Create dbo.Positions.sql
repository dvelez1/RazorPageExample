USE [Employees]
GO

select * from [dbo].[Positions] 

update [dbo].[Positions] 
set PositionDescription = 'Human Resrouces Supervisor'
where PositionId =1
select * from [dbo].[Positions] 