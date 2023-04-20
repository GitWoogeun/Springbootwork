<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	<!-- form태그에 action을 타서 Controller의 Mapping 지역으로 호출  -->
	<!-- JSON 방식으로 (자바스크립트) 회원가입하기 -->
	<form>
		<!-- 누구의 회원 정보를 수정하는지 알려주기위해 -->
		<input type="hidden" id="id" value="${principal.user.id}"/>
		<!-- User Name Input 영역  -->
		<div class="form-group">
			<label for="username">UserName :</label> 
			<input type="text" value="${principal.user.username}" placeholder="Enter username" class="form-control"  id="username" readonly="readonly">
		</div>
	<!-- Password  Input 영역  -->
		<div class="form-group">
			<label for="password">User Password :</label> 
			<input type="password"  class="form-control" placeholder="비밀번호를 수정해주세요." id="password">
		</div>
	<!-- Email  Input 영역 -->
		<div class="form-group">
			<label for="email">User Email :</label>
			 <input type="email" value="${principal.user.email}" class="form-control" placeholder="Enter email" id="email">
		</div>
	</form>
	<button id="btn-update" class="btn btn-primary">수정하기</button>
</div>

<!-- js 폴더 / user.js 파일 연결  -->
<script src="/js/user.js"></script>

<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>