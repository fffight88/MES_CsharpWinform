USE [DC_EDU_SH]
GO
/****** Object:  StoredProcedure [dbo].[TEAM04_PP_ERRORREG]    Script Date: 2022-07-19 오전 9:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		4조 참치
-- Create date: 2022-07-14
-- Description:	가동/고장 등록
-- =============================================
ALTER PROCEDURE [dbo].[TEAM04_PP_ERRORREG]
	@PLANTCODE        VARCHAR(10),  -- 공장.
	@ORDERNO		  VARCHAR(30),  -- 작업지시번호.
	@WORKCENTERCODE   VARCHAR(10),  -- 작업장
	--@STATUS           VARCHAR(1),   -- 가동/비가동 R: 가동 , S: 비가동.
    @ITEMCODE         VARCHAR(30),  -- 품번

	@LANG             VARCHAR(10)  = 'KO',
	@RS_CODE          VARCHAR(1)   OUTPUT,
	@RS_MSG	          VARCHAR(100) OUTPUT


AS
BEGIN 
		-- 현재 시간 정의 공통 변수
	DECLARE @LD_NOWDATE  DATETIME,
	        @LS_NOWDATE  VARCHAR(10);
		SET @LD_NOWDATE = GETDATE();
		SET @LS_NOWDATE = CONVERT(VARCHAR,@LD_NOWDATE,23); -- yyyy-MM-dd


	  -- 1. 작업장 등록된 작업자 여부 확인하기
	  DECLARE @LS_WORKERID VARCHAR(10);

	  SELECT @LS_WORKERID = WORKER
	    FROM TP_WorkcenterStatus WITH(NOLOCK)
	   WHERE PLANTCODE      = @PLANTCODE
	     AND WORKCENTERCODE = @WORKCENTERCODE;


	-- 2. 작업장 현재 상태 테이블에 (가동/고장) 등록 
	UPDATE TP_WorkcenterStatus
	   SET STATUS        = 'F'
	      ,EDITOR        = @LS_WORKERID
		  ,EDITDATE      = @LD_NOWDATE
		  ,REMARK		 = 'C01'
	WHERE PLANTCODE      = @PLANTCODE
	  AND WORKCENTERCODE = @WORKCENTERCODE


	-- 3. 작업장 별 작업지시 이력 테이블에 마지막 지시번호에 대한 SEQ 가져오기.
	DECLARE @LI_RSSEQ INT 
	       
	SELECT @LI_RSSEQ = ISNULL(MAX(RSSEQ),0)
	  FROM TP_WorkcenterStatusRec WITH(NOLOCK)
	 WHERE PLANTCODE      = @PLANTCODE
	   AND WORKCENTERCODE = @WORKCENTERCODE
	--   AND ORDERNO        = @ORDERNO


	UPDATE TP_WorkcenterStatusRec
	   SET RSENDDATE      = @LD_NOWDATE
	      ,EDITDATE       = @LD_NOWDATE
		  ,EDITOR         = @LS_WORKERID
		  
     WHERE PLANTCODE      = @PLANTCODE
	   AND WORKCENTERCODE = @WORKCENTERCODE
	   AND RSSEQ          = @LI_RSSEQ

	-- 변경되는 가동/고장 시작 시간 INSERT 
	INSERT INTO TP_WorkcenterStatusRec (PLANTCODE,   WORKCENTERCODE,   ORDERNO , RSSEQ,         WORKER,       ITEMCODE,   STATUS,    RSSTARTDATE,	REMARK,	MAKEDATE,    MAKER)
	                            VALUES (@PLANTCODE,  @WORKCENTERCODE,  @ORDERNO, @LI_RSSEQ + 1, @LS_WORKERID, @ITEMCODE,  'F',   @LD_NOWDATE, 'C01'	, @LD_NOWDATE, @LS_WORKERID); 
															    

   SET @RS_CODE = 'S';


END
