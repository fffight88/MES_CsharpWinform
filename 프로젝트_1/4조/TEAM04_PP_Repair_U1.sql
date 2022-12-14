USE [DC_EDU_SH]
GO
/****** Object:  StoredProcedure [dbo].[TEAM04_PP_Repair_U1]    Script Date: 2022-07-19 오전 9:38:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		4조참치
-- Create date: 2022-07-13
-- Description:	설비보전 통합관리 (고장실적 조회 그리드)
-- =============================================
ALTER PROCEDURE [dbo].[TEAM04_PP_Repair_U1]

	@PLANTCODE			VARCHAR(30) ,   -- 공장
	@WORKCENTERCODE		VARCHAR(30) ,	-- 작업장 코드
	@RECDATE			VARCHAR(30) ,	-- 고장 발생 일자
	@ERRORSEQ			VARCHAR(30) ,	-- 고장 순번
	@REMARKDETAIL		VARCHAR(300)	-- 고장 상세 사유



  , @LANG			VARCHAR(10) = 'KO'
  , @RS_CODE		VARCHAR(1) OUTPUT
  , @RS_MSG			VARCHAR(100) OUTPUT

AS
BEGIN

	UPDATE TB_ErrorDT
	   SET REMARKDETAIL = @REMARKDETAIL	
				
	 WHERE    WORKCENTERCODE  LIKE '%' + @WORKCENTERCODE
	   AND	  PLANTCODE		  LIKE '%' +  @PLANTCODE
	   AND    RECDATE		  LIKE '%' + @RECDATE
	   AND    ERRORSEQ        LIKE '%' + @ERRORSEQ

	SET @RS_CODE = 'S';

			  
END

