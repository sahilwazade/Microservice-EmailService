USE [TestSahil]
GO
/****** Object:  StoredProcedure [dbo].[GetEmailTemplate]    Script Date: 1/20/2025 4:35:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetEmailTemplate]
	@TemplateTypeId int
as
begin
	select * from EmailTemplates
End
GO
