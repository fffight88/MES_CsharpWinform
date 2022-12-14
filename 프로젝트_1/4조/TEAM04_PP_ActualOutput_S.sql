USE [DC_EDU_SH]
GO
/****** Object:  StoredProcedure [dbo].[TEAM04_PP_ActualOutput_S]    Script Date: 2022-07-19 오전 9:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		4조 참치
-- Create date: 2022-07-14
-- Description:	생산실적 등록 작업장 조회
-- =============================================
ALTER PROCEDURE [dbo].[TEAM04_PP_ActualOutput_S]

	@PLANTCODE        VARCHAR(10), -- 공장
	@WORKCENTERCODE   VARCHAR(10), -- 작업장.
	
	@LANG      VARCHAR(10)  = 'KO',
	@RS_CODE   VARCHAR(1)   OUTPUT,
	@RS_MSG	   VARCHAR(100) OUTPUT

AS
BEGIN
		SELECT A.PLANTCODE												AS PLANTCODE      -- 공장
		      ,A.WORKCENTERCODE											AS WORKCENTERCODE -- 작업장 코드 
		      ,A.WORKCENTERNAME											AS WORKCENTERNAME -- 작업장명
			  ,B.ORDERNO												AS ORDERNO        -- 현재 작업장 의 작업지시 번호.
			  ,B.ITEMCODE												AS ITEMCODE       -- 현재 작업장 의 작업 품번.
			  ,C.ITEMNAME												AS ITEMNAME       -- 현재 작어장 의 작업 품명
			  ,D.PLANQTY												AS ORDERQTY       -- 작업지시 수량.
			  ,D.PRODQTY												AS PRODQTY        -- 양품수량 
			  ,D.BADQTY													AS BADQTY         -- 불량수량
			  ,D.UNITCODE												AS UNITCODE	      -- 단위
			  ,B.STATUS													AS WORKSTATUSCODE -- 현재 상태코드(가동/비가동)
			  ,CASE WHEN B.STATUS = 'R' THEN '가동중' WHEN B.STATUS = 'S' THEN '비가동' ELSE '고장' END AS WORKSTATUS     -- 현재 상태명(가동/비가동/고장)
			  ,E.LOTNO													AS MATLOTNO       -- 작업장 투입 LOT
			  ,E.STOCKQTY												AS COMPONENTQTY   -- 투입LOT 잔량
			  ,B.WORKER												    AS WORKER         -- 작업자 코드
			  ,DBO.FN_WORKERNAME(B.WORKER)								AS WORKERNAME     -- 작업자 명.
			  ,D.FIRSTSTARTDATE										    AS STARTDATE -- 지시 시작 일시
			  ,D.ORDERCLOSEDATE											AS ENDDATE -- 지시 종료 일시
		  FROM TB_WorkCenterMaster A WITH(NOLOCK) LEFT JOIN TP_WorkcenterStatus B WITH(NOLOCK) -- 작업장 현재 상태 테이블
														 ON A.PLANTCODE      = B.PLANTCODE
														AND A.WORKCENTERCODE = B.WORKCENTERCODE
												  LEFT JOIN TB_ItemMaster C WITH(NOLOCK)
												         ON B.PLANTCODE = C.PLANTCODE
														AND B.ITEMCODE  = C.ITEMCODE
												  LEFT JOIN TB_ProductPlan D WITH(NOLOCK)
												         ON B.PLANTCODE = D.PLANTCODE
														AND B.ORDERNO   = D.ORDERNO
												  LEFT JOIN TB_StockHALB E WITH(NOLOCK)     -- 재공재고 테이블 (작업장 별 투입LOT 관리 테이블)
												         ON A.PLANTCODE      = E.PLANTCODE
														AND A.WORKCENTERCODE = E.WORKCENTERCODE

		 WHERE A.PLANTCODE           LIKE '%' + @PLANTCODE
		   AND A.WORKCENTERCODE      LIKE '%' + @WORKCENTERCODE
		   AND ISNULL(A.USEFLAG,'Y') <> 'N';

		   			 		  

END
