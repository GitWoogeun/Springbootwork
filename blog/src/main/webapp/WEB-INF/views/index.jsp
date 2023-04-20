<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

<%@ include file="layout/header.jsp"%>

<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	<!--  items는 boardController에서 Model에 담아서 보낸 데이터를 한건씩 var board에 담아서 뿌릴수 있다. -->
	<c:forEach var="board" items="${boards.content}">
		<div class="card m-2">
			<div class="card-body">
				<h4 class="card-title">${board.title}</h4>
				<!-- 실제로는 board.getTitle()이 호출 -->
				> <a href="#" class="btn btn-primary">상세보기</a>
			</div>
		</div>
	</c:forEach>
	<!-- 페이징 버튼 생성 , ( justify-content-center 버튼을 가운대로 이동 시키는 Bootstrap 문법 ) -->
	<!-- 버튼을 왼쪽으로 이동 => start , 버튼을 가운대로 이동 => center, 버튼을 오른쪽으로 이동 => end -->
	<ul class="pagination justify-content-center">
		<c:choose>
			<!-- 페이징 => 첫 번째 페이지 이면 이전 버튼 비활성화 -->
			<c:when test="${boards.first}">
				<li class="page-item disabled"><a class="page-link" href="?page=${boards.number-1}">이전</a></li>
			</c:when>
			<c:otherwise>
				<!-- 페이징 => 첫 번째 페이지가 아니라면 이전 버튼 활성화  -->
				<li class="page-item"><a class="page-link" href="?page=${boards.number-1}">이전</a></li>
			</c:otherwise>
		</c:choose>
		<c:choose>
			<!-- 페이징 => 마지막 페이지 이면 다음 버튼 비활성화 -->
			<c:when test="${boards.last}">
				<li class="page-item disabled"><a class="page-link" href="?page=${boards.number+1}">다음</a></li>
			</c:when>
			<c:otherwise>
				<!-- 페이징 => 마지막 페이지가 아니라면 다음 버튼 활성화 -->
				<li class="page-item"><a class="page-link" href="?page=${boards.number+1}">다음</a></li>
			</c:otherwise>
		</c:choose>
	</ul>
</div>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="layout/footer.jsp"%>
