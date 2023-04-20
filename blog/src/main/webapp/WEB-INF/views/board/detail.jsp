<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

<!-- 위치가 board이기 때문에 한칸 뒤로 가서 loayout/header.jsp로 이동해야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">

		<br><br>
		<div>
			<b>글 번호 : <span id="id"><i>${board.id} </i></span><br></b>
			<b>글 작성자 : <span><i>${board.user.username} </i></span></b>
		</div>
		<br>
		<div>
			<h3>${board.title}</h3>
		</div>
		<hr>
		<div>
  			<div>${board.content}</div>
		</div>
		<hr>
		<button class="btn btn-info" onclick="history.back()">돌아가기</button>
		<button id="btn-update" class="btn btn-success">수정</button>
		<c:if test="${board.user.id == principal.user.id}">
			<button id="btn-delete" class="btn btn-danger">삭제</button>
		</c:if>
</div>
<!-- js 폴더 / user.js 파일 연결  -->
<script src="/js/board.js"></script>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>
