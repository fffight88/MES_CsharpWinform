USE [DC_EDU_SH]
GO
/****** Object:  StoredProcedure [dbo].[TEAM04_PP_Repair_S1]    Script Date: 2022-07-19 오전 9:37:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		4조참치
-- Create date: 2022-07-13
-- Description:	설비보전 통합관리(작업장 그리드)
-- =============================================
ALTER PROCEDURE [dbo].[TEAM04_PP_Repair_S1]

	@PLANTCODE			VARCHAR(30) ,   -- 공장
	@WORKCENTERCODE		VARCHAR(30) ,	-- 작업장 코드
	@YEARMONTH			VARCHAR(30) ,	-- 년 / 월
	@ORDERNO			VARCHAR(30)		-- 작업지시 번호


  , @LANG			VARCHAR(10) = 'KO'
  , @RS_CODE		VARCHAR(1) OUTPUT
  , @RS_MSG			VARCHAR(100) OUTPUT

AS
BEGIN

	SELECT    A.PLANTCODE		AS PLANTCODE		,	-- 공장
			  A.WORKCENTERCODE	AS WORKCENTERCODE	,	-- 작업장 코드
			  A.ORDERNO     	AS ORDERNO     		  	-- 작업지시 번호	
			  											
			  
	  FROM	  TP_WorkcenterStatus A  WITH(NOLOCK)


	WHERE		PLANTCODE			LIKE	'%'		+	@PLANTCODE			-- 공장
	AND			WORKCENTERCODE		LIKE	'%'		+	@WORKCENTERCODE		-- 작업장 코드
	AND			ISNULL(ORDERNO,'')	LIKE	'%'		+   @ORDERNO	 + '%'	-- 작업지시 번호
		
END
