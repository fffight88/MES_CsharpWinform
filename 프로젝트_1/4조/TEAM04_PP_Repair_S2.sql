USE [DC_EDU_SH]
GO
/****** Object:  StoredProcedure [dbo].[TEAM04_PP_Repair_S2]    Script Date: 2022-07-19 오전 9:38:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		4조참치
-- Create date: 2022-07-13
-- Description:	설비보전 통합관리 (고장실적 조회 그리드)
-- =============================================
ALTER PROCEDURE [dbo].[TEAM04_PP_Repair_S2]

	@PLANTCODE			VARCHAR(30) ,   -- 공장
	@WORKCENTERCODE		VARCHAR(30)		-- 작업장 코드


  , @LANG			VARCHAR(10) = 'KO'
  , @RS_CODE		VARCHAR(1) OUTPUT
  , @RS_MSG			VARCHAR(100) OUTPUT

AS
BEGIN

	SELECT      			   
			  RECDATE			AS RECDATE		,	-- 고장 발생 일자
			  ERRORSEQ			AS ERRORSEQ		,	-- 고장 발생 순번
			  ERRORDT			AS ERRORDT		,	-- 고장 발생 일시
			  STARTRPDT			AS STARTRPDT	,	-- 수리 시작 일시
			  REMARKDETAIL		AS REMARKDETAIL	,	-- 상세 고장 사유
			  ENDRPDT			AS ENDRPDT			-- 수리 완료 일시
			  
			  
	  FROM	  TB_ErrorDT WITH(NOLOCK)
				
	 WHERE    WORKCENTERCODE  LIKE '%' + @WORKCENTERCODE
	   AND	  PLANTCODE		LIKE '%' +  @PLANTCODE

			  
END

