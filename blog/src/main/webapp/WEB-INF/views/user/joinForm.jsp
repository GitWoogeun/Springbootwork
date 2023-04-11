<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	<!-- form태그에 action을 타서 Controller의 Mapping 지역으로 호출  -->
	<!-- JSON 방식으로 (자바스크립트) 회원가입하기 -->
	<form>
	<!-- User Name Input 영역  -->
		<div class="form-group">
			<label for="username">UserName :</label> <input type="text" placeholder="Enter username" class="form-control"  id="username">
		</div>
	<!-- Password  Input 영역  -->
		<div class="form-group">
			<label for="password">User Password :</label> <input type="password" class="form-control" placeholder="Enter password" id="password">
		</div>
	<!-- Email  Input 영역 -->
		<div class="form-group">
			<label for="email">User Email :</label> <input type="email" class="form-control" placeholder="Enter email" id="email">
		</div>
		<div class="form-group form-check">
			<label class="form-check-label"> <input class="form-check-input" type="checkbox"> Remember me
			</label>
		</div>
	</form>
	<button id="btn-save" class="btn btn-primary">회원가입</button>
</div>

<!-- js 폴더 / user.js 파일 연결  -->
<script src="/blog/js/user.js"></script>

<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>